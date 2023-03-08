namespace ServerMonitorSystem
{
    /// <summary>provide access to
    /// Provides the method for writing a custom prompt and prompt symbols to the left of the cursor prior to using a ReadLine method.
    /// </summary>
    public interface IConsoleWritePrompts
    {
        /// <summary>
        /// Write the default console prompt that appears to the left of the blinking cursor which reads user input.
        /// </summary>
        void WritePrompt();
        /// <summary>
        /// Write the custom console prompt that appears to the left of the blinking cursor which reads user input.
        /// </summary>
        /// <param name="promptText">The text to display to the left of the cursor input prompt.</param>
        void WritePrompt(string promptText);
        /// <summary>
        /// Write the custom console prompt that appears to the left of the blinking cursor which reads user input.
        /// </summary>
        /// <param name="promptText">The text to display to the left of the cursor input prompt.</param>
        /// <param name="showPrompt">A boolean indicating whether to print the default prompt symbols after promptText.</param>
        void WritePrompt(string promptText, bool showPrompt);
    }
}
