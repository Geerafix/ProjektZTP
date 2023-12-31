﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.Strzalki
{
    internal class StrzalkaManager
    {
        private List<Strzala> strzalki;

        public StrzalkaManager()
        {
            strzalki = new List<Strzala>();
        }

        public void StworzObiekt(IPrzeciwnik przeciwnik, int x, int y)
        {
            Strzala nowaStrzalka = new Strzala();

            nowaStrzalka.SetStrzalaX(przeciwnik.GetX());
            nowaStrzalka.SetStrzalaY(przeciwnik.GetY());

            nowaStrzalka.SetXpostaci(x);
            nowaStrzalka.SetYpostaci(y);

            strzalki.Add(nowaStrzalka);

            nowaStrzalka = null;

            SprawdzIUsunStareObiekty();

        }

        public void RuszStrzalki()
        {
            foreach (var Str in strzalki)
            {
                Console.SetCursorPosition(Str.GetStrzalaX(), Str.GetStrzalaY());
                Console.Write(" ");
                if (Str.GetStrzalaX() >= Str.GetXpostaci())
                {
                    Str.SetStrzalaX(Str.GetStrzalaX() - 1);
                }
                if (Str.GetStrzalaX() <= Str.GetXpostaci())
                {
                    Str.SetStrzalaX(Str.GetStrzalaX() + 1);
                }
                if (Str.GetStrzalaY() >= Str.GetYpostaci())
                {
                    Str.SetStrzalaY(Str.GetStrzalaY() - 1);
                }
                if (Str.GetStrzalaY() <= Str.GetYpostaci())
                {
                    Str.SetStrzalaY(Str.GetStrzalaY() + 1);
                }

                RysujStrzalke(Str);
            }
        }

        public void RuszStrzalki2()
        {
            foreach (var Str in strzalki)
            {
                RysujStrzalke(Str);
            }
        }

        private void RysujStrzalke(Strzala strzala)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(strzala.GetStrzalaX(), strzala.GetStrzalaY());
            Console.Write("I");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        private void SprawdzIUsunStareObiekty()
        {

            for (int i = 0; i < strzalki.Count(); i++)
            {
                if (strzalki[i].GetStrzalaX() <= 21 || strzalki[i].GetStrzalaX() >= 109 || strzalki[i].GetStrzalaY() <= 5 || strzalki[i].GetStrzalaY() >= 31 || strzalki[i].GetStrzalaX() == strzalki[i].GetXpostaci() && strzalki[i].GetStrzalaY() == strzalki[i].GetYpostaci())
                {
                    Console.SetCursorPosition(strzalki[i].GetStrzalaX(), strzalki[i].GetStrzalaY());
                    Console.Write(" ");
                    strzalki.RemoveAt(i);
                }
            }
        }
    }

}
