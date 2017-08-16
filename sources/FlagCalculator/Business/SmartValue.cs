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

        public int[] ToDigits()
        {
            switch (NumericalBase)
            {
                case NumericalBase.None:
                    return new int[0];

                case NumericalBase.Decimal:
                case NumericalBase.Hexadecimal:
                    return ToDigits((int)NumericalBase);

                case NumericalBase.Binary:
                    return ToDigits((int)NumericalBase, BitCount);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int[] ToDigits(int numericalBase, int padding = 0)
        {
            IEnumerable<int> digits = ToDigitsInternal(numericalBase);
            List<int> allDigits = new List<int>(digits);

            while (padding > allDigits.Count)
                allDigits.Add(0);

            allDigits.Reverse();

            return allDigits.ToArray();
        }

        private IEnumerable<int> ToDigitsInternal(int numericalBase)
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

        public string ToSimpleString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.None:
                    return string.Empty;

                case NumericalBase.Decimal:
                    return ToSimpleStringDecimal();

                case NumericalBase.Hexadecimal:
                    return ToSimpleStringHexa();

                case NumericalBase.Binary:
                    return ToSimpleStringBinary();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string ToSimpleStringDecimal()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        private string ToSimpleStringHexa()
        {
            IEnumerable<int> digitsReversed = ToDigitsInternal(16);

            List<char> allChars = new List<char>();

            foreach (int digit in digitsReversed)
            {
                if (digit < 10)
                    allChars.Add((char)('0' + digit));

                if (digit >= 10)
                    allChars.Add((char)('A' + digit - 10));
            }

            allChars.Reverse();

            return string.Join(string.Empty, allChars);
        }

        private string ToSimpleStringBinary()
        {
            IEnumerable<int> digitsReversed = ToDigitsInternal(2);

            List<char> allChars = new List<char>();
            int digitCount = 0;

            foreach (int digit in digitsReversed)
            {
                allChars.Add(digit == 0 ? '0' : '1');
                digitCount++;
            }

            while (digitCount < BitCount)
            {
                allChars.Add('0');
                digitCount++;
            }

            allChars.Reverse();

            return string.Join(string.Empty, allChars);
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
            IEnumerable<int> digitsReversed = ToDigitsInternal(16);

            List<char> allChars = new List<char>();
            int digitCount = 0;

            foreach (int digit in digitsReversed)
            {
                if (digitCount > 0 && digitCount % 2 == 0)
                    allChars.Add(' ');

                if (digit < 10)
                    allChars.Add((char)('0' + digit));

                if (digit >= 10)
                    allChars.Add((char)('A' + digit - 10));

                digitCount++;
            }

            while (digitCount < BitCount / 4)
            {
                if (digitCount > 0 && digitCount % 2 == 0)
                    allChars.Add(' ');

                allChars.Add('0');

                digitCount++;
            }

            allChars.Reverse();

            return string.Join(string.Empty, allChars);
        }

        private string ToStringBinary()
        {
            ulong v = Value;
            int bitCount = BitCount;

            if (bitCount == 0)
                return "0";

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