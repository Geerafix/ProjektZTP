using EscapeRoom;
using EscapeRoom.Poziomy;
using System.Diagnostics;

internal class Poziom3 : Generator
{
    private SoundPlayer soundPlayer = new SoundPlayer();
    private ConsoleKeyInfo przycisk;
    private long czas;
    private int czasownik = 0;
    private Stopwatch stoper = new Stopwatch();

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
        Console.Clear();
        this.czas = czas;
        stoper.Start();
    }

    protected override void Rysuj()
    {
        InicjalizujPrzeciwnikow();

        for (; ; )
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
        Console.SetCursorPosition(62, 0);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write($"Czas: {(pozostalyCzas + czas) / 1000} s");
        Console.ResetColor();
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
        this.czas += stoper.ElapsedMilliseconds;
        stoper.Stop();
        soundPlayer.DzwiekPortalu();
        Generator poziom = new Poziom4(czas);
        poziom.GenerujPoziom();
    }

    private void ObslugaSmierci()
    {
        soundPlayer.DzwiekTrafienia();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(56, 15);
        Console.WriteLine("Dopadł cie");
        Console.SetCursorPosition(0, 0);

        soundPlayer.DzwiekTrafienia();

        this.czas += stoper.ElapsedMilliseconds;
        stoper.Stop();

        int liczCzas = 10000;

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
                    soundPlayer.DzwiekPortalu();
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



    private void KontynuujGre()
    {
        Console.ResetColor();
        stoper.Restart();
        Poziom3 poziom = new Poziom3(czas);
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

    private void RysujPostac()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(postac.GetX(), postac.GetY());
        Console.Write("██");
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }

    private void WyczyscPrzeciwnika((int x, int y) przeciwnik)
    {
        Console.SetCursorPosition(przeciwnik.x, przeciwnik.y);
        Console.Write("  ");
    }

    private void RysujPrzeciwnika(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("██");
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
    }

    private (int x, int y) PrzesunPrzeciwnika((int x, int y) przeciwnik)
    {
        Console.SetCursorPosition(przeciwnik.x, przeciwnik.y);
        Console.Write("  ");

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
        Menu menu = new Menu();
    }
}
