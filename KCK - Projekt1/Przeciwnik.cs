using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1 {

    public class Przeciwnik
    {
        public int Wielkosc { get; set; }
        public int Szybkosc { get; set; }

        public int LiczbaZyc { get; set; }

        public Przeciwnik()
        {
            Wielkosc = 5;
            Szybkosc = 5;
            LiczbaZyc = 5;
        }
    }

}
