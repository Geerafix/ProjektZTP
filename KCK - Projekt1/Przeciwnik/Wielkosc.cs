﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Przeciwnik
{

    public class Wielkosc : DekoratorPrzeciwnika {
        public Wielkosc(IPrzeciwnik przeciwnik) : base(przeciwnik) {
            ZmienWielkosc();
        }

        private void ZmienWielkosc() {
            Zwiekszenie = 3;
        }
    }
}