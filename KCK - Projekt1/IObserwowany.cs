using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal interface IObserwowany
    {
        void DodajObserwatora(IObserwator obserwator);
        void UsunObserwatora(IObserwator obserwator);
        void PowiadomObserwatorow(string nazwa, double czasWynik, DateTime data);
    }
}
