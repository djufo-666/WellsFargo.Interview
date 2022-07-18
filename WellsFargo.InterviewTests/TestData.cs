using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Interview.Models;

namespace WellsFargo.InterviewTests
{
    internal static class TestData
    {
        static (string[] headers, string headersLine, string[] args, string argsLine, Portfolio model) _set =
            (
                headers: new[] { "PortfolioId", "PortfolioCode" },
                headersLine: "PortfolioId,PortfolioCode",
                args: new[] { "11", "Code11" },
                argsLine: "11,Code11",
                model: new() { PortfolioId = 11, PortfolioCode = "Code11" }
            );

        public static (string[] headers, string headersLine, string[] args, string argsLine, Portfolio model) GetSet()
        {
            return _set;
        }
        public static bool Compare(Portfolio expected, Portfolio actual)
        {
            return expected is not null && actual is not null
                && !(expected is null || actual is null)
                && expected.PortfolioId == actual.PortfolioId
                && expected.PortfolioCode == actual.PortfolioCode;
        }

        public static async IAsyncEnumerable<string> GetStringsAsyncEnumerable()
        {
            yield return await Task.FromResult(_set.headersLine);
            yield return await Task.FromResult(_set.argsLine);
        }

        public static readonly List<Transaction> Transactions = new()
        {
            new () { SecurityId = 1, PortfolioId = 1,Nominal = 10, OMS = "AAA", TransactionType = "BUY" },
            new () { SecurityId = 1, PortfolioId = 1,Nominal = 2.3M, OMS = "AAA", TransactionType = "BUY" },
            new () { SecurityId = 1, PortfolioId = 1,Nominal = 2, OMS = "AAA", TransactionType = "SELL" },
            new () { SecurityId = 2, PortfolioId = 2,Nominal = 4.342552M, OMS = "AAA", TransactionType = "BUY" },
            new () { SecurityId = 2, PortfolioId = 2,Nominal = 20, OMS = "BBB", TransactionType = "SELL" },
            new () { SecurityId = 1, PortfolioId = 2,Nominal = 30, OMS = "CCC", TransactionType = "BUY" },
        };
        public static readonly List<Security> Securities = new()
        {
            new () { SecurityId = 1, ISIN = "ISIN11111111", Ticker = "s1", Cusip = "CUSIP0001" },
            new () { SecurityId = 2, ISIN = "ISIN22222222", Ticker = "s2", Cusip = "CUSIP0002" },
        };
        public static readonly List<Portfolio> Portfolios = new()
        {
            new () { PortfolioId = 1, PortfolioCode = "p1" },
            new () { PortfolioId = 2, PortfolioCode = "p2" },
        };
    }
}
