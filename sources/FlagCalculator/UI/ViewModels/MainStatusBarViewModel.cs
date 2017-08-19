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
    internal class MainStatusBarViewModel : ViewModelBase
    {
        private readonly ProjectContext projectContext;
        private readonly StatusInfo statusInfo;
        private bool displaySelected;
        private bool displayUnselected;
        private string statusText;

        public bool DisplaySelected
        {
            get { return displaySelected; }
            set
            {
                if (value == displaySelected)
                    return;

                displaySelected = value;
                OnPropertyChanged();

                projectContext.DisplaySelected = value;
            }
        }

        public bool DisplayUnselected
        {
            get { return displayUnselected; }
            set
            {
                if (value == displayUnselected)
                    return;

                displayUnselected = value;
                OnPropertyChanged();

                projectContext.DisplayUnselected = value;
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

        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public SelectNoFlagsCommand SelectNoFlagsCommand { get; }
        public StatusInfoCommand StatusInfoCommand { get; }

        public MainStatusBarViewModel(ProjectContext projectContext, StatusInfo statusInfo)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.projectContext = projectContext;
            this.statusInfo = statusInfo;

            SelectAllFlagsCommand = new SelectAllFlagsCommand(projectContext);
            SelectNoFlagsCommand = new SelectNoFlagsCommand(projectContext);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            DisplaySelected = projectContext.DisplaySelected;
            DisplayUnselected = projectContext.DisplayUnselected;

            statusInfo.StatusTextChanged += HandleStatusTextChanged;
            projectContext.DisplaySelectedChanged += HandleFlagsDisplaySelectedChanged;
        }

        private void HandleStatusTextChanged(object sender, EventArgs eventArgs)
        {
            StatusText = statusInfo.StatusText;
        }

        private void HandleFlagsDisplaySelectedChanged(object sender, EventArgs eventArgs)
        {
            DisplaySelected = projectContext.DisplaySelected;
            DisplayUnselected = projectContext.DisplayUnselected;
        }
    }
}