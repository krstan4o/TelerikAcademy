namespace CohesionAndCoupling
{
    using System;

    public static class TwoDimensionalUtils
    {
        /// <summary>
        /// Calculates distance between two points in 2D space.
        /// </summary>
        /// <param name="x1">First point x axis.</param>
        /// <param name="y1">First point y axis.</param>
        /// <param name="x2">Second point x axis.</param>
        /// <param name="y2">Second point x axis.</param>
        /// <returns></returns>
        public static double CalcDistance2D(double x1, double y1, double x2, double y2)
        {
            double distance = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
            return distance;
        }

        /// <summary>
        /// Calculates diagonal of figure in 2D space.
        /// </summary>
        /// <param name="firstSide">First side of the figure.</param>
        /// <param name="secondSide">Second side of the figure.</param>
        /// <returns>The calculated diagonal as double value.</returns>
        public static double CalcDiagonal(double firstSide, double secondSide)
        {
            double distance = Math.Sqrt((firstSide * firstSide) + (secondSide * secondSide));
            return distance;
        }
    }
}