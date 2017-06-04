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

        private void HandleFlagNumberBaseChanged(object sender, EventArgs eventArgs)
        {
            ValueAsString = CalculateValueAsString();
        }

        public override string ToString()
        {
            return Text;
        }
    }
}