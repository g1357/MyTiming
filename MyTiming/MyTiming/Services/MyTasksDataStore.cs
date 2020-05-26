using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTiming.Models;

namespace MyTiming.Services
{
    public class MyTasksDataStore : IDataStore<MyTask>
    {
        readonly List<MyTask> items;

        public MyTasksDataStore()
        {
            items = new List<MyTask>()
            {
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Разработка ПО", Description="Разработка программного обеспечения.", CategoryId = "2" },
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Написание книг.", Description="Написание технических книг по работе.", CategoryId = "2" },
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Проект GBD -Interim Manager", Description="Работы над проектом по Интерим менеджменту.", CategoryId = "2" },
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Сон", Description="Ночной и дневной сон.", CategoryId = "1" },
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Приём пищи", Description="Приём пищи.", CategoryId = "3" },
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Занятие финнесом", Description="Занятие фитнесом в зале наверху.", CategoryId = "4" },
                new MyTask { Id = Guid.NewGuid().ToString(), Name = "Поездки на работу и по делам", Description="Поездки на работку и по делам.", CategoryId = "5" }
            };
        }

        public async Task<bool> AddItemAsync(MyTask item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MyTask item)
        {
            var oldItem = items.Where((MyTask arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((MyTask arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MyTask> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MyTask>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}