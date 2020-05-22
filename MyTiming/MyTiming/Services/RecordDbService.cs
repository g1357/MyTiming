using MyTiming.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTiming.Services
{
    /// <summary>
    /// Сервис работы с записями в локальной БД SQLite
    /// </summary>
    public class RecordDbService
    {
        readonly SQLiteAsyncConnection _database;
        readonly string _dbPath;

        public RecordDbService(string dbPath)
        {
            _dbPath = dbPath;
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Record>().Wait();
        }

        public Task<List<Record>> GetRecordsAsync()
        {
            return _database.Table<Record>().ToListAsync();
        }

        public Task<Record> GetRecordAsync(int id)
        {
            return _database.Table<Record>()
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecordAsync(Record record)
        {
            if (record.Id != 0)
            {
                return _database.UpdateAsync(record);
            }
            else
            {
                return _database.InsertAsync(record)
;            }
        }

        public Task<int> DeleteRecordAsync(Record record)
        {
            return _database.DeleteAsync(record);
        }
    }
}
