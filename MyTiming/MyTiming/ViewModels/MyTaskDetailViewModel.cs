using System;
using System.Runtime.InteropServices.ComTypes;
using MyTiming.Models;
using MyTiming.Views;
using Xamarin.Forms;

namespace MyTiming.ViewModels
{
    public class MyTaskDetailViewModel : BaseViewModel
    {
        MyTaskDetailPage _page;

        /// <summary>
        /// Режим работы таймера
        /// </summary>
        public enum TimerMode
        {
            Stopped, // Таймер остановлен
            Started, // Таймер запущен
            Paused   // Таймер поставлен на паузу
        }
        private MyTaskEx _item;
        public MyTaskEx Item { 
            get => _item;
            set => SetProperty(ref _item, value);
        }

        MyTaskEx _savedItem;

        /// <summary>
        ///  Поле: Признак режима редактирования
        /// </summary>
        private bool _editFlag = false;
        /// <summary>
        /// Свойство: Признак режима редактирования
        /// </summary>
        public bool EditFlag
        {
            get => _editFlag;
            set
            {
                if (SetProperty(ref _editFlag, value))
                {
                    EditCommand.ChangeCanExecute();
                    CancelEditCommand.ChangeCanExecute();
                    SaveEditCommand.ChangeCanExecute();
                    ChangeCategoryCommand.ChangeCanExecute();
                    StartTimerCommand.ChangeCanExecute();
                    PauseTimerCommand.ChangeCanExecute();
                    ResumeTimerCommand.ChangeCanExecute();
                    StopTimerCommand.ChangeCanExecute();
                    ExitCommand.ChangeCanExecute();
                    OnPropertyChanged("ReadOnly");
                }
            }
        }
        public bool ReadOnly => !_editFlag;

        private TimerMode _currentTimerMode = TimerMode.Stopped;
        public TimerMode CurrentTimerMode
        {
            get => _currentTimerMode;
            set
            {
                if (SetProperty(ref _currentTimerMode, value))
                {
                    EditCommand.ChangeCanExecute();
                    CancelEditCommand.ChangeCanExecute();
                    SaveEditCommand.ChangeCanExecute();
                    StartTimerCommand.ChangeCanExecute();
                    PauseTimerCommand.ChangeCanExecute();
                    ResumeTimerCommand.ChangeCanExecute();
                    StopTimerCommand.ChangeCanExecute();
                    ExitCommand.ChangeCanExecute();
                }
            }
        }

        private int _tHours;
        public int THours
        {
            get => _tHours;
            set => SetProperty(ref _tHours, value);
        }


        private int _tMinutes;
        public int TMinutes
        {
            get => _tMinutes;
            set => SetProperty(ref _tMinutes, value);
        }

        private int _tSeconds;
        public int TSeconds
        {
            get => _tSeconds;
            set => SetProperty(ref _tSeconds, value);
        }

        private DateTime startDT;
        private TimeSpan _base;

        public Command EditCommand { get; private set; }
        public Command CancelEditCommand { get; private set; }
        public Command SaveEditCommand { get; private set; }
        public Command ChangeCategoryCommand { get; private set; }
        public Command StartTimerCommand { get; private set; }
        public Command PauseTimerCommand { get; private set; }
        public Command ResumeTimerCommand { get; private set; }
        public Command StopTimerCommand { get; private set; }
        public Command ExitCommand { get; private set; }

        public MyTaskDetailViewModel(ref MyTaskEx item) // = null)
        {
            Title = item?.Name;
            Item = item;

            EditCommand = new Command(
                execute: () =>
                {
                    _savedItem = new MyTaskEx(Item); 
                    EditFlag = true;
                },
                canExecute: () =>
                {
                    return !EditFlag;
                }
            );

            CancelEditCommand = new Command(
                execute: () =>
                {
                    Item.Name = _savedItem.Name;
                    Item.Description = _savedItem.Description;
                    OnPropertyChanged("Item");
                    EditFlag = false;
                },
                canExecute: () =>
                {
                    return EditFlag;
                }
            );

            SaveEditCommand = new Command(
                execute: () =>
                {
                    EditFlag = false;
                },
                canExecute: () =>
                {
                    return EditFlag;
                }
            );

            ChangeCategoryCommand = new Command(
                execute: async () =>
                {
                    Category cat = new Category();
                    MessagingCenter.Subscribe<CategoriesViewModel, Category>(this, "ChangeCatagory", (vm, c) => { ChangeCategory(vm, c); });
                    await _page.Navigation.PushAsync(new CategoriesPage(new CategoriesViewModel(ref cat)));
                },
                canExecute: () =>
                {
                    return EditFlag;
                }
            );

            StartTimerCommand = new Command(
                execute: () =>
                {
                    CurrentTimerMode = TimerMode.Started;
                    startDT = DateTime.Now;
                    _base = new TimeSpan(0);

                    ActivateTimer();
                },
                canExecute: () =>
                {
                    return !EditFlag && CurrentTimerMode == TimerMode.Stopped;
                }
            );

            PauseTimerCommand = new Command(
                execute: () =>
                {
                    CurrentTimerMode = TimerMode.Paused;
                    _base += DateTime.Now - startDT;
                },
                canExecute: () =>
                {
                    return !EditFlag && CurrentTimerMode == TimerMode.Started;
                }
            );

            ResumeTimerCommand = new Command(
                execute: () =>
                {
                    CurrentTimerMode = TimerMode.Started;
                    startDT = DateTime.Now;

                    ActivateTimer();
                },
                canExecute: () =>
                {
                    return !EditFlag && CurrentTimerMode == TimerMode.Paused;
                }
            );

            StopTimerCommand = new Command(
                execute: () =>
                {
                    StopTimer();
                },
                canExecute: () =>
                {
                    return !EditFlag && (CurrentTimerMode == TimerMode.Started || CurrentTimerMode == TimerMode.Paused);
                }
            );
            ExitCommand = new Command(
                execute: () =>
                {
                    StopTimer();
                    Item.TimeSpended = _base;
                    //App.Current.MainPage.Navigation.PopAsync();
                    App.Current.MainPage.SendBackButtonPressed();
                },
                canExecute: () =>
                {
                    return !EditFlag;
                }
            );
        }

        public void SetPage(MyTaskDetailPage page)
        {
            _page = page;
        }
        void ActivateTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                var flag = (CurrentTimerMode == TimerMode.Started);
                if (flag)
                {
                    TimeSpan ts = DateTime.Now - startDT + _base;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        THours = ts.Hours;
                        TMinutes = ts.Minutes;
                        TSeconds = ts.Seconds;
                    });
                }
                return flag;
            });
        }

        void StopTimer()
        {
            if (CurrentTimerMode == TimerMode.Started)
            {
                _base += DateTime.Now - startDT;
            }
            CurrentTimerMode = TimerMode.Stopped;
            var a = _base.TotalSeconds - _base.Seconds;
            int s = 0;
            if (a >= 0.5)
            {
                s = 1;
            }
            _base = new TimeSpan(_base.Hours, _base.Minutes, _base.Seconds + s);
        }

        void ChangeCategory(CategoriesViewModel viewModel, Category category)
        {
            Item.SetCategory(category);
            OnPropertyChanged("Item");
            MessagingCenter.Unsubscribe<CategoriesViewModel, Category>(this, "ChangeCatagory");

        }
    }
}
