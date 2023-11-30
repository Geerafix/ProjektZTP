using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK___Projekt1.Poziomy
{
    internal abstract class Generator
    {
        public void GenerateLevel()
        {
            Rysuj();
        }

        protected abstract void Rysuj();
    }
}
