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
    internal class MainFooterViewModel : ViewModelBase
    {
        private readonly ProjectContext projectContext;
        private readonly StatusInfo statusInfo;
        private bool displaySelected;
        private bool displayUnselected;
        private string statusText;
        private bool isEnabled;

        public bool DisplaySelected
        {
            get { return displaySelected; }
            private set
            {
                displaySelected = value;
                OnPropertyChanged();
            }
        }

        public bool DisplayUnselected
        {
            get { return displayUnselected; }
            private set
            {
                displayUnselected = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get { return statusText; }
            private set
            {
                statusText = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            private set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public SelectNoFlagsCommand SelectNoFlagsCommand { get; }
        public StatusInfoCommand StatusInfoCommand { get; }
        public ToggleDisplaySelectedCommand ToggleDisplaySelectedCommand { get; }
        public ToggleDisplayUnselectedCommand ToggleDisplayUnselectedCommand { get; }

        public MainFooterViewModel(ProjectContext projectContext, StatusInfo statusInfo, UserInterface userInterface)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.projectContext = projectContext;
            this.statusInfo = statusInfo;

            SelectAllFlagsCommand = new SelectAllFlagsCommand(userInterface, projectContext);
            SelectNoFlagsCommand = new SelectNoFlagsCommand(userInterface, projectContext);
            StatusInfoCommand = new StatusInfoCommand(statusInfo, userInterface);
            ToggleDisplaySelectedCommand = new ToggleDisplaySelectedCommand(userInterface, projectContext);
            ToggleDisplayUnselectedCommand = new ToggleDisplayUnselectedCommand(userInterface, projectContext);

            DisplaySelected = projectContext.DisplaySelected;
            DisplayUnselected = projectContext.DisplayUnselected;

            statusInfo.StatusTextChanged += HandleStatusTextChanged;

            this.projectContext.Loaded += HandleProjectLoaded;
            this.projectContext.Unloaded += HandleProjectUnloaded;
            this.projectContext.DisplaySelectedChanged += HandleFlagsDisplaySelectedChanged;

            IsEnabled = projectContext.IsLoaded;
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            StatusText = statusInfo.StatusText;
        }

        private void HandleFlagsDisplaySelectedChanged(object sender, EventArgs e)
        {
            DisplaySelected = projectContext.DisplaySelected;
            DisplayUnselected = projectContext.DisplayUnselected;
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            IsEnabled = projectContext.IsLoaded;
        }

        private void HandleProjectUnloaded(object sender, EventArgs e)
        {
            IsEnabled = projectContext.IsLoaded;
        }
    }
}