using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Triangle
    {
        public double a;
        public double b;
        public double c;
        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public void Show() 
        {
            Console.WriteLine("The side a is: {0}",this.a);
            Console.WriteLine("The side b is: {0}",this.b);
            Console.WriteLine("The side c is: {0}",this.c);
        }

        public bool ValidateTriangle()
        {
            if (this.a > 0 && this.b > 0 && this.c > 0 && a+b > c && b+c > a && a+c>b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double Face() 
        {
            double p = (a + b + c) / (double) 2;

            double s = p * (p - a) * (p - b) * (p - c);

            return Math.Sqrt(s);
        }
    }
}
