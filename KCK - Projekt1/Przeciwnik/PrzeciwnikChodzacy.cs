using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.Przeciwnik
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
            return X;
        }

        public int GetY()
        {
            return Y;
        }

        public void SetX(int x)
        {
            X = x;
        }
        public void SetY(int y)
        {
            Y = y;
        }
        public bool GetKierunek()
        {
            return Kierunek;
        }
        public void SetKierunek(bool way)
        {
            Kierunek = way;
        }
        public int GetPozycja()
        {
            return Pozycja;
        }
        public void SetPozycja(int pozycja)
        {
            Pozycja = pozycja;
        }
    }
}
