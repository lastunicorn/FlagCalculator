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
    internal class FlagItem
    {
        public FlagsNumber Parent { get; }
        public ulong Value { get; }
        public string Name { get; }

        public bool IsSet => Value == 0
            ? Parent.Value == 0
            : (Parent.Value & Value) == Value;

        public FlagItem(FlagsNumber parent, string name, ulong value)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (name == null) throw new ArgumentNullException(nameof(name));

            Parent = parent;
            Name = name;
            Value = value;
        }

        public void Set()
        {
            Parent.Value = Value == 0
                ? 0
                : Parent.Value | Value;
        }

        public void Reset()
        {
            Parent.Value = Parent.Value & ~Value;
        }

        public bool Equals(FlagItem flagItem)
        {
            return flagItem != null &&
                Parent == flagItem.Parent &&
                Value == flagItem.Value &&
                Name == flagItem.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FlagItem);
        }

        public override string ToString()
        {
            return $"{Name} - {Value}";
        }
    }
}