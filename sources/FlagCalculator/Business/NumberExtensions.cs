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

using System.Collections.Generic;
using System.Globalization;

namespace DustInTheWind.FlagCalculator.Business
{
    internal static class NumberExtensions
    {
        #region byte

        public static string ToStringDecimal(this byte value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this byte value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this byte value, int bitCount = 8)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = (byte)(value >> 1);
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region sbyte

        public static string ToStringDecimal(this sbyte value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this sbyte value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this sbyte value, int bitCount = 8)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = (sbyte)(value >> 1);
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region short

        public static string ToStringDecimal(this short value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this short value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this short value, int bitCount = 16)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = (short)(value >> 1);
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region ushort

        public static string ToStringDecimal(this ushort value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this ushort value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this ushort value, int bitCount = 16)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = (ushort)(value >> 1);
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region int

        public static string ToStringDecimal(this int value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this int value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this int value, int bitCount = 32)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = value >> 1;
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region uint

        public static string ToStringDecimal(this uint value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this uint value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this uint value, int bitCount = 32)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = value >> 1;
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region long

        public static string ToStringDecimal(this long value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this long value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this long value, int bitCount = 64)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = value >> 1;
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion

        #region ulong

        public static string ToStringDecimal(this ulong value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this ulong value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this ulong value, int bitCount = 64)
        {
            List<char> chars = new List<char>(bitCount + (bitCount / 4 - 1));

            for (int i = 0; i < bitCount; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = value >> 1;
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        #endregion
    }
}