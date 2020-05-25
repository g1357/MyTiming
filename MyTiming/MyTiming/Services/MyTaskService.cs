using MyTiming.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyTiming.Services
{
    public class MyTaskService : SQLiteService<MyTask>
    {
        //var database = new MyTaskService(
        //    Path.Combine(
        //        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        //        "Notes.db3"));

        const string _dbName = @"MyTask.db3";
        public MyTaskService(string dbName) : base(dbName)
        {
        }

        public MyTaskService() : base(_dbName)
        { }
        
    }
}
