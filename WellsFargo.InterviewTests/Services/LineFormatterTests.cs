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
    public class LineFormatterTests
    {
        LineFormatter _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new LineFormatter();
        }

        [TestMethod]
        [DataRow(',', "a,b", "a", "b")]
        [DataRow(',', "10,134,20.4213,AAA,BUY", "10", "134", "20.4213", "AAA", "BUY")]
        [DataRow('|', "10|134|20.4213|AAA|BUY", "10", "134", "20.4213", "AAA", "BUY")]
        [DataRow(',', "", "")]
        public void Write_ProvidedArray_ReturnsLine(char delimiter, string expected, params string[] args)
        {
            var actual = _sut.Format(delimiter, args);

            Assert.AreEqual(expected, actual);
        }
    }
}
