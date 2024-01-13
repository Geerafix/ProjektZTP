namespace EscapeRoom.ZapisGry {
    internal class WczytajGreKomenda : IKomenda {
        public void Wykonaj(StanGry stanGry) {
            String sciezka = "../../../ZapisGry/stan.txt";
            string[] linie = File.ReadAllLines(sciezka);
            stanGry.SetCzas(Int32.Parse(linie[0]));
            stanGry.SetPoziom(Int32.Parse(linie[1]));
        }
    }
}
