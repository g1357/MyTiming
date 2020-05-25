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
    public partial class SendMailPage : ContentPage
    {
        SendMailViewModel viewModel;

        public SendMailPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SendMailViewModel(this);
        }
    }
}