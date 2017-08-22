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
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal class CheckableItem : ViewModelBase
    {
        private readonly NumericalBaseService numericalBaseService;
        private readonly FlagItem flagItem;
        private string flagName;
        private string toolTip;
        private SmartValue flagValue;

        public bool IsChecked
        {
            get { return flagItem.IsSet; }
            set
            {
                if (flagItem.IsSet == value)
                    return;

                if (value)
                    flagItem.Set();
                else
                    flagItem.Reset();

                OnPropertyChanged();
            }
        }

        public string FlagName
        {
            get { return flagName; }
            set
            {
                flagName = value;
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

        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                toolTip = value;
                OnPropertyChanged();
            }
        }
        
        public StatusInfoCommand StatusInfoCommand { get; }

        public CheckableItem(NumericalBaseService numericalBaseService, FlagItem flagItem, StatusInfoCommand statusInfoCommand)
        {
            if (numericalBaseService == null) throw new ArgumentNullException(nameof(numericalBaseService));
            if (flagItem == null) throw new ArgumentNullException(nameof(flagItem));
            if (statusInfoCommand == null) throw new ArgumentNullException(nameof(statusInfoCommand));

            this.numericalBaseService = numericalBaseService;
            this.flagItem = flagItem;

            StatusInfoCommand = statusInfoCommand;

            IsChecked = false;
            FlagName = flagItem.Name;
            ToolTip = CalculateToolTip();

            UpdateFlagValue();

            numericalBaseService.NumericalBaseChanged += HandleNumericalBaseChanged;
        }

        private void HandleNumericalBaseChanged(object sender, EventArgs eventArgs)
        {
            UpdateFlagValue();
        }

        private void UpdateFlagValue()
        {
            FlagValue = new SmartValue
            {
                Value = flagItem.Value,
                NumericalBase = numericalBaseService.NumericalBase,
                BitCount = flagItem.Parent?.BitCount ?? 0
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

        private IEnumerable<int> GetSelectedIndexes()
        {
            ulong v = flagItem.Value;
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

        public override string ToString()
        {
            return FlagName;
        }
    }
}