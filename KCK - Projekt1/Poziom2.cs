using KCK___Projekt1;
using System.Diagnostics;

internal class Poziom2
{
    Postac postac = Postac.pobierzPostac();

    private ConsoleKeyInfo przycisk;
    char[] znakiPliku;

    private long czas;

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

    private Stopwatch stoper = new Stopwatch();

    public Poziom2(long czas)
    {
        Console.Clear();

        stoper.Start();

        this.czas = czas;

        string sciezkaDoPliku = "KCKPoziom2.txt";

        string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

        znakiPliku = zawartoscPliku.ToCharArray();

        int pom = 0; //zmienna która liczy ilość wgranych znaków z plików

        foreach (char c in znakiPliku)
        {
            pom++;
            if ((pom >= 175 && pom <= 200) || (pom >= 285 && pom <= 302))
            {
                Console.ForegroundColor = ConsoleColor.Green; //Brama do drugiego poziomu jst koloru zielonego
            }
            Console.Write(c);
            Console.ResetColor();
        }

        Rysuj();

    }

    public void Rysuj()
    {
        Console.WriteLine("\n");
        Console.WriteLine("   ___           _              ___ ");
        Console.WriteLine("  / _ \\___ ___ (____  __ _    |_  |");
        Console.WriteLine(" / ___/ _ /_ // / _ \\/  ' \\  / __/ ");
        Console.WriteLine("/_/   \\___/__/_/\\___/_/_/_/ /____/ ");

        Console.SetCursorPosition(40, 1);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("UWAŻAJ NA CZERWONE STRZAŁKI! NIE DAJ SIĘ USTRZELIĆ!");
        Console.ResetColor();

        //Ustaw pozycję postaci i narysują postać
        postac.UstawPozPoczatkowa();
        Console.SetCursorPosition(postac.GetX(), postac.GetY());
        Console.Write("██");
        Console.SetCursorPosition(0, 10);

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

                if (przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) //Jeżeli naciśnięta strzałka w górę lub "w"
                {
                    Console.SetCursorPosition(postac.GetX(), postac.GetY());
                    Console.Write("  ");
                    if (postac.GetY() >= 4) //Górna granica mapy
                    {
                        if((postac.GetX() == 35 && postac.GetY() == 28) || (postac.GetY() != 28) || (postac.GetX() == 93 && postac.GetY() == 28))
                        {
                            //postac = new Postac(postac.GetX(), postac.GetY() - 1); //Przesuń postać w górę
                            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1);
                        }
                    }
                    if (postac.GetY() == 5)
                    {
                        Console.SetCursorPosition(postac.GetX(), postac.GetY());
                        Console.Write("██");
                        Console.SetCursorPosition(0, 0);
                    }
                }
                if (przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) //Jeżeli naciśnięta strzałka w dół lub "s"
                {
                    Console.SetCursorPosition(postac.GetX(), postac.GetY());
                    Console.Write("  ");
                    if (postac.GetY() <= 31) //Dolna granica mapy
                    {
                        if((postac.GetX() == 35 && postac.GetY() == 26) || (postac.GetY() != 26) || (postac.GetX() == 93 && postac.GetY() == 26))
                        {
                            //postac = new Postac(postac.GetX(), postac.GetY() + 1); //Przesuń postać w dół
                            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1);
                        }
                    }
                    if (postac.GetY() == 32)
                    {
                        Console.SetCursorPosition(postac.GetX(), postac.GetY());
                        Console.Write("██");
                        Console.SetCursorPosition(0, 0);
                    }
                }
                if (przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) //Jeżeli naciśnięta strzałka w lewo lub "a"
                {
                    Console.SetCursorPosition(postac.GetX(), postac.GetY());
                    Console.Write("  ");
                    if (postac.GetX() >= 21) //Lewa granica mapy
                    {
                        if((postac.GetY() == 27 && postac.GetX() == 35) || (postac.GetY() == 27 && postac.GetX() == 93))
                        {

                        }
                        else
                        {
                            //postac = new Postac(postac.GetX() - 1, postac.GetY()); //Przesuń postać w lewo
                            postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY());
                        }
                    }
                    if (postac.GetX() == 20)
                    {
                        Console.SetCursorPosition(postac.GetX(), postac.GetY());
                        Console.Write("██");
                        Console.SetCursorPosition(0, 0);
                    }
                }
                if (przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) //Jeżeli naciśnięta strzałka w prawo lub "d"
                {
                    Console.SetCursorPosition(postac.GetX(), postac.GetY());
                    Console.Write("  ");
                    if (postac.GetX() <= 109) //Prawa granica mapy
                    {
                        if ((postac.GetY() == 27 && postac.GetX() == 35) || (postac.GetY() == 27 && postac.GetX() == 93))
                        {

                        }
                        else
                        {
                            //postac = new Postac(postac.GetX() + 1, postac.GetY()); //Przesuń postać w prawo
                            postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
                        }
                    }
                    if (postac.GetX() == 110)
                    {
                        Console.SetCursorPosition(postac.GetX(), postac.GetY());
                        Console.Write("██");
                        Console.SetCursorPosition(0, 0);
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape) //Wciśnij ESC aby wrócić do Menu
                {
                    Console.ResetColor();
                    stoper.Stop();
                    postac.UstawPozPoczatkowa();
                    Menu menu = new Menu();
                }

            }
            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 10);

            //Jeżeli postać jest na kordynatach bramy do poziomu numer 2
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 3 && postac.GetY() <= 4)
            {
                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                Poziom3 poziom3 = new Poziom3(czas); //Przenieś do poziomu trzeciego
            }

            //Jeżeli postać znajdzie się na terytorium Strzałka to zakończ grę
            if (CzyTrafiony())
            {
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
                            Poziom2 poziom = new Poziom2(czas);
                        }
                        if (przycisk.Key == ConsoleKey.Escape)
                        {
                            Console.ResetColor();
                            stoper.Stop();
                            Menu menu = new Menu();
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
}