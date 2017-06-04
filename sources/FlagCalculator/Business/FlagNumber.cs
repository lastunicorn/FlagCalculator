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
        public event EventHandler BaseChanged;

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

        private void OnBaseChanged()
        {
            BaseChanged?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.Decimal:
                    return value.ToString(CultureInfo.CurrentCulture);

                case NumericalBase.Hexadecimal:
                    return value.ToString("X");

                case NumericalBase.Binary:
                    return ToBinaryString();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string ToBinaryString()
        {
            ulong v = value;
            List<char> chars = new List<char>(BitCount + (BitCount / 4 - 1));

            for (int i = 0; i < BitCount; i++)
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
    }
}