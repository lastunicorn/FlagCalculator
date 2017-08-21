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
        public bool PadLeft { get; set; }

        public SmartValue()
        {
            PadLeft = true;
        }

        public string ToSimpleString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.None:
                    return string.Empty;

                case NumericalBase.Decimal:
                    return ToSimpleStringDecimal();

                case NumericalBase.Hexadecimal:
                    return ToStringHexa();

                case NumericalBase.Binary:
                    return ToStringBinary();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string ToSimpleStringDecimal()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.None:
                    return string.Empty;

                case NumericalBase.Decimal:
                    return ToStringDecimal();

                case NumericalBase.Hexadecimal:
                    return ToStringHexa(2);

                case NumericalBase.Binary:
                    return ToStringBinary(4);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string ToStringDecimal()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }

        private string ToStringHexa(int groupLength = 0)
        {
            IEnumerable<int> digitsReversed = ToDigitsReversed(16);

            List<char> allChars = new List<char>();
            int digitCount = 0;

            foreach (int digit in digitsReversed)
            {
                if (groupLength > 0 && digitCount > 0 && digitCount % groupLength == 0)
                    allChars.Add(' ');

                if (digit < 10)
                    allChars.Add((char)('0' + digit));

                if (digit >= 10)
                    allChars.Add((char)('A' + digit - 10));

                digitCount++;
            }

            if (PadLeft)
                while (digitCount < BitCount / 4)
                {
                    if (groupLength > 0 && digitCount > 0 && digitCount % groupLength == 0)
                        allChars.Add(' ');

                    allChars.Add('0');
                    digitCount++;
                }

            allChars.Reverse();

            return string.Join(string.Empty, allChars);
        }

        private string ToStringBinary(int groupLength = 0)
        {
            IEnumerable<int> digitsReversed = ToDigitsReversed(2);

            List<char> allChars = new List<char>();
            int digitCount = 0;

            foreach (int digit in digitsReversed)
            {
                if (groupLength > 0 && digitCount > 0 && digitCount % groupLength == 0)
                    allChars.Add(' ');

                allChars.Add(digit == 0 ? '0' : '1');
                digitCount++;
            }

            if (PadLeft)
                while (digitCount < BitCount)
                {
                    if (groupLength > 0 && digitCount > 0 && digitCount % groupLength == 0)
                        allChars.Add(' ');

                    allChars.Add('0');
                    digitCount++;
                }

            allChars.Reverse();

            return string.Join(string.Empty, allChars);
        }

        private IEnumerable<int> ToDigitsReversed(int numericalBase)
        {
            if (Value == 0)
            {
                yield return 0;
            }
            else
            {
                ulong v = Value;

                while (v != 0)
                {
                    int digit = (int)(v % (ulong)numericalBase);
                    v /= (ulong)numericalBase;

                    yield return digit;
                }
            }
        }
    }
}