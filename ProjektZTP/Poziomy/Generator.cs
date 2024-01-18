namespace EscapeRoom.Poziomy
{
    internal abstract class Generator
    {
        protected char[] znakiPliku;
        protected SoundPlayer soundPlayer = new SoundPlayer();
        protected Postac postac = Postac.pobierzPostac();

        public void GenerujPoziom()
        {
            Console.Clear();
            NarysujMape();
            NarysujPrzeszkodyINapis();
            NarysujPortal();
            UstawPostac();
            Rysuj();
        }

        protected abstract void NarysujMape();
        protected abstract void NarysujPrzeszkodyINapis();
        protected abstract void NarysujPortal();
        protected abstract void UstawPostac();
        protected abstract void Rysuj();

        protected void Narysuj(string sciezkaDoPliku, int x, int y, ConsoleColor? colour) {
            string zawartoscPliku = File.ReadAllText(sciezkaDoPliku);

            znakiPliku = zawartoscPliku.ToCharArray();

            if (colour != null) Console.ForegroundColor = colour.Value;

            Console.SetCursorPosition(x, y);

            foreach (char c in znakiPliku) {
                if (Console.CursorLeft == 0) {
                    Console.SetCursorPosition(x, Console.CursorTop);
                } else {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                }
                Console.Write(c);
            }

            Console.ResetColor();
        }

        protected void console(int x, int y, string znak, ConsoleColor kolor = ConsoleColor.White) {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = kolor;
            Console.Write(znak);
            Console.ResetColor();
        }
    }
}
