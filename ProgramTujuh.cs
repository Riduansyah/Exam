using System;
using System.Collections.Generic;

namespace MemoryLeakExample
{
    class Cache
    {
        private static Dictionary<int, SampleData> _cache = new Dictionary<int, SampleData>();

        public static void Add(int key, object value)
        {
            _cache.Add(key, new SampleData { Data = value, TimeExpired = DateTime.Now + TimeSpan.FromMinutes(5)});
        }

        public static object Get(int key)
        {
            if (!_cache.ContainsKey(key)) return null;

            SampleData item = _cache[key];
            if (item.TimeExpired < DateTime.Now)
            {
                _cache.Remove(key);
                return null;
            }

            return _cache[key];
        }

        private class SampleData
        {
            public object Data { get; set; }
            public DateTime TimeExpired { get; set; }
        }
    }

    class ProgramTujuh
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000000; i++)
            {
                Cache.Add(i, new object());
            }

            Console.WriteLine("Cache populated");
            Console.ReadLine();
        }
    }
}
