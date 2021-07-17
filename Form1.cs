using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace CombinationsTool
{
    public partial class MainForm : Form
    {
        IDictionary<double, double> dSize = new Dictionary<double, double>();

        double[] x;
        double[] y;
        double[] p;

        public MainForm()
        {
            InitializeComponent();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            DoTotalClear();
        }

        void btCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                DoPartialClear();

                string[] strValues = tbValues.Text.Split(";");
                string[] strCategories = tbCategories.Text.Split(";");
                float sumValue = float.Parse(tbFinalSum.Text);
                int cpu = Environment.ProcessorCount;

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

                EstimateCalculationTime(fValues.Count);

                Answer score = Combinations.CalculateCombinations(fValues, fCategories, sumValue, cpu);
                WriteAnswer(score.answerList, score.combinationsTime, sumValue);

                stopwatch.Stop();

                long totalTime = stopwatch.ElapsedMilliseconds;
                WriteScore(totalTime);

                FindFunction(fValues.Count, score.combinationsTime);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }       
        }

        void btClean_Click(object sender, EventArgs e)
        {
            DoTotalClear();
        }       

        void WriteAnswer(ConcurrentBag<List<string>> answerList, long combinationsTime, float sumValue)
        {
            if (combinationsTime >= 60000)
            {
                TimeSpan combinationsTimeSpan = TimeSpan.FromMilliseconds(combinationsTime);
                lbCombinationsTime.Text = "C: " + combinationsTimeSpan.ToString(@"mm\:ss");
            }
            else
            {
                lbCombinationsTime.Text = "C: " + combinationsTime.ToString() + " ms";
            }

            var writingClock = new Stopwatch();
            writingClock.Start();

            if (answerList.Count > 0)
            {
                foreach (List<string> l in answerList)
                {
                    foreach (string sl in l)
                    {
                        lbResult.Items.Add(sl);
                    }
                }

                answerList.Clear();
            }
            else
            {
                lbResult.Items.Add("Nenhuma combinação cujo somatório seja igual a " + sumValue.ToString() + " foi encontrada.");
            }

            writingClock.Stop();

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

        void WriteScore(long time)
        {
            if (time >= 60000)
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(time);
                lbTime.Text = "T: " + ts.ToString(@"mm\:ss");
            }
            else
            {
                lbTime.Text = "T: " + time.ToString() + " ms";
            }

            int rCount = lbResult.Items.Count / 3;
            if (rCount <= 999)
            {
                lbResultCount.Text = String.Format("{0:0000}", rCount);
            }
            else
            {
                lbResultCount.Text = "+999";
            }
        }

        void EstimateCalculationTime(int size)
        {
            lbAllCombinations.Text = Math.Truncate(Combinations.GetCombinationsNumber(size)).ToString() + " combinações";

            if (dSize.Count > 1)
            {
                List<double> axisX = x.ToList<double>();
                if (size >= axisX.Min()) {
                    axisX.Add(size);

                    x = axisX.ToArray();

                    double[] yh = Generate.Map(x, k => p[0] * Math.Exp(p[1] * k));

                    List<double> axisY = yh.ToList<double>();

                    double estimatedTime = Math.Truncate(axisY.Last());

                    if (estimatedTime >= 60000)
                    {
                        TimeSpan ts = TimeSpan.FromMilliseconds(estimatedTime);
                        lbEstimatedTime.Text = "CE: " + ts.ToString(@"mm\:ss");
                    }
                    else
                    {
                        lbEstimatedTime.Text = "CE: " + estimatedTime.ToString() + " ms";
                    }
                }
            }
        }

        void FindFunction(int id, long time)
        {
            try
            {
                if (time > 1000) {
                    dSize.Add(id, time);
                }
            }
            catch 
            { }

            if (dSize.Count > 1) {
                List<double> axisX = new List<double>();
                List<double> axisY = new List<double>();
                foreach (KeyValuePair<double, double> item in dSize)
                {
                    axisX.Add(item.Key);
                    axisY.Add(item.Value);
                }

                x = axisX.ToArray();
                y = axisY.ToArray();
                p = Exponential(x, y);
            }
        }

        double[] Exponential(double[] x, double[] y, DirectRegressionMethod method = DirectRegressionMethod.QR)
        {
            double[] y_hat = Generate.Map(y, Math.Log);
            double[] p_hat = Fit.LinearCombination(x, y_hat, method, t => 1.0, t => t);
            return new[] { Math.Exp(p_hat[0]), p_hat[1] };
        }

        void DoTotalClear()
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
            lbEstimatedTime.Text = String.Empty;
            tbValues.Focus();

            Combinations.ClearAnswer();
        }

        void DoPartialClear()
        {
            lbResultCount.Text = String.Empty;
            lbAllCombinations.Text = String.Empty;
            lbCombinationsTime.Text = String.Empty;
            lbWritingTime.Text = String.Empty;
            lbTime.Text = String.Empty;
            lbEstimatedTime.Text = String.Empty;
            lbResult.Items.Clear();

            Combinations.ClearAnswer();
        }
    }
}
