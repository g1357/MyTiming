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
    public class CategoriesViewModel : BaseViewModel
    {
        CategoriesPage _page;

        bool flagEdit;
        Category categor;

        public ObservableCollection<Category> Items { get; set; }

        public Command ItemSelectCommand { get; set; }

        public CategoriesViewModel(CategoriesPage page)
        {
            _page = page;
            Title = "Categories of My Tasks";

            Items = new ObservableCollection<Category>(CategoryData.TaskCategory);
            flagEdit = false;
        }

        public CategoriesViewModel(ref Category newCategory)
        {
            categor = newCategory;
            Title = "Categories of My Tasks";

            Items = new ObservableCollection<Category>(CategoryData.TaskCategory);
            flagEdit = true;

            ItemSelectCommand = new Command<Category>( async (cat) =>
            {
                if (flagEdit)
                {
                    //categor.Id = cat.Id;
                    //categor.Name = cat.Name;
                    //categor.Description = cat.Description;
                    //categor.IconFile = cat.IconFile;
                    //categor.Color = cat.Color;
                    MessagingCenter.Send<CategoriesViewModel, Category>(this, "ChangeCatagory", cat);
                    App.Current.MainPage.SendBackButtonPressed();
                }
            });
        }

    }
}
