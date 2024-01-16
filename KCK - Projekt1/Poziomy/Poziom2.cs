using EscapeRoom;
using EscapeRoom.Poziomy;
using EscapeRoom.ZapisGry;
using System.Diagnostics;

internal class Poziom2 : Generator
{
    private SoundPlayer soundPlayer = new SoundPlayer();
    private ConsoleKeyInfo przycisk;
    private long czas;
    private Stopwatch stoper = new Stopwatch();
    private bool running = true;

    int czas_strzalka = 0;
    int Strzalka1X = 109;
    int Strzalka1Y = 24;
    int Strzalka2X = 109;
    int Strzalka2Y = 20;
    int Strzalka3X = 109;
    int Strzalka3Y = 16;
    int Strzalka4X = 109;
    int Strzalka4Y = 12;

    int Strzalka5X = 21;
    int Strzalka5Y = 22;
    int Strzalka6X = 21;
    int Strzalka6Y = 18;
    int Strzalka7X = 21;
    int Strzalka7Y = 14;
    int Strzalka8X = 21;
    int Strzalka8Y = 26;

    public Poziom2(long czas)
    {
        Console.Clear();
        stoper.Start();
        this.czas = czas;
    }

    protected override void Rysuj()
    {
        while (running)
        {
            Thread.Sleep(1);

            // Wyświetl czas na ekranie.
            long pozostalyCzas = stoper.ElapsedMilliseconds;
            Console.SetCursorPosition(62, 0);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Czas: " + (pozostalyCzas + czas) / 1000 + " s");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            czas_strzalka++; //zmienna pomagająca ustalić prędkość strzałki
            if (czas_strzalka >= 10000) { czas_strzalka = 0; }

            //RYSUJ STRZAŁKI
            if (czas_strzalka % 1 == 0)
            {
                (Strzalka1X, Strzalka1Y) = PrzesunStrzalkeLewo(Strzalka1X, Strzalka1Y);
                (Strzalka5X, Strzalka5Y) = PrzesunStrzalkePrawo(Strzalka5X, Strzalka5Y);
            }

            if (czas_strzalka % 2 == 0)
            {
                (Strzalka2X, Strzalka2Y) = PrzesunStrzalkeLewo(Strzalka2X, Strzalka2Y);
                (Strzalka6X, Strzalka6Y) = PrzesunStrzalkePrawo(Strzalka6X, Strzalka6Y);
            }

            if (czas_strzalka % 3 == 0)
            {
                (Strzalka3X, Strzalka3Y) = PrzesunStrzalkeLewo(Strzalka3X, Strzalka3Y);
                (Strzalka7X, Strzalka7Y) = PrzesunStrzalkePrawo(Strzalka7X, Strzalka7Y);
            }

            if (czas_strzalka % 5 == 0)
            {
                (Strzalka4X, Strzalka4Y) = PrzesunStrzalkeLewo(Strzalka4X, Strzalka4Y);
                (Strzalka8X, Strzalka8Y) = PrzesunStrzalkePrawo(Strzalka8X, Strzalka8Y);
            }

            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                if ((przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) && (postac.GetY() >= 6)) //Jeżeli naciśnięta strzałka w górę lub "w"
                {
                    if ((postac.GetX() == 35 && postac.GetY() == 28) || (postac.GetY() != 28) || (postac.GetX() == 93 && postac.GetY() == 28))
                    {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1);
                    }
                }
                if ((przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) && (postac.GetY() <= 31)) //Jeżeli naciśnięta strzałka w dół lub "s"
                {
                    if ((postac.GetX() == 35 && postac.GetY() == 26) || (postac.GetY() != 26) || (postac.GetX() == 93 && postac.GetY() == 26))
                    {
                        postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1);
                    }
                }
                if ((przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) && (postac.GetX() >= 21)) //Jeżeli naciśnięta strzałka w lewo lub "a"
                {
                    if (!((postac.GetY() == 27 && postac.GetX() == 35)) || (postac.GetY() == 27 && postac.GetX() == 93))
                    {
                        postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY());
                    }
                }
                if ((przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) && (postac.GetX() <= 109)) //Jeżeli naciśnięta strzałka w prawo lub "d"
                {
                    if (!((postac.GetY() == 27 && postac.GetX() == 35)) || (postac.GetY() == 27 && postac.GetX() == 93))
                    {
                        postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape) //Wciśnij ESC aby wrócić do Menu
                {
                    postac.UstawPozPoczatkowa();
                    Wyjdz();
                    break;
                }
            }

            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 10);

            //Jeżeli postać jest na kordynatach bramy do poziomu numer 2
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
            {
                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                soundPlayer.DzwiekPortalu();
                Generator poziom3 = new Poziom3(czas); //Przenieś do poziomu trzeciego
                poziom3.GenerujPoziom();
            }

            //Jeżeli postać znajdzie się na terytorium Strzałka to zakończ grę
            if (CzyTrafiony())
            {
                soundPlayer.DzwiekTrafienia();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(55, 15);
                Console.WriteLine("Zostałeś trafiony"); //Komunikat o śmierci gracza
                Console.SetCursorPosition(50, 16);
                Console.WriteLine("*Wcisnij SPACE aby kontynuować*");

                this.czas += stoper.ElapsedMilliseconds;
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
                            Generator poziom = new Poziom2(czas);
                            poziom.GenerujPoziom();
                        }
                        if (przycisk.Key == ConsoleKey.Escape)
                        {
                            running = false;
                            Wyjdz();
                            break;
                        }
                    }
                }
            }
        }
    }

    private bool CzyTrafiony()
    {
        if ((Strzalka1X == postac.GetX() && Strzalka1Y == postac.GetY()) || ((Strzalka2X == postac.GetX() && Strzalka2Y == postac.GetY())) || (Strzalka3X == postac.GetX() && Strzalka3Y == postac.GetY()) || (Strzalka4X == postac.GetX() && Strzalka4Y == postac.GetY()) || (Strzalka5X + 1 == postac.GetX() && Strzalka5Y == postac.GetY()) || ((Strzalka6X + 1 == postac.GetX() && Strzalka6Y == postac.GetY())) || (Strzalka7X + 1 == postac.GetX() && Strzalka7Y == postac.GetY()) || (Strzalka8X + 1 == postac.GetX() && Strzalka8Y == postac.GetY()))
        {
            return true;
        }
        return false;
    }

    private (int x, int y) PrzesunStrzalkePrawo(int x, int y)
    {
        int nowyX = x;
        int nowyY = y;

        Console.SetCursorPosition(nowyX - 1, nowyY);
        Console.Write(" ");

        nowyX++;

        if (nowyX == 109)
        {
            Console.SetCursorPosition(nowyX - 1, nowyY);
            Console.Write("  ");
            nowyX = 21;
        }

        Console.SetCursorPosition(nowyX, nowyY); //Ustaw pozycję kursora na nową pozycję strzałki
        Console.ForegroundColor = ConsoleColor.Red; //Ustaw kolor czerowny
        Console.Write("->"); //Narysuj strzłkę
        Console.ResetColor(); //Zresetuj ustawiony wcześniej kolor
        Console.SetCursorPosition(0, 0);

        return (nowyX, nowyY);
    }

    private (int x, int y) PrzesunStrzalkeLewo(int x, int y)
    {
        int nowyX = x;
        int nowyY = y;

        Console.SetCursorPosition(nowyX + 1, nowyY);
        Console.Write(" ");

        nowyX--;

        if (nowyX == 20)
        {
            Console.SetCursorPosition(nowyX + 1, nowyY);
            Console.Write(" ");
            nowyX = 109;
        }

        Console.SetCursorPosition(nowyX, nowyY); //Ustaw pozycję kursora na nową pozycję strzałki
        Console.ForegroundColor = ConsoleColor.Red; //Ustaw kolor czerowny
        Console.Write("<-"); //Narysuj strzłkę
        Console.ResetColor(); //Zresetuj ustawiony wcześniej kolor
        Console.SetCursorPosition(0, 0);

        return (nowyX, nowyY);
    }

    public void Wyjdz() {
        stoper.Stop();
        Console.ResetColor();
        soundPlayer.DzwiekWyjsciaZGry();
        Menu menu = new Menu();
        menu.ZapiszPoziom(czas, 2);
    }
}