using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPR4
{
    class GameInput
    {
        public double[][] Matrix { get; private set; }
        public int M { get; private set; }
        public int N { get; private set; }

        public void GetValues()
        {
            string s = "";
            bool flag = false;
            do
            {
                Console.WriteLine("Enter M: ");
                s = Console.ReadLine();
                flag = int.TryParse(s, out int d);
                M = d;
            } while (!flag);
            do
            {
                Console.WriteLine("Enter N: ");
                s = Console.ReadLine();
                flag = int.TryParse(s, out int d);
                N = d;
            } while (!flag);

            Matrix = new double[N][];
            for (int i = 0; i < N; i++)
                Matrix[i] = new double[M];
            Console.WriteLine("Set random matrix? (y/n)");
            s = Console.ReadLine();
            if (s == "y")
                SetRandom();
            else
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {                       
                        do
                        {
                            Console.Write(string.Format("Enter a[{0}][{1}]: ",i,j ) );
                            s = Console.ReadLine();
                            flag = double.TryParse(s, out double d);
                            Matrix[i][j] = d;
                        } while (!flag);
                    }
                    Console.WriteLine("----------");
                }
            }
        }

        public void SetRandom()
        {
            Random r = new Random();
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    Matrix[i][j] = r.Next(-10, 10);

        }

    }
}
