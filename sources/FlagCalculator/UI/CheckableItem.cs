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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class CheckableItem : ViewModelBase
    {
        private readonly FlagNumber flagNumber;
        private readonly FlagInfo flagInfo;
        private bool isChecked;
        private Visibility visibility;
        private ulong value;
        private string text;
        private string valueAsString;
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

        public ulong Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public string ValueAsString
        {
            get { return valueAsString; }
            set
            {
                valueAsString = value;
                OnPropertyChanged();
            }
        }

        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
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

        public CheckableItem(FlagNumber flagNumber, FlagInfo flagInfo)
        {
            if (flagNumber == null) throw new ArgumentNullException(nameof(flagNumber));
            if (flagInfo == null) throw new ArgumentNullException(nameof(flagInfo));

            this.flagNumber = flagNumber;
            this.flagInfo = flagInfo;

            flagNumber.NumericalBaseChanged += HandleFlagNumberBaseChanged;

            FlagCheckedCommand = new FlagCheckedCommand(flagNumber);

            IsChecked = false;
            Value = flagInfo.Value;
            Text = flagInfo.Name;
            ValueAsString = CalculateValueAsString();
            ToolTip = CalculateToolTip();
        }

        private string CalculateValueAsString()
        {
            switch (flagNumber.NumericalBase)
            {
                case NumericalBase.Decimal:
                    return flagInfo.Value.ToStringDecimal();

                case NumericalBase.Hexadecimal:
                    return flagInfo.Value.ToStringHexa();

                case NumericalBase.Binary:
                    return flagInfo.Value.ToStringBinary(flagNumber.BitCount);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<int> GetSelectedIndexes()
        {
            ulong v = value;
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
            ValueAsString = CalculateValueAsString();
            ToolTip = CalculateToolTip();
        }

        private string CalculateToolTip()
        {
            IEnumerable<int> indexes = GetSelectedIndexes();

            if (!indexes.Any())
                return string.Empty;

            return "bit: " + string.Join(", ", indexes);
        }

        public override string ToString()
        {
            return Text;
        }
    }
}