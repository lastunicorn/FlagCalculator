﻿// FlagCalculator
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
        private readonly SmartNumber smartNumber;

        public FlagCheckedCommand(SmartNumber smartNumber)
        {
            if (smartNumber == null) throw new ArgumentNullException(nameof(smartNumber));
            this.smartNumber = smartNumber;
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

            ulong value = checkableItem.FlagValue;

            if (checkableItem.IsChecked)
            {
                if (value == 0)
                    smartNumber.Clear();
                else
                    smartNumber.AddFlags(value);
            }
            else
            {
                smartNumber.RemoveFlags(value);
            }
        }
    }
}