
namespace EscapeRoom
{
    internal class Instrukcja
    {
        private Postac postac = Postac.pobierzPostac();
        private ConsoleKeyInfo przycisk;

        public Instrukcja()
        {
            Console.Clear();
            RysujLogo();
            RysujRamke();
            RysujInstrukcje();

            postac.SetX(78);
            postac.SetY(12);

            //Ustaw pozycję postaci i narysują postać
            console(postac.GetX(), postac.GetY(), "██", ConsoleColor.White);

            for (; ; )
            {
                if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
                {
                    przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze
                    console(postac.GetX(), postac.GetY(), "  ", ConsoleColor.White);

                    if (przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) //Jeżeli naciśnięta strzałka w górę lub "w"
                    {
                        if (postac.GetY() >= 12) //Górna granica mapy
                        {
                            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1); //Przzesuń postać w górę
                        }
                    }

                    if (przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) //Jeżeli naciśnięta strzałka w dół lub "s"
                    {
                        if (postac.GetY() <= 14) //Dolna granica mapy
                        {
                            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() + 1); //Przesuń postać w dół
                        }
                    }

                    if (przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) //Jeżeli naciśnięta strzałka w lewo lub "a"
                    {
                        if (postac.GetX() >= 77) //Lewa granica mapy
                        {
                            postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY()); //Przesuń postać w lewo
                        }
                    }

                    if (przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) //Jeżeli naciśnięta strzałka w prawo lub "d"
                    {
                        if (postac.GetX() <= 82) //Prawa granica mapy
                        {
                            postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY()); //Przesuń postać w prawo
                        }
                    }

                    if(przycisk.Key == ConsoleKey.Escape)
                    {
                        Menu menu = new Menu();
                        break;
                    }

                    //Ustaw pozycję postaci i narysuj postać
                    console(postac.GetX(), postac.GetY(), "██", ConsoleColor.White);
                }
            }
        }

        private void RysujLogo()
        {
            string instrukcja = File.ReadAllText("instrukcja.txt");
            console(0, 2, instrukcja, ConsoleColor.Cyan);
        }

        private void RysujRamke()
        {
            string ramka = File.ReadAllText("ramka.txt");
            console(75, 8, ramka, ConsoleColor.White);
        }

        private void RysujInstrukcje() {
            console(5, 14, "W grze poruszasz się za pomocą strzałek albo za pomocą 'wsad': ", ConsoleColor.Yellow);

            console(3, 20, "Wejdź się do zielonego portalu, aby dostać się do następnego poziomu: ", ConsoleColor.Yellow);

            console(4, 25, "Unikaj wszytkiego co jest czerwone, w innym przypadku zginiesz: ", ConsoleColor.Yellow);

            console(78, 19, "╔╗", ConsoleColor.Green);
            console(78, 20, "╚╝\n", ConsoleColor.Green);

            console(70, 25, " -->       ██", ConsoleColor.Red);

            console(47, 31, "Wciśnij ESC aby wrócić do MENU", ConsoleColor.DarkYellow);
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
