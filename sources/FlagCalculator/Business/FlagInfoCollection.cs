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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class FlagInfoCollection : IEnumerable<FlagInfo>
    {
        private readonly List<FlagInfo> flags;

        public string Name { get; }
        public Type UnderlyingType { get; }
        public int BitCount { get; }

        public FlagInfoCollection(Type enumType)
        {
            Name = enumType.Name;
            UnderlyingType = enumType.GetEnumUnderlyingType();
            BitCount = Marshal.SizeOf(UnderlyingType) * 8;

            flags = BuildListOfFields(enumType);
        }

        private static List<FlagInfo> BuildListOfFields(Type enumType)
        {
            FieldInfo[] fieldInfos = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            Type enumUnderlyingType = enumType.GetEnumUnderlyingType();

            return fieldInfos
                .Select(x => new FlagInfo
                {
                    Value = ToUInt64Value(x.GetRawConstantValue(), enumUnderlyingType),
                    Name = x.Name
                })
                .ToList();
        }

        private static ulong ToUInt64Value(object rawValue, Type enumUnderlyingType)
        {
            if (enumUnderlyingType == typeof(ulong))
                return (ulong)rawValue;

            if (enumUnderlyingType == typeof(long))
                return (ulong)(long)rawValue;

            if (enumUnderlyingType == typeof(uint))
                return (ulong)(uint)rawValue;

            if (enumUnderlyingType == typeof(int))
                return (ulong)(int)rawValue;

            if (enumUnderlyingType == typeof(ushort))
                return (ulong)(ushort)rawValue;

            if (enumUnderlyingType == typeof(short))
                return (ulong)(short)rawValue;

            if (enumUnderlyingType == typeof(byte))
                return (ulong)(byte)rawValue;

            if (enumUnderlyingType == typeof(sbyte))
                return (ulong)(sbyte)rawValue;

            throw new Exception("The underlying type of the enum is unsupported.");
        }

        public IEnumerator<FlagInfo> GetEnumerator()
        {
            return flags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}