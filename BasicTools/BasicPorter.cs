namespace BasicTools
{
    public interface IBasicPorter : IBasicPorterArgs
    {
        string Name { get; }

        object Import(byte[] data);
        byte[] Export(object source);

        bool IsMatch(IBasicPorterArgs args);
    }
}
