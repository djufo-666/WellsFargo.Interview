namespace WellsFargo.Interview.Services
{
    public interface ILineFormatter
    {
        string Format(char delimiter, string[] args);
    }
}