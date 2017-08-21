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
using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagsNumberTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class ConstructorWithFlagsEnumTests
    {
        private FlagsNumber flagsNumber;

        [Flags]
        private enum FlagsEnum
        {
            None = 0,
            FlagZero = 1,
            FlagOne = 2,
            FlagTwo = 4,
            FlagThree = 8
        }

        [SetUp]
        public void SetUp()
        {
            flagsNumber = new FlagsNumber(typeof(FlagsEnum));
        }

        [Test]
        public void Value_is_zero()
        {
            Assert.That(flagsNumber.Value, Is.EqualTo(0));
        }

        [Test]
        public void Flags_list_contains_one_item_for_each_enum_value()
        {
            List<FlagItem> expectedValues = new List<FlagItem>
            {
                new FlagItem(flagsNumber, "None", 0),
                new FlagItem(flagsNumber, "FlagZero", 1),
                new FlagItem(flagsNumber, "FlagOne", 2),
                new FlagItem(flagsNumber, "FlagTwo", 4),
                new FlagItem(flagsNumber, "FlagThree", 8)
            };

            Assert.That(flagsNumber, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Name_is_the_name_of_the_enum()
        {
            Assert.That(flagsNumber.Name, Is.EqualTo("FlagsEnum"));
        }
    }
}
