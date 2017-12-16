using System.Collections.Generic;
using DustInTheWind.ButtonMenuPoc.Commands;
using DustInTheWind.ButtonMenuPoc.MenuModel;

namespace DustInTheWind.ButtonMenuPoc.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        public List<object> MenuItems { get; }
        private bool menuIsOpen;

        public bool MenuIsOpen
        {
            get { return menuIsOpen; }
            set
            {
                if (value == menuIsOpen) return;
                menuIsOpen = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel()
        {
            MenuItems = new List<object>
            {
                new MenuItem
                {
                    Text = "Open",
                    IconPath = "pack://application:,,,/Images/folder-open-12.png",
                    Command = new OpenCommand()
                },
                new MenuItem
                {
                    Text = "Open As",
                    IconPath = "pack://application:,,,/Images/folder-open-12.png",
                    Command = new OpenAsCommand()
                },
                new MenuItemSeparator(),
                new MenuItem
                {
                    Text = "Close",
                    IconPath = "pack://application:,,,/Images/folder-open-12.png",
                    Command = new CloseCommand()
                },
                new MenuItemSeparator(),
                "Text Item"
            };
        }
    }
}