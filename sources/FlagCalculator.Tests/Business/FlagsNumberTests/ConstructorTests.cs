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

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagsNumberTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class ConstructorTests
    {
        private FlagsNumber flagsNumber;

        [SetUp]
        public void SetUp()
        {
            flagsNumber = new FlagsNumber();
        }

        [Test]
        public void Value_is_zero()
        {
            Assert.That(flagsNumber.Value, Is.EqualTo(0));
        }

        [Test]
        public void Flags_list_is_empty()
        {
            Assert.That(flagsNumber, Is.Empty);
        }

        [Test]
        public void Name_is_empty_string()
        {
            Assert.That(flagsNumber.Name, Is.Empty);
        }

        [Test]
        public void BitCount_is_zero()
        {
            Assert.That(flagsNumber.BitCount, Is.EqualTo(0));
        }

        [Test]
        public void UnderlyingType_is_zero()
        {
            Assert.That(flagsNumber.UnderlyingType, Is.Null);
        }
    }
}
