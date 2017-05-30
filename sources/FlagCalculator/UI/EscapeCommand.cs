using System;
using System.Windows.Input;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class EscapeCommand : ICommand
    {
        private readonly FlagNumber flagNumber;

        public EscapeCommand(FlagNumber flagNumber)
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
            flagNumber.Clear();
        }
    }
}