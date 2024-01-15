using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using EscapeRoom.Przeciwnik;

namespace EscapeRoom
{
    internal class StrzalkaManager {
        private List<Strzala> strzalki;

        public StrzalkaManager() {
            strzalki = new List<Strzala>();
        }

        public void StworzObiekt(IPrzeciwnik przeciwnik, int x, int y) {
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
            for (int i = strzalki.Count - 1; i >= 0; i--)
            {
                Strzala Str = strzalki[i];

                Console.SetCursorPosition(Str.GetStrzalaX(), Str.GetStrzalaY());
                Console.Write(" ");

                int oldX = Str.GetStrzalaX();
                int oldY = Str.GetStrzalaY();

                if (Str.GetStrzalaX() > Str.GetXpostaci())
                {
                    Str.SetStrzalaX(Str.GetStrzalaX() - 1);
                }
                else if (Str.GetStrzalaX() < Str.GetXpostaci())
                {
                    Str.SetStrzalaX(Str.GetStrzalaX() + 1);
                }

                if (Str.GetStrzalaY() > Str.GetYpostaci())
                {
                    Str.SetStrzalaY(Str.GetStrzalaY() - 1);
                }
                else if (Str.GetStrzalaY() < Str.GetYpostaci())
                {
                    Str.SetStrzalaY(Str.GetStrzalaY() + 1);
                }

                if (oldX == Str.GetStrzalaX() && oldY == Str.GetStrzalaY())
                {
                    strzalki.RemoveAt(i);
                }
                else
                {
                    RysujStrzalke(Str);
                }
            }
        }



        public void RuszStrzalki2() {
            foreach (var Str in strzalki) {
                RysujStrzalke(Str);
            }
        }

        private void RysujStrzalke(Strzala strzala) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(strzala.GetStrzalaX(), strzala.GetStrzalaY());
            Console.Write("I");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }

        private void SprawdzIUsunStareObiekty()
        {
            for (int i = 0; i < strzalki.Count; i++)
            {
                Strzala currentArrow = strzalki[i];

                if (currentArrow.GetStrzalaX() <= 21 || currentArrow.GetStrzalaX() >= 109 ||
                    currentArrow.GetStrzalaY() <= 5 || currentArrow.GetStrzalaY() >= 31 ||
                    (currentArrow.GetStrzalaX() == currentArrow.GetXpostaci() && currentArrow.GetStrzalaY() == currentArrow.GetYpostaci()))
                {
                    Console.SetCursorPosition(currentArrow.GetStrzalaX(), currentArrow.GetStrzalaY());
                    Console.Write(" ");
                    strzalki.RemoveAt(i);
                }
            }
        }

        public List<Strzala> GetStrzalki()
        {
            return strzalki;
        }
    }

}
