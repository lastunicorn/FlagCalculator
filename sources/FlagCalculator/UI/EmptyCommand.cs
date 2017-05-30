using System;
using System.Windows;
using System.Windows.Input;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class EmptyCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show("success");
        }
    }
}