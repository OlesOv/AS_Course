using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
    class MyDict<TValue> : Dictionary<int, TValue>
    {
        public MyDict() : base() { }
        public MyDict(string path) : base()
        {
            Path = path;
        }
        public MyDict(Dictionary<int, TValue> dict) : base(dict) { }
        public virtual string Path { get; set; }
        public void UpdateJSON()
        {
            string jsonString = JsonSerializer.Serialize(Values);
            File.WriteAllText(Path, jsonString);
        }
    }
}
