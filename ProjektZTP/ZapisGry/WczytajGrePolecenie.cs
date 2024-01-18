namespace EscapeRoom.ZapisGry {
    internal class WczytajGrePolecenie : IPolecenie {
        public void Wykonaj(StanGry stanGry) {
            String sciezkaZapisuGry = "stan.txt";
            string[] liniePliku = File.ReadAllLines(sciezkaZapisuGry);
            stanGry.SetCzas(Int32.Parse(liniePliku[0]));
            stanGry.SetPoziom(Int32.Parse(liniePliku[1]));
        }
    }
}
