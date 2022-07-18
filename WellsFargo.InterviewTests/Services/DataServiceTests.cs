using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellsFargo.Interview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Interview.Models;
using Autofac.Extras.Moq;
using WellsFargo.InterviewTests;
using Moq.Protected;
using Moq;

namespace WellsFargo.Interview.Services.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        AutoMock mock;

        DataService<Portfolio> _sut;

        [TestInitialize]
        public void Initialize()
        {
            mock = AutoMock.GetLoose();

            _sut = mock.Create<DataService<Portfolio>>();
            
        }
        [TestCleanup]
        public void Cleanup()
        {
            mock?.Dispose();
        }

        [TestMethod()]
        public async Task ReadAsync_WillReturn_TypedList()
        {
            // Arrange
            var set = TestData.GetSet();
            var sourceName = @"////some/path/somefile.maybe";
            
            mock.Mock<ILineParser>()
                .Setup(x => x.Parse(set.headersLine))
                .Returns(set.headers)
                .Verifiable();
            mock.Mock<ILineParser>()
                .Setup(x => x.Parse(set.argsLine))
                .Returns(set.args)
                .Verifiable();

            mock.Mock<IDataReader>()
                .Setup(x => x.ReadLinesAsync(sourceName))
                .Returns(TestData.GetStringsAsyncEnumerable())
                .Verifiable();

            mock.Mock<IArgsTypeMapper<Portfolio>>()
                .Setup(x => x.ToType(set.headers, set.args))
                .Returns(set.model)
                .Verifiable();

            // Act
            var actual = await _sut.ReadAsync(sourceName);

            // Assert
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(TestData.Compare(set.model, actual[0]));
        }


        [TestMethod]
        public async Task WriteAsync_WillWrite_HeadersAndPropertiesFormatted()
        {
            // Arrange
            var set = TestData.GetSet();
            var sourceName = @"////some/path/somefile.maybe";

            mock.Mock<IArgsTypeMapper<Portfolio>>()
                .SetupGet(x => x.Headers)
                .Returns(set.headers)
                .Verifiable();
            mock.Mock<IArgsTypeMapper<Portfolio>>()
                .Setup(x => x.ToArgs(set.model))
                .Returns(set.args)
                .Verifiable();

            mock.Mock<ILineFormatter>()
                .Setup(x => x.Format(',', set.headers))
                .Returns(set.headersLine)
                .Verifiable();
            mock.Mock<ILineFormatter>()
                .Setup(x => x.Format(',', set.args))
                .Returns(set.argsLine)
                .Verifiable();

            // Act
            await _sut.WriteAsync(',', sourceName, new [] {set.model});

            // Assert
            mock.Mock<IDataWritter>()
               .Verify(x => x.WriteAsync(
                   sourceName,
                   set.headersLine,
                   It.Is<IEnumerable<string>>(x =>
                       x.Count() == 1
                       && x.FirstOrDefault(xv => xv == set.argsLine) != null
                   )
               ));
        }
    }
}
