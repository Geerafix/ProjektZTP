namespace EscapeRoom.ZapisGry {
    internal class ResetujGreKomenda : IKomenda {
        public void Wykonaj(StanGry stanGry) {
            String sciezkaZapisuGry = "../../../ZapisGry/stan.txt";
            using (StreamWriter sw = new StreamWriter(sciezkaZapisuGry)) {
                sw.WriteLine(0);
                sw.WriteLine(1);
            }
        }
    }
}
