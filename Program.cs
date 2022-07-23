using System;

namespace HILLS2
{

    class Calculation
    {
        double delta;
        public void DoMath()
        {
            Data data = new Data();

            if (data.PrepareControl())
            {
                if (data.v1[0] == 0 && data.v1[1] == 0)
                {
                    data.v1[0] = data.v2[0];
                    data.v1[1] = data.v2[1];
                }
                else
                {

                    double alpha = GetAlpha(data.v1[0], data.v2[0]);
                    double a = data.v1[1];
                    double b = data.v2[1];

                    data.EmptyStatus();

                    data.v1[1] = Parallelogram(a, b, alpha);
                    data.v1[0] = delta;
                }
            }
            else
            {
                data.EmptyStatus();
            }
        }

        private double Parallelogram(double a, double b, double alpha)
        {

            double radianAlpha = (alpha * (Math.PI)) / 180;
            double d = Math.Sqrt(a * a + 2 * a * b * Math.Cos(radianAlpha) + b * b);
            return d;

        }

        private double GetAlpha(double alpha, double beta)
        {
            double a = Math.Abs(alpha);
            double b = Math.Abs(beta);
            double c;
            double d;
            if (a < b)
            {
                c = b - a;
                d = a + b * (1 / 2);
            }
            else
            {
                c = a - b;
                d = b + a * (1 / 2);
            }
            delta = d;
            return c;
        }

    }

    class Data
    {
        public double[] v1 = new double[2];
        public double[] v2 = new double[2];
        public double[] v3 = new double[2];

        public void EmptyStatus()
        {
            int empty = 0;
            v1[0] = empty;
            v1[1] = empty;
            v2[0] = empty;
            v2[1] = empty;

        }

        public void DownloadData(int x, int y)
        {
            this.v2[0] = x;
            this.v2[1] = y;
        }
        public bool PrepareControl()
        {
            if (v2[0] == 0 && v2[1] == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void PrintVectorValue()
        {
            Console.WriteLine(v1[1]);
        }

    }

    class Order
    {
        Calculation calculation = new Calculation();
        public void Start()
        {
            int t = int.Parse(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                DoOrder();
            }
        }

        private void DoOrder()
        {
            Data data =  new Data();
            data.EmptyStatus();
            string line;
            string[] orders;

            int k1 = int.Parse(Console.ReadLine());
            for (int i = 0; i < k1; i++)
            {
                line = Console.ReadLine();
                if (line == "PREPARE")
                {
                    data.DownloadData(0, 0);
                    calculation.DoMath();
                }
                else
                {
                    orders = line.Split(" ");
                    if (orders[0] == "TURN")
                    {
                        data.DownloadData(int.Parse(orders[1]), 0);
                        calculation.DoMath();
                    }
                    else if (orders[0] == "MOVE")
                    {
                        data.DownloadData(0, int.Parse(orders[1]));
                        calculation.DoMath();
                    }
                }
            }
            data.PrintVectorValue();
        }

    }
    class Program
    {
        static void Main()
        {
            Order order = new Order();
            order.Start();
        }
    }
}
