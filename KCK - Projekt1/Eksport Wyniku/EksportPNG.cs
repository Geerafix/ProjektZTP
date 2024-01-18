using System.Drawing;

namespace EscapeRoom.Eksport_Wyniku
{
    internal class EksportPNG : IStrategiaEksportu
    {
        public void Eksportuj(Wyniki wyniki)
        {
            Bitmap bitmap = new Bitmap(500, 500);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
            }

            using (Graphics g = Graphics.FromImage(bitmap))

            using (Font font = new Font("Arial", 30))
            {
                g.DrawString("Mój wynik w Escaperoom", font, Brushes.Yellow, new PointF(20, 50));

                g.DrawString($"Czas: {wyniki.getTime()} sek.", font, Brushes.Orange, new PointF(10, 150));
                g.DrawString($"Nick: {wyniki.getUsername()}", font, Brushes.Orange, new PointF(10, 250));
                g.DrawString($"Data: {wyniki.getDate()}", font, Brushes.Orange, new PointF(10, 350));
            }

            bitmap.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "wynik_" + $"{wyniki.getUsername()}" + "_" + $"{wyniki.getDate().ToString("yyyyMMdd")}" + ".png"), System.Drawing.Imaging.ImageFormat.Png);

            Console.SetCursorPosition(41, 34);
            Console.WriteLine("Eksport wyników do pliku PNG zakończony.");
        }
    }
}
