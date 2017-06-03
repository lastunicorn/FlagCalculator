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
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class FlagCollectionProvider
    {
        public Func<Type, bool> OnEnumIsNotFlags { get; set; }

        public FlagInfoCollection LoadFlagCollection()
        {
            string enumTypeFullName = ConfigurationManager.AppSettings["enumTypeName"];

            if (enumTypeFullName == null)
                return new FlagInfoCollection();

            Type enumType = GetEnumeType(enumTypeFullName);
            List<FlagInfo> list = BuildListOfFields(enumType);

            return ToFlagCollection(list, enumType);
        }

        private Type GetEnumeType(string enumTypeFullName)
        {
            Type enumType = Type.GetType(enumTypeFullName);

            if (enumType == null)
                throw new ApplicationException(string.Format("The enum type '{0}' specified in the configuration file could not be loaded.", enumTypeFullName));

            if (!enumType.IsEnum)
                throw new ApplicationException("Specified type is not an enum.");

            if (Attribute.GetCustomAttribute(enumType, typeof(FlagsAttribute)) == null)
            {
                if (OnEnumIsNotFlags == null)
                    throw new ApplicationException("Specified type is not a flags enum.");

                bool allowToContinue = OnEnumIsNotFlags(enumType);

                if (!allowToContinue)
                    throw new ApplicationException("Specified type is not a flags enum.");
            }

            return enumType;
        }

        private static List<FlagInfo> BuildListOfFields(Type enumType)
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

        public static FlagInfoCollection ToFlagCollection(IEnumerable<FlagInfo> flagInfos, Type enumType)
        {
            FlagInfoCollection flagInfoCollection = new FlagInfoCollection
            {
                Name = enumType.Name,
                UnderlyingType = enumType.GetEnumUnderlyingType()
            };

            foreach (FlagInfo flagInfo in flagInfos)
                flagInfoCollection.Add(flagInfo);

            return flagInfoCollection;
        }
    }
}