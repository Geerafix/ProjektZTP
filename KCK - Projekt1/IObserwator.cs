namespace EscapeRoom
{
    public interface IObserwator
    {
        void Aktualizuj();
        void Aktualizuj(string nazwa, double czasWynik, DateTime data);
    }
}
