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

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagItemTests
{
    [TestFixture]
    public class IsSetTests
    {
        private FlagItem flagItem;

        [SetUp]
        public void SetUp()
        {
            flagItem = new FlagItem(new FlagsNumber(), "name1", 13);
        }

        [Test]
        public void initially_IsSet_return_false()
        {
            Assert.That(flagItem.IsSet, Is.False);
        }

        [Test]
        public void after_Set_method_is_called_IsSet_returns_true()
        {
            flagItem.Set();

            Assert.That(flagItem.IsSet, Is.True);
        }

        [Test]
        public void after_Set_method_is_called_twice_IsSet_returns_true()
        {
            flagItem.Set();
            flagItem.Set();

            Assert.That(flagItem.IsSet, Is.True);
        }

        [Test]
        public void after_Reset_method_is_called_IsSet_returns_false()
        {
            flagItem.Reset();

            Assert.That(flagItem.IsSet, Is.False);
        }

        [Test]
        public void after_Reset_method_is_called_twice_IsSet_returns_false()
        {
            flagItem.Reset();
            flagItem.Reset();

            Assert.That(flagItem.IsSet, Is.False);
        }

        [Test]
        public void after_Set_and_Reset_methods_are_called_IsSet_returns_false()
        {
            flagItem.Set();
            flagItem.Reset();

            Assert.That(flagItem.IsSet, Is.False);
        }

        [TestCase(0UL, 0UL, true)] // bit <none> - bit <none> => true
        [TestCase(8UL, 8UL, true)] // bit 3 - bit 3 => true
        [TestCase(10UL, 8UL, true)] // bit 1,3 - bit 3 => true
        [TestCase(8UL, 2UL, false)] // bit 3 - bit 1 => false
        [TestCase(8UL, 10UL, false)] // bit 3 - bit 1,3 => false
        [TestCase(8UL, 0UL, false)] // bit 3 - bit <none> => false
        public void test_IsSet_for_different_parent_and_flag_values(ulong parentValue, ulong flagValue, bool expected)
        {
            FlagsNumber parent = new FlagsNumber
            {
                Value = parentValue
            };
            FlagItem flagItem = new FlagItem(parent, "name1", flagValue);

            Assert.That(flagItem.IsSet, Is.EqualTo(expected));
        }
    }
}
