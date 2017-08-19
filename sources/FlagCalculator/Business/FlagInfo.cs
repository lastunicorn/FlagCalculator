namespace DustInTheWind.FlagCalculator.Business
{
    internal class FlagInfo
    {
        public ulong Value { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Value}";
        }
    }
}