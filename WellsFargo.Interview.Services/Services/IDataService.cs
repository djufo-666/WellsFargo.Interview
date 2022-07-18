namespace WellsFargo.Interview.Services
{
    public interface IDataService<TType> where TType : new()
    {
        Task<List<TType>> ReadAsync(string sourceName);
        Task WriteAsync(char delimiter, string sourceName, IEnumerable<TType> list);
    }
}