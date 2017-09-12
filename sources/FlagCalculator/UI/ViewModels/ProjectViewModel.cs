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
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal class ProjectViewModel : ViewModelBase
    {
        private bool isOpenPanelVisible;
        private string header;

        public ProjectContext ProjectContext { get; }

        public bool IsOpenPanelVisible
        {
            get { return isOpenPanelVisible; }
            private set
            {
                isOpenPanelVisible = value;
                OnPropertyChanged();
            }
        }

        public string Header
        {
            get { return header; }
            private set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        public OpenAssemblyCommand OpenAssemblyCommand { get; }
        public CloseProjectCommand CloseProjectCommand { get; }

        public FlagListViewModel FlagListViewModel { get; }
        public MainFooterViewModel MainFooterViewModel { get; }
        public MainHeaderViewModel MainHeaderViewModel { get; }

        public ProjectViewModel(UserInterface userInterface, StatusInfo statusInfo, OpenedProjects openedProjects, ProjectContext projectContext)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            
            ProjectContext = projectContext;

            // Create view models.

            FlagListViewModel = new FlagListViewModel(projectContext, statusInfo, userInterface);
            MainFooterViewModel = new MainFooterViewModel(projectContext, statusInfo, userInterface);
            MainHeaderViewModel = new MainHeaderViewModel(projectContext, statusInfo, userInterface);

            // Create commands

            OpenAssemblyCommand = new OpenAssemblyCommand(userInterface, projectContext);
            CloseProjectCommand = new CloseProjectCommand(userInterface, openedProjects);

            // Initialize everything

            ProjectContext.Loaded += HandleProjectLoaded;
            ProjectContext.Unloaded += HandleProjectUnloaded;

            UpdateOpenPanelVisibility();
            UpdateHeader();
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            UpdateOpenPanelVisibility();
            UpdateHeader();
        }

        private void HandleProjectUnloaded(object sender, EventArgs e)
        {
            UpdateOpenPanelVisibility();
            UpdateHeader();
        }

        private void UpdateOpenPanelVisibility()
        {
            IsOpenPanelVisible = !ProjectContext.IsLoaded;
        }

        private void UpdateHeader()
        {
            if (ProjectContext.IsLoaded)
            {
                FlagsNumber flagNumber = ProjectContext.FlagsNumber;

                string flagsName = flagNumber.Name;
                string enumTypeName = flagNumber.UnderlyingType.Name;

                Header = $"{flagsName} ({enumTypeName})";
            }
            else
            {
                Header = "Empty Tab";
            }
        }
    }
}
