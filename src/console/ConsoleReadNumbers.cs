using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Console Reader class handling numbers which implements <see cref="IConsoleReadNumbers"/> to
    /// provide access to methods for allowing input of only numbers and reading the user number(s) input into the console.
    /// </summary>
    class ConsoleReadNumbers : IConsoleReadNumbers
    {
        private const string InvalidEntryText = " -Invalid entry. Try again.";
        private const string InvalidChoiceText = " -Invalid choice. Try again.";

        /// <summary>
        /// Discards the last user input in the console by erasing the character and moving the cursor back one space.
        /// </summary>
        /// <param name="numbersEntered">The numbers entered so far in the console.</param>
        /// <returns></returns>
        private string DiscardInput(string numbersEntered)
        {
            numbersEntered = numbersEntered.Substring(0, numbersEntered.Length - 1);
            Console.Write("\b \b");
            return numbersEntered;
        }

        /// <summary>
        /// Allow only numeric entry into the console, and return that number when users press 'Enter'.
        /// </summary>
        /// <returns>The number(s) entered by the user; -1, if none or otherwise.</returns>
        public int ReadNumberInput()
        {
            string numbersEntered = "";

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter && Char.IsDigit(keyInfo.KeyChar))
                {
                    numbersEntered += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && numbersEntered.Length > 0)
                    {
                        numbersEntered = DiscardInput(numbersEntered);
                    }
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            if (Int32.TryParse(numbersEntered, out int numValue))
                return numValue;

            Console.WriteLine(InvalidEntryText);
            return -1;
        }

        /// <summary>
        /// A numeric choice selection which allows only numeric entry into the console up to the maximum number of elements
        /// in the supplied choicesList string array, and return that number when users press 'Enter'.
        /// </summary>
        /// <param name="choicesList">A string array of items to be chosen from based on their non-zero based index number.</param>
        /// <returns>The number(s) entered by the user pertaining to a non-zero based choice of an item in the supplied list; -1, if none or otherwise.</returns>
        public int ReadChoiceInput(string[] choicesList)
        {
            int maxNumber;
            int maxDigits;
            int enteredNumber;
            string numbersEntered = "";

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter && Char.IsDigit(keyInfo.KeyChar))
                {
                    maxNumber = choicesList.Length;
                    maxDigits = maxNumber.ToString().Length;
                    enteredNumber = Int32.Parse(keyInfo.KeyChar.ToString());

                    // Typed 0 to cancel
                    if (numbersEntered.Length == 0 && enteredNumber == 0)
                    {
                        Console.WriteLine();
                        return -1;
                    }

                    if (numbersEntered.Length < maxDigits)
                    {
                        numbersEntered += enteredNumber.ToString();
                        Console.Write(keyInfo.KeyChar);
                    }

                    // Clear any additional digits if the max number is reached
                    if (numbersEntered.Length == maxDigits)
                    {
                        enteredNumber = Int32.Parse(numbersEntered);

                        if (enteredNumber > maxNumber)
                        {
                            numbersEntered = DiscardInput(numbersEntered);
                        }
                    }
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && numbersEntered.Length > 0)
                    {
                        numbersEntered = DiscardInput(numbersEntered);
                    }
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            if (Int32.TryParse(numbersEntered, out int numberChoice))
                return numberChoice;

            Console.WriteLine(InvalidChoiceText);
            return -1;
        }

    }
}
