namespace GameJamToolkit.Data
{
    /// <summary>Localizable key. String wrapper to expose a specific field.</summary>
    [System.Serializable]
    public struct LocalizationKey
    {
        public string Key;
        public LocalizationKey(string key) { Key = key; }
    }
}
