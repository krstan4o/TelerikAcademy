namespace CohesionAndCoupling
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ThreeDimensionalUtils
    {
        /// <summary>
        /// Calculates distance between two points in 3D space.
        /// </summary>
        /// <param name="x1">First point x axis.</param>
        /// <param name="y1">First point y axis.</param>
        /// <param name="z1">First point z axis.</param>
        /// <param name="x2">Second point x axis.</param>
        /// <param name="y2">Second point y axis.</param>
        /// <param name="z2">Second point z axis.</param>
        /// <returns>The calculated distance as double value.</returns>
        public static double CalcDistance3D(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double distance = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)) + ((z2 - z1) * (z2 - z1)));
            return distance;
        }

        /// <summary>
        /// Calculates the 3D diagonal of figure with width, height and depth.
        /// <seealso cref="http://calculus-geometry.hubpages.com/hub/How-to-Find-the-Diagonal-of-a-Rectangular-Box-Rectangular-Prism-with-Examples"/>
        /// </summary>
        /// <param name="width">The width of the figure.</param>
        /// <param name="height">The height of the figure.</param>
        /// <param name="depth">The depth of the figure.</param>
        /// <returns>The calculated diagonal XYZ as double value.</returns>
        public static double CalcDiagonalXYZ(double width, double height, double depth)
        {
            double distance = Math.Sqrt((width * width) + (height * height) + (depth * depth));
            return distance;
        }

        public static double CalcVolume(double width, double height, double depth)
        {
            double volume = width * height * depth;
            return volume;
        }
    }
}