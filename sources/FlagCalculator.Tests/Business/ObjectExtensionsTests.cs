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

namespace DustInTheWind.FlagCalculator.Tests.Business
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void CastByteToULong()
        {
            object o = (byte)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastSByteToULong()
        {
            object o = (sbyte)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastShortToULong()
        {
            object o = (short)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastUShortToULong()
        {
            object o = (ushort)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastIntToULong()
        {
            object o = (int)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastUIntToULong()
        {
            object o = (uint)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastLongToULong()
        {
            object o = (long)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }

        [Test]
        public void CastULongToULong()
        {
            object o = (ulong)10;

            ulong actual = o.ToUInt64Value();

            Assert.That(actual, Is.EqualTo((ulong)10));
        }
    }
}
