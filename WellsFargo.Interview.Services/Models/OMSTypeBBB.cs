using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Models
{
    /// <summary>
    /// OMS Type - BBB
    /// Expected file format for BBB OMS type is a pipe delimited file with following fields.
    /// Expected extension for this file is “.bbb”
    /// </summary>
    public class OMSTypeBBB
    {
        /// <summary>
        /// Lookup from security file
        /// </summary>
        public string Cusip { get; set; }
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
