/*using System;
using System.Threading;

class smieci {
    static void Main() {
        Console.CursorVisible = false;
        Random random = new Random();

        int x = 0, y = 1;
        int speed = 1;

        while (true) {
            console(x, y, "  ", ConsoleColor.Black);
            x += speed;
            if (x <= 0 || x >= 100) {
                speed = -speed;
            }
            console(x, y, "██", ConsoleColor.Red);

            Thread.Sleep(20);
        }

    }

    public static void console(int x, int y, string str, ConsoleColor? colour) {
        Console.SetCursorPosition(x, y);
        if (colour != null) Console.ForegroundColor = colour.Value;
        Console.WriteLine(str);
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }
}

*/

/*using System;
using System.Threading;

class Przeciwnik {
    private int x;
    private int y;
    private int speed;

    public Przeciwnik(int initialX, int initialY, int initialSpeed) {
        x = initialX;
        y = initialY;
        speed = initialSpeed;
    }

    public void Move() {
        Console.CursorVisible = false;

        while (true) {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("  ");

            x += speed;

            if (x <= 0 || x >= 100) {
                speed = -speed;
            }

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("██");

            Thread.Sleep(100);
        }
    }
}

class smieci {
    static void Main() {
        Przeciwnik Przeciwnik = new Przeciwnik(0, 1, 1);
        Przeciwnik.Move();
    }
}
*/









// FUNKCJE GENERUJĄCE 16 BITOWY DŹWIĘK
/*using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

public class SoundGenerator {
    private WaveOutEvent waveOut;
    private SignalGenerator signalGenerator;

    public SoundGenerator() {
        waveOut = new WaveOutEvent();
        signalGenerator = new SignalGenerator();
        waveOut.Init(signalGenerator);
    }

    public void GenerateGameSound(double frequency, double amplitude, int durationInMilliseconds) {
        signalGenerator.Type = SignalGeneratorType.Square;
        signalGenerator.Frequency = frequency;
        signalGenerator.Gain = amplitude;
        waveOut.Play();
        Thread.Sleep(durationInMilliseconds);
        waveOut.Stop();
    }
}

class Program {
    static void Main() {
        SoundGenerator soundGenerator = new SoundGenerator();

        for (int i = 140 ; i >= 0 ; i -= 10) {
            soundGenerator.GenerateGameSound(i, 0.1, 8);
        }


    }
}*/