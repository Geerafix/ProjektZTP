namespace EscapeRoom.Przeciwnik
{

    public abstract class DekoratorPrzeciwnika : IPrzeciwnik
    {

        IPrzeciwnik przeciwnik = null;

        protected int Przyspieszenie = 0;
        protected int Zwiekszenie = 0;

        public DekoratorPrzeciwnika(IPrzeciwnik przeciwnik)
        {
            this.przeciwnik = przeciwnik;
        }

        public int Wielkosc()
        {
            return przeciwnik.Wielkosc() + Zwiekszenie;
        }
        public int Szybkosc()
        {
            return przeciwnik.Szybkosc() + Przyspieszenie;
        }

        public int GetX()
        {
            return przeciwnik.GetX();
        }

        public int GetY()
        {
            return przeciwnik.GetY();
        }

        public void SetX(int x)
        {
            przeciwnik.SetX(x);
        }

        public void SetY(int y)
        {
            przeciwnik.SetY(y);
        }

        public bool GetKierunek()
        {
            return przeciwnik.GetKierunek();
        }

        public void SetKierunek(bool way)
        {
            przeciwnik.SetKierunek(way);
        }

        public int GetPozycja()
        {
            return przeciwnik.GetPozycja();
        }

        public void SetPozycja(int pozycja)
        {
            przeciwnik.SetPozycja(pozycja);
        }
    }
}
