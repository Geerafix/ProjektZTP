namespace EscapeRoom.ZapisGry {
    internal class ZapiszGreKomenda : IKomenda {
        public void Wykonaj(StanGry stanGry) {
            String sciezkaZapisuGry = "../../../ZapisGry/stan.txt";
            using (StreamWriter sw = new StreamWriter(sciezkaZapisuGry)) {
                sw.WriteLine(stanGry.GetCzas());
                sw.WriteLine(stanGry.GetPoziom());
            }
        }
    }
}
