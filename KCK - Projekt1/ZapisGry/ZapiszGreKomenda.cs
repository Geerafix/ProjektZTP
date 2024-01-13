namespace EscapeRoom.ZapisGry {
    internal class ZapiszGreKomenda : IKomenda {
        public void Wykonaj(StanGry stanGry) {
            String sciezka = "../../../ZapisGry/stan.txt";
            using (StreamWriter sw = new StreamWriter(sciezka)) {
                sw.WriteLine(stanGry.GetCzas());
                sw.WriteLine(stanGry.GetPoziom());
            }
        }
    }
}
