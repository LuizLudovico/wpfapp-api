using System;
using System.Collections.Generic;

namespace WpfApp.Services
{
    public interface IDataService<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void SaveChanges();
    }
}

