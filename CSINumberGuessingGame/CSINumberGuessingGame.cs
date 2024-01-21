//Comes from Francesco Magliocco, the man that tells it like it is.

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
            Random r = new Random(); //class to referenct random number

            int winNum = r.Next(1, 100);//generates a winning number between 1-100

            bool win = false;//sets the norm to never win

            do//starts do-while loop
            {
                Console.Write("Guess a number between 1 and 100: ");//tells user to pick a number and writes to screen
                string s = Console.ReadLine()!;//reads input from the user

                int i = int.Parse(s);//makes input an integer

                if (i > winNum)//if else to check if input is higher than winning number
                {
                    Console.WriteLine("To high! Guess lower...");//tells user if input is too high, goes to line 42
                }
                else if (i < winNum)//if else to check if input is lower than winning number
                {
                    Console.WriteLine("To low! Guess lower...");//tells user if input is too low, goes to line 42
                }
                else if (i == winNum)//Checks if input is = to winning number
                {
                    Console.WriteLine("YOU WIN!");//tells user they won
                    win = true;//changes win to true instead of false
                }

                Console.WriteLine();//creates blank line after each if, else if statement
            } while (win == false);//as long as win is false it continues the loop, once win is true the loop ends

            Console.WriteLine("Thank you for playing the game!");//exit statement to make user friendly
            Console.Write("Press any key to  finish.");//tells user how to exit the game
            Console.ReadKey(true);//reads any keystroke and exits the game
        }
    }
}
