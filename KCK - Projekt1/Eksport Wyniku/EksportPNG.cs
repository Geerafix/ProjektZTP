using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Eksport_Wyniku
{
    internal class EksportPNG : IStrategiaEksportu
    {
        public void Eksportuj(Wyniki wyniki)
        {
            // Implement the logic to export wyniki to PNG
            Console.WriteLine($"{wyniki.data}");
        }
    }
}
