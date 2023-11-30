using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1 {
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





/*if (postac.GetX() > x) x++;
if (postac.GetX() < x) x--;
if (postac.GetY() > y) y++;
if (postac.GetY() < y) y--;


public virtual int Wielkosc { get; set; }
public virtual int Szybkosc { get; set; }
public virtual int LiczbaZyc { get; set; }

public Przeciwnik()
{
    Wielkosc = 0;
    Szybkosc = 0;
    LiczbaZyc = 0;
}

public void zmienWielkosc(int w) { Wielkosc = w; }
public void zmienSzybkosc(int w) { Szybkosc = w; }

public (int x, int y) PrzesunPrzeciwnika(int x, int y)
{
    Console.SetCursorPosition(x, y);
    Console.Write("  ");

    RysujPrzeciwnika(x, y);
    return (x, y);
}

public void RysujPrzeciwnika(int x, int y)
{
    Console.SetCursorPosition(x, y);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("██");
    Console.ResetColor();
    Console.SetCursorPosition(0, 0);
}*/