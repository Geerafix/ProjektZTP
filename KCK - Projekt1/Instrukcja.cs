

using System.Threading;

namespace EscapeRoom
{
    internal class Instrukcja
    {

        Postac postac = Postac.pobierzPostac();
        private ConsoleKeyInfo przycisk;

        public Instrukcja()
        {
            Console.Clear();
            RysujLogo();
            RysujRamke();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(5, 14);
            Console.WriteLine("W grze poruszasz się za pomocą strzałek albo za pomocą 'wsad': ");

            Console.SetCursorPosition(3, 20);
            Console.WriteLine("Wejdź się do zielonego portalu, aby dostać się do następnego poziomu: ");

            Console.SetCursorPosition(4, 25);
            Console.Write("Unikaj wszytkiego co jest czerwone, w innym przypadku zginiesz: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(78, 19);
            Console.WriteLine("╔╗");
            Console.SetCursorPosition(78, 20);
            Console.WriteLine("╚╝");
            Console.WriteLine();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(70, 25);
            Console.WriteLine(" -->       ██");
            Console.ResetColor();


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(47, 31);
            Console.WriteLine("Wciśnij ESC aby wrócić do MENU");
            Console.ResetColor();

            postac.SetX(78);
            postac.SetY(12);

            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 0);

            for (; ; )
            {
                if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
                {
                    przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze
                    Console.SetCursorPosition(postac.GetX(), postac.GetY());
                    Console.Write("  ");

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


                    //Ustaw pozycję postaci i narysują postać
                    Console.SetCursorPosition(postac.GetX(), postac.GetY());
                    Console.Write("██");
                    Console.SetCursorPosition(0, 0);
                }
            }
        }

        public void RysujLogo()
        {
            string instrukcja = File.ReadAllText("../../../Assety/instrukcja.txt");
            Console.SetCursorPosition(0, 2);
            console(instrukcja, ConsoleColor.Cyan);
        }

        public void RysujRamke()
        {
            string ramka = File.ReadAllText("../../../Assety/ramka.txt");
            Console.SetCursorPosition(75, 8);
            console(ramka, ConsoleColor.White);
        }

        public void console(string str, ConsoleColor? colour)
        {
            if (colour != null) Console.ForegroundColor = colour.Value;
            Console.Write(str);
            Console.ResetColor();
        }
    }
}
