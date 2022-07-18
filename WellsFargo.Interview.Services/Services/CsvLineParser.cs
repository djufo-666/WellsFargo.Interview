using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Services
{
    public class CsvLineParser : ILineParser
    {
        Regex _regex = new Regex(",");

        public string[] Parse(string line)
        {
            return _regex.Split(line);
        }
    }
}
