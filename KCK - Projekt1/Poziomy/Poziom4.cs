using EscapeRoom;
using EscapeRoom.Poziomy;
using EscapeRoom.Przeciwnik;
using System.Diagnostics;

internal class Poziom4 : Generator
{
    private SoundPlayer soundPlayer = new SoundPlayer();
    private ConsoleKeyInfo przycisk;
    private long czas;
    int mierz_czas = 0;
    bool KtoryStrzelecStrzela = true;
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

        Thread.Sleep(100);

        long pozostalyCzas;

        for (; ; )
        {
            Thread.Sleep(1);
            mierz_czas++;
            pozostalyCzas = stoper.ElapsedMilliseconds;
            console(62, 0, "Czas: " +  (pozostalyCzas + czas) / 1000 + " s", ConsoleColor.DarkBlue);


            if (mierz_czas % 1 == 0)
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

            if (mierz_czas % 100 == 0)
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

            if (mierz_czas % 7 == 0)
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
                        postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape) //Wyjdź do menu
                {
                    postac.UstawPozPoczatkowa();
                    Wyjdz();
                    break;
                }

            }

            //Jeżeli postać jest na kordynatach bramy
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
            {
                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                soundPlayer.DzwiekPortalu();
                
                TabelaWynikow tabelaWynikow = new TabelaWynikow(true);
                Wyniki wyniki = new Wyniki(czas, tabelaWynikow);
            }

            //JEŻELI POSTAĆ ZOSTAŁA ZABITA
            if (CzyTrafiony())
            {
                soundPlayer.DzwiekTrafienia();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(56, 15);
                Console.WriteLine("Dopadł cie"); //Komunikat o śmierci gracza
                Console.SetCursorPosition(0, 0);

                soundPlayer.DzwiekTrafienia();

                this.czas += stoper.ElapsedMilliseconds;
                stoper.Stop();

                int liczCzas = 10000; //zmienna pomocnicza, do migania wiadomościami

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
                            Generator poziom = new Poziom4(czas);
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
    //Czy nasz bohater został dorwany przez przeciwnika
    private bool CzyTrafiony()
    {
        foreach (var strzala in Strzalki.GetStrzalki())
        {
            if ((postac.GetX() >= strzala.GetStrzalaX() &&
                postac.GetX() <= strzala.GetStrzalaX() &&
                postac.GetY() >= strzala.GetStrzalaY() &&
                postac.GetY() <= strzala.GetStrzalaY()) ||
                (postac.GetX()+1 >= strzala.GetStrzalaX() &&
                postac.GetX()+1 <= strzala.GetStrzalaX() &&
                postac.GetY() >= strzala.GetStrzalaY() &&
                postac.GetY() <= strzala.GetStrzalaY()))
            {
                return true;
            }
        }

        if (postac.GetX() >= przeciwnik1.GetX() && postac.GetX() <= przeciwnik1.GetX() + przeciwnik1.Wielkosc() && postac.GetY() >= przeciwnik1.GetY() 
            && postac.GetY() <= przeciwnik1.GetY() + przeciwnik1.Wielkosc()) {
            return true;
        }
        if (postac.GetX() >= przeciwnik2.GetX() && postac.GetX() <= przeciwnik2.GetX() + przeciwnik2.Wielkosc() && postac.GetY() >= przeciwnik2.GetY() 
            && postac.GetY() <= przeciwnik2.GetY() + przeciwnik2.Wielkosc())
        {
            return true;
        }
        if (postac.GetX() >= przeciwnik3.GetX() && postac.GetX() <= przeciwnik3.GetX() + przeciwnik3.Wielkosc() && postac.GetY() >= przeciwnik3.GetY() 
            && postac.GetY() <= przeciwnik3.GetY() + przeciwnik3.Wielkosc())
        {
            return true;
        }
        if (postac.GetX() >= przeciwnik4.GetX() && postac.GetX() <= przeciwnik4.GetX() + przeciwnik4.Wielkosc() && postac.GetY() >= przeciwnik4.GetY() 
            && postac.GetY() <= przeciwnik4.GetY() + przeciwnik4.Wielkosc())
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
            soundPlayer.DzwiekOdbiciaOdSciany();
        }
        else if (przeciwnik.GetX() + przeciwnik.Wielkosc() >= 112)
        {
            przeciwnik.SetKierunek(true);
            soundPlayer.DzwiekOdbiciaOdSciany();
        }
    }

    private void Wyjdz() {
        stoper.Stop();
        Console.ResetColor();
        soundPlayer.DzwiekWyjsciaZGry();
        Menu menu = new Menu();
    }
}