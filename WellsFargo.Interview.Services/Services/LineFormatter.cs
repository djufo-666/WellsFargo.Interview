using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Services
{
    public class LineFormatter : ILineFormatter
    {
        public string Format(char delimiter, string[] args)
        {
            return string.Join(delimiter, args);
        }
    }
}
