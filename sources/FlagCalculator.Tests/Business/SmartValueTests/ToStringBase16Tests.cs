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

namespace DustInTheWind.FlagCalculator.Tests.Business.SmartValueTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ToStringBase16Tests
    {
        [Test]
        public void does_not_add_zeroes_to_the_left_if_BitCount_0()
        {
            SmartNumber smartValue = new SmartNumber
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 0
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("D"));
        }

        [Test]
        public void does_not_add_zeroes_to_the_left_if_BitCount_less_then_real_digit_count()
        {
            SmartNumber smartValue = new SmartNumber
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 2
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("D"));
        }

        [Test]
        public void adds_zeroes_to_the_left_if_BitCount_is_more_then_real_digit_count()
        {
            SmartNumber smartValue = new SmartNumber
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 16
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("00 0D"));
        }

        [Test]
        public void does_not_add_zeroes_to_the_left_if_PadLeft_is_false()
        {
            SmartNumber smartValue = new SmartNumber
            {
                Value = 13,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 16,
                PadLeft = false
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("D"));
        }

        [Test]
        public void groups_digits_by_two_no_padding()
        {
            SmartNumber smartValue = new SmartNumber
            {
                Value = 1000,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 16,
                PadLeft = false
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("3 E8"));
        }

        [Test]
        public void groups_digits_by_two_with_padding()
        {
            SmartNumber smartValue = new SmartNumber
            {
                Value = 1000,
                NumericalBase = NumericalBase.Hexadecimal,
                BitCount = 16
            };

            string actual = smartValue.ToString();

            Assert.That(actual, Is.EqualTo("03 E8"));
        }
    }
}
