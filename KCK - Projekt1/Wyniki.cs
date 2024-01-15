using EscapeRoom.Eksport_Wyniku;
using System;

namespace EscapeRoom
{
    public class Wyniki : IObservable
    {
        private long czas;
        private ConsoleKeyInfo przycisk;
        char[] znakiPliku;
        double czasWynik;
        DateTime data;
        string username;

        private List<IObserver> observers = new List<IObserver>();

        private IStrategiaEksportu strategiaEksportu;

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


        public Wyniki(long czas, TabelaWynikow tabelaWynikow)
        {
            this.czas = czas;
            czasWynik = czas;
            czasWynik = czasWynik / 1000;

            data = DateTime.Now;

            AddObserver(tabelaWynikow);

            Console.Clear();

            string sciezkaDoPliku = "KCKWyniki.txt";

            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            int pom = 0; //zmienna która liczy ilość wgranych znaków z plików

            foreach (char c in znakiPliku)
            {
                pom++;
                Console.Write(c);
                Console.ResetColor();
            }

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

            for (; ; )
            {
                if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
                {
                    przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                    if (przycisk.Key == ConsoleKey.D1)
                    {
                        string nazwa = "";
                        Console.SetCursorPosition(30, 22);
                        Console.Write("Wpisz swoją nazwę i kliknij ENTER: ");
                        for (; ; )
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                Menu menu = new Menu();
                            }

                            if (keyInfo.Key == ConsoleKey.Enter)
                            {
                                break; // Zakończ pętlę po naciśnięciu Enter.
                            }
                            else if (keyInfo.Key == ConsoleKey.Backspace && nazwa.Length > 0)
                            {
                                // Jeśli użytkownik naciśnie Backspace i nazwa nie jest pusta, usuń ostatni znak.
                                nazwa = nazwa.Substring(0, nazwa.Length - 1);
                            }
                            else if (nazwa.Length < 15 && keyInfo.Key != ConsoleKey.Spacebar)
                            {
                                // Jeśli długość nazwy jest mniejsza niż 15 znaków, dodaj kolejny znak.
                                nazwa += keyInfo.KeyChar;
                            }

                            username = nazwa;

                            // Wypisz aktualną zawartość nazwy.
                            Console.SetCursorPosition(65, 22);
                            Console.Write(new string(' ', 20)); // Wyczyść poprzednią zawartość
                            Console.SetCursorPosition(66, 22);
                            Console.Write(nazwa);
                            tabelaWynikow.setTabela(nazwa, czasWynik, data);
                        }
                        
                        NotifyObservers(); //Powiadom subskrybentów (w tym przypadku klasę TabelaWynikow)

                        Console.SetCursorPosition(43, 26);
                        Console.Write("Czy zapisać wynik w formie obrazka?");

                        Console.SetCursorPosition(45, 28);
                        Console.Write("1. Tak                2. Nie");

                        for (; ; )
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                Menu menu = new Menu();
                            }

                            if (keyInfo.Key == ConsoleKey.D1)
                            {
                                break;
                            }

                            if (keyInfo.Key == ConsoleKey.D2)
                            {
                                Menu menu = new Menu();
                            }
                        }

                        Console.SetCursorPosition(45, 30);
                        Console.Write("Wybierz format obrazka:");

                        Console.SetCursorPosition(45, 32);
                        Console.Write("1. PNG     2. JPEG     2. BMP");

                        for (; ; )
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                Menu menu = new Menu();
                            }

                            if (keyInfo.Key == ConsoleKey.D1)
                            {
                                SetStrategiaEksportu(new EksportPNG());
                                EksportujWyniki();
                                break;
                            }

                            if (keyInfo.Key == ConsoleKey.D2)
                            {
                                SetStrategiaEksportu(new EksportJPEG());
                                EksportujWyniki();
                                break;
                            }

                            if (keyInfo.Key == ConsoleKey.D3)
                            {
                                SetStrategiaEksportu(new EksportBMP());
                                EksportujWyniki();
                                break;
                            }
                        }

                        Console.SetCursorPosition(40, 35);
                        Console.Write("Naciśnij Esc by powrócić do menu głównego");
                    }

                    if (przycisk.Key == ConsoleKey.Escape)
                    {
                        Menu menu = new Menu();
                    }
                }
            }
        }

        // Implementacja IObservable
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }

    internal interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }
}
