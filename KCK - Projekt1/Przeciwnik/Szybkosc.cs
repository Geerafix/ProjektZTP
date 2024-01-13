using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Przeciwnik
{

    public class Szybkosc : DekoratorPrzeciwnika {
        public Szybkosc(IPrzeciwnik przeciwnik) : base(przeciwnik) {
            ZmienPrzyspieszenie();
        }

        private void ZmienPrzyspieszenie() {
            Przyspieszenie = 7;
        }
    }
}