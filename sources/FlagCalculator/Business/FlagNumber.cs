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
    internal sealed class FlagNumber
    {
        private ulong value;
        private NumericalBase numericalBase;

        public ulong Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnValueChanged();
            }
        }

        public NumericalBase NumericalBase
        {
            get { return numericalBase; }
            private set
            {
                numericalBase = value;
                OnBaseChanged();
            }
        }

        public int BitCount { get; set; }

        public event EventHandler ValueChanged;
        public event EventHandler NumericalBaseChanged;

        public FlagNumber()
        {
            numericalBase = NumericalBase.Decimal;
            BitCount = 64;
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

        private void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void RollBase()
        {
            switch (numericalBase)
            {
                case NumericalBase.Decimal:
                    NumericalBase = NumericalBase.Hexadecimal;
                    break;

                case NumericalBase.Hexadecimal:
                    NumericalBase = NumericalBase.Binary;
                    break;

                case NumericalBase.Binary:
                    NumericalBase = NumericalBase.Decimal;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddDigit(uint digit)
        {
            if (digit >= (uint)numericalBase)
                return;

            Value = Value * (uint)numericalBase + digit;
        }

        public void SetValue(string newValue)
        {
            try
            {
                int @base = (int)numericalBase;
                ulong uInt64 = Convert.ToUInt64(newValue.Replace(" ", string.Empty), @base);
                Value = uInt64;
            }
            catch { }
        }

        private void OnBaseChanged()
        {
            NumericalBaseChanged?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.Decimal:
                    return value.ToStringDecimal();

                case NumericalBase.Hexadecimal:
                    return value.ToStringHexa();

                case NumericalBase.Binary:
                    return value.ToStringBinary();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}