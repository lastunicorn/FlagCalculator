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
        private Type underlyingType;
        public string Name { get; private set; }

        public int BitCount
        {
            get { return underlyingType == null ? 0 : Marshal.SizeOf(underlyingType) * 8; }
        }

        public string UnderlyingTypeName
        {
            get { return underlyingType == null ? string.Empty : underlyingType.Name; }
        }

        public FlagInfoCollection()
        {
            flags = new List<FlagInfo>();
        }

        public IEnumerator<FlagInfo> GetEnumerator()
        {
            return flags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static FlagInfoCollection FromEnum(Type enumType)
        {
            IEnumerable<FlagInfo> list = BuildListOfFields(enumType);
            return ToFlagInfoCollection(list, enumType);
        }

        private static IEnumerable<FlagInfo> BuildListOfFields(Type enumType)
        {
            FieldInfo[] fieldInfos = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

            return fieldInfos
                .Select(x => new FlagInfo
                {
                    Value = (ulong)Convert.ChangeType(x.GetRawConstantValue(), typeof(ulong)),
                    Name = x.Name
                })
                .ToList();
        }

        private static FlagInfoCollection ToFlagInfoCollection(IEnumerable<FlagInfo> flagInfos, Type enumType)
        {
            FlagInfoCollection flagInfoCollection = new FlagInfoCollection
            {
                Name = enumType.Name,
                underlyingType = enumType.GetEnumUnderlyingType()
            };

            foreach (FlagInfo flagInfo in flagInfos)
                flagInfoCollection.flags.Add(flagInfo);

            return flagInfoCollection;
        }
    }
}