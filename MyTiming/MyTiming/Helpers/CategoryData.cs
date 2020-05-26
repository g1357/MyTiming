using MyTiming.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MyTiming.Helpers
{
    public static class CategoryData
    {
        public static readonly string[] CatIcon =
        {
            "folder.png", "sleeping.png", "work.png", "dinning.png", "sport.png", "car.png",
            "drawing.png", "entertainment.png", "reading.png", "house.png", "pray.png"    
        };

        public static readonly Color[] CatColor =
        {
            Color.AliceBlue, Color.PapayaWhip, Color.Gold, Color.SpringGreen,
            Color.Aqua, Color.LightSteelBlue, Color.DarkOrange, Color.DarkSeaGreen,
            Color.Aquamarine, Color.SandyBrown, Color.Violet
        };

        public static readonly Category[] TaskCategory =
        {
            new Category()
            {
                Id = "0", Name = "Other things", Description ="Всё не вошедшее в другие категории.",
                    IconFile = CatIcon[0], Color = CatColor[0]
            },
            new Category()
            {
                Id = "1", Name = "Sleep", Description ="Ночной и дневной сон.",
                    IconFile = CatIcon[1], Color = CatColor[1]
            },
            new Category()
            {
                Id = "2", Name = "Work", Description ="Работа для зарабатывания средств к существованию.",
                    IconFile = CatIcon[2], Color = CatColor[2]
            },
            new Category()
            {
                Id = "3", Name = "Meal", Description ="Все приёмы пищи и перекусы в течении дня.",
                    IconFile = CatIcon[3], Color = CatColor[3]
            },
            new Category()
            {
                Id = "4", Name = "Sport", Description ="Занятия спортом, фитнесом и другие физические нагрузки.",
                    IconFile = CatIcon[4], Color = CatColor[4]
            },
            new Category()
            {
                Id ="5", Name = "Trips", Description ="Любые поездки и затраты времени на перемещения.",
                    IconFile = CatIcon[5], Color = CatColor[5]
            },
            new Category()
            {
                Id = "6", Name = "Hobby", Description ="Хобби.",
                    IconFile = CatIcon[6], Color = CatColor[6]
            },
            new Category()
            {
                Id = "7", Name = "Entertainment", Description ="Все виды развлечения.",
                    IconFile = CatIcon[7], Color = CatColor[7]
            },
            new Category()
            {
                Id = "8", Name = "Learnin", Description ="Все виды обкчения и повышения квалификации.",
                    IconFile = CatIcon[8], Color = CatColor[8]
            },
            new Category()
            {
                Id = "9", Name = "Housekeeping", Description ="Всё виды работ по хозяйству.",
                    IconFile = CatIcon[9], Color = CatColor[9]
            },
            new Category()
            {
                Id = "10", Name = "Pray", Description ="Всё виды духовных практик: молитва, медитация и т.п.",
                    IconFile = CatIcon[10], Color = CatColor[10]
            }
        };

        public static readonly Dictionary<string, Category> CategoryDic = new Dictionary<string, Category>()
        {
            { TaskCategory[0].Id, TaskCategory[0] },
            { TaskCategory[1].Id, TaskCategory[1] },
            { TaskCategory[2].Id, TaskCategory[2] },
            { TaskCategory[3].Id, TaskCategory[3] },
            { TaskCategory[4].Id, TaskCategory[4] },
            { TaskCategory[5].Id, TaskCategory[5] },
            { TaskCategory[6].Id, TaskCategory[6] },
            { TaskCategory[7].Id, TaskCategory[7] },
            { TaskCategory[8].Id, TaskCategory[8] },
            { TaskCategory[9].Id, TaskCategory[9] },
            { TaskCategory[10].Id, TaskCategory[10] },
        };
    }
}
