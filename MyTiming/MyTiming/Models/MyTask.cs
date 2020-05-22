using System;
using System.Collections.Generic;
using System.Text;

namespace MyTiming.Models
{
    /// <summary>
    /// Мои задачи (проекты)
    /// </summary>
    public class MyTask
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
        /// Идентификатор категории задачи
        /// </summary>
        public string CategoryId { get; set; }
    }
}
