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

namespace DustInTheWind.FlagCalculator.Tests.Business.StatusInfoTests
{
    [TestFixture]
    public class DefaultStatusTextTests
    {
        private StatusInfo statusInfo;

        [SetUp]
        public void SetUp()
        {
            statusInfo = new StatusInfo();
        }

        [Test]
        public void when_setting_new_value_raises_StatusTextChanged_event()
        {
            bool eventWasRaised = false;
            statusInfo.StatusTextChanged += (s, e) => eventWasRaised = true;

            statusInfo.DefaultStatusText = "status1";

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void when_setting_same_value_does_not_raises_StatusTextChanged_event()
        {
            bool eventWasRaised = false;
            statusInfo.DefaultStatusText = "status1";
            statusInfo.StatusTextChanged += (s, e) => eventWasRaised = true;

            statusInfo.DefaultStatusText = "status1";

            Assert.That(eventWasRaised, Is.False);
        }
    }
}
