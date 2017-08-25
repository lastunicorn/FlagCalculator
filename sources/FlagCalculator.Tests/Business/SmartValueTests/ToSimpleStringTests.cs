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

namespace DustInTheWind.FlagCalculator.Tests.Business.SmartValueTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ToSimpleStringTests
    {
        [Test]
        public void throws_if_NumericalBase_has_invalid_value()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = (NumericalBase)1000,
                BitCount = 16,
                PadLeft = false
            };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                smartValue.ToSimpleString();
            });
        }

        [Test]
        public void returns_string_empty_if_numerical_base_is_None()
        {
            SmartValue smartValue = new SmartValue
            {
                Value = 13,
                NumericalBase = NumericalBase.None,
                BitCount = 16
            };

            string actual = smartValue.ToSimpleString();

            Assert.That(actual, Is.Empty);
        }
    }
}
