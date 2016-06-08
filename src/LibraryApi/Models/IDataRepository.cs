using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApi.Models
{
    public interface IDataRepository<T> where T : class
    {
        void Add(T item);

        IEnumerable<T> GetAll();

        T Find(int id);

        T Remove(int id);

        void Update(T item);
    }
}
