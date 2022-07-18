using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Models
{
    /// <summary>
    /// Portfolio information is a comma separated file in the following format:
    /// </summary>
    public class Portfolio
    {
        /// <summary>
        /// Unique portfolio identifier
        /// </summary>
        public int PortfolioId { get; set; }
        public string PortfolioCode { get; set; }
    }
}
