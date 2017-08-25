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

namespace DustInTheWind.FlagCalculator.Tests.Business.ProjectContextTests
{
    [TestFixture]
    [Parallelizable]
    public class DisplayUnselectedTests
    {
        private ProjectContext projectContext;

        [SetUp]
        public void SetUp()
        {
            projectContext = new ProjectContext();
        }

        [Test]
        public void initially_is_false()
        {
            Assert.That(projectContext.DisplayUnselected, Is.False);
        }

        [Test]
        public void is_true_if_previously_set_to_true()
        {
            projectContext.DisplayUnselected = true;

            Assert.That(projectContext.DisplayUnselected, Is.True);
        }

        [Test]
        public void is_false_after_setting_it_to_successfully_to_true_and_false()
        {
            projectContext.DisplayUnselected = true;
            projectContext.DisplayUnselected = false;

            Assert.That(projectContext.DisplayUnselected, Is.False);
        }

        [Test]
        public void when_set_resets_DisplaySelected_if_it_was_true()
        {
            projectContext.DisplaySelected = true;
            projectContext.DisplayUnselected = true;

            Assert.That(projectContext.DisplaySelected, Is.False);
        }

        [Test]
        public void when_set_does_not_change_DisplaySelected_if_it_was_false()
        {
            projectContext.DisplaySelected = false;
            projectContext.DisplayUnselected = true;

            Assert.That(projectContext.DisplaySelected, Is.False);
        }

        [Test]
        public void wen_set_raises_DisplaySelectedChanged_event()
        {
            bool eventWasRaised = false;
            projectContext.DisplaySelectedChanged += (o, e) => eventWasRaised = true;

            projectContext.DisplayUnselected = true;

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void wen_set_second_time_does_not_raise_DisplaySelectedChanged_event()
        {
            bool eventWasRaised = false;
            projectContext.DisplayUnselected = true;
            projectContext.DisplaySelectedChanged += (o, e) => eventWasRaised = true;

            projectContext.DisplayUnselected = true;

            Assert.That(eventWasRaised, Is.False);
        }
    }
}
