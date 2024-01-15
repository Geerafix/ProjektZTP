using EscapeRoom;
using EscapeRoom.Poziomy;
using EscapeRoom.ZapisGry;

internal class Menu
{
    private SoundPlayer soundPlayer = new SoundPlayer();
    private StanGry stanGry;
    private Thread thread;
    bool czyWatekDziala = true;
    private IKomenda ZapiszGreKomenda;
    private IKomenda WczytajGreKomenda;
    private IKomenda ResetujGreKomenda;

    public Menu()
    {
        Console.Clear();

        ZapiszGreKomenda = new ZapiszGreKomenda();
        WczytajGreKomenda = new WczytajGreKomenda();
        ResetujGreKomenda = new ResetujGreKomenda();

        this.stanGry = new StanGry();
        this.stanGry.WczytajGre(WczytajGreKomenda);

        NarysujOpcje();
        RysujLogo();
        WlaczOpcje();
    }

    public void RysujLogo() {
        string logoLewo = File.ReadAllText("../../../Assety/logoLewo.txt");
        string logoPrawo = File.ReadAllText("../../../Assety/logoPrawo.txt");
        int czestotliwosc = 300;
        int x = 0;

        thread = new(() => {
            while (czyWatekDziala) {
                Console.SetCursorPosition(0, 6);
                if (x % 2 == 0) {
                    console(logoLewo, ConsoleColor.Cyan);
                } else {
                    console(logoPrawo, ConsoleColor.Yellow);
                }
                Thread.Sleep(czestotliwosc);
                ++x;
            }
        });
        thread.Start();
    }

    public void NarysujOpcje() {
        int JedenLength = 14; //Długość Pierwszej opcji w menu
        int JedenPlace = (120 - JedenLength) / 2;
        int DwaLength = 6; //Długość opcji numer 2 w menu
        int DwaPlace = (120 - DwaLength) / 2; //obliczamy miejsce w poziomie według długości liter
        int TrzyLength = 8;
        int TrzyPlace = (120 - TrzyLength) / 2;
        int CzteryLength = 9;
        int CzteryPlace = (120 - CzteryLength) / 2;
        int PiecLength = 11;
        int PiecPlace = (120 - PiecLength) / 2;

        Console.SetCursorPosition(0, 18);
        Console.WriteLine("\n");
        Console.WriteLine(new string(' ', JedenPlace) + "1. Rozpocznij grę" + "\n\n");
        Console.WriteLine(new string(' ', DwaPlace) + "2. Opcje" + "\n\n");
        Console.WriteLine(new string(' ', TrzyPlace) + "3. Ranking" + "\n\n");
        Console.WriteLine(new string(' ', CzteryPlace) + "4. Jak Grać?" + "\n\n");
        Console.WriteLine(new string(' ', PiecPlace) + "5. Wyjdź z gry" + "\n\n");
    }

    public void WlaczOpcje() {
        ConsoleKeyInfo przycisk;
        for ( ; ; )
        {
            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true);

                if (przycisk.Key == ConsoleKey.D1) //Jeżeli wciśniemy 1 to idź do poziomu 1
                {
                    WczytajPoziom(this.stanGry);
                    break;
                }

                if (przycisk.Key == ConsoleKey.D2) //Jeżeli wciśniemy 2 to idź do opcji
                {
                    czyWatekDziala = false;
                    Opcje opcje = new Opcje();
                }

                if (przycisk.Key == ConsoleKey.D3) //Jeżeli wciśniemy 2 to idź do tabeli wyników
                {
                    czyWatekDziala = false;
                    TabelaWynikow tabela = new TabelaWynikow();
                }

                if (przycisk.Key == ConsoleKey.D4) //Jeżeli wciśniemy 4 to wyświetl instrukcję
                {
                    czyWatekDziala = false;
                    Instrukcja instrukcja = new Instrukcja();
                }

                if (przycisk.Key == ConsoleKey.D5) //Jeżeli wciśniemy 5 to wyjdź z gry
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
            }
        }
    }

    public void WczytajPoziom(StanGry stanGry)
    {
        czyWatekDziala = false;
        soundPlayer.DzwiekWejsciaDoGry();
        Generator poziom = null;
        switch (stanGry.GetPoziom())
        {
            case 1:
                poziom = new Poziom1(stanGry.GetCzas());
                break;
            case 2:
                poziom = new Poziom2(stanGry.GetCzas());
                break;
            case 3:
                poziom = new Poziom3(stanGry.GetCzas());
                break;
            case 4:
                poziom = new Poziom4(stanGry.GetCzas());
                break;
        }
        poziom.GenerujPoziom();
    }

    public void ZapiszPoziom(long czas, int poziom) {
        this.stanGry.SetCzas(czas);
        this.stanGry.SetPoziom(poziom);
        this.stanGry.ZapiszGre(ZapiszGreKomenda);
    }

    public void console(string str, ConsoleColor? colour) {
        if (colour != null) Console.ForegroundColor = colour.Value;
        Console.Write(str);
        Console.ResetColor();
    }
}