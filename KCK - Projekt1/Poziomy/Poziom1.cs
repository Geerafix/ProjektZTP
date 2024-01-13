using KCK___Projekt1;
using System.Diagnostics;
using System;
using KCK___Projekt1.Poziomy;
using KCK___Projekt1.Command;

internal class Poziom1 : Generator
{
    Postac postac = Postac.pobierzPostac();
    char[] znakiPliku;

    Lewo lewo;
    Prawo prawo;
    Gora gora;
    Dol dol;

    private ConsoleKeyInfo przycisk;
    private Stopwatch stoper = new Stopwatch();
    private long czas;
    Random random = new Random();

    public Poziom1(long czas)
    {
        lewo = new Lewo(postac);
        prawo = new Prawo(postac);
        gora = new Gora(postac);
        dol = new Dol(postac);
        this.czas = czas;
        stoper.Start();
    }

    protected override void Rysuj()
    {

        while(true)
        {
            Thread.Sleep(1);

            // Wyświetl czas na ekranie.
            long pozostalyCzas = stoper.ElapsedMilliseconds;
            console(62, 0, "Czas: " + (pozostalyCzas + czas) / 1000 + " s", ConsoleColor.DarkBlue);

            //Jeżeli postać znajdzie się na terytorium lavy to zakończ grę
            if ((postac.GetX() >= 22 && postac.GetX() <= 30 && postac.GetY() >= 23 && postac.GetY() <= 29) || (postac.GetX() >= 102 && postac.GetX() <= 110 && postac.GetY() >= 22 && postac.GetY() <= 30) || ((postac.GetX() >= 49 && postac.GetX() <= 66 && postac.GetY() >= 13 && postac.GetY() <= 22)))
            {
                console(54, 15, "Wpadłeś do lawy", ConsoleColor.Yellow);
                console(50, 16, "*Wcisnij SPACE aby kontynuować*", ConsoleColor.Yellow);

                czas += stoper.ElapsedMilliseconds;
                stoper.Stop();

                int liczCzas = 0; //zmienna pomocnicza, do migania wiadomości

                for (; ; )
                {

                    liczCzas++;
                    if (liczCzas % 13000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.WriteLine("                               ");
                    }
                    if (liczCzas % 15000 == 0)
                    {
                        console(50, 16, "*Wcisnij SPACE aby kontynuować*", ConsoleColor.Yellow);
                        liczCzas = 0;
                    }

                    if (Console.KeyAvailable)
                    {
                        przycisk = Console.ReadKey(true);

                        if (przycisk.Key == ConsoleKey.Escape)
                        {
                            Console.ResetColor();
                            stoper.Stop();
                            Menu menu = new Menu();
                        }
                        if (przycisk.Key == ConsoleKey.Spacebar)
                        {
                            Console.ResetColor();
                            stoper.Restart();
                            Generator poziom = new Poziom1(czas);
                        }
                    }
                }

            }

            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                if ((przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) && postac.GetY() >= 6) //Jeżeli naciśnięta strzałka w górę lub "w"
                {

                    if (!((postac.GetX() >= 31 && postac.GetX() <= 51) && postac.GetY() == 30) || ((postac.GetX() >= 67 && postac.GetX() <= 105 && postac.GetY() == 22))) {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1); //Przzesuń postać w górę
                    }
                }
                if ((przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) && postac.GetY() <= 31) //Jeżeli naciśnięta strzałka w dół lub "s"
                {
                    if (!((postac.GetX() >= 31 && postac.GetX() <= 51 && postac.GetY() == 22)) || ((postac.GetX() >= 67 && postac.GetX() <= 105 && postac.GetY() == 11))) {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1); //Przesuń postać w dół
                    }
                }
                if ((przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) && postac.GetX() >= 21) //Jeżeli naciśnięta strzałka w lewo lub "a"
                {
                    if (!((postac.GetY() <= 29 && postac.GetY() >= 23 && postac.GetX() == 52)) || ((postac.GetY() >= 12 && postac.GetY() <= 21 && postac.GetX() == 106))) {
                        postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY()); //Przesuń postać w lewo
                    }
                }
                if ((przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) && postac.GetX() <= 109) //Jeżeli naciśnięta strzałka w prawo lub "d"
                {
                    if (!((postac.GetY() <= 29 && postac.GetY() >= 23 && postac.GetX() == 30)) || ((postac.GetY() >= 12 && postac.GetY() <= 21 && postac.GetX() == 66))) {
                        postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY()); //Przesuń postać w prawo
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape) //Kliknij ESC aby wyjść do MENU
                {
                    stoper.Stop();
                    postac.UstawPozPoczatkowa();
                    Menu menu = new Menu();
                }
            }


            //Jeżeli postać jest na kordynatach bramy do poziomu numer 2
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
            {
                czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                Generator poziom2 = new Poziom2(czas); //Przenieś do poziomu drugiego
            }
        }

    }

    public void console(int x, int y, string str, ConsoleColor? colour) {
        Console.SetCursorPosition(x, y);
        if (colour != null) Console.ForegroundColor = colour.Value;
        Console.WriteLine(str);
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }
}