using System;
using System.Collections.Generic;
using System.Text;

namespace MyTiming.Models
{
    /// <summary>
    /// Запись 
    /// </summary>
    public class Record : IModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateTime { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string IconFile { get; set; }
        public string Color { get; set; }
    }
}
