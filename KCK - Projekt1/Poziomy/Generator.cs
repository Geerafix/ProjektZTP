namespace EscapeRoom.Poziomy
{
    internal abstract class Generator
    {
        private char[] znakiPliku;
        protected SoundPlayer soundPlayer = new SoundPlayer();

        protected Postac postac = Postac.pobierzPostac();

        public void GenerujPoziom()
        {
            Console.Clear();
            NarysujMape();
            NarysujPortal("../../../Assety/KCKPortal.txt", 64, 5, ConsoleColor.Green);

            string nazwaPoziomu = UstawNazwePoziomu();

            NarysujPrzeszkodyINapis(nazwaPoziomu);

            NarysujLogoPoziomu(nazwaPoziomu);
            NarysujPostac();

            Rysuj();
        }

        protected abstract void Rysuj();

        private string UstawNazwePoziomu()
        {
            string nazwaPoziomu = "";

            if (this is Poziom1)
            {
                nazwaPoziomu = "../../../Assety/Poziom1.txt";
                console(45, 2, "UNIKAJ CZERWONEJ LAWY! NIE WPADNIJ DO NIEJ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom2)
            {
                nazwaPoziomu = "../../../Assety/Poziom2.txt";
                console(40, 2, "UWAŻAJ NA CZERWONE STRZAŁKI! NIE DAJ SIĘ USTRZELIĆ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom3)
            {
                nazwaPoziomu = "../../../Assety/Poziom3.txt";
                console(35, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM! UWAGA! ONI CIĘ GONIĄ!", ConsoleColor.Yellow);
            }
            else if (this is Poziom4)
            {
                nazwaPoziomu = "../../../Assety/Poziom4.txt";
                console(45, 2, "NIE DAJ SIĘ ZŁAPAĆ CZERWONYM PRZECIWNIKOM!", ConsoleColor.Yellow);
            }

            return nazwaPoziomu;
        }

        private void NarysujPortal(string sciezkaDoPliku, int x, int y, ConsoleColor kolor)
        {
            Narysuj(sciezkaDoPliku, x, y, kolor);
        }

        private void NarysujPrzeszkodyINapis(string nazwaPoziomu)
        {
            if (this is Poziom1)
            {
                Narysuj("../../../Assety/KCKLava1.txt", 22, 23, ConsoleColor.Red);
                Narysuj("../../../Assety/KCKLava2.txt", 49, 13, ConsoleColor.Red);
                Narysuj("../../../Assety/KCKLava3.txt", 103, 22, ConsoleColor.Red);
                Narysuj("../../../Assety/KCKPrzeszkoda1.txt", 31, 23, null);
                Narysuj("../../../Assety/KCKPrzeszkoda2.txt", 67, 12, null);
            }
            else if (this is Poziom2)
            {
                Narysuj("../../../Assety/KCKPrzeszkoda3.txt", 20, 27, null);
            }
            else if (this is Poziom3)
            {
                // Brak dodatkowych przeszkód w poziomie 3
            }
            else if (this is Poziom4)
            {
                // Brak dodatkowych przeszkód w poziomie 4
            }

            Narysuj(nazwaPoziomu, 5, 35, null);
        }

        private void NarysujLogoPoziomu(string nazwaPoziomu)
        {
            Narysuj(nazwaPoziomu, 5, 35, null);
        }

        private void NarysujPostac()
        {
            postac.UstawPozPoczatkowa();
        }

        private void NarysujMape()
        {
            string sciezkaDoPliku = "../../../Assety/KCKMapa.txt";

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

        protected void console(int x, int y, string znak, ConsoleColor kolor = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = kolor;
            Console.Write(znak);
            Console.ResetColor();
        }
    }
}
