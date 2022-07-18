using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Services
{
    public class FileDataWritter : IDataWritter
    {
        public async Task WriteAsync(
            string fileName,
            string headers,
            IEnumerable<string> lines)
        {
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                // throw new Exception(string.Format($"File already exists: {0}", fileName));
                fi.Delete();
            }

            using StreamWriter writter = fi.CreateText();
            writter.WriteLine(headers);
            foreach (string line in lines)
            {
                writter.WriteLine(line);
            }
            await writter.FlushAsync();
        }
    }
}
