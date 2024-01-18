using EscapeRoom;
using EscapeRoom.Poziomy;
using EscapeRoom.Przeciwnik;
using System.Diagnostics;

internal class Poziom4 : Generator
{
    private ConsoleKeyInfo przycisk;
    private long czas;
    private int mierz_czas = 0;
    private bool KtoryStrzelecStrzela = true;
    private Stopwatch stoper = new Stopwatch();
    private bool running = true;

    private IPrzeciwnik przeciwnik1 = new Szybkosc(new PrzeciwnikChodzacy());
    private IPrzeciwnik przeciwnik2 = new Wielkosc(new PrzeciwnikStrzelajacy());
    private IPrzeciwnik przeciwnik3 = new Wielkosc(new Szybkosc(new PrzeciwnikChodzacy()));
    private IPrzeciwnik przeciwnik4 = new Szybkosc(new Wielkosc(new PrzeciwnikStrzelajacy()));

    private StrzalkaManager Strzalki = new StrzalkaManager();

    private List<IObserwator> obserwatorzy = new List<IObserwator>();

    public Poziom4(long czas)
    {
        this.czas = czas;
    }

    protected override void NarysujMape() {
        string sciezkaDoPliku = "Mapa.txt";
        string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);
        znakiPliku = zawartoscPliku.ToCharArray();

