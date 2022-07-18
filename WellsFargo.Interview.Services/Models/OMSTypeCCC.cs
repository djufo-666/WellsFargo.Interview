using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Models
{
    /// <summary>
    /// OMS Type - CCC
    /// Expected file format for CCC OMS type is a comma delimited file with following fields with no header.
    /// Expected extension for the file is “.ccc”
    /// </summary>
    public class OMSTypeCCC
    {
        /// <summary>
        /// Lookup from portfolio file
        /// </summary>
        public string PortfolioCode { get; set; }
        /// <summary>
        /// Lookup from security file
        /// </summary>
        public string Ticker { get; set; }
        /// <summary>
        /// Quantity of contracts to be purchased
        /// </summary>
        public decimal Nominal { get; set; }
        /// <summary>
        /// B or S
        /// </summary>
        public string TransactionType { get; set; }
    }
}
