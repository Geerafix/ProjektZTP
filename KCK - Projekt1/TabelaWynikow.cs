
namespace EscapeRoom {
    public class TabelaWynikow : IObserwator {
        string fileName = "../../../Assety/wyniki.txt";
        private ConsoleKeyInfo przycisk;
        char[] znakiPliku;
        string username = null;
        DateTime date;
        double time;
        bool wynik = false;
        private bool running = true;

        public TabelaWynikow() {
            string sciezkaDoPliku = "../../../Assety/TabelaWynikow.txt";
            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);
            znakiPliku = zawartoscPliku.ToCharArray();

            WyswietlRanking();
        }

        public TabelaWynikow(bool w) {
            string sciezkaDoPliku = "../../../Assety/TabelaWynikow.txt";
            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);
            znakiPliku = zawartoscPliku.ToCharArray();

            this.wynik = w; //sprawdzenie czy wywolano Tabelę z klasy Wynik
        }

        public void Aktualizuj() {
            ZapiszWynik();
        }

        public void setTabela(string username, double time, DateTime date) {
            this.username = username;
            this.time = time;
            this.date = date;
        }

        public void ZapiszWynik() {
            string fileName = "../../../Assety/wyniki.txt";

            using (StreamWriter wynik = new StreamWriter(fileName, true)) {
                wynik.WriteLine($"{username} {time} {date.ToShortDateString()}");
            }
        }

        public void WyswietlRanking() {
            Console.Clear();

            int pom = 0; //zmienna która liczy ilość wgranych znaków z plików

            foreach (char c in znakiPliku) {
                pom++;
                if (pom >= 0 && pom <= 230) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write(c);
                Console.ResetColor();
            }

            console(43, 38, "Wciśnij ESC. aby wrócić do MENU.", ConsoleColor.DarkYellow);

            string[] wiersze = File.ReadAllLines(fileName);

            //lista która przechowuje pary (nazwa, czas)
            List<(string, double, string)> wyniki = new List<(string, double, string)>();

            foreach (var wiersz in wiersze) {
                string[] czesci = wiersz.Split(' ');
                if (czesci.Length == 3) {
                    string nazwa = czesci[0];
                    string data = czesci[2];
                    if (double.TryParse(czesci[1], out double czas)) {
                        wyniki.Add((nazwa, czas, data));
                    }
                }
            }

            wyniki.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            pom = 0;
            
            //wyświetlanie rankingu
            for (int i = 0 ; i < wyniki.Count ; i++) {
                if (!running) return;
                if (i == 0) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                pom = i % 7;

                WypiszWynik(i, pom, wyniki[i].Item1, wyniki[i].Item2, wyniki[i].Item3);

                if ((i + 1) % 7 == 0) {

                    if (wyniki.Count > i + 1)
                    {
                        TekstPrawejStrzalki();
                    }
                    if (i > 6)
                    {
                        TekstLewejStrzalki();
                    }
                    if ((i + 1 == wyniki.Count()) || ((wyniki.Count() % 7 == 0) && (wyniki.Count() - i <= 6))) 
                    {
                        CzyscLewyTekst();
                    }
                    if (i == 6) {
                        CzyscPrawyTekst();
                    }

                    for ( ; ; )
                    {
                        if (Console.KeyAvailable)
                        {
                            przycisk = Console.ReadKey(true);

                            if (przycisk.Key == ConsoleKey.RightArrow && wyniki.Count() != i + 1) {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                PrawaStrzalka();
                                if (wyniki.Count - i <= 7)
                                {
                                    for (int j = 0 ; j < 7 ; j++) {
                                        Console.SetCursorPosition(40, 9 + j * 4);
                                        Console.Write("                                              ");
                                    }
                                    Console.SetCursorPosition(0, 0);
                                }
                                Thread.Sleep(100);
                                PrawaStrzalka();
                                break;
                            }

                            if (przycisk.Key == ConsoleKey.LeftArrow) {
                                if (i > 6)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    LewaStrzalka();
                                    i = i - 14;
                                    LewaStrzalka();
                                    break;
                                }
                            }

                            if (przycisk.Key == ConsoleKey.Escape) {
                                running = false;
                                Menu menu = new Menu();
                                break;
                            }

                        }
                    }
                }
                if (i + 1 == wyniki.Count)
                {
                    CzyscLewyTekst();
                    if (i + 1 >= 7) {
                        TekstLewejStrzalki();
                    }

                    for ( ; ; )
                    {
                        if (Console.KeyAvailable) {
                            przycisk = Console.ReadKey(true);

                            if (przycisk.Key == ConsoleKey.LeftArrow) {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                LewaStrzalka();
                                i = i - wyniki.Count % 7;
                                i = i - 7;
                                LewaStrzalka();
                                break;
                            }

                            if (przycisk.Key == ConsoleKey.Escape) {
                                running = false;
                                Menu menu = new Menu();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void TekstPrawejStrzalki() {
            console(94, 17, " Naciśnij strzałkę w prawo,", ConsoleColor.Yellow);
            console(92, 18, " aby sprawdzić reszte rankingu", ConsoleColor.Yellow);
        }

        private void TekstLewejStrzalki() {
            console(3, 17, "Naciśnij strzałkę w lewo,", ConsoleColor.Yellow);
            console(7, 18, " aby wrócić", ConsoleColor.Yellow);
        }

        private void PrawaStrzalka() {
            Console.SetCursorPosition(111, 19);
            Console.Write("_");
            Console.SetCursorPosition(107, 20);
            Console.Write("____"); Console.Write("\\ "); Console.Write("\\ ");
            Console.SetCursorPosition(106, 21);
            Console.Write("|____ ) )");
            Console.SetCursorPosition(111, 22);
            Console.Write("/_/");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        private void LewaStrzalka() {
            Console.SetCursorPosition(7, 19);
            Console.Write("_");
            Console.SetCursorPosition(6, 20);
            Console.Write("/ /"); Console.Write("____");
            Console.SetCursorPosition(5, 21);
            Console.Write("( ( ____|");
            Console.SetCursorPosition(6, 22);
            Console.Write("\\_\\");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(100);
        }

        private void CzyscLewyTekst() {
            Console.SetCursorPosition(93, 17);
            Console.Write("                            ");
            Console.SetCursorPosition(92, 18);
            Console.Write("                                ");
            Console.SetCursorPosition(0, 0);
        }

        private void CzyscPrawyTekst() {
            Console.SetCursorPosition(3, 17);
            Console.Write("                          ");
            Console.SetCursorPosition(7, 18);
            Console.Write("            ");
            Console.SetCursorPosition(0, 0);
        }

        private void WypiszWynik(int i, int pom, string item1, double item2, string item3) {
            Console.SetCursorPosition(40, 9 + pom * 4);
            Console.Write("                                               ");
            Console.SetCursorPosition(35, 9 + pom * 4);
            Console.Write($"{i + 1}. {item1}");
            Console.SetCursorPosition(55, 9 + pom * 4);
            Console.Write($"{item2} sekund");
            Console.SetCursorPosition(75, 9 + pom * 4);
            Console.Write($"{item3}");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        private void console(int x, int y, string znak, ConsoleColor kolor = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = kolor;
            Console.Write(znak);
            Console.ResetColor();
        }
    }
}
