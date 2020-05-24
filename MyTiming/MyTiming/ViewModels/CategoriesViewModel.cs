using MyTiming.Helpers;
using MyTiming.Models;
using MyTiming.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTiming.ViewModels
{
    class CategoriesViewModel : BaseViewModel
    {
        CategoriesPage _page;

        public ObservableCollection<Category> Items { get; set; }


        public CategoriesViewModel(CategoriesPage page)
        {
            _page = page;
            Title = "Categories of My Tasks";

            Items = new ObservableCollection<Category>(CategoryData.TaskCategory);

        }

    }
}
