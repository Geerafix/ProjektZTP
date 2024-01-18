using EscapeRoom;
using EscapeRoom.Poziomy;
using EscapeRoom.ZapisGry;

internal class Menu
{
    private SoundPlayer soundPlayer = new SoundPlayer();
    private StanGry stanGry;
    private IPolecenie ZapiszGreKomenda;
    private IPolecenie WczytajGreKomenda;
    private IPolecenie ResetujGreKomenda;

    public Menu()
    {
        Console.Clear();

        ZapiszGreKomenda = new ZapiszGrePolecenie();
        WczytajGreKomenda = new WczytajGrePolecenie();
        ResetujGreKomenda = new ResetujGrePolecenie();

        this.stanGry = new StanGry();
        this.stanGry.WczytajGre(WczytajGreKomenda);

        KomunikatWykrytegoStanu();

        NarysujOpcje();
        WlaczOpcje();
    }

    public Menu(long czas, int poziom) {
        Console.Clear();

        ZapiszGreKomenda = new ZapiszGrePolecenie();
        WczytajGreKomenda = new WczytajGrePolecenie();
        ResetujGreKomenda = new ResetujGrePolecenie();

        this.stanGry = new StanGry();
        if (poziom <= 4) {
            ZapiszPoziom(czas, poziom);
        } else {
            ResetujGre();
        }

        NarysujOpcje();
        WlaczOpcje();
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
        string logoLewo = File.ReadAllText("logoLewo.txt");
        string logoPrawo = File.ReadAllText("logoPrawo.txt");
        int czestotliwosc = 300;
        int x = 0;

        ConsoleKeyInfo przycisk;

        while (true)
        {
            Console.SetCursorPosition(0, 6);
            if (x % 2 == 0) {
                console(0, 8, logoLewo, ConsoleColor.Cyan);
            } else {
                console(0, 8, logoPrawo, ConsoleColor.Yellow);
            }
            Thread.Sleep(czestotliwosc);
            ++x;

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
                    Opcje opcje = new Opcje();
                }

                if (przycisk.Key == ConsoleKey.D3) //Jeżeli wciśniemy 2 to idź do tabeli wyników
                {
                    TabelaWynikow tabela = new TabelaWynikow();
                }

                if (przycisk.Key == ConsoleKey.D4) //Jeżeli wciśniemy 4 to wyświetl instrukcję
                {
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

    private void KomunikatWykrytegoStanu() {
        ConsoleKeyInfo przycisk;

        if (stanGry.GetCzas() != 0 || stanGry.GetPoziom() != 1) {
            console(49, 15, "Wykryto istniejący stan gry", ConsoleColor.Red);
            console(55, 18, "Co chcesz zrobić?", ConsoleColor.White);
            console(50, 21, "1. Wczytaj     2. Resetuj", ConsoleColor.Yellow);

            while (true) {
                if (Console.KeyAvailable) {
                    przycisk = Console.ReadKey(true);

                    if (przycisk.Key == ConsoleKey.D1) {
                        Console.Clear();
                        break;
                    }

                    if (przycisk.Key == ConsoleKey.D2) {
                        Console.Clear();
                        this.stanGry.ResetujGre(ResetujGreKomenda);
                        break;
                    }
                }
            }
        }
    }

    public void WczytajPoziom(StanGry stanGry)
    {
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
        stanGry.SetCzas(czas);
        stanGry.SetPoziom(poziom);
        stanGry.ZapiszGre(ZapiszGreKomenda);
    }

    public void ResetujGre()
    {
        stanGry.ResetujGre(ResetujGreKomenda);
    }

    protected void console(int x, int y, string znak, ConsoleColor kolor = ConsoleColor.White) {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = kolor;
        Console.Write(znak);
        Console.ResetColor();
    }
}