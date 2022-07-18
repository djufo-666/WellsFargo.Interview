using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Services
{
    public class FileDataReader : IDisposable, IDataReader
    {
        CancellationTokenSource _cts;
        public FileDataReader()
        {
            _cts = new CancellationTokenSource();
        }
        public async IAsyncEnumerable<string> ReadLinesAsync(string fileName)
        {
            using StreamReader reader = new FileInfo(fileName).OpenText();
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (_cts.IsCancellationRequested) yield break;
                yield return line;
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}
