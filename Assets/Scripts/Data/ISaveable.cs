namespace GameJamToolkit.Data
{
    /// <summary>Any object that wants to be serialized by <see cref="SaveManager"/> implements this interface.</summary>
    public interface ISaveable
    {
        string SaveKey { get; }
        string SerializeState();
        void DeserializeState(string state);
    }
}
