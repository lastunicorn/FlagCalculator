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

namespace DustInTheWind.FlagCalculator.Business
{
    internal class MainValue
    {
        private readonly NumericalBaseService numericalBaseService;
        private ulong value;

        private ulong Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnValueChanged();
            }
        }

        public int BitCount { get; set; }

        public event EventHandler ValueChanged;

        public MainValue(NumericalBaseService numericalBaseService)
        {
            if (numericalBaseService == null) throw new ArgumentNullException(nameof(numericalBaseService));
            this.numericalBaseService = numericalBaseService;
        }

        public void AddFlags(ulong flags)
        {
            Value = value | flags;
        }

        public void RemoveFlags(ulong flags)
        {
            Value = value & ~flags;
        }

        public void Clear()
        {
            Value = 0;
        }

        public bool IsFlagSet(ulong flags)
        {
            return flags == 0
                ? value == 0
                : (value & flags) == flags;
        }

        public void AddDigit(uint digit)
        {
            NumericalBase numericalBase = numericalBaseService.NumericalBase;

            if (digit >= (uint)numericalBase)
                return;

            Value = Value * (uint)numericalBase + digit;
        }

        public void SetValue(string newValue)
        {
            try
            {
                int numericalBase = (int)numericalBaseService.NumericalBase;
                ulong uInt64 = Convert.ToUInt64(newValue.Replace(" ", string.Empty), numericalBase);
                Value = uInt64;
            }
            catch { }
        }

        public void SetValue(ulong newValue)
        {
            Value = newValue;
        }

        public SmartValue ToSmartValue()
        {
            return new SmartValue
            {
                Value = value,
                NumericalBase = numericalBaseService.NumericalBase,
                BitCount = BitCount
            };
        }

        public override string ToString()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = value,
                NumericalBase = numericalBaseService.NumericalBase,
                BitCount = BitCount
            };

            return smartValue.ToString();
        }

        private void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}