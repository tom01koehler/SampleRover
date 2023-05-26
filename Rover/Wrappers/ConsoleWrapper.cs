using System;

namespace Rover.Wrappers
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        /// <summary>
        /// Exit Environment
        /// </summary>
        public void Exit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Read Line, Check for exit
        /// </summary>
        /// <param name="promptText"></param>
        /// <returns>value</returns>
        public string ReadPrompt(string promptText)
        {
            Console.Write(promptText);
            var value = Console.ReadLine().Trim();
            if (IsExit(value))
                Exit();

            return value;
        }

        /// <summary>
        /// Write to Console
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            Console.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputType"></param>
        public void InputErrorMessage(string inputType, string helperText)
        {
            Console.WriteLine($"Invalid input for {inputType}. {helperText}");
        }

        public void ErrorMessage(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public bool IsExit(string input)
        {
            return input.Equals("exit", StringComparison.InvariantCultureIgnoreCase) ||
                input.Equals("e", StringComparison.InvariantCultureIgnoreCase) ||
                input.Equals("no", StringComparison.InvariantCultureIgnoreCase) ||
                input.Equals("n", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
