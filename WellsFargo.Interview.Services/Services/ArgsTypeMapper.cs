using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Interview.Services
{
    public class ArgsTypeMapper<TType> : IArgsTypeMapper<TType> where TType : new()
    {
        Dictionary<string, (PropertyInfo pi, Func<string, object> convert)> _mapSet;

        public string[] Headers { get { return _mapSet.Keys.ToArray(); } }

        public ArgsTypeMapper()
        {
            Func<string, object> convert(Type type) =>
                type == typeof(string) ? (str) => str
                : type == typeof(int) ? (str) => Convert.ToInt32(str)
                : type == typeof(decimal) ? (str) => Convert.ToDecimal(str)
                : (str) => throw new NotSupportedException()
                ;


            var props = typeof(TType).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            _mapSet = props.ToDictionary(pi => pi.Name, pi => (pi: pi, convert: convert(pi.PropertyType)), StringComparer.CurrentCultureIgnoreCase);
        }

        public TType ToType(string[] headers, string[] args)
        {
            TType obj = new TType();
            for (int i = 0; i < headers.Length; i++)
            {
                var header = headers[i];
                var arg = args[i];
                var map = _mapSet[header];
                var value = map.convert(arg);
                map.pi.SetValue(obj, value);
            }
            return obj;
        }

        public string[] ToArgs(TType obj)
        {
            var result = _mapSet
                .Select(kv => kv.Value.pi.GetValue(obj))
                .Select(v => v != null ? v.ToString() : "")
                .ToArray();

            return result;
        }
    }
}
