using System.IO;

namespace BasicTools
{
    public interface IBasicPorterArgs
    {
        string Extension { get; }
    }

    public class BasicPorterArgs : IBasicPorterArgs
    {
        public string Extension { get; set; }

        public static IBasicPorterArgs FromExtension(string filePath)
        {
            return new BasicPorterArgs() { Extension = Path.GetExtension(filePath) };
        }
    }
}