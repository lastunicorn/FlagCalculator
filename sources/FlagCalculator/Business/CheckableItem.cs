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
using DustInTheWind.FlagCalculator.UI;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class CheckableItem : ViewModelBase
    {
        private readonly SmartNumber mainValue;
        private readonly FlagInfo flagInfo;
        private bool isChecked;
        private string text;
        private string toolTip;
        private SmartValue flagValue;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        public string FlagName
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                toolTip = value;
                OnPropertyChanged();
            }
        }

        public SmartValue FlagValue
        {
            get { return flagValue; }
            private set
            {
                flagValue = value;
                OnPropertyChanged();
            }
        }

        public FlagCheckedCommand FlagCheckedCommand { get; }
        public StatusInfoCommand StatusInfoCommand { get; }

        public CheckableItem(SmartNumber mainValue, FlagInfo flagInfo, StatusInfo statusInfo)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));
            if (flagInfo == null) throw new ArgumentNullException(nameof(flagInfo));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.mainValue = mainValue;
            this.flagInfo = flagInfo;

            mainValue.NumericalBaseChanged += HandleNumericalBaseChanged;

            FlagCheckedCommand = new FlagCheckedCommand(mainValue);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            IsChecked = false;
            FlagName = flagInfo.Name;
            ToolTip = CalculateToolTip();

            FlagValue = new SmartValue
            {
                Value = flagInfo.Value,
                NumericalBase = mainValue.NumericalBase,
                BitCount = mainValue.BitCount
            };
        }

        private IEnumerable<int> GetSelectedIndexes()
        {
            ulong v = flagInfo.Value;
            int index = -1;

            while (v != 0)
            {
                index++;

                ulong bitValue = v % 2;
                v = v / 2;

                if (bitValue == 1)
                    yield return index;
            }
        }

        private void HandleNumericalBaseChanged(object sender, EventArgs eventArgs)
        {
            FlagValue = new SmartValue
            {
                Value = flagInfo.Value,
                NumericalBase = mainValue.NumericalBase,
                BitCount = mainValue.BitCount
            };
        }

        private string CalculateToolTip()
        {
            IEnumerable<int> indexes = GetSelectedIndexes();

            string bitListCsv = string.Join(", ", indexes);

            if (string.IsNullOrEmpty(bitListCsv))
                bitListCsv = "<none>";

            return "Bit: " + bitListCsv;
        }

        public override string ToString()
        {
            return FlagName;
        }
    }
}