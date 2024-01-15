using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Eksport_Wyniku
{
    internal class EksportPNG : IStrategiaEksportu
    {
        public void Eksportuj(Wyniki wyniki)
        {
            // Stworzenie nowego obrazka o wymiarach 500x500 pikseli (możesz dostosować wymiary według potrzeb)
            Bitmap bitmap = new Bitmap(500, 500);

            // Ustawienie koloru tła
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Blue);
            }

            // Ustawienie koloru i czcionki tekstu
            using (Graphics g = Graphics.FromImage(bitmap))

            using (Font font = new Font("Arial", 30))
            {
                g.DrawString("Mój wynik w Escaperoom", font, Brushes.Yellow, new PointF(20, 50));

                g.DrawString($"Czas: {wyniki.getTime()} sek.", font, Brushes.Orange, new PointF(10, 150));
                g.DrawString($"Nick: {wyniki.getUsername()}", font, Brushes.Orange, new PointF(10, 250));
                g.DrawString($"Data: {wyniki.getDate()}", font, Brushes.Orange, new PointF(10, 350));
            }

            // Zapisanie obrazka do pliku PNG na pulpicie
            bitmap.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "wynik_" + $"{wyniki.getUsername()}" + "_" + $"{wyniki.getDate().ToString("yyyyMMdd")}" + ".png"), System.Drawing.Imaging.ImageFormat.Png);

            Console.SetCursorPosition(41, 34);
            Console.WriteLine("Eksport wyników do pliku PNG zakończony.");
        }
    }
}
