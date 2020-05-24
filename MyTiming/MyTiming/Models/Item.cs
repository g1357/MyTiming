using System;

namespace MyTiming.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeSpended { get; set; }
        public TimeSpan TotalTimeSpended { get; set; }
        public string CategoryId { get; set; }

    }
}