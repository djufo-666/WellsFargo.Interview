using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Models
{
    /// <summary>
    /// OMS Type - AAA
    /// Expected file format for AAA OMS type is a comma separated CSV file with following fields.
    /// Expected extension for this file is “.aaa”
    /// </summary>
public class OMSTypeAAA
    {
        /// <summary>
        /// Lookup from security file
        /// </summary>
        public string ISIN { get; set; }
        /// <summary>
        /// Lookup from portfolio file
        /// </summary>
        public string PortfolioCode { get; set; }
        /// <summary>
        /// Quantity of contracts
        /// </summary>
        public decimal Nominal { get; set; }
        /// <summary>
        /// BUY or SELL
        /// </summary>
        public string TransactionType { get; set; } 
    }
}
