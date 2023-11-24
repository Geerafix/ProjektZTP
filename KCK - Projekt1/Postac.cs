namespace KCK___Projekt1 {

    public class Postac {
        private int PostacX;
        private int PostacY;

        private static Postac postac = new Postac();

        private Postac() {
            PostacX = 60;
            PostacY = 30;
        }

        public int GetX() { return PostacX; }

        public int GetY() { return PostacY; }

        public void ZmienLokalizacje(int X, int Y) {
            Console.SetCursorPosition(X, Y);
            Console.Write("  ");

            this.PostacX = X;
            this.PostacY = Y;

            RysujPostac();
        }

        protected void RysujPostac() //Tutaj może trzeba zrobić jako public abstract
        {
            Console.SetCursorPosition(PostacX, PostacY);
            Console.Write("██");
            Console.SetCursorPosition(0, 0);
        }

        public void UstawPozPoczatkowa() {
            PostacX = 60;
            PostacY = 30;
            Console.SetCursorPosition(60, 30);
        }

        public static Postac pobierzPostac()
        {
            if (postac == null) {
                postac = new Postac();
            }
            return postac;
        }

        public void ZmienPozycje()
        {

        }
    }
}
