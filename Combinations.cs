using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CombinationsTool
{
    public static class Combinations
    {
        private static ConcurrentBag<List<string>> answerList = new ConcurrentBag<List<string>>();

        public static void ClearAnswer()
        {
            answerList.Clear();
        }

        static double GetCombinationsPerArraySize(int n, int p)
        {
            return (Fatorial(n)) / (Fatorial(p) * Fatorial(n - p));
        }

        static public double GetCombinationsNumber(int n)
        {
            double s = 0;
            for (int p = 1; p <= n; p++)
            {
                s += GetCombinationsPerArraySize(n, p);
            }

            return s;
        }

        static double Fatorial(double n)
        {
            double f = 1;
            for (int i = 1; i <= n; i++)
            {
                f *= i;
            }

            return f;
        }

        static List<string> FormatList(List<float> combination, List<float> allValues, List<string> categories)
        {
            List<string> result = new List<string>();

            string fdValues = "In: ";
            foreach (float f in combination)
            {
                fdValues += string.Format("{0:N2}", f) + " (" + FindCategory(f, allValues, categories) + "); ";
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
                    nfValues += string.Format("{0:N2}", allValues[i]) + " (" + FindCategory(allValues[i], allValues, categories) + "); ";
                }
            }

            if (nfValues.Length > 5)
            {
                nfValues = nfValues.Substring(0, nfValues.Length - 2);
            }

            result.Add(nfValues);

            result.Add(String.Empty);

            return result;
        }

        static string FindCategory(float value, List<float> values, List<string> categories)
        {
            if (categories.Count > 1)
            {
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

        static void GenerateCombinations(float[] arr, int n, int r, int index, float[] data, int i, List<float> values, List<string> categories, float total)
        {
            if (index == r)
            {
                float vSum = 0;
                for (int j = 0; j < r; j++)
                {
                    vSum += data[j];
                }

                if (Math.Round(vSum, 2) == Math.Round(total, 2))
                {
                    List<string> combination = FormatList(data.ToList<float>(), values, categories);

                    answerList.Add(combination);
                }

                return;
            }

            if (i >= n)
            {
                return;
            }

            data[index] = arr[i];

            GenerateCombinations(arr, n, r, index + 1, data, i + 1, values, categories, total);

            GenerateCombinations(arr, n, r, index, data, i + 1, values, categories, total);
        }

        static public void GetCombinations(float[] arr, int n, List<int> lSize, List<float> values, List<string> categories, float total)
        {
            foreach (int r in lSize)
            {
                float[] data = new float[r];

                GenerateCombinations(arr, n, r, 0, data, 0, values, categories, total);
            }
        }

        public static Answer CalculateCombinations(List<float> fValues, List<string> fCategories, float fFSum, int cpu)
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

                Thread thread = new Thread(() => GetCombinations(arr, n, lSize, fValues, fCategories, fFSum));
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

            combinationsClock.Stop();
            long combinationsTime = combinationsClock.ElapsedMilliseconds;

            Answer score = new Answer(answerList, combinationsTime);

            return score;
        }
    }
}
