using MyTiming.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyTiming.Services
{
    /// <summary>
    /// Сервис работы с записями в локальной БД SQLite
    /// </summary>
    abstract public class SQLiteService<T> where T : class, IModel, new()     {
        readonly SQLiteAsyncConnection _database;
        readonly string _dbPath;

        public SQLiteService(string dbName)
        {
            _dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
            _database = new SQLiteAsyncConnection(_dbPath);
            _database.CreateTableAsync<T>().Wait();
        }

        public Task<List<T>> GetRecordsAsync()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<T> GetRecordAsync(string id)
        {
            return _database.Table<T>()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecordAsync(T record)
        {
            if (!string.IsNullOrEmpty(record.Id))
            {
                return _database.UpdateAsync(record);
            }
            else
            {
                return _database.InsertAsync(record)
;            }
        }

        public Task<int> DeleteRecordAsync(T record)
        {
            return _database.DeleteAsync(record);
        }
    }
}
