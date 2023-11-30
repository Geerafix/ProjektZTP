using KCK___Projekt1;
using System.Diagnostics;
using System.Threading;

internal class Poziom4 : Generator
{
    Postac postac = Postac.pobierzPostac();
    SoundPlayer sp = new SoundPlayer();

    private ConsoleKeyInfo przycisk;
    char[] znakiPliku;
    Random random = new Random();

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


    protected override void Rysuj()
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



        Thread.Sleep(1000);

        console(35, 1, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM!", ConsoleColor.Yellow);

        //Ustaw pozycję postaci i narysuj postać
        postac.UstawPozPoczatkowa();

        long pozostalyCzas = stoper.ElapsedMilliseconds;

        for (; ; )
        {
            Thread.Sleep(0);

            console(62, 0, "Czas: " + (pozostalyCzas + czas) / 1000 + " s", ConsoleColor.DarkBlue);

            if(czas % 10 == 0)
            {
                WyczyscPrzeciwnika(przeciwnik1);
                RuszPrzeciwnika(przeciwnik1);
            }

            if(czas % 15 == 0)
            {
                WyczyscPrzeciwnika(przeciwnik2);
                RuszPrzeciwnika(przeciwnik2);
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

        for (int i = 0 ; i < przeciwnik.Wielkosc() ; i++) {
            for (int j = 0 ; j < przeciwnik.Wielkosc() ; j++) {
                Console.SetCursorPosition(przeciwnik.GetX() + i, przeciwnik.GetY() + j);
                Console.Write("█");
            }
        }

        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }

    private void WyczyscPrzeciwnika(IPrzeciwnik przeciwnik)
    {
        for (int i = 0; i < przeciwnik.Wielkosc(); i++)
        {
            for (int j = 0; j < przeciwnik.Wielkosc(); j++)
            {
                Console.SetCursorPosition(przeciwnik.GetX() + i, przeciwnik.GetY() + j);
                Console.Write(" ");
            }
        }
    }

    private void RuszPrzeciwnika(IPrzeciwnik przeciwnik)
    {
        if (przeciwnik.GetKierunek() == true) 
        {
            przeciwnik.SetX(przeciwnik.GetX() - 1);
            przeciwnik.SetY((int)(15 + (Math.Sin(przeciwnik.GetX() / 10.0) * 3)));
        } 
        else 
        {
            przeciwnik.SetX(przeciwnik.GetX() + 1);
            przeciwnik.SetY((int)(15 + (Math.Sin(przeciwnik.GetX() / 10.0) * 3)));
        }

        // Zmiana kierunku przy osiągnięciu granic
        if (przeciwnik.GetX() <= 21)
        {
            przeciwnik.SetKierunek(false);
            Thread thread = new Thread(() => {
                sp.generate(400, 0.1, 8);
            });
            thread.Start();
            thread.Join();
        }
        else if (przeciwnik.GetX() >= 108)
        {
            przeciwnik.SetKierunek(true);
            Thread thread = new Thread(() => {
                sp.generate(400, 0.1, 8);
            });
            thread.Start();
            thread.Join();
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
