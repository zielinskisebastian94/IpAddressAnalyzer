namespace IpAddressAnalyzer.Runner
{
    public interface IDataHelper
    {
         DataSerializer SerializeData(IDataAnalyzer analyzer);
        void Subscribe(IDataAnalyzer d);
    }
}