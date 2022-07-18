namespace WellsFargo.Interview.Services
{
    public interface IDataReader
    {
        void Dispose();
        IAsyncEnumerable<string> ReadLinesAsync(string sourceName);
    }
}