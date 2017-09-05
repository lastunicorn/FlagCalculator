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
    internal class CloseProjectCommand : CommandBase
    {
        private readonly OpenedProjects openedProjects;

        public CloseProjectCommand(UserInterface userInterface, OpenedProjects openedProjects)
            : base(userInterface)
        {
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));

            this.openedProjects = openedProjects;
            this.openedProjects.CurrentProjectChanged += HandleCurrentProjectChanged;

            if (this.openedProjects.CurrentProject != null)
            {
                this.openedProjects.CurrentProject.Loaded += HandleProjectLoaded;
                this.openedProjects.CurrentProject.Unloaded += HandleProjectUnloaded;
            }
        }

        private void HandleCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
        {
            if (e.OldProject != null)
            {
                e.OldProject.Loaded -= HandleProjectLoaded;
                e.OldProject.Unloaded -= HandleProjectUnloaded;
            }

            if (e.NewProject != null)
            {
                e.NewProject.Loaded += HandleProjectLoaded;
                e.NewProject.Unloaded += HandleProjectUnloaded;
            }

            OnCanExecuteChanged();
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        private void HandleProjectUnloaded(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return openedProjects.CurrentProject != null && openedProjects.CurrentProject.IsLoaded;
        }

        protected override void DoExecute(object parameter)
        {
            openedProjects.CurrentProject?.Unload();
        }
    }
}