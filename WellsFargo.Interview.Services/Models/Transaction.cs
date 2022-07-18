using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Models
{
    /// <summary>
    /// Portfolio managers send transactions in a comma delimited CSV file which has the following definition
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Unique security identifier 
        /// </summary>
        public int SecurityId { get; set; }
        /// <summary>
        /// Unique portfolio Id
        /// </summary>
        public int PortfolioId { get; set; }
        /// <summary>
        /// Quantity of contracts to be purchased
        /// </summary>
        public decimal Nominal { get; set; }
        /// <summary>
        /// Possible values AAA, BBB and CCC
        /// </summary>
        public string OMS { get; set; }
        /// <summary>
        /// Possible values BUY or SELL
        /// </summary>
        public string TransactionType { get; set; }
    }
}
