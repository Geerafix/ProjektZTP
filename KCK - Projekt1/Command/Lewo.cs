namespace KCK___Projekt1.Command {
    internal class Lewo : IKomenda {
        private Postac postac;
        public Lewo(Postac postac) {
            this.postac = postac;
        }

        public void Wykonaj() {
            postac.ZmienLokalizacje(postac.GetX() - 1, postac.GetY());
        }
    }
}
