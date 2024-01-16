namespace EscapeRoom.Przeciwnik
{
    public class Wielkosc : DekoratorPrzeciwnika {
        public Wielkosc(IPrzeciwnik przeciwnik) : base(przeciwnik) {
            ZmienWielkosc();
        }

        private void ZmienWielkosc() {
            this.Zwiekszenie = 3;
        }
    }
}