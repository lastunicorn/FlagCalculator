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
    internal class FlagCollectionProvider
    {
        public FlagCollection LoadFlagCollection()
        {
            string enumTypeFullName = ConfigurationManager.AppSettings["enumTypeName"];

            Type enumType = Type.GetType(enumTypeFullName);

            if (!enumType.IsEnum)
                throw new Exception("Specified type is not an enum.");

            if (Attribute.GetCustomAttribute(enumType, typeof(FlagsAttribute)) == null)
                Console.WriteLine("Specified type is not a flags enum.");

            Array enumValues = Enum.GetValues(enumType);

            FlagCollection flagCollection = new FlagCollection
            {
                Name = enumType.Name
            };

            for (int i = 0; i < enumValues.Length; i++)
            {
                object enumValue = enumValues.GetValue(i);

                FlagInfo flagInfo = new FlagInfo
                {
                    Value = (ulong)Convert.ChangeType(enumValue, typeof(ulong)),
                    Name = enumValue.ToString()
                };

                flagCollection.Add(flagInfo);
            }

            return flagCollection;
        }
    }
}