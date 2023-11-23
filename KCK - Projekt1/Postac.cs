

namespace KCK___Projekt1
{
    internal class Postac
    {
        int PostacX; //Pozycja postaci x
        int PostacY; //Współrzędna postaci y

        public Postac(int PostacX, int PostacY)
        {
            Console.SetCursorPosition(PostacX, PostacY);
            Console.Write("  ");
            Console.SetCursorPosition(0 ,0);
            this.PostacX = PostacX;
            this.PostacY = PostacY;
        }
        public int GetX()
        {
            return PostacX;
        }

        public int GetY()
        {
            return PostacY;
        }
    }
}
