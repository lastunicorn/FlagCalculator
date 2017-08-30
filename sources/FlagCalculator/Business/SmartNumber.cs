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

namespace DustInTheWind.FlagCalculator.Business
{
    internal class SmartNumber
    {
        public ulong Value { get; set; }
        public NumericalBase NumericalBase { get; set; }
        public int BitCount { get; set; }
        public bool PadLeft { get; set; }
        public int? GroupLength { get; set; }

        public int DigitCount
        {
            get
            {
                switch (NumericalBase)
                {
                    case NumericalBase.None:
                        return 0;

                    case NumericalBase.Binary:
                        return BitCount;

                    case NumericalBase.Decimal:
                        return 0;

                    case NumericalBase.Hexadecimal:
                        return BitCount / 4;

                    default:
                        return 0;
                }
            }
        }

        public SmartNumber()
        {
            PadLeft = true;
        }

        public override string ToString()
        {
            switch (NumericalBase)
            {
                case NumericalBase.None:
                    return string.Empty;

                case NumericalBase.Decimal:
                case NumericalBase.Hexadecimal:
                case NumericalBase.Binary:
                    return To();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string To()
        {
            IEnumerable<uint> digitsReversed = ToDigitsReversed();

            List<char> allChars = new List<char>();
            int digitCount = 0;

            int requiredGroupLength = GroupLength ?? CalculateDefaultGroupLength();

            foreach (uint digit in digitsReversed)
            {
                if (requiredGroupLength > 0 && digitCount > 0 && digitCount % requiredGroupLength == 0)
                    allChars.Add(' ');

                if (digit < 10)
                    allChars.Add((char)('0' + digit));
                else
                    allChars.Add((char)('A' + digit - 10));

                digitCount++;
            }

            if (PadLeft)
            {
                int neededDigitCount = DigitCount;

                while (digitCount < neededDigitCount)
                {
                    if (requiredGroupLength > 0 && digitCount > 0 && digitCount % requiredGroupLength == 0)
                        allChars.Add(' ');

                    allChars.Add('0');
                    digitCount++;
                }
            }

            allChars.Reverse();

            return string.Join(string.Empty, allChars);
        }

        private IEnumerable<uint> ToDigitsReversed()
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
                    uint digit = (uint)(v % (ulong)NumericalBase);
                    v /= (ulong)NumericalBase;

                    yield return digit;
                }
            }
        }

        private int CalculateDefaultGroupLength()
        {
            switch (NumericalBase)
            {
                case NumericalBase.None:
                    return 0;

                case NumericalBase.Binary:
                    return 4;

                case NumericalBase.Decimal:
                    return 3;

                case NumericalBase.Hexadecimal:
                    return 2;

                default:
                    return 0;
            }
        }
    }
}