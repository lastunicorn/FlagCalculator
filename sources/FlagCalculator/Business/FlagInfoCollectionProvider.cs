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
using System.Configuration;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class FlagInfoCollectionProvider
    {
        public Func<Type, bool> OnEnumIsNotFlags { get; set; }

        public FlagInfoCollection LoadFlagCollection()
        {
            string enumTypeFullName = ConfigurationManager.AppSettings["enumTypeName"];

            if (enumTypeFullName == null)
                return null;

            Type enumType = GetEnumType(enumTypeFullName);

            return new FlagInfoCollection(enumType);
        }

        private Type GetEnumType(string enumTypeFullName)
        {
            Type enumType = Type.GetType(enumTypeFullName);

            if (enumType == null)
                throw new ApplicationException($"The enum type '{enumTypeFullName}' specified in the configuration file could not be loaded.");

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
    }
}