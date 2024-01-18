using EscapeRoom.Eksport_Wyniku;

namespace EscapeRoom
{
    public class Wyniki : IObserwowany
    {
        private ConsoleKeyInfo przycisk;
        private char[] znakiPliku;
        private double czasWynik;
        private DateTime data;
        private string username;
        private List<IObserwator> obserwatorzy = new List<IObserwator>();
        private IStrategiaEksportu strategiaEksportu;
        private bool running = true;

        public Wyniki(long czas, List<IObserwator> obserwatorzy)
        {
            czasWynik = czas;
            czasWynik = czasWynik / 1000;
            data = DateTime.Now;

            foreach (var o in obserwatorzy)
            {
                DodajObserwatora(o); //dodanie obserwatorow
            }

            GenerujWyniki();
        }

        public DateTime getDate()
        {
            return data;
        }

        public double getTime()
        {
            return czasWynik;
        }

        public string getUsername()
        {
            return username;
        }

        public void SetStrategiaEksportu(IStrategiaEksportu strategia)
        {
            strategiaEksportu = strategia;
        }

        public void EksportujWyniki()
        {
            if (strategiaEksportu != null)
            {
                strategiaEksportu.Eksportuj(this);
            }

            else
            {
                Console.WriteLine("Nie wybrano strategii eksportu.");
            }
        }

        public void ZapiszObrazek()
        {
            if (running) {
                while (true) {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.D1) {
                        SetStrategiaEksportu(new EksportPNG());
                        EksportujWyniki();
                        Console.SetCursorPosition(40, 35);
                        Console.Write("Naciśnij Esc by powrócić do menu głównego");
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.D2) {
                        SetStrategiaEksportu(new EksportJPEG());
                        EksportujWyniki();
                        Console.SetCursorPosition(40, 35);
                        Console.Write("Naciśnij Esc by powrócić do menu głównego");
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.D3) {
                        SetStrategiaEksportu(new EksportBMP());
                        EksportujWyniki();
                        Console.SetCursorPosition(40, 35);
                        Console.Write("Naciśnij Esc by powrócić do menu głównego");
                        break;
                    }
                }

                while (true) {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Escape) {
                        Wyjdz();
                        break;
                    }
                }
            }
        }

        public void CzyZapisacObrazek()
        {
            if (running) {
                Console.SetCursorPosition(43, 26);
                Console.Write("Czy zapisać wynik w formie obrazka?");

                Console.SetCursorPosition(45, 28);
                Console.Write("1. Tak                2. Nie");

                while (running)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        running = false;
                        Wyjdz();
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.D1)
                    {
                        Console.SetCursorPosition(45, 30);
                        Console.Write("Wybierz format obrazka:");

                        Console.SetCursorPosition(45, 32);
                        Console.Write("1. PNG     2. JPEG     3. BMP");
                        ZapiszObrazek();
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.D2)
                    {
                        running = false;
                        Wyjdz();
                        break;
                    }
                }
            }
        }

        public void WpiszNazwe()
        {
            string nazwa = "";
            Console.SetCursorPosition(30, 22);
            Console.Write("Wpisz swoją nazwę i kliknij ENTER: ");

            for (; ; )
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    running = false;
                    Wyjdz();
                    break;
                }

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && nazwa.Length > 0)
                {
                    nazwa = nazwa.Substring(0, nazwa.Length - 1);
                }
                else if (nazwa.Length < 15 && keyInfo.Key != ConsoleKey.Spacebar)
                {
                    nazwa += keyInfo.KeyChar;
                }

                username = nazwa;

                //wypisz aktualną zawartość nazwy.
                Console.SetCursorPosition(65, 22);
                Console.Write(new string(' ', 20));
                Console.SetCursorPosition(66, 22);
                Console.Write(nazwa);
            }

            PowiadomObserwatorow(nazwa, czasWynik, data); //powiadom subskrybentów (w tym przypadku obiekt TabelaWynikow)
        }

        public void WypiszWynik()
        {
            Console.SetCursorPosition(49, 14);
            Console.Write("TWÓJ CZAS: ");
            Console.Write(czasWynik);
            Console.Write(" sekund.");

            Console.SetCursorPosition(28, 16);
            Console.Write("Jeżeli chcesz zapisać swój wynik, kliknij 1 i wpisz swóją nazwę.");
            Console.SetCursorPosition(30, 17);
            Console.Write("Jeżeli nie chcesz zapisać wyniku kliknij 0 i wróć do menu.");

            Console.SetCursorPosition(28, 19);
            Console.Write("  1. Wpisz swoją nazwę");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(67, 19);
            Console.Write("ESC. Wróc do menu");
            Console.ResetColor();

            Console.SetCursorPosition(0, 0);
        }

        public void WypiszRamke()
        {
            Console.Clear();

            string sciezkaDoPliku = "../../../Assety/KCKWyniki.txt";

            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            int pom = 0; //zmienna która liczy ilość wgranych znaków z plików

            foreach (char c in znakiPliku)
            {
                pom++;
                Console.Write(c);
                Console.ResetColor();
            }
        }

        public void GenerujWyniki()
        {
            WypiszRamke();

            WypiszWynik();

            while (running)
            {
                if (Console.KeyAvailable)
                {
                    przycisk = Console.ReadKey(true);

                    if (przycisk.Key == ConsoleKey.D1)
                    {
                        WpiszNazwe();

                        CzyZapisacObrazek();

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

        //implementacja obserwatora
        public void DodajObserwatora(IObserwator obserwator)
        {
            obserwatorzy.Add(obserwator);
        }

        public void UsunObserwatora(IObserwator obserwator)
        {
            obserwatorzy.Remove(obserwator);
        }

        public void PowiadomObserwatorow(string nazwa, double czasWynik, DateTime data)
        {
            foreach (var obserwator in obserwatorzy)
            {
                obserwator.Aktualizuj(nazwa, czasWynik, data);
            }
        }

        private void Wyjdz() {
            Menu menu = new Menu(0, 5);
            menu.ResetujGre();
        }
    }
}
