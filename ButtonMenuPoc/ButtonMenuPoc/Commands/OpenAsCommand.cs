using System;
using System.Windows;
using System.Windows.Input;

namespace DustInTheWind.ButtonMenuPoc.Commands
{
    internal class OpenAsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("Open As");
        }
    }
}