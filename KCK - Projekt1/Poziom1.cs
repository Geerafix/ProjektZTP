using KCK___Projekt1;
using System.Diagnostics;


internal class Poziom1
{

    Postac postac = new Postac(60, 30);
    private ConsoleKeyInfo przycisk;
    private Stopwatch stoper = new Stopwatch();
    private long czas;

    public Poziom1(long czas)
    {
        this.czas = czas;
        stoper.Start();
        Rysuj();

    }

    public void Rysuj()
    {
        Console.Clear();

        //Rysowanie mapy gry:
        NarysujMape();

        Console.WriteLine("   ___          _              ___");
        Console.WriteLine("  / _ \\___ ___ (____  __ _    <  /");
        Console.WriteLine(" / ___/ _ /_ // / _ \\/  ' \\   / / ");
        Console.WriteLine("/_/   \\___/__/_/\\___/_/_/_/  /_/  ");
        Console.WriteLine("                                  ");


        Console.SetCursorPosition(45, 2);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("UNIKAJ CZERWONEJ LAWY! NIE WPADNIJ DO NIEJ!");
        Console.ResetColor();

        for (; ; )
        {
            Thread.Sleep(1);

            // Wyświetl czas na ekranie.
            long pozostalyCzas = stoper.ElapsedMilliseconds;
            Console.SetCursorPosition(62, 0);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Czas: " + (pozostalyCzas + czas) / 1000 + " s");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 0);

            //Jeżeli postać znajdzie się na terytorium lavy to zakończ grę
            if ((postac.GetX() >= 22 && postac.GetX() <= 30 && postac.GetY() >= 23 && postac.GetY() <= 29) || (postac.GetX() >= 102 && postac.GetX() <= 110 && postac.GetY() >= 22 && postac.GetY() <= 30) || ((postac.GetX() >= 49 && postac.GetX() <= 66 && postac.GetY() >= 13 && postac.GetY() <= 22)))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(54, 15);
                Console.WriteLine("Wpadłeś do lawy"); //Komunikat o śmierci gracza
                Console.SetCursorPosition(50, 16);
                Console.WriteLine("*Wcisnij SPACE aby kontynuować*");

                czas += stoper.ElapsedMilliseconds;
                stoper.Stop();

                int liczCzas = 0; //zmienna pomocnicza, do migania wiadomości

                for (; ; )
                {

                    liczCzas++;
                    if (liczCzas % 13000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.WriteLine("                               ");
                    }
                    if (liczCzas % 15000 == 0)
                    {
                        Console.SetCursorPosition(50, 16);
                        Console.WriteLine("*Wcisnij SPACE aby kontynuować*");
                        liczCzas = 0;
                    }

                    if (Console.KeyAvailable)
                    {
                        przycisk = Console.ReadKey(true);

                        if (przycisk.Key == ConsoleKey.Escape)
                        {
                            Console.ResetColor();
                            stoper.Stop();
                            Menu menu = new Menu();
                        }
                        if (przycisk.Key == ConsoleKey.Spacebar)
                        {
                            Console.ResetColor();
                            stoper.Restart();
                            Poziom1 poziom = new Poziom1(czas);
                        }
                    }
                }

            }

            if (Console.KeyAvailable) //Sprawdza czy jest wciśnięty przycisk
            {
                przycisk = Console.ReadKey(true); //Przypisanie przycisku który klikneło się na klawiaturze

                Console.SetCursorPosition(postac.GetX(), postac.GetY());
                Console.Write("  ");

                if (przycisk.Key == ConsoleKey.UpArrow || przycisk.Key == ConsoleKey.W) //Jeżeli naciśnięta strzałka w górę lub "w"
                {
                    if (postac.GetY() >= 6) //Górna granica mapy
                    {
                        if ((postac.GetX() >= 31 && postac.GetX() <= 51 && postac.GetY() == 30) || (postac.GetX() >= 67 && postac.GetX() <= 105 && postac.GetY() == 22)) //Kordynaty przeszkody
                        {

                        }
                        else
                        {
                            postac = new Postac(postac.GetX(), postac.GetY() - 1); //Przzesuń postać w górę
                        }
                    }
                }
                if (przycisk.Key == ConsoleKey.DownArrow || przycisk.Key == ConsoleKey.S) //Jeżeli naciśnięta strzałka w dół lub "s"
                {
                    if (postac.GetY() <= 31) //Dolna granica mapy
                    {
                        if ((postac.GetX() >= 31 && postac.GetX() <= 51 && postac.GetY() == 22) || (postac.GetX() >= 67 && postac.GetX() <= 105 && postac.GetY() == 11)) //Kordynaty przeszkody
                        {

                        }
                        else
                        {
                            postac = new Postac(postac.GetX(), postac.GetY() + 1); //Przesuń postać w dół
                        }
                    }
                }
                if (przycisk.Key == ConsoleKey.LeftArrow || przycisk.Key == ConsoleKey.A) //Jeżeli naciśnięta strzałka w lewo lub "a"
                {
                    if (postac.GetX() >= 21) //Lewa granica mapy
                    {
                        if ((postac.GetY() <= 29 && postac.GetY() >= 23 && postac.GetX() == 52) || (postac.GetY() >= 12 && postac.GetY() <= 21 && postac.GetX() == 106)) //Kordynaty przeszkody
                        {

                        }
                        else
                        {
                            postac = new Postac(postac.GetX() - 1, postac.GetY()); //Przesuń postać w lewo
                        }
                    }
                }
                if (przycisk.Key == ConsoleKey.RightArrow || przycisk.Key == ConsoleKey.D) //Jeżeli naciśnięta strzałka w prawo lub "d"
                {
                    if (postac.GetX() <= 109) //Prawa granica mapy
                    {
                        if ((postac.GetY() <= 29 && postac.GetY() >= 23 && postac.GetX() == 30) || (postac.GetY() >= 12 && postac.GetY() <= 21 && postac.GetX() == 66)) //Kordynaty przeszkody
                        {

                        }
                        else
                        {
                            postac = new Postac(postac.GetX() + 1, postac.GetY()); //Przesuń postać w prawo
                        }
                    }
                }
                if (przycisk.Key == ConsoleKey.Escape) //Kliknij ESC aby wyjść do MENU
                {
                    stoper.Stop();
                    Menu menu = new Menu();
                }

            }


            //Ustaw pozycję postaci i narysują postać
            Console.SetCursorPosition(postac.GetX(), postac.GetY());
            Console.Write("██");
            Console.SetCursorPosition(0, 0);


            //Jeżeli postać jest na kordynatach bramy do poziomu numer 2
            if (postac.GetX() >= 64 && postac.GetX() <= 66 && postac.GetY() >= 5 && postac.GetY() <= 6)
            {
                czas += stoper.ElapsedMilliseconds;
                stoper.Stop();
                Poziom2 poziom2 = new Poziom2(czas); //Przenieś do poziomu drugiego
            }
        }

    }




    public void NarysujMape()
    {
        Console.WriteLine("\n");
        Console.WriteLine("\n");
        Console.WriteLine("                   ╔════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.Write("                   ║                                             ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("╔╗"); Console.ResetColor();
        Console.WriteLine("                                             ║");
        Console.Write("                   ║                                             ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("╚╝"); Console.ResetColor();
        Console.WriteLine("                                             ║");
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ║                                                ╔════════════════════════════════════╗      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("─────────────────"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ║XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX║      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("|███████████████|"); Console.ResetColor();
        Console.WriteLine(" ╚════════════════════════════════════╝      ║");
        Console.Write("                   ║                              ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("─────────────────                                    "); Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("─────────║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("──────── "); Console.ResetColor();
        Console.Write("╔══════════════════╗                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("│██████│"); Console.ResetColor();
        Console.Write(" ║XXXXXXXXXXXXXXXXXX║                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("│██████│"); Console.ResetColor();
        Console.Write(" ║XXXXXXXXXXXXXXXXXX║                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("│██████│"); Console.ResetColor();
        Console.Write(" ║XXXXXXXXXXXXXXXXXX║                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("│██████│"); Console.ResetColor();
        Console.Write(" ║XXXXXXXXXXXXXXXXXX║                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("│██████│"); Console.ResetColor();
        Console.Write(" ║XXXXXXXXXXXXXXXXXX║                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("────────"); Console.ResetColor();
        Console.Write(" ╚══════════════════╝                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|████████║"); Console.ResetColor();
        Console.Write("                   ║                                                                                   ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("─────────║"); Console.ResetColor();
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ║                                                                                            ║");
        Console.WriteLine("                   ╚════════════════════════════════════════════════════════════════════════════════════════════╝");

    }
}