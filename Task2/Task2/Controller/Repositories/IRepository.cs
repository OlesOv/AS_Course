using System;
using System.Collections.Generic;
using Model;

namespace Task2
{
    interface IRepository<T>: IDisposable where T : MyData
    {
        Dictionary<int, T> GetList();
        T GetElement(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
        int Count();
        T this[int ID] { get; set; }
    }
}
