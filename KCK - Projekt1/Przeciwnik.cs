/*namespace KCK___Projekt1 {
    internal class Przeciwnik : IPrzeciwnik {
        private int x;
        private int y;
        private int kroki;

        public Przeciwnik(int initialX, int initialY, int initialkroki) {
            x = initialX;
            y = initialY;
            kroki = initialkroki;
        }

        public void Move() {
            while (true) {
                Console.SetCursorPosition(x, y);
                Console.ResetColor();
                Console.WriteLine("  ");
                x += kroki;
                if (x <= 0 || x >= 10) { kroki = -kroki; }
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("██");
                Thread.Sleep(100);
            }
        }

        public int Szybkosc() {
            throw new NotImplementedException();
        }

        public int Wielkosc() {
            throw new NotImplementedException();
        }
    }
}
*/