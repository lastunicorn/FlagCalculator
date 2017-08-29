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
using System.Windows;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal class PasteCommand : CommandBase
    {
        private readonly ProjectContext projectContext;

        public PasteCommand(ProjectContext projectContext, UserInterface userInterface)
            : base(userInterface)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            this.projectContext = projectContext;
        }

        protected override void DoExecute(object parameter)
        {
            string newValue = Clipboard.GetText();

            try
            {
                int numericalBase = (int)projectContext.NumericalBaseService.NumericalBase;
                ulong uInt64 = Convert.ToUInt64(newValue.Replace(" ", string.Empty), numericalBase);
                projectContext.FlagsNumber.Value = uInt64;
            }
            catch { }
        }
    }
}