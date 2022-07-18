using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellsFargo.Interview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellsFargo.Interview.Models;
using WellsFargo.InterviewTests;

namespace WellsFargo.Interview.Services.Tests
{
    [TestClass()]
    public class ArgsTypeMapperTests
    {
        ArgsTypeMapper<Portfolio> _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new ArgsTypeMapper<Portfolio>();
        }

        [TestMethod()]
        public void Headers_Generated_Based_On_Properties()
        {
            var expected = new[] { "PortfolioId", "PortfolioCode" };

            var actual = _sut.Headers;

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void HeadersAndArgs_Provided_TypedObject_WillBeMapped()
        {
            // Arrange
            var it = TestData.GetSet();

            // Act
            var actual = _sut.ToType(it.headers, it.args);

            // Assert
            Assert.IsTrue(TestData.Compare(it.model, actual));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void Headers_Provided_HaveNoMatchingProperties_WillThrowException()
        {
            // Arrange
            var headers = new[] { "XXX", "PortfolioCode" };
            var args = new[] { "11", "Code11" };

            // Act
            _sut.ToType(headers, args);
        }

        [TestMethod]
        public void Object_Provided_PropertyValuesResults()
        {
            var it = TestData.GetSet();

            var actual = _sut.ToArgs(it.model);

            Assert.IsTrue(it.args.SequenceEqual(actual));
        }
    }
}