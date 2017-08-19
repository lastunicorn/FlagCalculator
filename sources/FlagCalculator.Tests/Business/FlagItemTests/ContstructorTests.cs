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

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagItemTests
{
    [TestFixture]
    internal class ContstructorTests
    {
        [Test]
        public void Name_has_the_value_set_on_constructor()
        {
            FlagItem flagItem = new FlagItem(new FlagsNumber(), "name1", 13);

            Assert.That(flagItem.Name, Is.EqualTo("name1"));
        }

        [Test]
        public void Value_has_the_value_set_on_constructor()
        {
            FlagItem flagItem = new FlagItem(new FlagsNumber(), "name2", 23);

            Assert.That(flagItem.Value, Is.EqualTo(23));
        }

        [Test]
        public void throws_if_name_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FlagItem(new FlagsNumber(), null, 23);
            });
        }

        [Test]
        public void throws_if_parent_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FlagItem(null, "name2", 23);
            });
        }

        [Test]
        public void IsSet_returns_false()
        {
            FlagItem flagItem = new FlagItem(new FlagsNumber(), "name1", 13);

            bool actual = flagItem.IsSet;

            Assert.That(actual, Is.False);
        }
    }
}
