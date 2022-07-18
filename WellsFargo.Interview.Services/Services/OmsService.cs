using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Interview.Models;

namespace WellsFargo.Interview.Services
{
    /// <summary>
    /// Oms Service is main app service that reads data and generates oms results
    /// </summary>
    public class OmsService : IOmsService
    {
        public const bool GROUP_TRANSACTIONS = false;

        private readonly ILogger<OmsService> _logger;
        private readonly IDataService<Transaction> _transactionService;
        private readonly IDataService<Portfolio> _portfolioService;
        private readonly IDataService<Security> _securityService;
        private readonly IDataService<OMSTypeAAA> _omsAaaService;
        private readonly IDataService<OMSTypeBBB> _omsBbbService;
        private readonly IDataService<OMSTypeCCC> _omsCccService;

        private List<Transaction> _transactions;
        private List<Portfolio> _portfolios;
        private List<Security> _securities;

        public OmsService(
            ILoggerFactory loggerFactory,
            IDataService<Transaction> transactionService,
            IDataService<Portfolio> portfolioService,
            IDataService<Security> securityService,
            IDataService<OMSTypeAAA> omsAaaService,
            IDataService<OMSTypeBBB> omsBbbService,
            IDataService<OMSTypeCCC> omsCccService)
        {
            _logger = loggerFactory.CreateLogger<OmsService>();

            _transactionService = transactionService;
            _portfolioService = portfolioService;
            _securityService = securityService;
            _omsAaaService = omsAaaService;
            _omsBbbService = omsBbbService;
            _omsCccService = omsCccService;
        }

        public async Task ReadDataAsync(string securitiesSourceName, string portfolioSourceName, string transactionsSourceName)
        {
            _logger.LogInformation("$Reading data from: {0}, {1}, {2}", securitiesSourceName, portfolioSourceName, transactionsSourceName);

            _securities = await _securityService.ReadAsync(securitiesSourceName); // new[] { "SecurityId", "ISIN", "Ticker", "Cusip" }
            _portfolios = await _portfolioService.ReadAsync(portfolioSourceName); // new[] { "PortfolioId", "PortfolioCode" }
            _transactions = await _transactionService.ReadAsync(transactionsSourceName); // new[] { "SecurityId", "PortfolioId", "Nominal", "OMS", "TransactionType" }
        }

        /// <summary>
        /// Do we need to provide grouped transactions and acumulated Nominal ??????
        /// </summary>
        /// <returns>Returns grouped transactions with accumulated nominal</returns>
        private IEnumerable<Transaction> GroupTransactions()
        {
            return this._transactions
                .GroupBy(t => (t.PortfolioId, t.SecurityId, t.TransactionType, t.OMS))
                .Select(g => new Transaction
                {
                    PortfolioId = g.Key.PortfolioId,
                    SecurityId = g.Key.SecurityId,
                    TransactionType = g.Key.TransactionType,
                    OMS = g.Key.OMS,
                    Nominal = g.Select(x => x.Nominal).Sum()
                });
        }
        private IEnumerable<Transaction> GetTransactions()
        {
            return GROUP_TRANSACTIONS 
                ? GroupTransactions()
                : _transactions;
        }

        public async Task WriteOmsAaaAsync(string sourceName)
        {
            var q = from transaction in GetTransactions()
                    join portfolio in _portfolios on transaction.PortfolioId equals portfolio.PortfolioId
                    join security in _securities on transaction.SecurityId equals security.SecurityId
                    where transaction.OMS == "AAA"
                    select new OMSTypeAAA
                    {
                        ISIN = security.ISIN,
                        Nominal = transaction.Nominal,
                        PortfolioCode = portfolio.PortfolioCode,
                        TransactionType = transaction.TransactionType
                    };

            await _omsAaaService.WriteAsync(',', sourceName, q);
        }

        public async Task WriteOmsBbbAsync(string sourceName)
        {
            var q = from transaction in GetTransactions()
                    join portfolio in _portfolios on transaction.PortfolioId equals portfolio.PortfolioId
                    join security in _securities on transaction.SecurityId equals security.SecurityId
                    where transaction.OMS == "BBB"
                    select new OMSTypeBBB
                    {
                        Cusip = security.Cusip,
                        Nominal = transaction.Nominal,
                        PortfolioCode = portfolio.PortfolioCode,
                        TransactionType = transaction.TransactionType
                    };

            await _omsBbbService.WriteAsync('|', sourceName, q);
        }
        public async Task WriteOmsCccAsync(string sourceName)
        {
            var q = from transaction in GetTransactions()
                    join portfolio in _portfolios on transaction.PortfolioId equals portfolio.PortfolioId
                    join security in _securities on transaction.SecurityId equals security.SecurityId
                    where transaction.OMS == "CCC"
                    select new OMSTypeCCC
                    {
                        Ticker = security.Ticker,
                        Nominal = transaction.Nominal,
                        PortfolioCode = portfolio.PortfolioCode,
                        TransactionType = 
                            transaction.TransactionType == "BUY"  ? "B"
                            : transaction.TransactionType == "SEll" ? "S" 
                            : transaction.TransactionType
                    };

            await _omsCccService.WriteAsync(',', sourceName, q);
        }
    }
}
