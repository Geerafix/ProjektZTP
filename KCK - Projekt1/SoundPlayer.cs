using NAudio.Wave.SampleProviders;
using NAudio.Wave;

namespace EscapeRoom {
    internal class SoundPlayer {
        private WaveOutEvent fala;
        private SignalGenerator sygnal;

        public SoundPlayer() {
            fala = new WaveOutEvent();
            sygnal = new SignalGenerator();
            fala.Init(sygnal);
        }

        public void GenerujDzwiek(double czestotliowsc, double amplituda, int milisekundy) {
            sygnal.Type = SignalGeneratorType.Square;
            sygnal.Frequency = czestotliowsc;
            sygnal.Gain = amplituda;
            fala.Play();
            Thread.Sleep(milisekundy);
            fala.Stop();
        }

        public void DzwiekPortalu() {
            Thread thread = new(() => {
                GenerujDzwiek(500, 0.5, 10);
                GenerujDzwiek(400, 0.5, 10);
                GenerujDzwiek(600, 0.5, 10);
            });
            thread.Start();
        }

        public void DzwiekTrafienia() {
            Thread thread = new(() => {
                for (int i = 140 ; i >= 0 ; i -= 10) {
                    GenerujDzwiek(i, 0.5, 7);
                }
            });
            thread.Start();
        }

        public void DzwiekOdbiciaOdSciany() {
            Thread thread = new(() => {
                GenerujDzwiek(200, 0.5, 10);
            });
            thread.Start();
        }

        public void DzwiekWejsciaDoGry() {
            Thread thread = new(() => {
                GenerujDzwiek(200, 0.5, 20);
                GenerujDzwiek(300, 0.5, 60);
            });
            thread.Start();
        }

        public void DzwiekWyjsciaZGry() {
            Thread thread = new(() => {
                GenerujDzwiek(300, 0.5, 60);
                GenerujDzwiek(200, 0.5, 20);
            });
            thread.Start();
        }
    }
}
