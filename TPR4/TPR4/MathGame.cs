using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR4
{
    class MathGame
    {
        int m, n;
        double[][] a;
        double[] sAns;
        double[] p;
        double minValue;
        public void Start()
        {
            GameInput gi = new GameInput();
            gi.GetValues();
            m = gi.M;
            n = gi.N;
            a = gi.Matrix;
            p = new double[n];
            Print();
            FindAB();
            Normalize();
            Print();
            Simplex();
            FindValue();
        }

        void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(a[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        void FindAB()
        {
            var maxI = new double[n];
            var minJ = new double[m];

            for (int i = 0; i < n; i++)
            {
                var max = a[i][0];
                for (int j = 0; j < m; j++)
                    max = max < a[i][j] ? a[i][j] : max;
                maxI[i] = max;
            }
            for (int j = 0; j < m; j++)
            {
                var min = a[0][j];
                for (int i = 0; i < n; i++)
                    min = min > a[i][j] ? a[i][j] : min;
                minJ[j] = min;
            }
            var lp = maxI.Min();
            var hp = minJ.Max();
            Console.WriteLine("a = " + lp.ToString() + " b = " + hp.ToString());
            if (lp < hp)
                throw (new Exception("a < b"));
        }

        void Normalize()
        {
            var min = a[0][0];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    min = min > a[i][j] ? a[i][j] : min;
            if (min < 0)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        a[i][j] = a[i][j] - min;
            minValue = min;
        }

        void Simplex()
        {
            Equation e = new Equation(n);
            for (int i = 0; i < n; i++)
                e.SetRow(i, a[i], true, 1);
            var f = new double[m];
            for (int i = 0; i < m; i++)
                f[i] = 1;
            e.SetFunc(f, false);
            e.Print();            
            e.Canonize();
            Console.WriteLine("Canonize:");
            e.Print();
            e.FindDbr();
            e.PrintDBR();
            e.Simplex();
            e.PrintAnswer();
            sAns = e.Answer;
        }

        void FindValue()
        {
            double sum = 0;
            foreach (var d in sAns)
                sum += d;
            var v = 1 / sum;
            Console.WriteLine("v = " + v.ToString());
            Console.WriteLine("Unmodified v = " + (v + minValue).ToString());
            Console.Write("S = [");
            for (int i = 0; i < n; i++)
            {
                p[i] = sAns[i] * v;
                Console.Write(p[i].ToString() + " ");
            }
            Console.WriteLine("]");
        }

    }
}
