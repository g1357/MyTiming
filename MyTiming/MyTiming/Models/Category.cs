using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyTiming.Models
{
    /// <summary>
    /// Категории задач (проектов)
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description { get; set; }
       
        /// <summary>
        /// Иконка категории
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// Цвет категории
        /// </summary>
        public Color Color { get; set; }
    }
}
