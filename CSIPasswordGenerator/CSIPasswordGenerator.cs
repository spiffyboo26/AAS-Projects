//https://github.com/esayago/Random-Password-Generator/blob/master/Program.cs

using System;
using System.Windows.Forms;

namespace CSIPasswordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables with characters to use for the password and making an array out of the string 
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";//letters variable with the standard characters that can be used in the password
            string specChar = "!#$%&'()*+-/:;<=?@[_{|~";//specChar variable with the special characters that can be used in the password
            string allCharacters = letters + specChar;//variable allCharacters says to use both letters and special characters
            char[] lettersArr = letters.ToCharArray();//variable lettersArr creates an array with the letters
            char[] allCharactersArr = allCharacters.ToCharArray();//variable allCharactersArr creates an array with letters and characters
            string password = "";//variable password uses the array

            
            Console.WriteLine("             RANDOM PASSWORD GENERATOR            ");//displays random password generator to the user
            // User input to determine how the password will be and if special characters will be added or not
            Console.Write("Enter number of characters for password length: ");//displays to user asking for input
            int numOfChar = Convert.ToInt32(Console.ReadLine());//variable numOfChar reads user input and turns it into an integer and reads the integer
            Console.Write("Special Characters? (Y/N): ");//asks the user if they want special characters and gets input
            string userInp = Console.ReadLine().ToUpper();//variable ueserInp reads user input and converts to upper case letters

            // Running the GeneratoPassword method with just string letters or string allCharacters
            //if else statement reading userInp
            if (userInp == "Y")
                password = GeneratePassword(password, numOfChar, allCharacters, allCharactersArr);//if user enters Y password is created with variables password, numOfChar, allCharacters, and allCharactersArr
            else
                password = GeneratePassword(password, numOfChar, letters, lettersArr);//if user enters N password is created with variables password, numOfChar, letters, lettersArr

            // Print password
            Console.WriteLine("\nPassword: " + "\n\n" + password);//displays Password: and then the password to the user
            Console.ReadKey();
        }

        // Method that generates a random password taking as parameters the number of characters, the string and array to reference
        // The Random method is used to select a random character from the array and then added to the string password
        static string GeneratePassword(string password, int numberOfCharacters, string charactersToUse, char[] ArrayToUse)
        {
            Random rand = new Random();//used to get random characters from the array created above to actually create the password
                for (int y = 0; y < numberOfCharacters; y++)//goes through the characters and picks one until it reaches the number of characters input by the user
                {
                    password += Convert.ToString(ArrayToUse[rand.Next(0, charactersToUse.Length - 1)]);//creates string out of characters
                }
              
            
            return password;//returns password variable to be used in the rest of the code
        }
    }
}
