using System.Media;

internal class Opcje
{
    private ConsoleKeyInfo przycisk;
    private byte jeden = 0; //Zmienna pomocnicza, określająca czy opcja numer jeden jest włączona czy wyłączona
    private byte dwa = 0;
    private SoundPlayer player = new SoundPlayer();

    int OryginalnaSzerokoscKonsoli = 125;
    int OryginalnaDlugoscKonsoli = 45;

    public Opcje()
    {
        Console.Clear();

        console(38, 4, "Naciśnij przycisk aby zmienić opcje:", ConsoleColor.White);
        console(47, 7, "1. Włącz muzykę []", ConsoleColor.White);
        console(45, 10, "2. Włącz fullscreena []", ConsoleColor.White);
        console(41, 13, "Wciśnij ESC aby wrócić do menu.", ConsoleColor.DarkYellow);

        if (Console.WindowWidth != 125 && Console.WindowHeight != 45)
        {
            console(66, 10, "██", ConsoleColor.White);
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
                        console(63, 7, "██", ConsoleColor.White);
                        jeden = 1;
                        GrajMuzyke(); //Włącz muzykę
                    }
                    else if (jeden == 1)
                    {
                        jeden = 0;
                        console(63, 7, "[]", ConsoleColor.White);
                        player.Stop(); //Wyłącz muzykę
                    }
                }

                if (przycisk.Key == ConsoleKey.D2)
                {
                    if (dwa == 0)
                    {
                        console(66, 10, "██", ConsoleColor.White);
                        dwa = 1;

                        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.WindowHeight = Console.LargestWindowHeight;
                        Console.WindowWidth = Console.LargestWindowWidth;
                        Console.SetWindowPosition(0, 0);
                    }
                    else if (dwa == 1)
                    {
                        console(66, 10, "[]", ConsoleColor.White);
                        dwa = 0;

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

    private void GrajMuzyke()
    {
        player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "muzyka.wav";
        player.Play(); //Włącz muzykę
    }

    private void console(int x, int y, string znak, ConsoleColor kolor = ConsoleColor.White)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = kolor;
        Console.Write(znak);
        Console.ResetColor();
    }
}