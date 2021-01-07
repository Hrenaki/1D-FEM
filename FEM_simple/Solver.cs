using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM_simple
{
    class Solver
    {
        private int elemCount; // количество элементов
        private int vertixesCount; // количество узлов
        private List<int[]> elems; // список элементов 
        private List<double> vertixes; // список вершин
        private List<int[]> regions; // список расчётных областей

        private double[,] globalMatrix;

        private double[] globalB; // глобальный вектор правой части

        private Dictionary<int, double> materials; //словарь соответствия номеру расчётной области материала (гамма)
        private Dictionary<int, F> diffCoeffs; //словарь соответствия номеру расчётной области диффузии (лямбда)
        private Dictionary<int, F> function; // правая часть уравнения

        private BoundaryCondition[] conditions;

        public Solver(int elemCount, int vertixesCount, List<int[]> elems, List<double> vertixes,
            List<int[]> regions,
            Dictionary<int, double> materials, Dictionary<int, F> diffCoeffs,
            Dictionary<int, F> function, params BoundaryCondition[] conditions)
        {
            this.elemCount = elemCount;
            this.vertixesCount = vertixesCount;
            this.elems = elems;
            this.vertixes = vertixes;
            this.regions = regions;
            this.materials = materials;
            this.diffCoeffs = diffCoeffs;
            this.function = function;
            this.conditions = conditions;

            globalMatrix = new double[vertixesCount, vertixesCount];

            globalB = new double[vertixesCount];
            Array.Sort(conditions, (a, b) => { if (a.Type > b.Type) return 0; else return 1; });
        }

        private void createGlobalMatrixAndGlobalVector()
        {
            double[,] localMatrix = new double[2, 2];
            double[] localB = new double[2];
            F localF;

            int region;

            double v1, v2;
            int k;
            int[] curRegion;
            int[] numbers;
            double hk;

            F lambda;
            double gamma;
            double temp;

            int i, j;
            for (region = 0; region < regions.Count; region++)
            {
                curRegion = regions[region];

                diffCoeffs.TryGetValue(region, out lambda);
                materials.TryGetValue(region, out gamma);
                function.TryGetValue(region, out localF);

                for (k = 0; k < curRegion.Length; k++)
                {
                    numbers = elems[curRegion[k]];
                    Array.Sort(numbers);

                    v1 = vertixes[numbers[0]];
                    v2 = vertixes[numbers[1]];

                    hk = v2 - v1;

                    localMatrix[0, 0] = (lambda(v1) + lambda(v2)) / (2.0 * hk) + gamma * hk / 3.0;
                    localMatrix[1, 1] = localMatrix[0, 0];
                    localMatrix[0, 1] = -1.0 * (lambda(v1) + lambda(v2)) / (2.0 * hk) + gamma * hk / 6.0;
                    localMatrix[1, 0] = localMatrix[0, 1];

                    temp = localF(v1);
                    localB[0] = 2.0 * temp;
                    localB[1] = temp;

                    temp = localF(v2);
                    localB[0] = hk / 6.0 * (localB[0] + temp);
                    localB[1] = hk / 6.0 * (localB[1] + 2.0 * temp);

                    for (i = 0; i < localMatrix.GetLength(0); i++)
                        for (j = 0; j < localMatrix.GetLength(0); j++)
                            globalMatrix[numbers[i], numbers[j]] += localMatrix[i, j];

                    for (i = 0; i < conditions.Length; i++)
                    {
                        if (conditions[i].Type == 2)
                        {
                            if (conditions[i].Vertix == numbers[0])
                                globalB[conditions[i].Vertix] += conditions[i].Value(conditions[i].Vertix);

                            if(conditions[i].Vertix == numbers[1])
                                globalB[conditions[i].Vertix] += conditions[i].Value(conditions[i].Vertix);                            
                        }
                    }

                    globalB[numbers[0]] += localB[0];
                    globalB[numbers[1]] += localB[1];
                }
            }

            // Применение краевых условий 1-го рода
            foreach (BoundaryCondition condition in conditions)
            {
                if (condition.Type == 1)
                {
                    for (i = 0; i < globalMatrix.GetLength(0); i++)
                        globalMatrix[condition.Vertix, i] = 0;
                    globalMatrix[condition.Vertix, condition.Vertix] = 1;
                    globalB[condition.Vertix] = condition.Value(vertixes[condition.Vertix]);
                }
            }
        }

        private void solve()
        {
            for (int i = 0; i < globalMatrix.GetLength(0) - 1; i++)
            {
                double maxValue = globalMatrix[i, i];
                int maxValueIndex = i;
                for (int j = i + 1; j < globalMatrix.GetLength(0); j++)
                    if (globalMatrix[j, i] > maxValue)
                    {
                        maxValue = globalMatrix[j, i];
                        maxValueIndex = j;
                    }
                if (maxValueIndex != i)
                {
                    double tmp = globalB[i];
                    globalB[i] = globalB[maxValueIndex];
                    globalB[maxValueIndex] = tmp;
                    for (int j = 0; j < globalMatrix.GetLength(0); j++)
                    {
                        tmp = globalMatrix[i, j];
                        globalMatrix[i, j] = globalMatrix[maxValueIndex, j];
                        globalMatrix[maxValueIndex, j] = tmp;
                    }
                }
                for (int j = i + 1; j < globalMatrix.GetLength(0); j++)
                {
                    double coef = globalMatrix[j, i] / globalMatrix[i, i];
                    for (int k = 0; k < globalMatrix.GetLength(0); k++)
                        globalMatrix[j, k] -= globalMatrix[i, k] * coef;
                    globalB[j] -= globalB[i] * coef;
                }
            }

            for (int i = globalMatrix.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < globalMatrix.GetLength(0); j++)
                    globalB[i] -= globalMatrix[i, j] * globalB[j];
                globalB[i] /= globalMatrix[i, i];
            }
        }
        public Func<double, double> Solve()
        {
            createGlobalMatrixAndGlobalVector();
            solve();

            Func<double, double> U = x =>
            {
                int[] numbers = null;
                double[] s = new double[2];
                double v1, v2;

                int i;
                for (i = 0; i < elemCount; i++)
                {
                    numbers = elems[i];
                    Array.Sort(numbers);

                    v1 = vertixes[numbers[0]];
                    v2 = vertixes[numbers[1]];

                    if(v1 <= x && x <= v2)
                    {
                        s[0] = (v2 - x) / (v2 - v1);
                        s[1] = (x - v1) / (v2 - v1);
                        break;
                    }
                }
                return globalB[numbers[0]] * s[0] + globalB[numbers[1]] * s[1];
            };
            return U;
        }
    }
}
