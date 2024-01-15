﻿using EscapeRoom;
using EscapeRoom.Poziomy;
using System.Diagnostics;


internal class Poziom3 : Generator
{
    private SoundPlayer soundPlayer = new SoundPlayer();
    private ConsoleKeyInfo przycisk;
    private long czas;
    int czasownik = 0;
    private Stopwatch stoper = new Stopwatch();

    private int EnemyX1 = 40;
    private int EnemyY1 = 7;

    private int EnemyX2 = 90;
    private int EnemyY2 = 7;

    private int EnemyX3 = 25;
    private int EnemyY3 = 9;

    private int EnemyX4 = 60;
    private int EnemyY4 = 9;

    private int EnemyX5 = 75;
    private int EnemyY5 = 8;

    public Poziom3(long czas)
    {
        Console.Clear();
        this.czas = czas;
        stoper.Start();
    }

    protected override void Rysuj()
    {
        //Ustaw pozycję przeciwnika i narysują postać
        RysujPrzeciwnika(EnemyX1, EnemyY1);
        RysujPrzeciwnika(EnemyX2, EnemyY2);
        RysujPrzeciwnika(EnemyX3, EnemyY3);
        RysujPrzeciwnika(EnemyX4, EnemyY4);
        RysujPrzeciwnika(EnemyX5, EnemyY5);

        for (; ; )
        {
            Thread.Sleep(1);

            // Wyświetl czas na ekranie.
            long pozostalyCzas = stoper.ElapsedMilliseconds;
            Console.SetCursorPosition(62, 0);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Czas: " + (pozostalyCzas + czas) / 1000 + " s");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            czasownik++; //Szybkość przeciwników zależy od "czasownika"
            if (czasownik >= 10000) 
                czasownik = 0;

            //RYSOWANIE PRZECIWNIKÓW
            //Przeciwnik 1
            if (czasownik % 10 == 0)
                (EnemyX1, EnemyY1) = PrzesunPrzeciwnika(EnemyX1, EnemyY1);

            //Przeciwnik 2
            if (czasownik % 15 == 0)
                (EnemyX2, EnemyY2) = PrzesunPrzeciwnika(EnemyX2, EnemyY2);

            //Przeciwnik 3 i 4
            if (czasownik % 20 == 0)
            {
                (EnemyX3, EnemyY3) = PrzesunPrzeciwnika(EnemyX3, EnemyY3);
                (EnemyX4, EnemyY4) = PrzesunPrzeciwnika(EnemyX4, EnemyY4);
            }

            // Przeciwnik 5
            if (czasownik % 25 == 0)
                (EnemyX5, EnemyY5) = PrzesunPrzeciwnika(EnemyX5, EnemyY5);


            //PORUSZANIE POSTACIĄ
            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                if (przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) //Jeżeli naciśnięta strzałka w górę lub "w"
                {
                    if (postac.GetY() >= 6) //Górna granica mapy
                    {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1);
                    }
                }
                if (przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) //Jeżeli naciśnięta strzałka w dół lub "s"
                {
                    if (postac.GetY() <= 31) //Dolna granica mapy
                    {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1);
                    }
                }
                if (przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) //Jeżeli naciśnięta strzałka w lewo lub "a"
                {
                    if (postac.GetX() >= 21) //Lewa granica mapy
                    {
                        postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY());
                    }
                }
                if (przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) //Jeżeli naciśnięta strzałka w prawo lub "d"
                {
                    if (postac.GetX() <= 109) //Prawa granica mapy
                    {
                        postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY()); //Przesuń postać w prawo
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape) //Wyjdź do menu
                {
                    postac.UstawPozPoczatkowa();
                    Wyjdz();
                    break;
                }

            }
            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 0);

            //Jeżeli postać jest na kordynatach bramy
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
            {
                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                Generator poziom = new Poziom4(czas); //Przenieś do poziomu czwartego
                poziom.GenerujPoziom();
            }

            //JEŻELI POSTAĆ ZOSTAŁA ZABITA
            if (CzyTrafiony())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(56, 15);
                Console.WriteLine("Dopadł cie"); //Komunikat o śmierci gracza
                Console.SetCursorPosition(0, 0);

                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();

                int liczCzas = 10000; //zmienna pomocnicza, do migania wiadomości

                for (; ; )
                {
                    //Wyświetlenie wiadomości po śmierci gracza
                    liczCzas++;
                    if (liczCzas % 13000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.WriteLine("                               ");
                    }
                    if (liczCzas % 15000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.WriteLine("*Wcisnij SPACE aby kontynuować*");
                        liczCzas = 0;
                    }

                    if (Console.KeyAvailable)
                    {
                        przycisk = Console.ReadKey(true);

                        if (przycisk.Key == ConsoleKey.Spacebar)
                        {
                            Console.ResetColor();
                            stoper.Restart();
                            Poziom3 poziom = new Poziom3(czas);
                            poziom.GenerujPoziom();
                        }
                        if (przycisk.Key == ConsoleKey.Escape)
                        {
                            Wyjdz();
                            break;
                        }
                    }
                }

            }
        }
    }

    private bool CzyTrafiony() //Czy nasz bohater został dorwany przez przeciwnika
    {
        if ((EnemyX1 == postac.GetX() && EnemyY1 == postac.GetY()) || (EnemyX2 == postac.GetX() && EnemyY2 == postac.GetY()) || (EnemyX3 == postac.GetX() && EnemyY3 == postac.GetY()) || (EnemyX4 == postac.GetX() && EnemyY4 == postac.GetY()) || (EnemyX5 == postac.GetX() && EnemyY5 == postac.GetY()))
        {
            return true;
        }
        if ((EnemyX1 + 1 == postac.GetX() && EnemyY1 == postac.GetY()) || (EnemyX2 + 1 == postac.GetX() && EnemyY2 == postac.GetY()) || (EnemyX3 + 1 == postac.GetX() && EnemyY3 == postac.GetY()) || (EnemyX4 + 1 == postac.GetX() && EnemyY4 == postac.GetY()) || (EnemyX5 + 1 == postac.GetX() && EnemyY5 == postac.GetY()))
        {
            return true;
        }
        return false;
    }

    private (int x, int y) PrzesunPrzeciwnika(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write("  ");

        if (postac.GetX() > x) x++;
        if (postac.GetX() < x) x--;
        if (postac.GetY() > y) y++;
        if (postac.GetY() < y) y--;

        RysujPrzeciwnika(x, y);
        return (x, y);
    }

    private void RysujPrzeciwnika(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("██");
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }

    private void Wyjdz() {
        stoper.Stop();
        Console.ResetColor();
        soundPlayer.DzwiekWyjsciaZGry();
        Menu menu = new Menu();
        menu.NarysujOpcje();
        menu.RysujLogo();
        menu.WlaczOpcje();
    }
}


