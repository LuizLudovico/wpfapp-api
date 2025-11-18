using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WpfApp.Services
{
    public class JsonDataService<T> : IDataService<T> where T : class
    {
        private readonly string _filePath;
        private List<T> _data;

        public JsonDataService(string fileName)
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            _filePath = Path.Combine(dataFolder, fileName);
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    _data = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
                }
                catch
                {
                    _data = new List<T>();
                }
            }
            else
            {
                _data = new List<T>();
            }
        }

        public List<T> GetAll()
        {
            return _data.ToList();
        }

        public T GetById(Guid id)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null) return null;

            return _data.FirstOrDefault(item =>
            {
                var itemId = property.GetValue(item);
                return itemId != null && itemId.Equals(id);
            });
        }

        public void Add(T entity)
        {
            _data.Add(entity);
        }

        public void Update(T entity)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null) return;

            var entityId = property.GetValue(entity) as Guid?;
            if (entityId == null) return;

            var index = _data.FindIndex(item =>
            {
                var itemId = property.GetValue(item);
                return itemId != null && itemId.Equals(entityId);
            });

            if (index >= 0)
            {
                _data[index] = entity;
            }
        }

        public void Delete(Guid id)
        {
            var property = typeof(T).GetProperty("Id");
            if (property == null) return;

            var item = _data.FirstOrDefault(i =>
            {
                var itemId = property.GetValue(i);
                return itemId != null && itemId.Equals(id);
            });

            if (item != null)
            {
                _data.Remove(item);
            }
        }

        public void SaveChanges()
        {
            string json = JsonConvert.SerializeObject(_data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}

