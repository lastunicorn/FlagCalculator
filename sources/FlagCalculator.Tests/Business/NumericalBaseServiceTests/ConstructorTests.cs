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
    public class ConstructorTests
    {
        [Test]
        public void NumericalBase_is_initially_Decimal()
        {
            NumericalBaseService numericalBaseService = new NumericalBaseService();

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Decimal));
        }
    }
}
