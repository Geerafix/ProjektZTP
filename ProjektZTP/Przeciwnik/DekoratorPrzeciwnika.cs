namespace EscapeRoom.Przeciwnik
{
    public abstract class DekoratorPrzeciwnika : IPrzeciwnik
    {
        private IPrzeciwnik przeciwnik = null;
        protected int Przyspieszenie = 0;
        protected int Zwiekszenie = 0;

        public DekoratorPrzeciwnika(IPrzeciwnik przeciwnik)
        {
            this.przeciwnik = przeciwnik;
        }

        public int Wielkosc()
        {
            return this.przeciwnik.Wielkosc() + this.Zwiekszenie;
        }
        public int Szybkosc()
        {
            return this.przeciwnik.Szybkosc() + this.Przyspieszenie;
        }

        public int GetX()
        {
            return this.przeciwnik.GetX();
        }

        public int GetY()
        {
            return this.przeciwnik.GetY();
        }

        public void SetX(int x)
        {
            this.przeciwnik.SetX(x);
        }

        public void SetY(int y)
        {
            this.przeciwnik.SetY(y);
        }

        public bool GetKierunek()
        {
            return this.przeciwnik.GetKierunek();
        }

        public void SetKierunek(bool way)
        {
            this.przeciwnik.SetKierunek(way);
        }

        public int GetPozycja()
        {
            return this.przeciwnik.GetPozycja();
        }

        public void SetPozycja(int pozycja)
        {
            this.przeciwnik.SetPozycja(pozycja);
        }
    }
}