        foreach (char c in znakiPliku) {
            Console.Write(c);
        }
    }

    protected override void NarysujPrzeszkodyINapis() {
        string nazwaPoziomu = "Poziom4.txt";
        console(45, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM!", ConsoleColor.Yellow);
        Narysuj(nazwaPoziomu, 5, 35, null);
    }

    protected override void NarysujPortal() {
        Narysuj("Portal.txt", 64, 5, ConsoleColor.Green);
    }

    protected override void UstawPostac() {
        postac.UstawPozPoczatkowa();
    }

    protected override void Rysuj()
    {
        stoper.Start();

        InicjalizujPrzeciwnikow();

        Thread.Sleep(100);

        long pozostalyCzas;

        while (running)
        {
            Thread.Sleep(1);
            mierz_czas++;
            pozostalyCzas = stoper.ElapsedMilliseconds;
            console(62, 0, $"Czas: {(pozostalyCzas + czas) / 1000} s", ConsoleColor.DarkBlue);

            AktualizujPrzeciwnikow();

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

            RysujPrzeciwnikow();
            PoruszaniePostacia();
            SprawdzWarunkiKonca();
        }
    }

    private void InicjalizujPrzeciwnikow()
    {
        InicjalizujPrzeciwnika(przeciwnik1, 30, 29, true, 9);
        InicjalizujPrzeciwnika(przeciwnik2, 50, 22, false, 12);
        InicjalizujPrzeciwnika(przeciwnik3, 80, 15, true, 15);
        InicjalizujPrzeciwnika(przeciwnik4, 70, 25, false, 18);
    }

    private void InicjalizujPrzeciwnika(IPrzeciwnik przeciwnik, int x, int y, bool kierunek, int pozycja)
    {
        przeciwnik.SetX(x);
        przeciwnik.SetY(y);
        przeciwnik.SetKierunek(kierunek);
        przeciwnik.SetPozycja(pozycja);
    }

    private void AktualizujPrzeciwnikow()
    {
        AktualizujPrzeciwnika(przeciwnik1);
        AktualizujPrzeciwnika(przeciwnik2);
        AktualizujPrzeciwnika(przeciwnik3);
        AktualizujPrzeciwnika(przeciwnik4);
    }

    private void AktualizujPrzeciwnika(IPrzeciwnik przeciwnik)
    {
        WyczyscPrzeciwnika(przeciwnik);
        RuszPrzeciwnika(przeciwnik);
    }

    private void RysujPrzeciwnikow()
    {
        RysujPostac(przeciwnik1);
        RysujPostac(przeciwnik2);
        RysujPostac(przeciwnik3);
        RysujPostac(przeciwnik4);
    }

    private void PoruszaniePostacia()
    {
        if (Console.KeyAvailable)
        {
            przycisk = Console.ReadKey(true);
            ObslugaKlawiszy();
        }
    }

    private void ObslugaKlawiszy()
    {
        switch (przycisk.Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                PoruszanieWGore();
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                PoruszanieWDol();
                break;
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                PoruszanieWLewo();
                break;
            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                PoruszanieWPrawo();
                break;
            case ConsoleKey.Escape:
                running = false;
                Wyjdz();
                break;
        }
    }

    private void PoruszanieWGore()
    {
        if (postac.GetY() >= 6)
        {
            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1);
        }
    }

    private void PoruszanieWDol()
    {
        if (postac.GetY() <= 31)
        {
            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1);
        }
    }

    private void PoruszanieWLewo()
    {
        if (postac.GetX() >= 21)
        {
            postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY());
        }
    }

    private void PoruszanieWPrawo()
    {
        if (postac.GetX() <= 109)
        {
            postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
        }
    }

    private void SprawdzWarunkiKonca()
    {
        if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
        {
            ZakonczPoziom();
        }

        if (CzyTrafiony())
        {
            ObslugaSmierci();
        }
    }

    private void ZakonczPoziom()
    {
        this.czas += stoper.ElapsedMilliseconds;
        stoper.Stop();
        soundPlayer.DzwiekPortalu();
        running = false;
        
        TabelaWynikow tabelaWynikow = new TabelaWynikow(true);
        obserwatorzy.Add(tabelaWynikow); //można dodać więcej obserwatorów do listy w przyszłości
        Wyniki wyniki = new Wyniki(czas, obserwatorzy);
    }

    private void ObslugaSmierci()
    {
        soundPlayer.DzwiekTrafienia();
        console(56, 15, "Dopadł cie", ConsoleColor.Yellow);

        this.czas += stoper.ElapsedMilliseconds;
        stoper.Stop();

        int liczCzas = 10000; //zmienna pomocnicza, do migania wiadomościami

        for (; ; )
        {
            //Wyświetlenie wiadomości po śmierci gracza
            liczCzas++;
            if (liczCzas % 13000 == 0)
            { 
                console(50, 16, "                               ", ConsoleColor.Yellow);
            }
            if (liczCzas % 15000 == 0)
            {
                console(50, 16, "*Wcisnij SPACE aby kontynuować*", ConsoleColor.Yellow);
                liczCzas = 0;
            }

            if (Console.KeyAvailable)
            {
                przycisk = Console.ReadKey(true);

                if (przycisk.Key == ConsoleKey.Spacebar)
                {
                    stoper.Restart();
                    running = false;
                    Generator poziom = new Poziom4(czas);
                    poziom.GenerujPoziom();
                    break;
                }
                if (przycisk.Key == ConsoleKey.Escape)
                {
                    Wyjdz();
                    break;
                }
            }
        }
    }

    private void KontynuujGre()
    {
        Console.ResetColor();
        stoper.Restart();
        running = false;
        Generator poziom = new Poziom4(czas);
        poziom.GenerujPoziom();
    }

    private bool CzyTrafiony()
    {
        foreach (var strzala in Strzalki.GetStrzalki())
        {
            if ((postac.GetX() >= strzala.GetStrzalaX() &&
                postac.GetX() <= strzala.GetStrzalaX() &&
                postac.GetY() >= strzala.GetStrzalaY() &&
                postac.GetY() <= strzala.GetStrzalaY()) ||
                (postac.GetX() + 1 >= strzala.GetStrzalaX() &&
                postac.GetX() + 1 <= strzala.GetStrzalaX() &&
                postac.GetY() >= strzala.GetStrzalaY() &&
                postac.GetY() <= strzala.GetStrzalaY()))
            {
                return true;
            }
        }

        foreach (var przeciwnik in new IPrzeciwnik[] { przeciwnik1, przeciwnik2, przeciwnik3, przeciwnik4 })
        {
            if (CzyKolizjaZPrzeciwnikiem(przeciwnik))
            {
                return true;
            }
        }

        return false;
    }

    private bool CzyKolizjaZPrzeciwnikiem(IPrzeciwnik przeciwnik)
    {
        if (postac.GetX() >= przeciwnik.GetX() && postac.GetX() <= przeciwnik.GetX() + przeciwnik.Wielkosc()
            && postac.GetY() >= przeciwnik.GetY() && postac.GetY() <= przeciwnik.GetY() + przeciwnik.Wielkosc())
        {
            return true;
        }
        return false;
    }

    private void RysujPostac(IPrzeciwnik przeciwnik)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        for (int i = 0; i < przeciwnik.Wielkosc(); i++)
        {
            for (int j = 0; j < przeciwnik.Wielkosc(); j++)
            {
                console(przeciwnik.GetX() + i, przeciwnik.GetY() + j, "█", ConsoleColor.Red);
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
        int kierunek = przeciwnik.GetKierunek() ? -1 : 1;
        przeciwnik.SetX(przeciwnik.GetX() + kierunek + przeciwnik.Szybkosc() / 100000);
        przeciwnik.SetY((int)(przeciwnik.GetPozycja() + (Math.Sin(przeciwnik.GetX() / 5.0) * 2)));

        ZmianaKierunku(przeciwnik);
    }

    private void ZmianaKierunku(IPrzeciwnik przeciwnik)
    {
        if (przeciwnik.GetX() <= 20 || przeciwnik.GetX() + przeciwnik.Wielkosc() >= 112)
        {
            przeciwnik.SetKierunek(!przeciwnik.GetKierunek());
            soundPlayer.DzwiekOdbiciaOdSciany();
        }
    }

    private void Wyjdz()
    {
        running = false;
        stoper.Stop();
        Console.ResetColor();
        soundPlayer.DzwiekWyjsciaZGry();
        Menu menu = new Menu(czas, 4);
    }
}
