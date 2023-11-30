using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1 {
    internal class SoundPlayer {
        private WaveOutEvent waveOut;
        private SignalGenerator signalGenerator;

        public SoundPlayer() {
            waveOut = new WaveOutEvent();
            signalGenerator = new SignalGenerator();
            waveOut.Init(signalGenerator);
        }

        public void generate(double frequency, double amplitude, int durationInMilliseconds) {
            signalGenerator.Type = SignalGeneratorType.Square;
            signalGenerator.Frequency = frequency;
            signalGenerator.Gain = amplitude;
            waveOut.Play();
            Thread.Sleep(durationInMilliseconds);
            waveOut.Stop();
        }
    }
}
