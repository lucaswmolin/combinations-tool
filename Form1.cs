using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CombinationsTool
{
    public partial class MainForm : Form
    {
        private static ConcurrentBag<List<float>> allCombinations = new ConcurrentBag<List<float>>();
        private static ConcurrentBag<List<string>> lResult = new ConcurrentBag<List<string>>();

        public MainForm()
        {
            InitializeComponent();
        }

        void btCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                clean();

                string[] strValues = tbValues.Text.Split(",");
                string[] strCategories = tbCategories.Text.Split(",");
                float fFSum = float.Parse(tbFinalSum.Text);

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

                if (tbCategories.Text.Length > 0) {
                    if (fValues.Count != fCategories.Count)
                    {
                        throw new Exception("Erro: a quantidade de valores e categorias está diferente.");
                    }
                }

                int cpu = Environment.ProcessorCount;

                Run(fValues, fCategories, fFSum, cpu);

                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds >= 60000)
                {
                    TimeSpan ts = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
                    lbTime.Text = "T: " + ts.ToString(@"mm\:ss");
                } else
                {
                    lbTime.Text = "T: " + stopwatch.ElapsedMilliseconds.ToString() + " ms";
                }

                if (lResult.Count <= 999)
                {
                    lbResultCount.Text = String.Format("{0:0000}", lResult.Count);
                }
                else
                {
                    lbResultCount.Text = "+999";
                }

                lbAllCombinations.Text = allCombinations.Count.ToString() + " combinações";
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

            if (nfValues.Length > 5) {
                nfValues = nfValues.Substring(0, nfValues.Length - 2);
            }

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

                    lResult.Add(combination);
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

                allCombinations.Add(combination);

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

        void Run(List<float> fValues, List<string> fCategories, float fFSum, int cpu)
        {       
            var combinationsClock = new Stopwatch();
            combinationsClock.Start();

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

            var combinations = allCombinations.ToList();

            int interval = combinations.Count / cpu;
            int residue = combinations.Count % cpu;

            int mn = 0;
            int mx = interval;

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

            if (combinationsClock.ElapsedMilliseconds >= 60000)
            {
                TimeSpan combinationsTime = TimeSpan.FromMilliseconds(combinationsClock.ElapsedMilliseconds);
                lbCombinationsTime.Text = "C: " + combinationsTime.ToString(@"mm\:ss");
            }
            else
            {
                lbCombinationsTime.Text = "C: " + combinationsClock.ElapsedMilliseconds.ToString() + " ms";
            }


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

            if (writingClock.ElapsedMilliseconds >= 60000)
            {
                TimeSpan writingTime = TimeSpan.FromMilliseconds(writingClock.ElapsedMilliseconds);
                lbWritingTime.Text = "E: " + writingTime.ToString(@"mm\:ss");
            }
            else
            {
                lbWritingTime.Text = "E: " + writingClock.ElapsedMilliseconds.ToString() + " ms";
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            cleanAll();
        }
    }
}
