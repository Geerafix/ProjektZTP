using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Przeciwnik
{
    public interface IPrzeciwnik
    {
        int Wielkosc();
        int Szybkosc();
        int GetX();
        int GetY();
        void SetX(int x);
        void SetY(int y);
        bool GetKierunek();
        void SetKierunek(bool way);
        int GetPozycja();
        void SetPozycja(int pozycja);
    }
}