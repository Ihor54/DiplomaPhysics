using System;
using System.Collections.Generic;
using System.Text;

namespace DiplomaPhysics
{
    public class Amplifier
    {
        public double AmplifierEntry { get; set; }
        public double AmplifierExit { get; set; }
        public List<double[]> PowerOfPieces { get; set; }

        public Amplifier(double i1, double x0, double d, double intensity, int piecesNumber)
        {
            AmplifierEntry = i1;

            double[] powers = new double[piecesNumber+1];
            double[] values_x0 = new double[piecesNumber+1];
            values_x0[0] = x0;
            for (int i = 0; i <= piecesNumber; i++)
            {
                if (i == 0)
                {
                    powers[i] = i1 * Math.Pow(Math.E, x0 * d);
                }
                else
                {
                    var x0_result = CalcStrengthIndicator(powers[i - 1], values_x0[i-1], intensity);
                    values_x0[i] = x0_result;
                    powers[i] = powers[i-1] * Math.Pow(Math.E, x0_result * d);
                }
            }
            
            AmplifierExit = powers[piecesNumber - 1];
        }

        private double CalcStrengthIndicator(double i, double x0, double intensity)
        {
            return x0 / (1 + (i / intensity));
        }
    }
}
