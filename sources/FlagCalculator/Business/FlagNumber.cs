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
using System.Globalization;

namespace DustInTheWind.FlagCalculator.Business
{
    internal sealed class FlagNumber
    {
        private ulong value;

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

        public void AddFlags(ulong flags)
        {
            Value = value | flags;
        }

        public void RemoveFlags(ulong flags)
        {
            Value = value & ~flags;
        }

        public void Clear()
        {
            Value = 0;
        }

        public bool IsFlagSet(ulong flags)
        {
            return flags == 0
                ? value == 0
                : (value & flags) == flags;
        }

        private void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return value.ToString(CultureInfo.CurrentCulture);
        }
    }
}