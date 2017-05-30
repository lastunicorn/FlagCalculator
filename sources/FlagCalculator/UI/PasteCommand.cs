using System;
using System.Windows;
using System.Windows.Input;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class PasteCommand : ICommand
    {
        private readonly FlagNumber flagNumber;

        public PasteCommand(FlagNumber flagNumber)
        {
            if (flagNumber == null) throw new ArgumentNullException("flagNumber");
            this.flagNumber = flagNumber;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ulong value;

            bool success = ulong.TryParse(Clipboard.GetText(), out value);

            if (success)
                flagNumber.Value = value;
        }
    }
}