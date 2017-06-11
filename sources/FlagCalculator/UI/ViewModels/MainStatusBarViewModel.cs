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
        private readonly FlagsList flags;
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

                flags.DisplaySelected = value;
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

                flags.DisplayUnselected = value;
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

        public MainStatusBarViewModel(FlagsList flags, StatusInfo statusInfo)
        {
            if (flags == null) throw new ArgumentNullException(nameof(flags));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.flags = flags;
            this.statusInfo = statusInfo;

            SelectAllFlagsCommand = new SelectAllFlagsCommand(flags);
            SelectNoFlagsCommand = new SelectNoFlagsCommand(flags);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            DisplaySelected = flags.DisplaySelected;
            DisplayUnselected = flags.DisplayUnselected;

            statusInfo.StatusTextChanged += HandleStatusTextChanged;
            flags.SelectionChanged += HandleFlagsSelectionChanged;
        }

        private void HandleStatusTextChanged(object sender, EventArgs eventArgs)
        {
            StatusText = statusInfo.StatusText;
        }

        private void HandleFlagsSelectionChanged(object sender, EventArgs eventArgs)
        {
            DisplaySelected = flags.DisplaySelected;
            DisplayUnselected = flags.DisplayUnselected;
        }
    }
}