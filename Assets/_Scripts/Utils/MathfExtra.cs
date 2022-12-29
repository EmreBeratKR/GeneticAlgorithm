using System;

namespace Utils
{
    public static class MathfExtra
    {
        public static float Sigmoid(float x, double @base = Math.E)
        {
            var numerator = 1d;
            var denominator = 1d + Math.Pow(@base, -x);
            return (float) (numerator / denominator);
        }
    }
}