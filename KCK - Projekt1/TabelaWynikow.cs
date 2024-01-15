

using static System.Formats.Asn1.AsnWriter;
using System.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EscapeRoom
{
    public class TabelaWynikow : IObserver {
        string fileName = "wyniki.txt";
        private ConsoleKeyInfo przycisk;
        char[] znakiPliku;
        string username = null;
        DateTime date;
        double time;
        bool w = false;

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

            this.w = w;
        }

        public void Update() {
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

            int pom1 = 0; //zmienna która liczy ilość wgranych znaków z plików

            foreach (char c in znakiPliku) {
                pom1++;
                if (pom1 >= 0 && pom1 <= 250) {
                    Console.ForegroundColor = ConsoleColor.Cyan; //Brama do drugiego poziomu jest koloru zielonego
                }
                Console.Write(c);
                Console.ResetColor();
            }

            Console.SetCursorPosition(43, 38);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Wciśnij ESC. aby wrócić do MENU.");
            Console.ResetColor();

            //WyswietlRanking();

            // Odczytaj wszystkie linie z pliku
            string[] wiersze = File.ReadAllLines(fileName);

            // Lista która przechowuje pary (nazwa, czas)
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

            // Sortowanie wyników według czasu (rosnąco)
            wyniki.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            int pom = 0;

            // Wyświetlanie rankingu
            for (int i = 0 ; i < wyniki.Count ; i++) {
                if (i == 0) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                pom = i % 7;
                Console.SetCursorPosition(40, 9 + pom * 4);
                Console.Write("                                               ");
                Console.SetCursorPosition(35, 9 + pom * 4);
                Console.Write($"{i + 1}. {wyniki[i].Item1}");
                Console.SetCursorPosition(55, 9 + pom * 4);
                Console.Write($"{wyniki[i].Item2} sekund");
                Console.SetCursorPosition(75, 9 + pom * 4);
                Console.Write($"{wyniki[i].Item3}");
                Console.ResetColor();
                Console.SetCursorPosition(0, 0);


                if ((i + 1) % 7 == 0) {

                    if (wyniki.Count > i + 1) //Narysuj strzałkę w prawo
                    {
                        Console.SetCursorPosition(94, 17);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" Naciśnij strzałkę w prawo,");
                        Console.SetCursorPosition(92, 18);
                        Console.Write(" aby sprawdzić reszte rankingu");
                        Console.ResetColor();
                        Console.SetCursorPosition(0, 0);
                    }
                    if (i > 6) //Narysuj strzałkę w lewo
                    {
                        Console.SetCursorPosition(3, 17);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Naciśnij strzałkę w lewo,");
                        Console.SetCursorPosition(7, 18);
                        Console.Write(" aby wrócić");
                        Console.ResetColor();
                        Console.SetCursorPosition(0, 0);
                    }
                    if ((i + 1 == wyniki.Count()) || ((wyniki.Count() % 7 == 0) && (wyniki.Count() - i <= 6))) {
                        Console.SetCursorPosition(93, 17);
                        Console.Write("                            ");
                        Console.SetCursorPosition(92, 18);
                        Console.Write("                                ");
                        Console.SetCursorPosition(0, 0);
                    }
                    if (i == 6) {
                        Console.SetCursorPosition(3, 17);
                        Console.Write("                          ");
                        Console.SetCursorPosition(7, 18);
                        Console.Write("            ");
                        Console.SetCursorPosition(0, 0);
                    }

                    for ( ; ; )
                    {
                        if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
                        {
                            przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                            if (przycisk.Key == ConsoleKey.RightArrow && wyniki.Count() != i + 1) {
                                //Rysowanie strzałki podczas kliknięcia
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
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

                                if (wyniki.Count - i <= 7) //Czyść nieużywane końcowe miejsca w tabeli
                                {
                                    for (int j = 0 ; j < 7 ; j++) {
                                        Console.SetCursorPosition(40, 9 + j * 4);
                                        Console.Write("                                              ");
                                    }
                                    Console.SetCursorPosition(0, 0);
                                }
                                Thread.Sleep(100);

                                //Odrysowanie strzałki podczas kliknięcia
                                Console.SetCursorPosition(111, 19);
                                Console.Write("_");
                                Console.SetCursorPosition(107, 20);
                                Console.Write("____"); Console.Write("\\ "); Console.Write("\\ ");
                                Console.SetCursorPosition(106, 21);
                                Console.Write("|____ ) )");
                                Console.SetCursorPosition(111, 22);
                                Console.Write("/_/");
                                Console.SetCursorPosition(0, 0);
                                break;
                            }
                            if (przycisk.Key == ConsoleKey.LeftArrow) {
                                if (i > 6) //Jeżeli jesteśmy nie na pierwszej stronie tabeli
                                {
                                    //Rysowanie strzałki podczas kliknięcia
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
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

                                    i = i - 14;

                                    Console.SetCursorPosition(7, 19);
                                    Console.Write("_");
                                    Console.SetCursorPosition(6, 20);
                                    Console.Write("/ /"); Console.Write("____");
                                    Console.SetCursorPosition(5, 21);
                                    Console.Write("( ( ____|");
                                    Console.SetCursorPosition(6, 22);
                                    Console.Write("\\_\\");
                                    Console.SetCursorPosition(0, 0);

                                    break;
                                }
                            }

                            if (przycisk.Key == ConsoleKey.Escape) {
                                Menu menu = new Menu();
                            }

                        }
                    }
                }
                if (i + 1 == wyniki.Count) //Jeżeli pętla jest na ostatnim wyniku
                {

                    Console.SetCursorPosition(93, 17);
                    Console.Write("                            ");
                    Console.SetCursorPosition(92, 18);
                    Console.Write("                                ");
                    Console.SetCursorPosition(0, 0);

                    if (i + 1 >= 7) {
                        Console.SetCursorPosition(3, 17);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Naciśnij strzałkę w lewo,");
                        Console.SetCursorPosition(7, 18);
                        Console.Write(" aby wrócić");
                        Console.ResetColor();
                        Console.SetCursorPosition(0, 0);
                    }
                    for ( ; ; )
                    {
                        if (Console.KeyAvailable) {
                            przycisk = Console.ReadKey(true);

                            if (przycisk.Key == ConsoleKey.LeftArrow) {
                                //Rysowanie strzałki podczas kliknięcia
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
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

                                int pom2 = wyniki.Count % 7;
                                i = i - pom2;
                                i = i - 7;

                                Console.SetCursorPosition(7, 19);
                                Console.Write("_");
                                Console.SetCursorPosition(6, 20);
                                Console.Write("/ /"); Console.Write("____");
                                Console.SetCursorPosition(5, 21);
                                Console.Write("( ( ____|");
                                Console.SetCursorPosition(6, 22);
                                Console.Write("\\_\\");
                                Console.SetCursorPosition(0, 0);

                                break;
                            }

                            if (przycisk.Key == ConsoleKey.Escape) {
                                Menu menu = new Menu();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
