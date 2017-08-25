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
    [Parallelizable]
    public class ClearTests
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
        public void initially_no_flags_are_selected()
        {
            flagsNumber.Clear();

            Assert.That(flagsNumber.Value, Is.EqualTo(0));
        }

        [Test]
        public void initially_FlagOne_is_selected()
        {
            flagsNumber.Value = 2; // FlagOne

            flagsNumber.Clear();

            Assert.That(flagsNumber.Value, Is.EqualTo(0));
        }

        [Test]
        public void initially_all_flags_are_selected()
        {
            flagsNumber.Value = 15; // all flags

            flagsNumber.Clear();

            Assert.That(flagsNumber.Value, Is.EqualTo(0));
        }
    }
}
