using System;

namespace EmreBeratKR.GeneticAlgorithm
{
    public static class MathfExtra
    {
        public static float Sigmoid(float x)
        {
            var numerator = 1d;
            var denominator = 1d + Math.Pow(Math.E, x);
            return (float) (numerator / denominator);
        }
    }
}