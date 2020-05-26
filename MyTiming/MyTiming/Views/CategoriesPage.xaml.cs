using MyTiming.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTiming.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        CategoriesViewModel viewModel;

        public CategoriesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoriesViewModel(this);

        }

        public CategoriesPage(CategoriesViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}