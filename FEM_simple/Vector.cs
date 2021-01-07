using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM_simple
{
    public class Vector
    {
        public double[] values;
        public int size { get { return values.Length; } }
        public double this[int i]
        {
            get { return values[i]; }
            set { values[i] = value; }
        }
        public Vector(int size)
        {
            values = new double[size];
        }
        public Vector(params double[] values)
        {
            this.values = values;
        }
        public static Vector GenerateSimpleVector(int size)
        {
            Vector vec = new Vector(size);
            for (int i = 0; i < size; i++)
                vec[i] = i + 1;
            return vec;
        }
        public override string ToString()
        {
            return string.Join(" ", values.Select(value => value.ToString()));
        }
        public static Vector Parse(string text)
        {
            return new Vector(text.Trim().Split(' ').Select((string word) => { return double.Parse(word); }).ToArray());
        }
        public static double operator *(Vector a, Vector b)
        {
            double res = 0;
            for (int i = 0; i < a.size; i++)
                res += a[i] * b[i];
            return res;
        }
        public static Vector operator -(Vector a, Vector b)
        {
            Vector res = new Vector(a.size);
            for (int i = 0; i < a.size; i++)
                res[i] = a[i] - b[i];
            return res;
        }
    }
}
