using EscapeRoom;
using EscapeRoom.Poziomy;
using EscapeRoom.ZapisGry;

internal class Menu
{
    private ConsoleKeyInfo przycisk;
    Postac postac = Postac.pobierzPostac();

    public Menu()
    {
        StanGry s = new(1, 2);
        s.Zapisz(new ZapiszGreKomenda());
        postac.UstawPozPoczatkowa();

        s.Resetuj(new ResetujGreKomenda());

        Console.Clear();
        //Zmienne dotyczące MENU
        int LogoLenght = 65; //Długość loga gry
        int LogoPlace = (120 - LogoLenght) / 2; //Ilość spacji do środka konsoli
        int JedenLenght = 14; //Długość Pierwszej opcji w menu
        int JedenPlace = (120 - JedenLenght) / 2;
        int DwaLenght = 6; //Długość opcji numer 2 w menu
        int DwaPlace = (120 - DwaLenght) / 2; //obliczamy miejsce w poziomie według długości liter
        int TrzyLenght = 8;
        int TrzyPlace = (120 - TrzyLenght) / 2;
        int CzteryLenght = 9;
        int CzteryPlace = (120 - CzteryLenght) / 2;
        int PiecLenght = 11;
        int PiecPlace = (120 - PiecLenght) / 2;

        Console.SetCursorPosition(0, 16);
        Console.WriteLine("\n");
        Console.WriteLine(new string(' ', JedenPlace) + "1. Rozpocznij grę");

        Console.WriteLine("\n");
        Console.WriteLine(new string(' ', DwaPlace) + "2. Opcje");

        Console.WriteLine("\n");
        Console.WriteLine(new string(' ', TrzyPlace) + "3. Ranking");

        Console.WriteLine("\n");
        Console.WriteLine(new string(' ', CzteryPlace) + "4. Jak Grać?");

        Console.WriteLine("\n");
        Console.WriteLine(new string(' ', PiecPlace) + "5. Wyjdź z gry");
        Console.SetCursorPosition(0, 0);

        Console.WriteLine("\n");
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        for (; ; )
        {
            Console.SetCursorPosition(0, 3);

            Console.Write(new string(' ', LogoPlace)); Console.WriteLine("  ______                                                      ");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine(" |  ____|                                                     ");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine(" | |__   ___  ___ __ _ _ __   ___   _ __ ___   ___  _ __ ___  ");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine(" |  __| / __|/ __/ _` | '_ \\ / _ \\ | '__/ _ \\ / _ \\| '_ ` _ \\ ");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine(" | |____\\__ \\ (_| (_| | |_) |  __/ | | | (_) | (_) | | | | | |");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine(" |______|___/\\___\\__,_| .__/ \\___| |_|  \\___/ \\___/|_| |_| |_|");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine("                      | |                                     ");
            Console.Write(new string(' ', LogoPlace)); Console.WriteLine("                      |_|                                     ");

            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                     Kliknij przycisk na klawiaturze aby wybrać opcję:");
            Console.ResetColor();

            Thread.Sleep(300);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.SetCursorPosition(0, 3);

            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("   ______                                                      ");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("  |  ____|                                                     ");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("  | |__   ___  ___ __ _ _ __   ___   _ __ ___   ___  _ __ ___  ");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("  |  __| / __|/ __/ _` | '_ \\ / _ \\ | '__/ _ \\ / _ \\| '_ ` _ \"");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("  | |____\\__ \\ (_| (_| | |_) |  __/ | | | (_) | (_) | | | | | |");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("  |______|___/\\___\\__,_| .__/ \\___| |_|  \\___/ \\___/|_| |_| |_|");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("                       | |                                     ");
            Console.Write(new string(' ', LogoPlace + 1)); Console.WriteLine("                       |_|                                     ");

            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("                                     Kliknij przycisk na klawiaturze aby wybrać opcję:");
            Thread.Sleep(300);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.WriteLine(new string(' ', LogoPlace * 4));
            Console.SetCursorPosition(0, 3);

            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true);

                if (przycisk.Key == ConsoleKey.D1) //Jeżeli wciśniemy 1 to idź do poziomu 1
                {
                    Generator poziom = new Poziom3(0);
                    poziom.GenerujPoziom();
                }
                if (przycisk.Key == ConsoleKey.D2) //Jeżeli wciśniemy 2 to idź do opcji
                {
                    Opcje opcje = new Opcje();
                }
                if (przycisk.Key == ConsoleKey.D3) //Jeżeli wciśniemy 2 to idź do opcji
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


}

