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

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal sealed class TabItem : ViewModelBase
    {
        private readonly ProjectContext projectContext;
        private string header;

        public string Header
        {
            get { return header; }
            private set
            {
                header = value;
                OnPropertyChanged();
            }
        }

        public ProjectViewModel Content { get; set; }

        public TabItem(UserInterface userInterface, StatusInfo statusInfo, ProjectContext projectContext)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));

            this.projectContext = projectContext;

            this.projectContext.Loaded += HandleProjectLoaded;
            this.projectContext.Unloaded += HandleProjectUnloaded;

            UpdateHeader();
            Content = new ProjectViewModel(userInterface, statusInfo, projectContext);
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            UpdateHeader();
        }

        private void HandleProjectUnloaded(object sender, EventArgs eventArgs)
        {
            UpdateHeader();
        }

        private void UpdateHeader()
        {
            if (projectContext.IsLoaded)
            {
                FlagsNumber flagNumber = projectContext.FlagsNumber;

                string flagsName = flagNumber.Name;
                string enumTypeName = flagNumber.UnderlyingType.Name;

                Header = $"{flagsName} ({enumTypeName})";
            }
            else
            {
                Header = "< New Tab >";
            }
        }
    }
}