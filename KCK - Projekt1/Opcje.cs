using System.Media;

internal class Opcje
{
    private ConsoleKeyInfo przycisk;
    private byte jeden = 0; //Zmienna pomocnicza, określająca czy opcja numer jeden jest włączona czy wyłączona
    private byte dwa = 0;

    int OryginalnaSzerokoscKonsoli = 125;
    int OryginalnaDlugoscKonsoli = 45;

    public SoundPlayer player = new SoundPlayer();

    public Opcje()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Console.WriteLine("\n");
        Console.WriteLine("                                      Naciśnij przycisk aby zmienić opcje:");
        Console.ResetColor();
        Console.WriteLine("\n");
        Console.WriteLine("                                               1. Włącz muzykę []");
        Console.WriteLine("\n");
        Console.WriteLine("                                             2. Włącz fullscreena []");
        Console.WriteLine("\n");
        Console.WriteLine("\n");
        Console.ForegroundColor= ConsoleColor.DarkYellow;
        Console.WriteLine("                                         Wciśnij ESC aby wrócić do menu.");
        Console.ResetColor();

        if(Console.WindowWidth != 125 && Console.WindowHeight != 45)
        {
            Console.SetCursorPosition(66, 10);
            Console.Write("██");
            Console.SetCursorPosition(0, 0);
            dwa = 1;
        }

        for (; ; )
        {
            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                if (przycisk.Key == ConsoleKey.D1)
                {
                    if (jeden == 0)
                    {
                        Console.SetCursorPosition(63, 7);
                        Console.Write("██");
                        Console.SetCursorPosition(0, 0);
                        jeden = 1;
                        GrajMuzyke(); //Włącz muzykę
                    }
                    else if (jeden == 1)
                    {
                        jeden = 0;
                        Console.SetCursorPosition(63, 7);
                        Console.Write("[]");
                        Console.SetCursorPosition(0, 0);
                        player.Stop(); //Wyłącz muzykę
                    }
                }

                if (przycisk.Key == ConsoleKey.D2)
                {
                    if (dwa == 0)
                    {
                        Console.SetCursorPosition(66, 10);
                        Console.Write("██");
                        Console.SetCursorPosition(0, 0);
                        dwa = 1;

                        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.WindowHeight = Console.LargestWindowHeight;
                        Console.WindowWidth = Console.LargestWindowWidth;
                        Console.SetWindowPosition(0, 0);

                    }
                    else if (dwa == 1)
                    {
                        dwa = 0;
                        Console.SetCursorPosition(66, 10);
                        Console.Write("[]");
                        Console.SetCursorPosition(0, 0);

                        Console.SetWindowSize(OryginalnaSzerokoscKonsoli, OryginalnaDlugoscKonsoli);
                        Console.SetBufferSize(OryginalnaSzerokoscKonsoli, OryginalnaDlugoscKonsoli);
                        Console.WindowHeight = OryginalnaDlugoscKonsoli;
                        Console.WindowWidth = OryginalnaSzerokoscKonsoli;
                    }
                }

                if(przycisk.Key == ConsoleKey.Escape)
                {
                    Menu menu = new Menu();
                    break;
                }
            }
        }
    }

    public void GrajMuzyke()
    {
        player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "muzyka.wav";
        player.Play(); //Włącz muzykę
    }
}

