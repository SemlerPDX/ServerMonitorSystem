namespace ServerMonitorSystem
{
    /// <summary>
    /// Provides methods for allowing input of only numbers and reading the user number(s) input into the console.
    /// </summary>
    public interface IConsoleReadNumbers
    {
        /// <summary>
        /// Allow only numeric entry into the console, and return that number when users press 'Enter'.
        /// </summary>
        /// <returns>The number(s) entered by the user; -1, if none or otherwise.</returns>
        int ReadNumberInput();

        /// <summary>
        /// A numeric choice selection which allows only numeric entry into the console up to the maximum number of elements
        /// in the supplied choicesList string array, and return that number when users press 'Enter'.
        /// </summary>
        /// <param name="choicesList">A string array of items to be chosen from based on their non-zero based index number.</param>
        /// <returns>The number(s) entered by the user pertaining to a choice of an item in the supplied list; -1, if none or otherwise.</returns>
        int ReadChoiceInput(string[] choicesList);
    }
}
