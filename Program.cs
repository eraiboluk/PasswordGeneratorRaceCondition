using System;
using System.Threading;
using System.Text; // Required for StringBuilder
using System.Collections.Generic; // Required for List

public class Counter
{
    public int Count; // Public field, directly accessible
    private long _effectiveScramble; // Changed to long to handle large values

    // Constructor now takes the already multiplied effective scramble value
    public Counter(long effectiveScramble)
    {
        Count = 0;
        _effectiveScramble = effectiveScramble;
    }

    public void Up()
    {
        for (long i = 0; i < _effectiveScramble; i++)
        {
            Count++;
        }
    }

    public void Down()
    {
        for (long i = 0; i < _effectiveScramble; i++)
        {
            Count--;
        }
    }
}

public class PasswordGenerator
{
    public static void Main(string[] args)
    {
        int userScrambleValue;
        int passwordLength;

        // Get Scramble Value from console input
        while (true)
        {
            Console.Write("Enter scramble value (1-100): ");
            string scrambleInput = Console.ReadLine();
            if (int.TryParse(scrambleInput, out userScrambleValue) && userScrambleValue > 0)
            {
                if (userScrambleValue > 100)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Input capped. Using scramble value: 100");
                    Console.ResetColor();
                    userScrambleValue = 100;
                }
                break; // Valid input, exit loop
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Scramble value must be an integer between 1 and 100 (or higher, will be capped to 100).");
                Console.ResetColor();
            }
        }

        // Get Password Length from console input (between 8 and 20)
        while (true)
        {
            Console.Write("Enter password length (8-20): ");
            string lengthInput = Console.ReadLine();
            if (int.TryParse(lengthInput, out passwordLength) && passwordLength >= 8 && passwordLength <= 20)
            {
                break; // Valid input, exit loop
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Password length must be an integer between 8 and 20.");
                Console.ResetColor();
            }
        }

        bool generateAgain = true;

        while (generateAgain)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nUsing Scramble Value: {userScrambleValue}");
            Console.WriteLine($"Password Length: {passwordLength}");
            Console.ResetColor();

            // Calculate effective scramble: user value * 1,000,000
            long effectiveScrambleAmount = (long)userScrambleValue * 1000000L;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Effective iterations per Up/Down call: {effectiveScrambleAmount:N0}");
            Console.WriteLine("(This might take some time per character...)");
            Console.ResetColor();

            StringBuilder password = new StringBuilder(passwordLength);
            List<int> counterValues = new List<int>();

            Console.WriteLine("Generating password...");

            for (int i = 0; i < passwordLength; i++)
            {
                Counter counter = new Counter(effectiveScrambleAmount);

                Thread threadUp = new Thread(counter.Up);
                Thread threadDown = new Thread(counter.Down);

                threadUp.Start();
                threadDown.Start();

                threadUp.Join();
                threadDown.Join();

                counterValues.Add(counter.Count);

                int desiredRangeSize = 93;
                int charValue = (Math.Abs(counter.Count) % desiredRangeSize) + 33;
                char nextChar = (char)charValue;
                password.Append(nextChar);
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nGenerated Password: {password}");
            Console.ResetColor();

            Console.WriteLine("Generated Password (Raw counter values): " + string.Join(", ", counterValues));

            Console.Write("Generated Password (ASCII values): ");
            foreach (char c in password.ToString())
            {
                Console.Write($"{(int)c} ");
            }
            Console.WriteLine();

            while (true)
            {
                Console.Write("\nGenerate another password with the same settings? (y/n): ");
                string choice = Console.ReadLine()?.ToLower();
                if (choice == "y")
                {
                    generateAgain = true;
                    Console.Clear();
                    break;
                }
                else if (choice == "n")
                {
                    generateAgain = false;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter 'y' or 'n'.");
                    Console.ResetColor();
                }
            }
        }

        Console.WriteLine("\nExiting program. Press any key to close...");
        Console.ReadKey();
    }
}
