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

        private SmartValue smartValue;
        private string numericalBase;

        public SmartValue MainValue
        {
            get { return smartValue; }
            private set
            {
                smartValue = value;
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

        public MainHeaderViewModel(ProjectContext projectContext, StatusInfo statusInfo)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.projectContext = projectContext;

            CopyCommand = new CopyCommand(projectContext);
            PasteCommand = new PasteCommand(projectContext);
            NumericalBaseRollCommand = new NumericalBaseRollCommand(projectContext.NumericalBaseService);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            projectContext.FlagsNumberChanged += HandleFlagsNumberChanged;
            projectContext.FlagsNumber.ValueChanged += HandleMainValueChanged;
            projectContext.NumericalBaseService.NumericalBaseChanged += HandleNumericalBaseChanged;

            UpdateNumericalBaseText();
        }

        private void HandleFlagsNumberChanged(object sender, FlagsNumberChangedEventArgs e)
        {
            if (e.OldValue != null)
                e.OldValue.ValueChanged -= HandleMainValueChanged;

            if (e.NewValue != null)
                e.NewValue.ValueChanged += HandleMainValueChanged;
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            MainValue = new SmartValue
            {
                Value = projectContext.FlagsNumber.Value,
                NumericalBase = projectContext.NumericalBaseService.NumericalBase,
                BitCount = projectContext.FlagsNumber.BitCount
            };
        }

        private void HandleNumericalBaseChanged(object sender, EventArgs e)
        {
            UpdateNumericalBaseText();
            MainValue = new SmartValue
            {
                Value = projectContext.FlagsNumber.Value,
                NumericalBase = projectContext.NumericalBaseService.NumericalBase,
                BitCount = projectContext.FlagsNumber.BitCount
            };
        }

        private void UpdateNumericalBaseText()
        {
            NumericalBase = ((int)projectContext.NumericalBaseService.NumericalBase).ToString(CultureInfo.CurrentCulture);
        }
    }
}
