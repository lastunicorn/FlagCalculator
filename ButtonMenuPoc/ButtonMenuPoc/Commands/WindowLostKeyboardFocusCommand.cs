using System;
using System.Windows.Input;
using DustInTheWind.ButtonMenuPoc.Properties;
using DustInTheWind.ButtonMenuPoc.ViewModels;

namespace DustInTheWind.ButtonMenuPoc.Commands
{
    internal class WindowLostKeyboardFocusCommand : ICommand
    {
        private readonly MenuViewModel menuViewModel;

        public event EventHandler CanExecuteChanged;

        public WindowLostKeyboardFocusCommand([NotNull] MenuViewModel menuViewModel)
        {
            if (menuViewModel == null) throw new ArgumentNullException(nameof(menuViewModel));
            this.menuViewModel = menuViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            menuViewModel.MenuIsOpen = false;
        }
    }
}