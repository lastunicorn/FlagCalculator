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
using System.Collections.Generic;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal class OpenAssemblyCommand : CommandBase
    {
        private ProjectContext projectContext;

        public OpenAssemblyCommand(UserInterface userInterface, OpenedProjects openedProjects)
            : base(userInterface)
        {
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));

            projectContext = openedProjects.CurrentProject;
            openedProjects.CurrentProjectChanged += HandleCurrentProjectChanged;

            if (projectContext != null)
            {
                projectContext.Loaded += HandleProjectLoaded;
                projectContext.Unloaded += HandleProjectUnloaded;
            }
        }

        public OpenAssemblyCommand(UserInterface userInterface, ProjectContext projectContext)
            : base(userInterface)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));

            this.projectContext = projectContext;
            this.projectContext.Loaded += HandleProjectLoaded;
            this.projectContext.Unloaded += HandleProjectUnloaded;
        }

        private void HandleCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
        {
            if (projectContext != null)
            {
                projectContext.Loaded -= HandleProjectLoaded;
                projectContext.Unloaded -= HandleProjectUnloaded;
            }

            projectContext = e.NewProject;

            if (e.NewProject != null)
            {
                projectContext.Loaded += HandleProjectLoaded;
                projectContext.Unloaded += HandleProjectUnloaded;
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
            return projectContext != null && !projectContext.IsLoaded;
        }

        protected override void DoExecute(object parameter)
        {
            GuiEnumProvider enumProvider = new GuiEnumProvider();

            IEnumerable<Type> enumTypes = enumProvider.LoadEnum();

            if (enumTypes == null)
                return;

            foreach (Type enumType in enumTypes)
                projectContext.LoadFlagCollection(enumType);
        }
    }
}