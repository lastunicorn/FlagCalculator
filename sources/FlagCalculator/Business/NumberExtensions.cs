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
        #region int

        public static string ToStringDecimal(this int value)
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }

        public static string ToStringHexa(this int value)
        {
            return value.ToString("X", CultureInfo.CurrentCulture);
        }

        public static string ToStringBinary(this int value)
        {
            List<char> chars = new List<char>(32 + (32 / 4 - 1));

            for (int i = 0; i < 32; i++)
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

        public static string ToStringBinary(this uint value)
        {
            List<char> chars = new List<char>(32 + (32 / 4 - 1));

            for (int i = 0; i < 32; i++)
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

        public static string ToStringBinary(this long value)
        {
            List<char> chars = new List<char>(64 + (64 / 4 - 1));

            for (int i = 0; i < 64; i++)
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

        public static string ToStringBinary(this ulong value)
        {
            List<char> chars = new List<char>(64 + (64 / 4 - 1));

            for (int i = 0; i < 64; i++)
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