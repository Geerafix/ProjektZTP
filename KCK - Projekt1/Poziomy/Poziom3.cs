using EscapeRoom.Poziomy;
using System.Diagnostics;

internal class Poziom3 : Generator
{
    private ConsoleKeyInfo przycisk;
    private long czas;
    private int czasownik = 0;
    private Stopwatch stoper = new Stopwatch();
    private bool running = true;

    private (int x, int y)[] przeciwnicy = new (int x, int y)[]
    {
        (40, 7),
        (90, 7),
        (25, 9),
        (60, 9),
        (75, 8)
    };

    public Poziom3(long czas)
    {
        this.czas = czas;
    }

    protected override void NarysujMape() {
        string sciezkaDoPliku = "../../../Assety/KCKMapa.txt";
        string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);
        znakiPliku = zawartoscPliku.ToCharArray();

        foreach (char c in znakiPliku) {
            Console.Write(c);
        }
    }

    protected override void NarysujPrzeszkodyINapis() {
        string nazwaPoziomu = "../../../Assety/Poziom3.txt";
        console(35, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM! UWAGA! ONI CIĘ GONIĄ!", ConsoleColor.Yellow);
        Narysuj(nazwaPoziomu, 5, 35, null);
    }

    protected override void NarysujPortal() {
        Narysuj("../../../Assety/KCKPortal.txt", 64, 5, ConsoleColor.Green);
    }

    protected override void UstawPostac() {
        postac.UstawPozPoczatkowa();
    }

    protected override void Rysuj()
    {
        stoper.Start(); 

        InicjalizujPrzeciwnikow();

        while(running)
        {
            Thread.Sleep(1);
            AktualizujCzas();
            RysujPrzeciwnikow();
            PoruszaniePostacia();
            SprawdzWarunkiKonca();
        }
    }

    private void InicjalizujPrzeciwnikow()
    {
        foreach (var przeciwnik in przeciwnicy)
        {
            RysujPrzeciwnika(przeciwnik.x, przeciwnik.y);
        }
    }

    private void AktualizujCzas()
    {
        long pozostalyCzas = stoper.ElapsedMilliseconds;
        console(62, 0, $"Czas: {(pozostalyCzas + czas) / 1000} s", ConsoleColor.DarkBlue);
        Console.SetCursorPosition(0, 0);

        czasownik++;
        if (czasownik >= 10000)
            czasownik = 0;
    }

    private void RysujPrzeciwnikow()
    {
        //RYSOWANIE PRZECIWNIKÓW
        //Przeciwnik 1
        if (czasownik % 10 == 0)
            przeciwnicy[0] = PrzesunPrzeciwnika(przeciwnicy[0]);

        //Przeciwnik 2
        if (czasownik % 15 == 0)
            przeciwnicy[1] = PrzesunPrzeciwnika(przeciwnicy[1]);

        //Przeciwnik 3 i 4
        if (czasownik % 20 == 0)
        {
            przeciwnicy[2] = PrzesunPrzeciwnika(przeciwnicy[2]);
            przeciwnicy[3] = PrzesunPrzeciwnika(przeciwnicy[3]);
        }

        // Przeciwnik 5
        if (czasownik % 25 == 0)
            przeciwnicy[4] = PrzesunPrzeciwnika(przeciwnicy[4]);
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
        foreach (var przeciwnik in przeciwnicy)
        {
            if (CzyKolizjaZPrzeciwnikiem(przeciwnik))
            {
                ObslugaSmierci();
            }
        }

        if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
        {
            ZakonczPoziom();
        }
    }

    private void ZakonczPoziom()
    {
        soundPlayer.DzwiekPortalu();
        this.czas += stoper.ElapsedMilliseconds;
        stoper.Stop();
        running = false;
        Generator poziom = new Poziom4(czas);
        poziom.GenerujPoziom();
    }

    private void ObslugaSmierci()
    {
        soundPlayer.DzwiekTrafienia();
        console(56, 15, "Dopadł cie", ConsoleColor.Yellow);
        this.czas += stoper.ElapsedMilliseconds;

        stoper.Stop();

        int liczCzas = 10000;

        for(; ;)
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
                    soundPlayer.DzwiekPortalu();
                    stoper.Restart();
                    running = false;
                    Generator poziom = new Poziom3(czas);
                    poziom.GenerujPoziom();
                    break;
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

    private void KontynuujGre()
    {
        Console.ResetColor();
        stoper.Restart();
        running = false;
        Generator poziom = new Poziom3(czas);
        poziom.GenerujPoziom();
    }

    private bool CzyKolizjaZPrzeciwnikiem((int x, int y) przeciwnik)
    {
        if (postac.GetX() >= przeciwnik.x && postac.GetX() <= przeciwnik.x + 1
            && postac.GetY() >= przeciwnik.y && postac.GetY() <= przeciwnik.y)
        {
            return true;
        }
        return false;
    }

    private void RysujPrzeciwnika(int x, int y)
    {
        console(x, y, "██", ConsoleColor.Red);
    }

    private (int x, int y) PrzesunPrzeciwnika((int x, int y) przeciwnik)
    {
        console(przeciwnik.x, przeciwnik.y, "  ", ConsoleColor.White);

        if (postac.GetX() > przeciwnik.x) przeciwnik.x++;
        if (postac.GetX() < przeciwnik.x) przeciwnik.x--;
        if (postac.GetY() > przeciwnik.y) przeciwnik.y++;
        if (postac.GetY() < przeciwnik.y) przeciwnik.y--;

        RysujPrzeciwnika(przeciwnik.x, przeciwnik.y);
        return przeciwnik;
    }

    private void Wyjdz()
    {
        stoper.Stop();
        Console.ResetColor();
        soundPlayer.DzwiekWyjsciaZGry();
        Menu menu = new Menu(czas, 3);
    }
}