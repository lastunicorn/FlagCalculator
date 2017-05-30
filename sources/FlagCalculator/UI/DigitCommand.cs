using System;
using System.Windows.Input;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class DigitCommand : ICommand
    {
        private readonly FlagNumber flagNumber;

        public DigitCommand(FlagNumber flagNumber)
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
            int digit = int.Parse((string)parameter);
            flagNumber.Value = flagNumber.Value * 10 + (ulong)digit;
        }
    }
}