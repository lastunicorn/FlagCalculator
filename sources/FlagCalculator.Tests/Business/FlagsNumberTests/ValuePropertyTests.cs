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
using System.Linq;
using DustInTheWind.FlagCalculator.Business;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.FlagsNumberTests
{
    [TestFixture]
    public class ValuePropertyTests
    {
        private FlagsNumber flagsNumber;

        [Flags]
        private enum FlagsEnum
        {
            None = 0,
            Flag0 = 1,
            Flag1 = 2,
            Falg2 = 4,
            Flag3 = 8
        }

        [SetUp]
        public void SetUp()
        {
            flagsNumber = new FlagsNumber(typeof(FlagsEnum));
        }

        [Test]
        public void get_Value_returns_value_set_with_set_Value()
        {
            flagsNumber.Value = 13;

            Assert.That(flagsNumber.Value, Is.EqualTo(13));
        }

        [Test]
        public void set_Value_raises_ValueChanged_event()
        {
            bool eventWasRaised = false;
            flagsNumber.ValueChanged += (s, e) => eventWasRaised = true;

            flagsNumber.Value = 20;

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void setting_Value_to_13_only_flags_3_2_and_0_are_set()
        {
            flagsNumber.Value = 13;

            bool[] actual = flagsNumber
                .Select(x => x.IsSet)
                .ToArray();
            bool[] expected = { false, true, false, true, true };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void setting_Value_to_zero_only_None_flag_is_set()
        {
            flagsNumber.Value = 13;

            flagsNumber.Value = 0;

            bool[] actual = flagsNumber
                .Select(x => x.IsSet)
                .ToArray();
            bool[] expected = { true, false, false, false, false };
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
