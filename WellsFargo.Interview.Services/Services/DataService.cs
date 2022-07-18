using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WellsFargo.Interview.Services
{
    public class DataService<TType> : IDataService<TType> where TType : new()
    {
        private readonly IDataReader _dataReader;
        private readonly IDataWritter _dataWritter;
        private readonly ILineParser _parser;
        private readonly ILineFormatter _formatter;
        private readonly IArgsTypeMapper<TType> _argsTypeMapper;

        public DataService(
            ILineParser parser,
            ILineFormatter formatter,
            IDataReader dataReader,
            IDataWritter dataWritter,
            IArgsTypeMapper<TType> argsTypeMapper)
        {
            _parser = parser;
            _formatter = formatter;
            _dataReader = dataReader;
            _dataWritter = dataWritter;
            _argsTypeMapper = argsTypeMapper;
        }

        public async Task<List<TType>> ReadAsync(string sourceName)
        {
            List<TType> list = new List<TType>();

            var enumerator = _dataReader.ReadLinesAsync(sourceName).GetAsyncEnumerator();
            bool next = await enumerator.MoveNextAsync();
            string line = enumerator.Current;
            string[] headers = _parser.Parse(line);
            while(await enumerator.MoveNextAsync())
            {
                line = enumerator.Current;
                var args = _parser.Parse(line);
                var value = _argsTypeMapper.ToType(headers, args);
                list.Add(value);
            }

            return list;
        }
        public async Task WriteAsync(char delimiter, string sourceName, IEnumerable<TType> list)
        {
            string headerLine = _formatter.Format(delimiter, _argsTypeMapper.Headers);

            var q = list.Select(value => _argsTypeMapper.ToArgs(value))
                .Select(args => _formatter.Format(delimiter, args))
                ;

            await _dataWritter.WriteAsync(sourceName, headerLine, q);
        }
    }
}
