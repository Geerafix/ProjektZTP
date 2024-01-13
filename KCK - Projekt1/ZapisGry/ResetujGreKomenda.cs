namespace KCK___Projekt1.ZapisGry {
    internal class ResetujGreKomenda : IKomenda {
        public void Wykonaj(StanGry stanGry) {
            String sciezka = "../../../ZapisGry/stan.txt";
            using (StreamWriter sw = new StreamWriter(sciezka)) {
                sw.WriteLine(0);
                sw.WriteLine(1);
            }
        }
    }
}
