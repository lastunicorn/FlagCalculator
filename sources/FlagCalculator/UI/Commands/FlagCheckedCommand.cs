// FlagCalculator
// Copyright (C) 2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows.Input;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal class FlagCheckedCommand : ICommand
    {
        private readonly FlagNumber flagNumber;

        public FlagCheckedCommand(FlagNumber flagNumber)
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
            CheckableItem checkableItem = parameter as CheckableItem;

            if (checkableItem == null)
                return;

            ulong value = checkableItem.Value;

            if (checkableItem.IsChecked)
            {
                if (value == 0)
                    flagNumber.Clear();
                else
                    flagNumber.AddFlags(value);
            }
            else
            {
                flagNumber.RemoveFlags(value);
            }
        }
    }
}