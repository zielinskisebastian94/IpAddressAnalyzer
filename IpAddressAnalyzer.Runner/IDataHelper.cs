namespace IpAddressAnalyzer.Runner
{
    public interface IDataHelper
    {
        void SerializeData(IDataAnalyzer analyzer);
        void Subscribe(IDataAnalyzer d);
    }
}