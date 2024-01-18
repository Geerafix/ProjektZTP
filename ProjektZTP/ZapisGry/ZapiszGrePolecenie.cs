namespace EscapeRoom.ZapisGry {
    internal class ZapiszGrePolecenie : IPolecenie {
        public void Wykonaj(StanGry stanGry) {
            String sciezkaZapisuGry = "stan.txt";
            using (StreamWriter sw = new StreamWriter(sciezkaZapisuGry)) {
                sw.WriteLine(stanGry.GetCzas());
                sw.WriteLine(stanGry.GetPoziom());
            }
        }
    }
}
