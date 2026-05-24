namespace BasicTools
{
    public interface IBasicDefinition<T>
    {
        T Definition { get; set; }

        void FromDefinition(IBasicDefinition<T> otherDefinition);
        byte[] ToBytes();
        IBasicDefinition<T> Clone();
    }
}