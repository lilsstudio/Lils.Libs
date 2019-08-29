using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lils.DataLib.Csv
{
    public static class CsvConvert
    {
        /// <summary>
        /// 反序列化字符串，有列头，仅支持字符串格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> DeserializeCollection<T>(string value) where T : new()
        {
            var lines = value.Split('\n')
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();
            if (lines.Count < 1)
            {
                return null;
            }
            var firstLine = lines.First();

            var headers = firstLine.Split(',').ToList();
            var properties = typeof(T).GetProperties();
            var matches = headers.Join(properties,
                header => header, property => property.Name,
                (header, property) => new
                {
                    Property = property,
                    Index = headers.IndexOf(header),
                });

            var objects = new List<T>();

            for (int i = 1; i < lines.Count; i++)
            {
                var obj = new T();
                var line = lines[i];
                var parts = line.Split(',');
                foreach (var match in matches)
                {
                    if (parts.Count() > match.Index)
                    {
                        match.Property.SetValue(obj, parts[match.Index]);
                    }
                }
                objects.Add(obj);
            }
            return objects;
        }
    }
}
