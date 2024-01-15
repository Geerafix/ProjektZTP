using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Eksport_Wyniku
{
    internal class EksportBMP : IStrategiaEksportu
    {
        public void Eksportuj(Wyniki wyniki)
        {
            // Stworzenie nowego obrazka o wymiarach 500x500 pikseli (można dostosować wymiary według potrzeb)
            // Używamy PixelFormat.Format24bppRgb dla obrazu, co oznacza 24-bitową głębokość kolorów RGB. To pozwala na uzyskanie pełnego koloru bez kompresji.
            Bitmap bitmap = new Bitmap(500, 500, PixelFormat.Format24bppRgb);

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

            // Zapisanie obrazka do pliku BMP na pulpicie
            bitmap.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"wynik_{wyniki.getUsername()}_{wyniki.getDate().ToString("yyyyMMdd")}.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);

            Console.SetCursorPosition(41, 34);
            Console.WriteLine("Eksport wyników do pliku BMP zakończony.");
        }
    }
}
