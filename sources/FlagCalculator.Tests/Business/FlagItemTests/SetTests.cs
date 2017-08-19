﻿// FlagCalculator
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

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagItemTests
{
    [TestFixture]
    public class SetTests
    {
        [Test]
        public void if_parent_Value_is_default_and_flag_is_Set_parent_Value_is_equal_to_flag_Value()
        {
            FlagsNumber flagsNumber = new FlagsNumber();
            FlagItem flagItem = new FlagItem(flagsNumber, "name1", 8); // bit 3

            flagItem.Set();

            Assert.That(flagsNumber.Value, Is.EqualTo(8)); // bit 3
        }

        [TestCase(0UL, 0UL, 0UL)] // bit <none> - bit <none> - bit <none>
        [TestCase(0UL, 2UL, 2UL)] // bit <none> - bit 1 - bit 1
        [TestCase(2UL, 2UL, 2UL)] // bit 1 - bit 1 - bit 1
        [TestCase(2UL, 0UL, 2UL)] // bit 1 - bit <none> - bit 1
        [TestCase(2UL, 8UL, 10UL)] // bit 1 - bit 3 - bit 1,3
        [TestCase(2UL, 10UL, 10UL)] // bit 1 - bit 1,3 - bit 1,3
        [TestCase(2UL, 12UL, 14UL)] // bit 1 - bit 2,3 - bit 1,2,3
        public void sets_the_flag_Value_bits_into_the_parent_Value(ulong parentValue, ulong flagValue, ulong expectedParentValue)
        {
            FlagsNumber flagsNumber = new FlagsNumber
            {
                Value = parentValue
            };
            FlagItem flagItem = new FlagItem(flagsNumber, "name1", flagValue);

            flagItem.Set();

            Assert.That(flagsNumber.Value, Is.EqualTo(expectedParentValue));
        }
    }
}
