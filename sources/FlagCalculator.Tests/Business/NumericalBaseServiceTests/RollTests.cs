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

namespace DustInTheWind.FlagCalculator.Tests.Business.NumericalBaseServiceTests
{
    [TestFixture]
    [Parallelizable]
    public class RollTests
    {
        private NumericalBaseService numericalBaseService;

        [SetUp]
        public void SetUp()
        {
            numericalBaseService = new NumericalBaseService();
        }

        [Test]
        public void if_current_NumericalBase_is_None_sets_NumericalBase_to_Decimal()
        {
            numericalBaseService.NumericalBase = NumericalBase.None;

            numericalBaseService.Roll();

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Decimal));
        }

        [Test]
        public void if_current_NumericalBase_is_Decimal_sets_NumericalBase_to_Hexadecimal()
        {
            numericalBaseService.NumericalBase = NumericalBase.Decimal;

            numericalBaseService.Roll();

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Hexadecimal));
        }

        [Test]
        public void if_current_NumericalBase_is_Hexadecimal_sets_NumericalBase_to_Binary()
        {
            numericalBaseService.NumericalBase = NumericalBase.Hexadecimal;

            numericalBaseService.Roll();

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Binary));
        }

        [Test]
        public void if_current_NumericalBase_is_Binary_sets_NumericalBase_to_Decimal()
        {
            numericalBaseService.NumericalBase = NumericalBase.Binary;

            numericalBaseService.Roll();

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Decimal));
        }

        [Test]
        public void if_current_NumericalBase_is_invalid_value_sets_NumericalBase_to_Decimal()
        {
            numericalBaseService.NumericalBase = (NumericalBase)1000;

            numericalBaseService.Roll();

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Decimal));
        }
    }
}
