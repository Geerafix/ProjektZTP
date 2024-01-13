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
            NarysujPortal();

            if(this is Poziom1)
            {
                NarysujLogoPoziomu("Poziom1.txt");
                console(45, 2, "UNIKAJ CZERWONEJ LAWY! NIE WPADNIJ DO NIEJ!", ConsoleColor.Yellow);
            }
            else if(this is Poziom2) 
            {
                NarysujLogoPoziomu("Poziom2.txt");
                console(40, 2, "UWAŻAJ NA CZERWONE STRZAŁKI! NIE DAJ SIĘ USTRZELIĆ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom3)
            {
                NarysujLogoPoziomu("Poziom3.txt");
                console(35, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM! UWAGA! ONI CIĘ GONIĄ!", ConsoleColor.Yellow);
            }

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

        private void NarysujPortal()
        {
            string sciezkaDoPliku = "KCKPortal.txt";

            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            int x = 64;
            int y = 5;

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

        }

        private void NarysujLogoPoziomu(string sciezkaDoPliku)
        {
            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            int x = 5;
            int y = 35;

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
