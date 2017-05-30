using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class CopyCommand : ICommand
    {
        private readonly FlagNumber flagNumber;

        public CopyCommand(FlagNumber flagNumber)
        {
            if (flagNumber == null) throw new ArgumentNullException(nameof(flagNumber));
            this.flagNumber = flagNumber;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Clipboard.SetText(flagNumber.Value.ToString(CultureInfo.CurrentCulture));
        }
    }
}