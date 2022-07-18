using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellsFargo.Interview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using WellsFargo.Interview.Models;
using WellsFargo.InterviewTests;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Autofac;

namespace WellsFargo.Interview.Services.Tests
{
    [TestClass()]
    public class OmsServiceTests
    {
        AutoMock mock;

        readonly string SourceName = @"////some/path/somefile.maybe";
        readonly string SourceNameSecurities = @"////some/path/somefile-sec.maybe";
        readonly string SourceNamePortfolios = @"////some/path/somefile-port.maybe";
        readonly string SourceNameTransactions = @"////some/path/somefile-tr.maybe";
        OmsService _sut;

        [TestInitialize]
        public void Initialize()
        {
            mock = AutoMock.GetLoose();

            mock.Mock<IDataService<Security>>()
                .Setup(x => x.ReadAsync(SourceNameSecurities))
                .ReturnsAsync(TestData.Securities)
                .Verifiable();

            mock.Mock<IDataService<Portfolio>>()
                .Setup(x => x.ReadAsync(SourceNamePortfolios))
                .ReturnsAsync(TestData.Portfolios)
                .Verifiable();

            mock.Mock<IDataService<Transaction>>()
                .Setup(x => x.ReadAsync(SourceNameTransactions))
                .ReturnsAsync(TestData.Transactions)
                .Verifiable();

            _sut = mock.Create<OmsService>(new NamedParameter("loggerFactory", new NullLoggerFactory()));
        }
        [TestCleanup]
        public void Cleanup()
        {
            mock?.Dispose();
        }


        [TestMethod()]
        public async Task ReadDataAsync_WillCallReadData_FromCorrectFiles()
        {
            await _sut.ReadDataAsync(SourceNameSecurities, SourceNamePortfolios, SourceNameTransactions);

            mock.Mock<IDataService<Security>>().VerifyAll();
            mock.Mock<IDataService<Portfolio>>().VerifyAll();
            mock.Mock<IDataService<Transaction>>().VerifyAll();
        }

        [TestMethod()]
        public async Task WriteOmsAaaAsyncTest()
        {
            // Arrange
            await _sut.ReadDataAsync(SourceNameSecurities, SourceNamePortfolios, SourceNameTransactions);

            Func<IEnumerable<OMSTypeAAA>, bool> validateQ = (it) =>
            {
                var result = it.Count() == 4
                    && it.ElementAt(0).ISIN == "ISIN11111111"
                    && it.ElementAt(1).PortfolioCode == "p1"
                    && it.ElementAt(2).TransactionType == "SELL"
                    && it.ElementAt(3).Nominal == 4.342552M
                    ;
                return result;
            };

            // Act
            await _sut.WriteOmsAaaAsync(SourceName);


            // Assert
            mock.Mock<IDataService<OMSTypeAAA>>()
                .Verify(x => x.WriteAsync(
                    ',',
                    SourceName,
                    It.Is<IEnumerable<OMSTypeAAA>>(it => validateQ(it))
                ));
        }

        [TestMethod()]
        public async Task WriteOmsBbbAsyncTest()
        {
            // Arrange
            await _sut.ReadDataAsync(SourceNameSecurities, SourceNamePortfolios, SourceNameTransactions);

            Func<IEnumerable<OMSTypeBBB>, bool> validateQ = (it) =>
            {
                var result = it.Count() == 1
                    && it.ElementAt(0).Cusip == "CUSIP0002"
                    && it.ElementAt(0).PortfolioCode == "p2"
                    && it.ElementAt(0).TransactionType == "SELL"
                    && it.ElementAt(0).Nominal == 20
                    ;
                return result;
            };

            // Act
            await _sut.WriteOmsBbbAsync(SourceName);

            // Assert
            mock.Mock<IDataService<OMSTypeBBB>>()
                .Verify(x => x.WriteAsync(
                    '|',
                    SourceName,
                    It.Is<IEnumerable<OMSTypeBBB>>(it => validateQ(it))                    
                ));
        }

        [TestMethod()]
        public async Task WriteOmsCccAsyncTest()
        {
            // Arrange
            await _sut.ReadDataAsync(SourceNameSecurities, SourceNamePortfolios, SourceNameTransactions);

            Func<IEnumerable<OMSTypeCCC>, bool> validateQ = (it) =>
            {
                var result = it.Count() == 1
                    && it.ElementAt(0).Ticker == "s1"
                    && it.ElementAt(0).PortfolioCode == "p2"
                    && it.ElementAt(0).TransactionType == "B"
                    && it.ElementAt(0).Nominal == 30
                    ;
                return result;
            };

            // Act
            await _sut.WriteOmsCccAsync(SourceName);

            // Assert
            mock.Mock<IDataService<OMSTypeCCC>>()
                .Verify(x => x.WriteAsync(
                    ',',
                    SourceName,
                    It.Is<IEnumerable<OMSTypeCCC>>(it => validateQ(it))
                ));
        }
    }
}