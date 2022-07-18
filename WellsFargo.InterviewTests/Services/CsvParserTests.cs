using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellsFargo.Interview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Services.Tests
{
    [TestClass()]
    public class CsvParserTests
    {
        CsvLineParser _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new CsvLineParser();
        }

        [TestMethod]
        [DataRow("a,b", "a", "b")]
        [DataRow("10,134,20.4213,AAA,BUY", "10", "134", "20.4213", "AAA", "BUY")]
        [DataRow("", "")]
        public void Parse_ProvidedLine_ReturnsArray(string line, params string[] expected)
        {
            var actual = _sut.Parse(line);

            Assert.AreEqual(expected.Length, actual.Length);
            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
