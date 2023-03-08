using System;

namespace ServerMonitorSystem
{
    /// <summary>
    /// The Console Prompt Writer class implementing <see cref="IConsoleWritePrompts"/> to provide access to
    /// the method for writing a custom prompt and prompt symbols to the left of the cursor prior to using a ReadLine method.
    /// </summary>
    class ConsoleWritePrompts : IConsoleWritePrompts
    {
        private const string DEFAULT_PROMPT = @":\> ";
        private const string DEFAULT_PROMPT_TEXT = "Type 'help' for commands list";

        /// <summary>
        /// Write the default console prompt that appears to the left of the blinking cursor which reads user input.
        /// </summary>
        public void WritePrompt() { WritePrompt(null, true); }
        /// <summary>
        /// Write the custom console prompt that appears to the left of the blinking cursor which reads user input.
        /// </summary>
        /// <param name="promptText">The text to display to the left of the cursor input prompt.</param>
        public void WritePrompt(string promptText) { WritePrompt(promptText, true); }
        /// <summary>
        /// Write the custom console prompt that appears to the left of the blinking cursor which reads user input.
        /// </summary>
        /// <param name="promptText">The text to display to the left of the cursor input prompt.</param>
        /// <param name="showPrompt">A boolean indicating whether to print the default prompt symbols after promptText.</param>
        public void WritePrompt(string promptText, bool showPrompt)
        {
            if (String.IsNullOrWhiteSpace(promptText))
                promptText = DEFAULT_PROMPT_TEXT;

            // Write default prompt symbols after text
            if (showPrompt)
                promptText += DEFAULT_PROMPT;

            // Enter spacer below last text/output
            Console.WriteLine();

            // Enter spacer below user input prompt
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition(0, (Console.CursorTop - 3));// Move cursor up three lines

            // Reprint new user input prompt
            Console.Write(promptText);
        }

    }
}
