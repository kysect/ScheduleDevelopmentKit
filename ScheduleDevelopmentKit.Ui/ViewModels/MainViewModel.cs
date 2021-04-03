using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using ScheduleDevelopmentKit.Ui.Commands;
using ScheduleDevelopmentKit.Ui.Views;

namespace ScheduleDevelopmentKit.Ui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            NavigationItems = new List<NavigationViewItemBase>
            {
                new NavigationViewItem
                {
                    Icon = new SymbolIcon(Symbol.Home),
                    Content = "Расписание",
                    Tag = NavigationTarget.Schedule,
                    IsSelected = true
                },
                new NavigationViewItem
                {
                    Icon = new SymbolIcon(Symbol.Add),
                    Content = "Добавить курс",
                    Tag = NavigationTarget.AddCourse
                },
                new NavigationViewItem
                {
                    Icon = new SymbolIcon(Symbol.Add),
                    Content = "Добавить группу",
                    Tag = NavigationTarget.AddGroup
                },
            };

            Navigate(NavigationItems[0]);


            NavigationItemInvoked = new BaseCommand(args =>
            {
                if (args is NavigationViewItemInvokedEventArgs item)
                    Navigate(item.InvokedItemContainer);
            });
        }

        private void Navigate(NavigationViewItemBase item)
        {
            ActivePage = (NavigationTarget) item.Tag switch
            {
                NavigationTarget.Schedule => new SchedulePage(),
                NavigationTarget.AddCourse => new AddCoursePage(),
                NavigationTarget.AddGroup => new AddGroupPage(),
                _ => throw new ArgumentOutOfRangeException()
            };

            Header = (string) item.Content;
        }

        public List<NavigationViewItemBase> NavigationItems { get; }

        public ICommand NavigationItemInvoked { get; }

        public Page ActivePage
        {
            get => _activePage;

            private set
            {
                _activePage = value;
                OnPropertyChanged();
            }
        }
        private Page _activePage;

        public string Header
        {
            get => _header;

            private set
            {
                _header = value;
                OnPropertyChanged();
            }
        }
        private string _header;
    }

    public enum NavigationTarget
    {
        Schedule, 
        AddCourse,
        AddGroup
    }
}