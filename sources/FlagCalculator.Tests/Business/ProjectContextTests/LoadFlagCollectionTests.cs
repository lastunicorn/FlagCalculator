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
using Moq;
using NUnit.Framework;

namespace DustInTheWind.FlagCalculator.Tests.Business.ProjectContextTests
{
    [TestFixture]
    [Parallelizable]
    public class LoadFlagCollectionTests
    {
        private ProjectContext projectContext;
        
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
            projectContext = new ProjectContext();
        }

        #region EnumProvider return null Enum Type

        [Test]
        public void EnumProvider_returns_null_enum_Type_FlagsNumber_is_not_changed()
        {
            FlagsNumber initialFlagsNumber = projectContext.FlagsNumber;

            projectContext.LoadFlagCollection(null);

            Assert.That(projectContext.FlagsNumber, Is.SameAs(initialFlagsNumber));
        }

        [Test]
        public void EnumProvider_returns_null_enum_Type_FlagsNumberChanged_event_is_not_raised()
        {
            bool eventWasRaised = false;
            projectContext.FlagsNumberChanged += (o, e) => eventWasRaised = true;

            projectContext.LoadFlagCollection(null);

            Assert.That(eventWasRaised, Is.False);
        }

        [Test]
        public void EnumProvider_returns_null_enum_Type_Loaded_event_is_not_raised()
        {
            bool eventWasRaised = false;
            projectContext.Loaded += (o, e) => eventWasRaised = true;

            projectContext.LoadFlagCollection(null);

            Assert.That(eventWasRaised, Is.False);
        }

        #endregion

        #region EnumProvider returns valid Enum Type

        [Test]
        public void creates_new_FlagsNumber()
        {
            FlagsNumber initialFlagsNumber = projectContext.FlagsNumber;

            projectContext.LoadFlagCollection(typeof(FlagsEnum));

            Assert.That(projectContext.FlagsNumber, Is.Not.SameAs(initialFlagsNumber));
        }

        [Test]
        public void FlagsNumberChanged_event_is_raised()
        {
            bool eventWasRaised = false;
            projectContext.FlagsNumberChanged += (o, e) => eventWasRaised = true;

            projectContext.LoadFlagCollection(typeof(FlagsEnum));

            Assert.That(eventWasRaised, Is.True);
        }

        [Test]
        public void FlagsNumberChanged_event_contains_old_FlagsNumber()
        {
            FlagsNumberChangedEventArgs args = null;
            FlagsNumber oldFlagsNumber = projectContext.FlagsNumber;
            projectContext.FlagsNumberChanged += (o, e) => args = e;

            projectContext.LoadFlagCollection(typeof(FlagsEnum));

            Assert.That(args.OldValue, Is.SameAs(oldFlagsNumber));
        }

        [Test]
        public void FlagsNumberChanged_event_contains_new_FlagsNumber()
        {
            FlagsNumberChangedEventArgs args = null;
            projectContext.FlagsNumberChanged += (o, e) => args = e;

            projectContext.LoadFlagCollection(typeof(FlagsEnum));

            Assert.That(args.NewValue, Is.SameAs(projectContext.FlagsNumber));
        }

        [Test]
        public void Loaded_event_is_raised()
        {
            bool eventWasRaised = false;
            projectContext.Loaded += (o, e) => eventWasRaised = true;

            projectContext.LoadFlagCollection(typeof(FlagsEnum));

            Assert.That(eventWasRaised, Is.True);
        }

        #endregion
    }
}
