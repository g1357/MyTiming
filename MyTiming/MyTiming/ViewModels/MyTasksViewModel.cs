using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyTiming.Models;
using MyTiming.Views;
using MyTiming.Services;
using MyTiming.Helpers;

namespace MyTiming.ViewModels
{
    public class MyTasksViewModel : BaseViewModel
    {
        MyTasksPage _page;
        public IDataStore<MyTask> DataStore => DependencyService.Get<IDataStore<MyTask>>();

        public ObservableCollection<MyTaskEx> Items { get; set; }
        public Command LoadItemsCommand { get; private set; }
        public Command AddItemCommand { get; private set; }
        public Command ItemSelectCommand { get; private set; }

        public MyTasksViewModel(MyTasksPage page)
        {
            _page = page;
            Title = "Browse";
            Items = new ObservableCollection<MyTaskEx>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command(async () => await ExecuteAddItemCommand());
            ItemSelectCommand = new Command<Item>(async (item) => await ExecuteItemSelectCommand(item)); 

            MessagingCenter.Subscribe<NewItemPage, MyTask>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as MyTask;
                var newItemEx = new MyTaskEx(newItem);
                Items.Add(newItemEx);
                await DataStore.AddItemAsync(newItem);
            });
        }

        internal async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                MyTaskEx itemEx;
                foreach (var item in items)
                {
                    itemEx = new MyTaskEx(item);
                    Items.Add(itemEx);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteAddItemCommand()
        {
            await _page.Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
        async Task ExecuteItemSelectCommand(Item item)
        {
            await _page.Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(ref item)));
        }


    }
}