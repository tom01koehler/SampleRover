namespace Rover.Wrappers
{
    public interface IConsoleWrapper
    {
        string ReadPrompt(string promptText);
        void Write(string value);
        void InputErrorMessage(string inputType, string helperText);
        void ErrorMessage(string message);
        void Exit();
        bool IsExit(string input);
    }
}
