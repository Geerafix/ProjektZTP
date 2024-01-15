using System.Runtime.CompilerServices;

namespace EscapeRoom.Poziomy
{
    internal abstract class Generator
    {
        char[] znakiPliku;

        Postac postac = Postac.pobierzPostac();

        public void GenerateLevel()
        {
            Console.Clear();

            NarysujMape();

            //Narysuj Portal
            Narysuj("KCKPortal.txt", 64, 5, ConsoleColor.Green);

            string nazwaPoziomu = "";

            if (this is Poziom1)
            {
                nazwaPoziomu = "Poziom1.txt";

                //Rysowanie Przeszkód poziomu:
                Narysuj("KCKLava1.txt", 22, 23, ConsoleColor.Red);
                Narysuj("KCKLava2.txt", 49, 13, ConsoleColor.Red);
                Narysuj("KCKLava3.txt", 102, 22, ConsoleColor.Red);
                Narysuj("KCKPrzeszkoda1.txt", 31, 23, null);
                Narysuj("KCKPrzeszkoda2.txt", 67, 12, null);


                console(45, 2, "UNIKAJ CZERWONEJ LAWY! NIE WPADNIJ DO NIEJ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom2)
            {
                nazwaPoziomu = "Poziom2.txt";

                //Rysowanie Przeszkód poziomu:
                Narysuj("KCKPrzeszkoda3.txt", 1, 32, null);

                console(40, 2, "UWAŻAJ NA CZERWONE STRZAŁKI! NIE DAJ SIĘ USTRZELIĆ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom3)
            {
                nazwaPoziomu = "Poziom3.txt";

                console(35, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM! UWAGA! ONI CIĘ GONIĄ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom4)
            {
                nazwaPoziomu = "Poziom4.txt";

                console(35, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM!", ConsoleColor.Yellow);
            }
            else if (this is Poziom5)
            {
                nazwaPoziomu = "Poziom5.txt";

                console(35, 2, "", ConsoleColor.Yellow);
            }

            //Narysuj Logo Poziomu
            Narysuj(nazwaPoziomu, 5, 35, null);

            NarysujPostac();

            Rysuj();
        }

        protected abstract void Rysuj();

        private void NarysujMape()
        {
            string sciezkaDoPliku = "KCKMapa.txt";

            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            foreach (char c in znakiPliku)
            {
                Console.Write(c);
            }
        }

        private void Narysuj(string sciezkaDoPliku, int x, int y, ConsoleColor? colour)
        {
            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            if (colour != null) Console.ForegroundColor = colour.Value;

            Console.SetCursorPosition(x, y);

            foreach (char c in znakiPliku)
            {
                if (Console.CursorLeft == 0)
                {
                    Console.SetCursorPosition(x, Console.CursorTop);
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                }
                Console.Write(c);
            }

            Console.ResetColor();

        }

        private void NarysujPostac()
        {
            console(postac.GetX(), postac.GetY(), "██", null);
        }

        public void console(int x, int y, string str, ConsoleColor? colour)
        {
            Console.SetCursorPosition(x, y);
            if (colour != null) Console.ForegroundColor = colour.Value;
            Console.WriteLine(str);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }
    }
}
