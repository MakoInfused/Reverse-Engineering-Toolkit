using System;

namespace BasicTools
{
    public class BasicLoggers : BasicSingleton<BasicLoggers>
    {
        public ILogger Logger { get; } = new BasicReleaseLogger();
    }

    public class BasicReleaseLogger : ILogger
    {
        public void Flush()
        {
            
        }

        public void WriteLine(string line)
        {
            
        }

        public void WriteText(string text)
        {
            
        }
    }


    public class BasicConsoleLogger : ILogger
    {
        public void Flush()
        {
            Console.Clear();
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void WriteText(string text)
        {
            Console.Write(text);
        }
    }
}
