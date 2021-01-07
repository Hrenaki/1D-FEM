using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM_simple
{
    public delegate double F(double x);
    class Program
    {
        static void Main(string[] args)
        {
            int elemCount = 10;
            int vertixesCount = 11;
            List<double> vertixes = new List<double>();
            List<int[]> elems = new List<int[]>();
            List<int[]> regions = new List<int[]>();
            Dictionary<int, double> materials = new Dictionary<int, double>();
            Dictionary<int, F> diffCoeffs = new Dictionary<int, F>();
            Dictionary<int, F> function = new Dictionary<int, F>();
            int i;
            #region test1 h = 0.01
            //elemCount = 100;
            //vertixesCount = 101;
            //
            //Console.WriteLine("h = 0.01");
            //for (i = 0; i < vertixesCount; i++)
            //    vertixes.Add((double)i / elemCount);
            //
            //for (i = 1; i < elemCount + 1; i++)
            //    elems.Add(new int[] { i - 1, i });
            //
            //materials.Add(0, 0);
            //diffCoeffs.Add(0, 1);
            //function.Add(0, x => -2.0);
            //
            //int[] e = new int[elemCount];
            //for (i = 0; i < e.Length; i++)
            //    e[i] = i;
            //
            //regions.Add(e);
            //
            //BoundaryCondition S1_1 = new BoundaryCondition(1, x => 0, 0);
            //BoundaryCondition S1_2 = new BoundaryCondition(1, x => 1, 100);
            //BoundaryCondition S2_1 = new BoundaryCondition(2, x => 0, 0);
            //BoundaryCondition S2_2 = new BoundaryCondition(2, x => -2, 100);
            //
            //Solver s = new Solver(elemCount, vertixesCount,
            //    elems, vertixes, regions, materials, diffCoeffs,
            //    function, S1_1, S1_2, S2_1, S2_2);
            //
            //Func<double, double> Uh = s.Solve();
            //Func<double, double> U = x =>
            //{
            //    return x * x;
            //};
            //double h = 0.01;
            //for (i = 0; i < vertixesCount; i++)
            //    if (i % (elemCount / 10) == 0)
            //        Console.WriteLine((h * i) + "\t" + U(h * i).ToString("E6") +
            //            "\t" + Uh(h * i).ToString("E6") + "\t" +
            //            Math.Abs(U(h * i) - Uh(h * i)).ToString("E6"));
            //Console.WriteLine();
            #endregion

            #region test1 h = 0.005
            //elemCount = 200;
            //vertixesCount = 201;
            //
            //Console.WriteLine("h = 0.005");
            //for (i = 0; i < vertixesCount; i++)
            //    vertixes.Add((double)i / elemCount);
            //
            //for (i = 1; i < elemCount + 1; i++)
            //    elems.Add(new int[] { i - 1, i });
            //
            //materials.Add(0, 0);
            //diffCoeffs.Add(0, 1);
            //function.Add(0, x => -2.0);
            //
            //int[] e = new int[elemCount];
            //for (i = 0; i < e.Length; i++)
            //    e[i] = i;
            //
            //regions.Add(e);
            //
            //BoundaryCondition S1_1 = new BoundaryCondition(1, x => 0, 0);
            //BoundaryCondition S1_2 = new BoundaryCondition(1, x => 1, 200);
            //BoundaryCondition S2_1 = new BoundaryCondition(2, x => 0, 0);
            //BoundaryCondition S2_2 = new BoundaryCondition(2, x => -2, 200);
            //
            //Solver s = new Solver(elemCount, vertixesCount,
            //    elems, vertixes, regions, materials, diffCoeffs,
            //    function, S1_1, S1_2, S2_1, S2_2);
            //
            //Func<double, double> Uh = s.Solve();
            //Func<double, double> U = x =>
            //{
            //    return x * x;
            //};
            //double h = 0.005;
            //for (i = 0; i < vertixesCount; i++)
            //    if (i % 20 == 0)
            //        Console.WriteLine((h * i) + "\t" + U(h * i).ToString("E6") +
            //            "\t" + Uh(h * i).ToString("E6") + "\t" +
            //            Math.Abs(U(h * i) - Uh(h * i)).ToString("E6"));
            //Console.WriteLine();
            #endregion

            #region test1 h = 0.0025
            //elemCount = 400;
            //vertixesCount = 401;
            //
            //Console.WriteLine("h = 0.0025");
            //for (i = 0; i < vertixesCount; i++)
            //    vertixes.Add((double)i / elemCount);
            //
            //for (i = 1; i < elemCount + 1; i++)
            //    elems.Add(new int[] { i - 1, i });
            //
            //materials.Add(0, 0);
            //diffCoeffs.Add(0, 1);
            //function.Add(0, x => -2.0);
            //
            //int[] e = new int[elemCount];
            //for (i = 0; i < e.Length; i++)
            //    e[i] = i;
            //
            //regions.Add(e);
            //
            //BoundaryCondition S1_1 = new BoundaryCondition(1, x => 0, 0);
            //BoundaryCondition S1_2 = new BoundaryCondition(1, x => 1, 400);
            //BoundaryCondition S2_1 = new BoundaryCondition(2, x => 0, 0);
            //BoundaryCondition S2_2 = new BoundaryCondition(2, x => -2, 400);
            //
            //Solver s = new Solver(elemCount, vertixesCount,
            //    elems, vertixes, regions, materials, diffCoeffs,
            //    function, S1_1, S1_2, S2_1, S2_2);
            //
            //Func<double, double> Uh = s.Solve();
            //Func<double, double> U = x =>
            //{
            //    return x * x;
            //};
            //double h = 0.0025;
            //for (i = 0; i < vertixesCount; i++)
            //    if (i % 40 == 0)
            //        Console.WriteLine((h * i) + "\t" + U(h * i).ToString("E6") +
            //            "\t" + Uh(h * i).ToString("E6") + "\t" +
            //            Math.Abs(U(h * i) - Uh(h * i)).ToString("E6"));
            //Console.WriteLine();
            #endregion

            #region test2 h = 0.01
            //elemCount = 100;
            //vertixesCount = 101;
            //
            //Console.WriteLine("h = 0.01");
            //for (i = 0; i < vertixesCount; i++)
            //    vertixes.Add((double)i / elemCount);
            //
            //for (i = 1; i < elemCount + 1; i++)
            //    elems.Add(new int[] { i - 1, i });
            //
            //materials.Add(0, 0);
            //diffCoeffs.Add(0, x => Math.Exp(x));
            //function.Add(0, x =>
            //{
            //    return Math.PI * Math.Exp(x) * (Math.PI * Math.Sin(Math.PI * x) - Math.Cos(Math.PI * x));
            //});
            //
            //int[] e = new int[elemCount];
            //for (i = 0; i < e.Length; i++)
            //    e[i] = i;
            //
            //regions.Add(e);
            //
            //BoundaryCondition S1_1 = new BoundaryCondition(1, x => 0, 0);
            //BoundaryCondition S1_2 = new BoundaryCondition(1, x => 0, 100);
            //BoundaryCondition S2_1 = new BoundaryCondition(2, x => Math.PI, 0);
            //BoundaryCondition S2_2 = new BoundaryCondition(2, x => Math.PI * Math.E, 100);
            //
            //Solver s = new Solver(elemCount, vertixesCount,
            //    elems, vertixes, regions, materials, diffCoeffs,
            //    function, S1_1, S1_2, S2_1, S2_2);
            //
            //Func<double, double> Uh = s.Solve();
            //Func<double, double> U = x =>
            //{
            //    return Math.Sin(Math.PI * x);
            //};
            //double h = 0.01;
            //for (i = 0; i < vertixesCount; i++)
            //    if (i % 10 == 0)
            //        Console.WriteLine((h * i) + "\t" + U(h * i).ToString("E6") +
            //            "\t" + Uh(h * i).ToString("E6") + "\t" +
            //            Math.Abs(U(h * i) - Uh(h * i)).ToString("E6"));
            //Console.WriteLine();
            #endregion

            #region test2 h = 0.005
            //elemCount = 200;
            //vertixesCount = 201;
            //
            //Console.WriteLine("h = 0.005");
            //for (i = 0; i < vertixesCount; i++)
            //    vertixes.Add((double)i / elemCount);
            //
            //for (i = 1; i < elemCount + 1; i++)
            //    elems.Add(new int[] { i - 1, i });
            //
            //materials.Add(0, 0);
            //diffCoeffs.Add(0, x => Math.Exp(x));
            //function.Add(0, x =>
            //{
            //    return Math.PI * Math.Exp(x) * (Math.PI * Math.Sin(Math.PI * x) - Math.Cos(Math.PI * x));
            //});
            //
            //int[] e = new int[elemCount];
            //for (i = 0; i < e.Length; i++)
            //    e[i] = i;
            //
            //regions.Add(e);
            //
            //BoundaryCondition S1_1 = new BoundaryCondition(1, x => 0, 0);
            //BoundaryCondition S1_2 = new BoundaryCondition(1, x => 0, 200);
            //BoundaryCondition S2_1 = new BoundaryCondition(2, x => Math.PI, 0);
            //BoundaryCondition S2_2 = new BoundaryCondition(2, x => Math.PI * Math.E, 200);
            //
            //Solver s = new Solver(elemCount, vertixesCount,
            //    elems, vertixes, regions, materials, diffCoeffs,
            //    function, S1_1, S1_2, S2_1, S2_2);
            //
            //Func<double, double> Uh = s.Solve();
            //Func<double, double> U = x =>
            //{
            //    return Math.Sin(Math.PI * x);
            //};
            //double h = 0.005;
            //for (i = 0; i < vertixesCount; i++)
            //    if (i % 20 == 0)
            //        Console.WriteLine((h * i) + "\t" + U(h * i).ToString("E6") +
            //            "\t" + Uh(h * i).ToString("E6") + "\t" +
            //            Math.Abs(U(h * i) - Uh(h * i)).ToString("E6"));
            //Console.WriteLine();
            #endregion

            #region test2 h = 0.0025
            elemCount = 400;
            vertixesCount = 401;

            Console.WriteLine("h = 0.0025");
            for (i = 0; i < vertixesCount; i++)
                vertixes.Add((double)i / elemCount);

            for (i = 1; i < elemCount + 1; i++)
                elems.Add(new int[] { i - 1, i });

            materials.Add(0, 0);
            diffCoeffs.Add(0, x => Math.Exp(x));
            function.Add(0, x =>
            {
                return Math.PI * Math.Exp(x) * (Math.PI * Math.Sin(Math.PI * x) - Math.Cos(Math.PI * x));
            });

            int[] e = new int[elemCount];
            for (i = 0; i < e.Length; i++)
                e[i] = i;

            regions.Add(e);

            BoundaryCondition S1_1 = new BoundaryCondition(1, x => 0, 0);
            BoundaryCondition S1_2 = new BoundaryCondition(1, x => 0, 400);
            BoundaryCondition S2_1 = new BoundaryCondition(2, x => Math.PI, 0);
            BoundaryCondition S2_2 = new BoundaryCondition(2, x => Math.PI * Math.E, 400);

            Solver s = new Solver(elemCount, vertixesCount,
                elems, vertixes, regions, materials, diffCoeffs,
                function, S1_1, S1_2, S2_1, S2_2);

            Func<double, double> Uh = s.Solve();
            Func<double, double> U = x =>
            {
                return Math.Sin(Math.PI * x);
            };
            double h = 0.0025;
            for (i = 0; i < vertixesCount; i++)
                if (i % 40 == 0)
                    Console.WriteLine((h * i) + "\t" + U(h * i).ToString("E6") +
                        "\t" + Uh(h * i).ToString("E6") + "\t" +
                        Math.Abs(U(h * i) - Uh(h * i)).ToString("E6"));
            Console.WriteLine();
            #endregion

            Console.ReadLine();
        }
    }
}