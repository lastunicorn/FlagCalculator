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

using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.SmartValueTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ToSimpleStringBase10Tests
    {
        [Test]
        public void does_not_add_zeroes_to_the_left_if_BitCount_0()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 0
            };

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        [Test]
        public void does_not_add_zeroes_to_the_left_if_BitCount_less_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 2
            };

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        [Test]
        public void adds_zeroes_to_the_left_if_BitCount_is_more_then_real_digit_count()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 16
            };

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.EqualTo("13"));
        }

        [Test]
        public void does_not_add_zeroes_to_the_left_if_PadLeft_is_false()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.Decimal,
                BitCount = 16,
                PadLeft = false
            };

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.EqualTo("13"));
        }
    }
}
