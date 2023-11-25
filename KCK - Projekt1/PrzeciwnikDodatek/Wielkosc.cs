using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.PrzeciwnikDodatek {

    public class Wielki : DekoratorPrzeciwnika
    {
        public Wielki(Przeciwnik przeciwnik) : base(przeciwnik) { }

    }
}
