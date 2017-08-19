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

            return fieldInfos
                .Select(x => new FlagInfo
                {
                    Value = x.GetRawConstantValue().ToUInt64Value(),
                    Name = x.Name
                })
                .ToList();
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