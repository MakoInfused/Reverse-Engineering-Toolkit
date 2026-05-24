namespace BasicTools
{
    public interface ILogger
    {
        void WriteText(string text);
        void WriteLine(string line);
        void Flush();
    }
}
