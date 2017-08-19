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
    internal class FlagsNumber : IEnumerable<FlagItem>
    {
        private ulong value;
        private readonly List<FlagItem> flags;

        public string Name { get; }
        public int BitCount { get; }
        public Type UnderlyingType { get; }

        public ulong Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnValueChanged();
            }
        }

        public event EventHandler ValueChanged;

        public FlagsNumber()
        {
            flags = new List<FlagItem>();
            Name = string.Empty;
        }

        public FlagsNumber(Type enumType)
        {
            Name = enumType.Name;
            UnderlyingType = enumType.GetEnumUnderlyingType();
            BitCount = Marshal.SizeOf(UnderlyingType) * 8;
            flags = BuildListOfFlags(enumType);
        }

        private List<FlagItem> BuildListOfFlags(Type enumType)
        {
            FieldInfo[] fieldInfos = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

            return fieldInfos
                .Select(x =>
                {
                    string name = x.Name;
                    ulong value = x.GetRawConstantValue().ToUInt64Value();

                    return new FlagItem(this, name, value);
                })
                .ToList();
        }

        public IEnumerator<FlagItem> GetEnumerator()
        {
            return flags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
