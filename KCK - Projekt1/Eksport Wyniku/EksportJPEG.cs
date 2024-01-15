using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom.Eksport_Wyniku
{
    internal class EksportJPEG : IStrategiaEksportu
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

            // Ustawienia jakości obrazu JPEG
            // Ustawienia jakości obrazu są dostosowane poprzez EncoderParameter
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)80); // Ustaw jakość od 0 do 100
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            // Zapisanie obrazka do pliku JPEG na pulpicie z dostosowanymi ustawieniami jakości
            bitmap.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "wynik_" + $"{wyniki.getUsername()}" + "_" + $"{wyniki.getDate().ToString("yyyyMMdd")}" + ".jpeg"), GetEncoder(ImageFormat.Jpeg), encoderParams);
            Console.SetCursorPosition(40, 34);
            Console.WriteLine("Eksport wyników do pliku JPEG zakończony.");
        }

        // Metoda pomocnicza do uzyskania odpowiedniego kodera
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
