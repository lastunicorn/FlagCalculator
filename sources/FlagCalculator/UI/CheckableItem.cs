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

namespace DustInTheWind.FlagCalculator.UI
{
    internal class CheckableItem : ViewModelBase
    {
        private readonly SmartNumber mainValue;
        private bool isChecked;
        private string text;
        private string toolTip;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        public string Text
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
        
        public FlagCheckedCommand FlagCheckedCommand { get; }

        private SmartNumber flagValue;

        public SmartNumber FlagValue
        {
            get { return flagValue; }
            private set
            {
                flagValue = value;
                OnPropertyChanged();
            }
        }

        public CheckableItem(SmartNumber mainValue, FlagInfo flagInfo)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));
            if (flagInfo == null) throw new ArgumentNullException(nameof(flagInfo));

            this.mainValue = mainValue;

            mainValue.NumericalBaseChanged += HandleFlagNumberBaseChanged;

            FlagCheckedCommand = new FlagCheckedCommand(mainValue);

            IsChecked = false;
            flagValue = new SmartNumber(flagInfo.Value)
            {
                NumericalBase = mainValue.NumericalBase,
                BitCount = mainValue.BitCount
            };
            Text = flagInfo.Name;
            ToolTip = CalculateToolTip();
        }

        private IEnumerable<int> GetSelectedIndexes()
        {
            ulong v = flagValue;
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

        private void HandleFlagNumberBaseChanged(object sender, EventArgs eventArgs)
        {
            flagValue.NumericalBase = mainValue.NumericalBase;
            FlagValue = flagValue;
        }

        private string CalculateToolTip()
        {
            IEnumerable<int> indexes = GetSelectedIndexes();

            string bitListCsv = string.Join(", ", indexes);

            if (string.IsNullOrEmpty(bitListCsv))
                bitListCsv = "<none>";

            return "bit: " + bitListCsv;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}