using MyTiming.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyTiming.Models
{
    /// <summary>
    /// Мои задачи (проекты)
    /// </summary>
    public class MyTaskEx : IModel
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название задачи (проекта)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание задачи (проекта)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Последнее время задачи
        /// </summary>
        public TimeSpan TimeSpended { get; set; }

        /// <summary>
        /// Общее время задачи
        /// </summary>
        public TimeSpan TotalTimeSpended { get; set; }

        /// <summary>
        /// Идентификатор категории задачи
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// Название категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        public string CategoryDescription { get; set; }

        /// <summary>
        /// Индекс иконки категории
        /// </summary>
        public string CategoryIconFile { get; set; }

        /// <summary>
        /// Цвет категории
        /// </summary>
        public Color CategoryColor { get; set; }

        public MyTaskEx()
        { }

        public MyTaskEx(MyTask item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            TimeSpended = item.TimeSpended;
            TotalTimeSpended = item.TotalTimeSpended;
            CategoryId = item.CategoryId;
            var cat = CategoryData.CategoryDic[item.CategoryId];
            CategoryName = cat.Name;
            CategoryDescription = cat.Description;
            CategoryIconFile = cat.IconFile;
            CategoryColor = cat.Color;
        }

    }
}
