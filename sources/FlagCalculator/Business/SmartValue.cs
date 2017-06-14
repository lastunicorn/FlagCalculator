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
    internal class SmartValue
    {
        public ulong Value { get; set; }
        public NumericalBase NumericalBase { get; set; }
        public int BitCount { get; set; }

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
            return Value.ToString(CultureInfo.CurrentCulture);
        }

        private string ToStringHexa()
        {
            return Value.ToString("X", CultureInfo.CurrentCulture);
        }

        private string ToStringBinary()
        {
            ulong v = Value;
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
    }
}