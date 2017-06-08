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
using System.Globalization;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class SmartNumber
    {
        private ulong value;
        private NumericalBase numericalBase;

        private ulong Value
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
            set
            {
                numericalBase = value;
                OnNumericalBaseChanged();
            }
        }

        public int BitCount { get; set; }

        public event EventHandler ValueChanged;
        public event EventHandler NumericalBaseChanged;

        public SmartNumber()
            : this(0)
        {
        }

        public SmartNumber(ulong value)
        {
            this.value = value;
            numericalBase = NumericalBase.Decimal;
            BitCount = 64;
        }

        public static implicit operator ulong(SmartNumber smartNumber)
        {
            return smartNumber.value;
        }

        public static implicit operator SmartNumber(ulong value)
        {
            return new SmartNumber(value);
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

        public override string ToString()
        {
            return CalculateValueAsString();
        }

        private string CalculateValueAsString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.Decimal:
                    return ToStringDecimal();

                case NumericalBase.Hexadecimal:
                    return ToStringHexa();

                case NumericalBase.Binary:
                    return ToStringBinary();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string ToStringDecimal()
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        private string ToStringHexa()
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        private string ToStringBinary()
        {
            ulong v = value;
            int bitCount = BitCount;

            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (v & 1) == 1;
                chars.Add(bit ? '1' : '0');

                v = v >> 1;
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        private void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void OnNumericalBaseChanged()
        {
            NumericalBaseChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}