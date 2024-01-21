using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSINumberGuessingGame
{

    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();

            int winNum = r.Next(1, 100);

            bool win = false;

            do
            {
                Console.Write("Guess a number between 1 and 100: ");
                string s = Console.ReadLine()!;

                int i = int.Parse(s);

                if (i > winNum)
                {
                    Console.WriteLine("To high! Guess lower...");
                }
                else if (i < winNum)
                {
                    Console.WriteLine("To low! Guess lower...");
                }
                else if (i == winNum)
                {
                    Console.WriteLine("YOU WIN!");
                    win = true;
                }

                Console.WriteLine();
            } while (win == false);

            Console.WriteLine("Thank you for playing the game!");
            Console.Write("Press any key to  finish.");
            Console.ReadKey(true);
        }
    }
}
