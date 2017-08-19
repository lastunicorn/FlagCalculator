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

namespace DustInTheWind.FlagCalculator.Business
{
    internal static class ObjectExtensions
    {
        public static ulong ToUInt64Value(this object rawValue)
        {
            if (rawValue is ulong)
                return (ulong)rawValue;

            if (rawValue is long)
                return (ulong)(long)rawValue;

            if (rawValue is uint)
                return (ulong)(uint)rawValue;

            if (rawValue is int)
                return (ulong)(int)rawValue;

            if (rawValue is ushort)
                return (ulong)(ushort)rawValue;

            if (rawValue is short)
                return (ulong)(short)rawValue;

            if (rawValue is byte)
                return (ulong)(byte)rawValue;

            if (rawValue is sbyte)
                return (ulong)(sbyte)rawValue;

            throw new Exception("Error casting the object to UInt64.");
        }
    }
}