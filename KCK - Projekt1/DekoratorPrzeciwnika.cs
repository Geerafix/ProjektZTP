namespace KCK___Projekt1
{

    public abstract class DekoratorPrzeciwnika : Przeciwnik {

        protected Przeciwnik przeciwnik;

        public DekoratorPrzeciwnika(Przeciwnik przeciwnik)
        {
            this.przeciwnik = przeciwnik;
        }
    }
}
