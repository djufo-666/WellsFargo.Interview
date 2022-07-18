using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Models
{
    /// <summary>
    /// In addition, you have been provided with metadata files to lookup security and portfolio information.
    /// Security information is a comma separated file in the following format:
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Unique security identifier
        /// </summary>
        public int SecurityId { get; set; }
        public string ISIN { get; set; }
        public string Ticker { get; set; }
        public string Cusip { get; set; }
    }
}
