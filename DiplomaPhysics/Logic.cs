using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DiplomaPhysics
{
    public class Logic
    {
        private const double FadingIndicator_a = -0.03;
        private const double EnteringSignal_I0 = 100;
        private const double PieceLength_d = 0.1; //not known yet

        private int _amplifierNumber;
        private double _intensity;
        private double _strengthIndicator_x0;
        private double _noisePower;
        private double _areaLength_R;
        private int _numberOfPieces;
        private double _eAR;

        private Amplifier[] _amplifiers;
        public Amplifier[] Amplifiers { get => _amplifiers; }
        private AmplifierNoise[] _noises;
        public AmplifierNoise[] Noises { get => _noises; }

        public Logic(int amplifierNumbers, double intensity, double strengthIndicator, double noisePower, double areaLength, int numberOfPieces)
        {
            _amplifierNumber = amplifierNumbers;
            _intensity = intensity;
            _strengthIndicator_x0 = strengthIndicator;
            _noisePower = noisePower;
            _areaLength_R = areaLength;
            _numberOfPieces = numberOfPieces;
            _eAR = Math.Pow(Math.E, FadingIndicator_a * _areaLength_R);
            CalcSignalsForAmplifiers();
            CalcNoise();
        }

        private void CalcSignalsForAmplifiers()
        {
            var i1 = EnteringSignal_I0 * _eAR;
            var amplifiers = new List<Amplifier>();
            for(int i = 0;  i < _amplifierNumber; i++)
            {
                if (i == 0) {
                    amplifiers.Add(new Amplifier(i1, _strengthIndicator_x0, PieceLength_d, _intensity, _numberOfPieces));
                }
                else
                {
                    var amplifierSignalEnter = amplifiers[i - 1].AmplifierExit * _eAR;
                    amplifiers.Add(new Amplifier(amplifierSignalEnter, _strengthIndicator_x0, PieceLength_d, _intensity, _numberOfPieces));
                }
            }

            //amplifiers[amplifiers.Count - 1].AmplifierExit = amplifiers[amplifiers.Count - 1].AmplifierEntry * Math.Pow(Math.E, -1 * FadingIndicator_a * _areaLength_R);

            _amplifiers = amplifiers.ToArray();
        }

        public void CalcNoise()
        {
            var x0 = 5.3;
            var noise1 = new AmplifierNoise { NoiseEntry = 0, NoiseExit = _noisePower };
            var noises = new List<AmplifierNoise> { noise1 };
            for(int i = 1; i < _amplifierNumber; i++)
            {
                var previousExitNoise = noises[noises.Count - 1].NoiseExit;
                var entry = previousExitNoise * _eAR;
                var exit = entry * Math.Pow(Math.E, x0 * _numberOfPieces * PieceLength_d) + _noisePower;
                noises.Add(new AmplifierNoise { NoiseEntry = entry, NoiseExit = exit });
            }

            _noises = noises.ToArray();
        }

        public double[] CalcBer(Amplifier[] amplifiers, AmplifierNoise[] noises)
        {
            double[] berPoints = new double[_amplifierNumber + 1];
            berPoints[0] = 0;
            for (int i = 0; i < amplifiers.Length; i++)
            {
                var m1 = amplifiers[i].AmplifierExit;
                var m0 = noises[i].NoiseExit;
                var Q = Math.Abs(m1 - m0) / (m1 * 0.1 + m0 * 0.1);
                berPoints[i + 1] = Math.Pow(Math.E, -0.5 * Q) / (Q * Math.Sqrt(2 * Math.PI)) / Math.Pow(10, 10);
            }

            return berPoints;
        }
    }
    
    public class AmplifierNoise
    {
        public double NoiseEntry { get; set; }
        public double NoiseExit { get; set; }
    }
}
