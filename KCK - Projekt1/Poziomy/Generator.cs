namespace KCK___Projekt1.Poziomy
{
    internal abstract class Generator
    {
        public void GenerateLevel()
        {
            Rysuj();
        }

        protected abstract void Rysuj();
    }
}
