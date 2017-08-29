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
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal class DigitCommand : CommandBase
    {
        private readonly ProjectContext projectContext;

        public DigitCommand(ProjectContext projectContext, UserInterface userInterface)
            : base(userInterface)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            this.projectContext = projectContext;
        }

        protected override void DoExecute(object parameter)
        {
            uint digit = uint.Parse((string)parameter);
            uint numericalBase = projectContext.NumericalBaseService;

            if (digit >= numericalBase)
                return;

            projectContext.FlagsNumber.Value = projectContext.FlagsNumber.Value * numericalBase + digit;
        }
    }
}