using KCK___Projekt1;
using KCK___Projekt1.Poziomy;
using KCK___Projekt1.Przeciwnik;
using System.Diagnostics;

internal class Poziom4 : Generator
{
    Postac postac = Postac.pobierzPostac();
    SoundPlayer sp = new SoundPlayer();

    private ConsoleKeyInfo przycisk;
    char[] znakiPliku;
    Random random = new Random();

    private long czas;

    int pom = 0;

    bool KtoryStrzelecStrzela = true;

    int czasownik = 0;

    private Stopwatch stoper = new Stopwatch();

    IPrzeciwnik przeciwnik1 = new PrzeciwnikChodzacy();
    IPrzeciwnik przeciwnik2 = new PrzeciwnikStrzelajacy();
    IPrzeciwnik przeciwnik3 = new PrzeciwnikChodzacy();
    IPrzeciwnik przeciwnik4 = new PrzeciwnikStrzelajacy();

    StrzalkaManager Strzalki = new StrzalkaManager();

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
    }


    protected override void Rysuj()
    {
        przeciwnik1 = new Szybkosc(przeciwnik1);

        przeciwnik2 = new Wielkosc(przeciwnik2);

        przeciwnik3 = new Wielkosc(new Szybkosc(przeciwnik3));

        przeciwnik4 = new Szybkosc(new Wielkosc(przeciwnik4));

        przeciwnik1.SetX(30);
        przeciwnik1.SetY(29);
        przeciwnik1.SetKierunek(true);
        przeciwnik1.SetPozycja(9);

        przeciwnik2.SetX(50);
        przeciwnik2.SetY(22);
        przeciwnik2.SetKierunek(false);
        przeciwnik2.SetPozycja(12);

        przeciwnik3.SetX(80);
        przeciwnik3.SetY(15);
        przeciwnik3.SetKierunek(true);
        przeciwnik3.SetPozycja(15);

        przeciwnik4.SetX(70);
        przeciwnik4.SetY(25);
        przeciwnik4.SetKierunek(false);
        przeciwnik4.SetPozycja(18);


        Thread.Sleep(1000);

        console(35, 1, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM!", ConsoleColor.Yellow);

        //Ustaw pozycję postaci i narysuj postać
        postac.UstawPozPoczatkowa();

        long pozostalyCzas;

        for (; ; )
        {
            Thread.Sleep(1);

            pom++;

            pozostalyCzas = stoper.ElapsedMilliseconds;

            console(62, 0, "Czas: " +  (pozostalyCzas + czas) / 1000 + " s", ConsoleColor.DarkBlue);

            if (pom % 1 == 0) //Tutaj zrobić żeby poruszali się z różną prędkością
            {
                WyczyscPrzeciwnika(przeciwnik1);
                RuszPrzeciwnika(przeciwnik1);
                WyczyscPrzeciwnika(przeciwnik2);
                RuszPrzeciwnika(przeciwnik2);
                WyczyscPrzeciwnika(przeciwnik3);
                RuszPrzeciwnika(przeciwnik3);
                WyczyscPrzeciwnika(przeciwnik4);
                RuszPrzeciwnika(przeciwnik4);
            }

            if (pom % 1000 == 0)
            {

                //Strzelcy strzelają na zmianę
                if (KtoryStrzelecStrzela == true)
                {
                    Strzalki.StworzObiekt(przeciwnik2, postac.GetX(), postac.GetY());
                    KtoryStrzelecStrzela = false;
                }
                else
                {
                    Strzalki.StworzObiekt(przeciwnik4, postac.GetX(), postac.GetY());
                    KtoryStrzelecStrzela = true;
                }
            }

            if (pom % 15 == 0)
            {
                Strzalki.RuszStrzalki();
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
                        postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
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
                Thread thread = new Thread(() => {
                    sp.generate(500, 0.5, 10);
                    sp.generate(400, 0.5, 10);
                    sp.generate(600, 0.5, 10);
                });
                thread.Start();
                Wyniki wynik = new Wyniki(czas); //Przenieś do Wyników
            }

            //JEŻELI POSTAĆ ZOSTAŁA ZABITA
            if (CzyTrafiony())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(56, 15);
                Console.WriteLine("Dopadł cie"); //Komunikat o śmierci gracza
                Console.SetCursorPosition(0, 0);

                Thread thread = new Thread(() => {
                    for (int i = 140 ; i >= 0 ; i -= 10) {
                        sp.generate(i, 0.5, 7);
                    }
                });
                thread.Start();
                thread.Join();

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

                    Thread.Sleep(2000);

                    if (Console.KeyAvailable)
                    {
                        przycisk = Console.ReadKey(true);

                        if (przycisk.Key == ConsoleKey.Spacebar)
                        {
                            Console.ResetColor();
                            stoper.Restart();
                            Generator poziom = new Poziom3(czas);
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
    private bool CzyTrafiony() //Czy nasz bohater został dorwany przez przeciwnika
    {
        if (postac.GetX() >= przeciwnik1.GetX() && postac.GetX() <= przeciwnik1.GetX() + przeciwnik1.Wielkosc() && postac.GetY() >= przeciwnik1.GetY() && postac.GetY() <= przeciwnik1.GetY() + przeciwnik1.Wielkosc()) {
            return true;
        }
        if (postac.GetX() >= przeciwnik2.GetX() && postac.GetX() <= przeciwnik2.GetX() + przeciwnik2.Wielkosc() && postac.GetY() >= przeciwnik2.GetY() && postac.GetY() <= przeciwnik2.GetY() + przeciwnik2.Wielkosc())
        {
            return true;
        }
        if (postac.GetX() >= przeciwnik3.GetX() && postac.GetX() <= przeciwnik3.GetX() + przeciwnik3.Wielkosc() && postac.GetY() >= przeciwnik3.GetY() && postac.GetY() <= przeciwnik3.GetY() + przeciwnik3.Wielkosc())
        {
            return true;
        }
        if (postac.GetX() >= przeciwnik4.GetX() && postac.GetX() <= przeciwnik4.GetX() + przeciwnik4.Wielkosc() && postac.GetY() >= przeciwnik4.GetY() && postac.GetY() <= przeciwnik4.GetY() + przeciwnik4.Wielkosc())
        {
            return true;
        }
        return false;
    }

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
            przeciwnik.SetY((int)(przeciwnik.GetPozycja() + (Math.Sin(przeciwnik.GetX() / 5.0) * 2)));
        } 
        else 
        {
            przeciwnik.SetX(przeciwnik.GetX() + 1);
            przeciwnik.SetY((int)(przeciwnik.GetPozycja() + (Math.Sin(przeciwnik.GetX() / 5.0) * 2)));
        }

        // Zmiana kierunku przy osiągnięciu granic
        if (przeciwnik.GetX() <= 20)
        {
            przeciwnik.SetKierunek(false);
            Thread thread = new Thread(() => {
                sp.generate(200, 0.5, 10);
            });
            thread.Start();
            thread.Join();
        }
        else if (przeciwnik.GetX() + przeciwnik.Wielkosc() >= 112)
        {
            przeciwnik.SetKierunek(true);
            Thread thread = new Thread(() => {
                sp.generate(200, 0.5, 10);
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