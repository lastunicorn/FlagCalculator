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
    internal class MainValueViewModel : ViewModelBase
    {
        private SmartNumber mainValue;
        private string numericalBase;

        public SmartNumber MainValue
        {
            get { return mainValue; }
            private set
            {
                this.mainValue = value;
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

        public MainValueViewModel(SmartNumber mainValue, StatusInfo statusInfo)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.mainValue = mainValue;

            CopyCommand = new CopyCommand(MainValue);
            PasteCommand = new PasteCommand(MainValue);
            NumericalBaseRollCommand = new NumericalBaseRollCommand(MainValue);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            mainValue.ValueChanged += HandleMainValueChanged;
            MainValue.NumericalBaseChanged += HandleMainValueNumericalBaseChanged;

            UpdateNumericalBase();
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            MainValue = mainValue;
        }

        private void HandleMainValueNumericalBaseChanged(object sender, EventArgs e)
        {
            UpdateNumericalBase();
            MainValue = mainValue;
        }

        private void UpdateNumericalBase()
        {
            NumericalBase = ((int)MainValue.NumericalBase).ToString(CultureInfo.CurrentCulture);
        }
    }
}
