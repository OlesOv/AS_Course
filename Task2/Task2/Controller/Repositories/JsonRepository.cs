using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Model;

namespace Task2.Controller
{
    abstract class JsonRepository<T> : IRepository<T> where T : MyData
    {
        protected string path;
        private Dictionary<int, T> Data;
        public int Count() => Data.Count();
        public JsonRepository(string path)
        {
            this.path = path;
            if (File.Exists(path))
            {
                Data = GetList();
            }
            else
            {
                Data = new Dictionary<int, T>();
            }
        }
        public Dictionary<int, T> GetList()
        {
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(jsonString).ToDictionary(p => p.ID);
        }

        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(Data.Values);
            File.WriteAllText(path, jsonString);
        }
        public T GetElement(int id)
        {
            if (!Data.ContainsKey(id)) return null;
            return Data[id];
        }
        public void Update(T item)
        {
            Data[item.ID] = item;
        }
        public void Delete(int id)
        {
            Data.Remove(id);
        }
        public void Add(T item) {
            Data.Add(item.ID, item);
        }
        public void Dispose() { }
        public T this[int ID]
        {
            get
            {
                return Data[ID];
            }
            set
            {
                Data[ID] = value;
            }
        }
    }
}
