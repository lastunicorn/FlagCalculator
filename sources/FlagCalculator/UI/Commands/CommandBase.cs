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

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal abstract class CommandBase : ICommand
    {
        protected UserInterface UserInterface { get; }

        public event EventHandler CanExecuteChanged;

        protected CommandBase(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            UserInterface = userInterface;
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                DoExecute(parameter);
            }
            catch (Exception ex)
            {
                UserInterface.DisplayError(ex);
            }
        }

        protected abstract void DoExecute(object parameter);

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}