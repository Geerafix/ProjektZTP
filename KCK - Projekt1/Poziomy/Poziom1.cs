using EscapeRoom;
using EscapeRoom.Poziomy;
using System;
using System.Diagnostics;
using System.Threading;

internal class Poziom1 : Generator
{
    private ConsoleKeyInfo przycisk;
    private Stopwatch stoper = new Stopwatch();
    private long czas;
    private Random random = new Random();
    private bool running = true;

    public Poziom1(long czas)
    {
        this.czas = czas;
        stoper.Start();
    }

    protected override void Rysuj()
    {
        while (running)
        {
            Thread.Sleep(1);

            long pozostalyCzas = stoper.ElapsedMilliseconds;
            Console.SetCursorPosition(62, 0);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Czas: " + (pozostalyCzas + czas) / 1000 + " s");
            Console.ResetColor();

            if ((postac.GetX() >= 22 && postac.GetX() <= 30 && postac.GetY() >= 23 && postac.GetY() <= 29) ||
                (postac.GetX() >= 102 && postac.GetX() <= 110 && postac.GetY() >= 22 && postac.GetY() <= 30) ||
                ((postac.GetX() >= 49 && postac.GetX() <= 66 && postac.GetY() >= 13 && postac.GetY() <= 22)))
            {
                soundPlayer.DzwiekTrafienia();
                Console.SetCursorPosition(54, 15);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Wpadłeś do lawy");
                Console.SetCursorPosition(50, 16);
                Console.Write("*Wcisnij SPACE aby kontynuować*");
                Console.ResetColor();

                czas += stoper.ElapsedMilliseconds;
                stoper.Stop();

                int liczCzas = 0;

                for (; ; )
                {
                    liczCzas++;
                    if (liczCzas % 13000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.Write("                               ");
                    }
                    if (liczCzas % 15000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("*Wcisnij SPACE aby kontynuować*");
                        Console.ResetColor();
                        liczCzas = 0;
                    }

                    if (Console.KeyAvailable)
                    {
                        przycisk = Console.ReadKey(true);

                        if (przycisk.Key == ConsoleKey.Escape)
                        {
                            running = false;
                            Wyjdz();
                            break;
                        }
                        if (przycisk.Key == ConsoleKey.Spacebar)
                        {
                            Console.ResetColor();
                            stoper.Restart();
                            running = false;
                            Generator poziom = new Poziom1(czas);
                            poziom.GenerujPoziom();
                            break;
                        }
                    }
                }
            }

            if (Console.KeyAvailable)
            {
                przycisk = Console.ReadKey(true);

                if ((przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) && postac.GetY() >= 6)
                {
                    if (!((postac.GetX() >= 30 && postac.GetX() <= 50 && postac.GetY() == 30) ||
                        (postac.GetX() >= 67 && postac.GetX() <= 105 && postac.GetY() == 22)))
                    {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1);
                    }
                }
                if ((przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) && postac.GetY() <= 31)
                {
                    if (!((postac.GetX() >= 29 && postac.GetX() <= 49 && postac.GetY() == 22) ||
                        (postac.GetX() >= 66 && postac.GetX() <= 104 && postac.GetY() == 11)))
                    {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1);
                    }
                }
                if ((przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) && postac.GetX() >= 21)
                {
                    if (!((postac.GetY() <= 29 && postac.GetY() >= 23 && postac.GetX() == 51) ||
                        (postac.GetY() >= 12 && postac.GetY() <= 21 && postac.GetX() == 105)))
                    {
                        postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY());
                    }
                }
                if ((przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) && postac.GetX() <= 109)
                {
                    if (!((postac.GetY() <= 29 && postac.GetY() >= 23 && postac.GetX() == 30) ||
                        (postac.GetY() >= 12 && postac.GetY() <= 21 && postac.GetX() == 65)))
                    {
                        postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape)
                {
                    postac.UstawPozPoczatkowa();
                    running = false;
                    Wyjdz();
                    break;
                }
            }

            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
            {
                czas += stoper.ElapsedMilliseconds;
                running = false;
                stoper.Stop();
                soundPlayer.DzwiekPortalu();
                Generator poziom2 = new Poziom2(czas);
                poziom2.GenerujPoziom();
                break;
            }
        }
    }

    private void Wyjdz()
    {
        stoper.Stop();
        Console.ResetColor();
        soundPlayer.DzwiekWyjsciaZGry();
        Menu menu = new Menu();
    }
}
