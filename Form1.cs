using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CombinationsTool
{
    public partial class MainForm : Form
    {
        private static Semaphore semaphore1;
        private static Semaphore semaphore2;

        private static List<List<float>> allCombinations = new List<List<float>>();
        private static List<List<string>> lResult = new List<List<string>>();

        public MainForm()
        {
            InitializeComponent();
        }

        void btCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                clean();

                int cpu = Int32.Parse(cbThreads.SelectedItem.ToString());

                Run(tbValues.Text, tbFinalSum.Text, tbCategories.Text, cpu);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }       
        }

        void btClean_Click(object sender, EventArgs e)
        {
            cleanAll();
        }

        void cleanAll()
        {
            tbValues.Clear();
            tbCategories.Clear();
            tbFinalSum.Clear();
            lbResult.Items.Clear();
            lbResultCount.Text = String.Empty;
            lbAllCombinations.Text = String.Empty;
            lbCombinationsTime.Text = String.Empty;
            lbWritingTime.Text = String.Empty;
            lbTime.Text = String.Empty;
            tbValues.Focus();

            allCombinations.Clear();
            lResult.Clear();
        }

        void clean()
        {
            lbResultCount.Text = String.Empty;
            lbAllCombinations.Text = String.Empty;
            lbCombinationsTime.Text = String.Empty;
            lbWritingTime.Text = String.Empty;
            lbTime.Text = String.Empty;
            lbResult.Items.Clear();
            allCombinations.Clear();
            lResult.Clear();
        }

        List<string> FormatList(List<float> combination, List<float> allValues, List<string> categories)
        {
            List<string> result = new List<string>();

            string fdValues = "In: ";
            foreach (float f in combination)
            {
                fdValues += string.Format("{0:N2}", f) + " (" + FindCategory(f, allValues, categories) + "), ";
            }

            fdValues = fdValues.Substring(0, fdValues.Length - 2);

            result.Add(fdValues);

            string nfValues = "Out: ";
            for (int i = 0; i < allValues.Count; i++)
            {
                bool found = false;
                foreach (float f in combination)
                {
                    if (allValues[i] == f)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    nfValues += string.Format("{0:N2}", allValues[i]) + " (" + FindCategory(allValues[i], allValues, categories) + "), ";
                }
            }

            nfValues = nfValues.Substring(0, nfValues.Length - 2);

            result.Add(nfValues);

            result.Add(String.Empty);

            return result;
        }

        string FindCategory(float value, List<float> values, List<string> categories)
        {
            if (categories.Count > 1) {
                for (int i = 0; i < values.Count; i++)
                {
                    if (value == values[i])
                    {
                        return categories[i];
                    }
                }
            }

            return String.Empty;
        }

        void Calculate(List<List<float>> combinations, List<float> values, List<string> categories, float total)
        {
            for (int i = 0; i < combinations.Count; i++)
            {
                float vSum = 0;
                foreach (float n in combinations[i])
                {
                    vSum += n;
                }

                if (Math.Round(vSum, 2) == Math.Round(total, 2))
                {
                    List<string> combination = FormatList(combinations[i], values, categories);

                    semaphore2.WaitOne();

                    lResult.Add(combination);

                    semaphore2.Release();
                }
            }
        }

        void GenerateCombinations(float[] arr, int n, int r, int index, float[] data, int i)
        {
            if (index == r)
            {
                List<float> combination = new List<float>();
                for (int j = 0; j < r; j++)
                {
                    combination.Add(data[j]);
                }

                semaphore1.WaitOne();

                allCombinations.Add(combination);

                semaphore1.Release();

                return;
            }

            if (i >= n)
            {
                return;
            }

            data[index] = arr[i];

            GenerateCombinations(arr, n, r, index + 1, data, i + 1);

            GenerateCombinations(arr, n, r, index, data, i + 1);
        }

        void GetCombinations(float[] arr, int n, List<int> lSize)
        {
            foreach (int r in lSize)
            {
                float[] data = new float[r];

                GenerateCombinations(arr, n, r, 0, data, 0);
            }
        }

        void Run(string values, string finalSum, string categories, int cpu)
        {       
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] strValues = values.Split(",");
            string[] strCategories = categories.Split(",");
            float fFSum = float.Parse(finalSum);

            List<float> fValues = new List<float>();
            for (int i = 0; i < strValues.Length; i++)
            {
                fValues.Add(float.Parse(strValues[i]) + ((float)i + 1) / 100000);
            }

            List<string> fCategories = new List<string>();
            for (int i = 0; i < strCategories.Length; i++)
            {
                fCategories.Add(strCategories[i]);
            }


            var combinationsClock = new Stopwatch();
            combinationsClock.Start();

            semaphore1 = new Semaphore(1, 1);

            float[] arr = fValues.ToArray();
            int n = arr.Length;

            List<Thread> kThread = new List<Thread>();
            for (int i = 1; i <= cpu; i++)
            {
                List<int> lSize = new List<int>();

                int id = i;
                while (id <= fValues.Count)
                {
                    lSize.Add(id);
                    id += cpu;
                }

                Thread thread = new Thread(() => GetCombinations(arr, n, lSize));
                kThread.Add(thread);
            }

            foreach (Thread t in kThread)
            {
                t.Start();
            }

            foreach (Thread t in kThread)
            {
                t.Join();
            }

            lbAllCombinations.Text = allCombinations.Count.ToString();

            var combinations = allCombinations;

            int interval = combinations.Count / cpu;
            int residue = combinations.Count % cpu;

            int mn = 0;
            int mx = interval;

            semaphore2 = new Semaphore(1, 1);

            List<Thread> lThread = new List<Thread>();
            for (int i = 0; i < cpu; i++)
            {
                if (i == (cpu - 1))
                {
                    mx += residue;
                }

                List<List<float>> subCombinations = new List<List<float>>();
                for (int j = mn; j < mx; j++)
                {
                    subCombinations.Add(combinations[j]);
                }

                mn += interval;
                mx += interval;

                Thread thread = new Thread(() => Calculate(subCombinations, fValues, fCategories, fFSum));
                lThread.Add(thread);
            }

            foreach (Thread t in lThread)
            {
                t.Start();
            }

            foreach (Thread t in lThread)
            {
                t.Join();
            }

            combinationsClock.Stop();

            TimeSpan combinationsTime = TimeSpan.FromMilliseconds(combinationsClock.ElapsedMilliseconds);
            lbCombinationsTime.Text = "C: " + combinationsTime.ToString(@"hh\:mm\:ss\:ms");


            var writingClock = new Stopwatch();
            writingClock.Start();

            if (lResult.Count > 0) {
                foreach (List<string> l in lResult)
                {
                    foreach (string sl in l)
                    {
                        lbResult.Items.Add(sl);
                    }
                }
            } else
            {
                lbResult.Items.Add("Nenhuma combinação foi encontrada.");
            }

            writingClock.Stop();

            TimeSpan writingTime = TimeSpan.FromMilliseconds(writingClock.ElapsedMilliseconds);
            lbWritingTime.Text = "E: " + writingTime.ToString(@"hh\:mm\:ss\:ms");


            stopwatch.Stop();

            TimeSpan ts = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
            lbTime.Text = "T: " + ts.ToString(@"hh\:mm\:ss\:ms");

            if (lResult.Count <= 999){
                lbResultCount.Text = String.Format("{0:0000}", lResult.Count);
            } else
            {
                lbResultCount.Text = "+999";
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            cleanAll();

            for (int i = 1; i <= Environment.ProcessorCount; i++)
            {
                cbThreads.Items.Add(i.ToString());
            }
        }
    }
}
