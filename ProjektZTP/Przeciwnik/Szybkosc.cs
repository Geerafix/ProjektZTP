namespace EscapeRoom.Przeciwnik
{
    public class Szybkosc : DekoratorPrzeciwnika {
        public Szybkosc(IPrzeciwnik przeciwnik) : base(przeciwnik) {
            ZmienPrzyspieszenie();
        }

        private void ZmienPrzyspieszenie() {
            this.Przyspieszenie = 7;
        }
    }
}