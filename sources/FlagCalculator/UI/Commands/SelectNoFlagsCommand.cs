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
    internal class SelectNoFlagsCommand : CommandBase
    {
        private ProjectContext projectContext;

        public SelectNoFlagsCommand(UserInterface userInterface, OpenedProjects openedProjects)
            : base(userInterface)
        {
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));

            projectContext = openedProjects.CurrentProject;
            openedProjects.CurrentProjectChanged += HandleCurrentProjectChanged;
        }

        public SelectNoFlagsCommand(UserInterface userInterface, ProjectContext projectContext)
            : base(userInterface)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            this.projectContext = projectContext;
        }

        private void HandleCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
        {
            projectContext = e.NewProject;

            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return projectContext != null;
        }

        protected override void DoExecute(object parameter)
        {
            projectContext.FlagsNumber.Clear();
        }
    }
}