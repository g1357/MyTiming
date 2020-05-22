using System;

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

        public Command EditCommand { get; private set; }
        public Command CancelEditCommand { get; private set; }
        public Command SaveEditCommand { get; private set; }
        public Command StartTimerCommand { get; private set; }
        public Command PauseTimerCommand { get; private set; }
        public Command ResumeTimerCommand { get; private set; }
        public Command StopTimerCommand { get; private set; }
        public Command ExitCommand { get; private set; }

        public ItemDetailViewModel(Item item = null)
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
                },
                canExecute: () =>
                {
                    return !EditFlag && (CurrentTimerMode == TimerMode.Started || CurrentTimerMode == TimerMode.Paused);
                }
            );
            ExitCommand = new Command(
                execute: async () =>
                {
                },
                canExecute: () =>
                {
                    return !EditFlag;
                }
            );
        }
    }
}
