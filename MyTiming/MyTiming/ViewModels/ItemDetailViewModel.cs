using System;
using System.Runtime.InteropServices.ComTypes;
using MyTiming.Models;
using Xamarin.Forms;

namespace MyTiming.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// Режим работы таймера
        /// </summary>
        public enum TimerMode
        {
            Stopped, // Таймер остановлен
            Started, // Таймер запущен
            Paused   // Таймер поставлен на паузу
        }
        public Item Item { get; set; }

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
                    StartTimerCommand.ChangeCanExecute();
                    PauseTimerCommand.ChangeCanExecute();
                    ResumeTimerCommand.ChangeCanExecute();
                    StopTimerCommand.ChangeCanExecute();
                    ExitCommand.ChangeCanExecute();
                }
            }
        }

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
            set
            {
                SetProperty(ref _tHours, value);
            }
        }


        private int _tMinutes;
        public int TMinutes
        {
            get => _tMinutes;
            set
            {
                SetProperty(ref _tMinutes, value);
            }
        }

        private int _tSeconds;
        public int TSeconds
        {
            get => _tSeconds;
            set
            {
                SetProperty(ref _tSeconds, value);
            }
        }

        private DateTime startDT;
        private TimeSpan _base;

        public Command EditCommand { get; private set; }
        public Command CancelEditCommand { get; private set; }
        public Command SaveEditCommand { get; private set; }
        public Command StartTimerCommand { get; private set; }
        public Command PauseTimerCommand { get; private set; }
        public Command ResumeTimerCommand { get; private set; }
        public Command StopTimerCommand { get; private set; }
        public Command ExitCommand { get; private set; }

        public ItemDetailViewModel(ref Item item) // = null)
        {
            Title = item?.Text;
            Item = item;

            EditCommand = new Command(
                execute: async () =>
                {
                    EditFlag = true;
                },
                canExecute: () =>
                {
                    return !EditFlag;
                }
            );

            CancelEditCommand = new Command(
                execute: async () =>
                {
                    EditFlag = false;
                },
                canExecute: () =>
                {
                    return EditFlag;
                }
            );

            SaveEditCommand = new Command(
                execute: async () =>
                {
                    EditFlag = false;
                },
                canExecute: () =>
                {
                    return EditFlag;
                }
            );

            StartTimerCommand = new Command(
                execute: async () =>
                {
                    CurrentTimerMode = TimerMode.Started;
                    startDT = DateTime.Now;
                    _base = new TimeSpan(0);

                    ActivatedTimer();
                },
                canExecute: () =>
                {
                    return !EditFlag && CurrentTimerMode == TimerMode.Stopped;
                }
            );

            PauseTimerCommand = new Command(
                execute: async () =>
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
                execute: async () =>
                {
                    CurrentTimerMode = TimerMode.Started;
                    startDT = DateTime.Now;

                    ActivatedTimer();
                },
                canExecute: () =>
                {
                    return !EditFlag && CurrentTimerMode == TimerMode.Paused;
                }
            );

            StopTimerCommand = new Command(
                execute: async () =>
                {
                    CurrentTimerMode = TimerMode.Stopped;
                    _base += DateTime.Now - startDT;
                    var a = _base.TotalSeconds - _base.Seconds;
                    int s = 0;
                    if (a >= 0.5)
                    {
                        s = 1;
                    }
                    _base = new TimeSpan(_base.Hours, _base.Minutes, _base.Seconds + s);

                },
                canExecute: () =>
                {
                    return !EditFlag && (CurrentTimerMode == TimerMode.Started || CurrentTimerMode == TimerMode.Paused);
                }
            );
            ExitCommand = new Command(
                execute: async () =>
                {
                    CurrentTimerMode = TimerMode.Stopped;
                    Item.TimeSpended = _base;
                },
                canExecute: () =>
                {
                    return !EditFlag;
                }
            );
        }

        void ActivatedTimer()
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
    }
}
