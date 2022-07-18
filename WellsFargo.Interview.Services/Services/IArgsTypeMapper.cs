namespace WellsFargo.Interview.Services
{
    public interface IArgsTypeMapper<TType> where TType : new()
    {
        string[] Headers { get; }
        string[] ToArgs(TType obj);
        TType ToType(string[] headers, string[] args);
    }
}