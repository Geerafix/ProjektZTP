using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.Sortowania {
    internal class KontekstFiltru {
        private IStrategiaFiltrowania isf;

        public void UstawStrategie(IStrategiaFiltrowania isf) {
            this.isf = isf;
        }

        public void Wykonaj(List<int> list) {
            isf.Filtruj();
        }
    }
}
