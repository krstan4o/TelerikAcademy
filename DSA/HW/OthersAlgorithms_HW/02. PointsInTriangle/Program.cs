using System;
using System.Collections.Generic;
using System.Linq;
   
class PointInTriangle
{
    static void Main()
    {
        Console.WriteLine("Given 3 points A, B and C, forming triangle, and a point P");
        Console.WriteLine("Check if the point P is in the triangle or not\n");
     
        //  triangle points
        Point a = new Point(-3.45, -2.12, 0);
        Point b = new Point(6.82, 1.9, 0);
        Point c = new Point(3.1, 10.4, 0);
     
        /*           Point a = new Point(1, 1, 0);
        Point b = new Point(1, 2, 0);
        Point c = new Point(2, 1, 0); */
     
        Vector3D va = new Vector3D(a.X,a.Y, 0);
        Vector3D vb = new Vector3D(b.X,b.Y, 0);
        Vector3D vc = new Vector3D(c.X,c.Y, 0);
     
        //  points to check
     
        //            Point p = new Point(-4.8, 9.2, 0);
        //            Vector3D vp = new Vector3D(p.X, p.Y, 0);
     
        Point p = new Point(3.2, 4.82, 0);
        Vector3D vp = new Vector3D(p.X, p.Y, 0);
     
        //            Point p = new Point(1.9, 1.9, 0);
        //            Vector3D vp = new Vector3D(p.X,p.Y,0);
     
        //  check if the point is in the triangle or not
        bool inTriangle = Vector3D.PointTriangle(vp, va, vb, vc);
        Console.Write("Is the point {0} inside the triangle\n\n{1} {2} {3} ? \t", p, a, b, c);
        Console.WriteLine(inTriangle);
        Console.WriteLine();
    }
}
    
class Vector3D
{
    private double x;
    private double y;
    private double z;
     
    public double X
    {
        get
        {
            return this.x;
        }
    }
     
    public double Y
    {
        get
        {
            return this.y;
        }
    }
     
    public double Z
    {
        get
        {
            return this.z;
        }
    }
     
    public double Length
    {
        get
        {
            double result = Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            return result;
        }
    }
     
    public Vector3D(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
     
    public static Vector3D operator +(Vector3D first, Vector3D second)
    {
        double resultX = first.X + second.X;
        double resultY = first.Y + second.Y;
        double resultZ = first.Z + second.Z;
        Vector3D result = new Vector3D(resultX, resultY, resultZ);
     
        return result;
    }
     
    public static Vector3D operator -(Vector3D first, Vector3D second)
    {
        double resultX = first.X - second.X;
        double resultY = first.Y - second.Y;
        double resultZ = first.Z - second.Z;
        Vector3D result = new Vector3D(resultX, resultY, resultZ);
     
        return result;
    }
     
    public static double DotProduct(Vector3D first, Vector3D second)
    {
        double result = first.X * second.X + first.Y * second.Y + first.Z * second.Z;
     
        return result;
    }
     
    public static Vector3D CrossProduct(Vector3D first, Vector3D second)
    {
        double resultX = first.Y * second.Z - first.Z * second.Y;
        double resultY = first.Z * second.X - first.X * second.Z;
        double resultZ = first.X * second.Y - first.Y * second.X;
        Vector3D result = new Vector3D(resultX, resultY, resultZ);
     
        return result;
    }
     
    public static bool SameSide(Vector3D p, Vector3D v1, Vector3D v2, Vector3D v3)
    {
        Vector3D cp1 = CrossProduct(v2 - v1, p - v1);
        Vector3D cp2 = CrossProduct(v2 - v1, v3 - v1);
        bool result = DotProduct(cp1, cp2) >= 0;
     
        return result;
    }
     
    public static bool PointTriangle(Vector3D p, Vector3D a, Vector3D b, Vector3D c)
    {
        bool result = SameSide(p, a, b, c) && SameSide(p, b, c, a) && SameSide(p, c, a, b);
        return result;
    }
     
    public override string ToString()
    {
        string result = string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
        return result;
    }
}
  
class Point
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }
     
    public Point(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
     
    public override string ToString()
    {
        string result = string.Format("({0}, {1})", this.X, this.Y, this.Z);
        return result;
    }
}


