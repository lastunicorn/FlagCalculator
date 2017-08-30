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
using System.Globalization;
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal class MainHeaderViewModel : ViewModelBase
    {
        private readonly ProjectContext projectContext;

        private SmartNumber mainValue;
        private string numericalBase;

        public SmartNumber MainValue
        {
            get { return mainValue; }
            private set
            {
                mainValue = value;
                OnPropertyChanged();
            }
        }

        public string NumericalBase
        {
            get { return numericalBase; }
            set
            {
                numericalBase = value;
                OnPropertyChanged();
            }
        }

        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public NumericalBaseRollCommand NumericalBaseRollCommand { get; }
        public StatusInfoCommand StatusInfoCommand { get; }

        public MainHeaderViewModel(ProjectContext projectContext, StatusInfo statusInfo, UserInterface userInterface)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.projectContext = projectContext;

            CopyCommand = new CopyCommand(projectContext, userInterface);
            PasteCommand = new PasteCommand(projectContext, userInterface);
            NumericalBaseRollCommand = new NumericalBaseRollCommand(projectContext.NumericalBaseService, userInterface);
            StatusInfoCommand = new StatusInfoCommand(statusInfo, userInterface);

            projectContext.FlagsNumberChanged += HandleFlagsNumberChanged;
            projectContext.FlagsNumber.ValueChanged += HandleMainValueChanged;
            projectContext.NumericalBaseService.NumericalBaseChanged += HandleNumericalBaseChanged;

            UpdateMainValue();
            UpdateNumericalBaseText();
        }

        private void HandleFlagsNumberChanged(object sender, FlagsNumberChangedEventArgs e)
        {
            if (e.OldValue != null)
                e.OldValue.ValueChanged -= HandleMainValueChanged;

            if (e.NewValue != null)
            {
                e.NewValue.ValueChanged += HandleMainValueChanged;
                UpdateMainValue();
            }
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            UpdateMainValue();
        }

        private void HandleNumericalBaseChanged(object sender, EventArgs e)
        {
            UpdateNumericalBaseText();
            MainValue = new SmartNumber
            {
                Value = projectContext.FlagsNumber.Value,
                NumericalBase = projectContext.NumericalBaseService,
                BitCount = projectContext.FlagsNumber.BitCount
            };
        }

        private void UpdateMainValue()
        {
            MainValue = new SmartNumber
            {
                Value = projectContext.FlagsNumber.Value,
                NumericalBase = projectContext.NumericalBaseService,
                BitCount = projectContext.FlagsNumber.BitCount
            };
        }

        private void UpdateNumericalBaseText()
        {
            uint newValue = projectContext.NumericalBaseService;
            NumericalBase = newValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
