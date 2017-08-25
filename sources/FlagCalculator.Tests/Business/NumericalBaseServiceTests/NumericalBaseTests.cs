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
    public class NumericalBaseTests
    {
        private NumericalBaseService numericalBaseService;

        [SetUp]
        public void SetUp()
        {
            numericalBaseService = new NumericalBaseService();
        }

        [Test]
        public void returns_value_that_is_set()
        {
            numericalBaseService.NumericalBase = NumericalBase.Binary;

            Assert.That(numericalBaseService.NumericalBase, Is.EqualTo(NumericalBase.Binary));
        }

        [Test]
        public void when_value_is_set_raises_NumericalBaseChanged_event()
        {
            bool eventWasRaised = false;
            numericalBaseService.NumericalBaseChanged += (s, e) => eventWasRaised = true;

            numericalBaseService.NumericalBase = NumericalBase.Binary;

            Assert.That(eventWasRaised, Is.True);
        }
    }
}
