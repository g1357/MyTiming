using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyTiming.Models;
using MyTiming.ViewModels;

namespace MyTiming.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MyTaskDetailPage : ContentPage
    {
        MyTaskDetailViewModel viewModel;

        public MyTaskDetailPage(MyTaskDetailViewModel viewModel)
        {
            InitializeComponent();

            viewModel.SetPage(this);
            BindingContext = this.viewModel = viewModel;
        }

        public MyTaskDetailPage()
        {
            InitializeComponent();

            var item = new MyTaskEx
            {
                Name = "Item 1",
                Description = "This is an item description."
            };

            BindingContext = viewModel = new MyTaskDetailViewModel(ref item);
        }
    }
}