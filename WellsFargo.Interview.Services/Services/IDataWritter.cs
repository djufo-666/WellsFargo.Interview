namespace WellsFargo.Interview.Services
{
    public interface IDataWritter
    {
        Task WriteAsync(string sourceName, string headers, IEnumerable<string> lines);
    }
}