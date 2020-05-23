using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyTiming.Models;
using MyTiming.Views;

namespace MyTiming.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        ItemsPage _page;
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; private set; }
        public Command AddItemCommand { get; private set; }
        public Command ItemSelectCommand { get; private set; }

        public ItemsViewModel(ItemsPage page)
        {
            _page = page;
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command(async () => await ExecuteAddItemCommand());
            ItemSelectCommand = new Command<Item>(async (item) => await ExecuteItemSelectCommand(item)); 

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
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
                foreach (var item in items)
                {
                    Items.Add(item);
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