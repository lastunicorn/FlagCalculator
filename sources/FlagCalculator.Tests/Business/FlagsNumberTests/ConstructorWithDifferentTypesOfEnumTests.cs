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
using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagsNumberTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ConstructorWithDifferentTypesOfEnumTests
    {
        [Flags]
        enum SByteEnum : sbyte
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = sbyte.MaxValue
        }

        [Flags]
        enum ByteEnum : byte
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = byte.MaxValue
        }

        [Flags]
        enum ShortEnum : short
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = short.MaxValue
        }

        [Flags]
        enum UShortEnum : ushort
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = ushort.MaxValue
        }

        [Flags]
        enum IntEnum : int
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = int.MaxValue
        }

        [Flags]
        enum UIntEnum : uint
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = uint.MaxValue
        }

        [Flags]
        enum LongEnum : long
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = long.MaxValue
        }

        [Flags]
        enum ULongEnum : ulong
        {
            None = 0,
            Value1 = 1,
            Value2 = 1 << 1,
            Value3 = 1 << 2,
            All = ulong.MaxValue
        }

        #region BitCount

        [Test]
        public void BitCount_is_8_for_byte_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(ByteEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(8));
        }

        [Test]
        public void BitCount_is_8_for_sbyte_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(SByteEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(8));
        }

        [Test]
        public void BitCount_is_16_for_short_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(ShortEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(16));
        }

        [Test]
        public void BitCount_is_16_for_ushort_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(UShortEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(16));
        }

        [Test]
        public void BitCount_is_32_for_int_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(IntEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(32));
        }

        [Test]
        public void BitCount_is_32_for_uint_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(UIntEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(32));
        }

        [Test]
        public void BitCount_is_64_for_long_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(LongEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(64));
        }

        [Test]
        public void BitCount_is_64_for_ulong_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(ULongEnum));
            Assert.That(flagsNumber.BitCount, Is.EqualTo(64));
        }

        #endregion

        #region UnderlyingType

        [Test]
        public void UnderlyingType_is_byte_for_byte_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(ByteEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(byte)));
        }

        [Test]
        public void UnderlyingType_is_sbyte_for_sbyte_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(SByteEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(sbyte)));
        }

        [Test]
        public void UnderlyingType_is_short_for_short_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(ShortEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(short)));
        }

        [Test]
        public void UnderlyingType_is_ushort_for_ushort_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(UShortEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(ushort)));
        }

        [Test]
        public void UnderlyingType_is_int_for_int_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(IntEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(int)));
        }

        [Test]
        public void UnderlyingType_is_uint_for_uint_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(UIntEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(uint)));
        }

        [Test]
        public void UnderlyingType_is_long_for_long_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(LongEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(long)));
        }

        [Test]
        public void UnderlyingType_is_ulong_for_ulong_enum()
        {
            FlagsNumber flagsNumber = new FlagsNumber(typeof(ULongEnum));
            Assert.That(flagsNumber.UnderlyingType, Is.EqualTo(typeof(ulong)));
        }

        #endregion
    }
}
