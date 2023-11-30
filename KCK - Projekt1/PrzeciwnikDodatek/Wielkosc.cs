using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1 {

    public class Wielkosc : DekoratorPrzeciwnika
    {
        public Wielkosc(IPrzeciwnik przeciwnik) : base(przeciwnik)
        {
            this.Zwiekszenie = 4;
        }
    }
}