using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.Command {
    internal class Prawo : IKomenda {
        private Postac postac;
        public Prawo(Postac postac) {
            this.postac = postac;
        }

        public void Wykonaj() {
            postac.ZmienLokalizacje(postac.GetX() + 1, postac.GetY());
        }
    }
}
