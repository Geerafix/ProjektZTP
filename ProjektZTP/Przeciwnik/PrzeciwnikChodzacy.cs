namespace EscapeRoom.Przeciwnik
{
    internal class PrzeciwnikChodzacy : IPrzeciwnik
    {
        private int X;
        private int Y;
        private bool Kierunek;
        private int Pozycja;

        public int Wielkosc()
        {
            return 3;
        }
        public int Szybkosc()
        {
            return 4;
        }

        public int GetX()
        {
            return this.X;
        }

        public int GetY()
        {
            return this.Y;
        }

        public void SetX(int x)
        {
            this.X = x;
        }

        public void SetY(int y)
        {
            this.Y = y;
        }

        public bool GetKierunek()
        {
            return this.Kierunek;
        }

        public void SetKierunek(bool way)
        {
            this.Kierunek = way;
        }

        public int GetPozycja()
        {
            return this.Pozycja;
        }

        public void SetPozycja(int pozycja)
        {
            this.Pozycja = pozycja;
        }
    }
}
