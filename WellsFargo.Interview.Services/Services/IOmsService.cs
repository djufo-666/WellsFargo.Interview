namespace WellsFargo.Interview.Services
{
    public interface IOmsService
    {
        Task ReadDataAsync(string securitiesSourceName, string portfolioSourceName, string transactionsSourceName);
        Task WriteOmsAaaAsync(string sourceName);
        Task WriteOmsBbbAsync(string sourceName);
        Task WriteOmsCccAsync(string sourceName);
    }
}