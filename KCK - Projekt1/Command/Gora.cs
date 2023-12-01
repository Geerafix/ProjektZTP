﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.Command {
    internal class Gora : IKomenda {
        private Postac postac;

        public Gora(Postac postac) {
            this.postac = postac;
        }

        public void Wykonaj() {
            postac.ZmienLokalizacje(postac.GetX(), postac.GetY() - 1);
        }
    }
}
