using System.Numerics;

namespace Mastermind;

class Program
{
    private static Random seed = new Random();

    static string generateFeedback(int[] password, int[] attempt) {
        int plusCount = 0;
        int minusCount = 0;
        bool[] guessCheck = new bool[4];
        for (int i = 0; i < 4; i++) {
            if (attempt[i] == password[i]) {
                plusCount++;
                guessCheck[i] = true;
            }
        }
        for (int i = 0; i < 4; i++) {
            if (attempt[i] != password[i]) {
                for (int j = 0; j < 4; j++) {
                    if (attempt[j] == password[i] && !guessCheck[j]) {
                        minusCount++;
                        guessCheck[j] = true;
                        break;
                    }
                }
            }                
        }

        return new string('+', plusCount) + new string('-', minusCount);
    }


    static void Main(string[] args)
    {
        int numAttempts = 10;

        int[] pwd = new int[4];
        for (int i = 0; i < 4; i++) {
            int digit = seed.Next(1, 7);
            // Console.WriteLine(digit);
            pwd[i] = digit;
        }
        string result = string.Join("", pwd);

        Console.WriteLine("Mastermind");
        Console.WriteLine("----------");
        Console.WriteLine("Guess the 4-digit code (digits 1-6). You have " + numAttempts + " attempts.");

        
        for (int i = 0; i < numAttempts; i++) {

            Console.WriteLine("You have " + (10-i) + " attempts left.");
            
            var input = Console.ReadLine();
            while (string.IsNullOrEmpty(input) || input.Length != 4 || !input.All(c => "123456".Contains(c))) {
                Console.WriteLine("Invalid input. Enter exactly 4 digits that are each between 1-6.");
                input = Console.ReadLine();
            }
            int[] attempt = input.Select(c => c - '0').ToArray();

            if (attempt.SequenceEqual(pwd)) {
                Console.WriteLine("You won!");
                Console.WriteLine("The pasword was: " + result + ".");
                return;
            }
            string feedback = generateFeedback(pwd, attempt);
            Console.WriteLine(feedback);
        }
        Console.WriteLine("You lost!");
        Console.WriteLine("The pasword was: " + result + ".");

    }
}