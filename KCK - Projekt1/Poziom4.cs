using KCK___Projekt1;
using System.Diagnostics;

internal class Poziom4
{
    Postac postac = Postac.pobierzPostac();

    private ConsoleKeyInfo przycisk;
    char[] znakiPliku;

    private long czas;

    int czasownik = 0;

    private Stopwatch stoper = new Stopwatch();

    public Poziom4(long czas)
    {
        Console.Clear();

        this.czas = czas;

        stoper.Start();

        string sciezkaDoPliku = "KCKPoziom3.txt";

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

        IPrzeciwnik przeciwnik1 = new PrzeciwnikChodzacy();
        przeciwnik1 = new Szybkosc(przeciwnik1);

        IPrzeciwnik przeciwnik2 = new PrzeciwnikStrzelajacy();
        przeciwnik2 = new Wielkosc(przeciwnik2);

        IPrzeciwnik przeciwnik3 = new PrzeciwnikChodzacy();
        przeciwnik3 = new Wielkosc(new Szybkosc(przeciwnik3));

        IPrzeciwnik przeciwnik4 = new PrzeciwnikStrzelajacy();
        przeciwnik4 = new Szybkosc(new Wielkosc(przeciwnik4));

        przeciwnik1.SetX(30);
        przeciwnik1.SetY(30);
        przeciwnik1.SetKierunek(true);

        przeciwnik2.SetX(50);
        przeciwnik2.SetY(20);
        przeciwnik1.SetKierunek(false);

        przeciwnik3.SetX(80);
        przeciwnik3.SetY(10);
        przeciwnik1.SetKierunek(true);

        przeciwnik4.SetX(50);
        przeciwnik4.SetY(15);
        przeciwnik1.SetKierunek(false);

        RysujPostac(przeciwnik1);
        RysujPostac(przeciwnik2);
        RysujPostac(przeciwnik3);
        RysujPostac(przeciwnik4);



        Thread.Sleep(1000);

        Console.SetCursorPosition(35, 1);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM!");
        Console.ResetColor();

        //Ustaw pozycję postaci i narysuj postać
        postac.UstawPozPoczatkowa();
        
        Console.SetCursorPosition(postac.GetX(), postac.GetY());
        Console.Write("██");
        Console.SetCursorPosition(0, 0);


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

            if(czas % 2 == 0)
            {
                RuszPrzeciwnika(przeciwnik1);
            }

            RysujPostac(przeciwnik1);
            RysujPostac(przeciwnik2);
            RysujPostac(przeciwnik3);
            RysujPostac(przeciwnik4);

            //PORUSZANIE POSTACIĄ
            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                if (przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) //Jeżeli naciśnięta strzałka w górę lub "w"
                {
                    if (postac.GetY() >= 4) //Górna granica mapy
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
                    Console.ResetColor();
                    stoper.Stop();
                    postac.UstawPozPoczatkowa();
                    Menu menu = new Menu();
                }

            }
            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 0);

            //Jeżeli postać jest na kordynatach bramy
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 3 && postac.GetY() <= 4)
            {
                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                Wyniki wynik = new Wyniki(czas); //Przenieś do Wyników
            }

            //JEŻELI POSTAĆ ZOSTAŁA ZABITA
            if (false) //CzyTrafiony()
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
/*    private bool CzyTrafiony() //Czy nasz bohater został dorwany przez przeciwnika
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
    }*/

    private void RysujPostac(IPrzeciwnik przeciwnik)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        for (int i = 0; i < przeciwnik.Wielkosc(); i++) 
        {
            for (int j = 0; j < przeciwnik.Wielkosc(); j++)
            {
                Console.SetCursorPosition(przeciwnik.GetX() + i, przeciwnik.GetY() + j);
                Console.Write("█");
            }
        }

        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }

    private void RuszPrzeciwnika(IPrzeciwnik przeciwnik)
    {
        if(przeciwnik.GetKierunek() == true)
        {
            przeciwnik.SetX(przeciwnik.GetX()-1);
        }
        if (przeciwnik.GetKierunek() == false)
        {
            przeciwnik.SetX(przeciwnik.GetX() + 1);
        }
        if (przeciwnik.GetX() <= 21)
        {
            przeciwnik.SetKierunek(false);
        }
        if (przeciwnik.GetX() >= 109)
        {
            przeciwnik.SetKierunek(true);
        }
    }
}
