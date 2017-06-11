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
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class FlagsList : ObservableCollection<CheckableItem>
    {
        private readonly SmartNumber mainValue;
        private bool displaySelected;
        private bool displayUnselected;

        public bool DisplaySelected
        {
            get { return displaySelected; }
            set
            {
                if (value == displaySelected)
                    return;

                displaySelected = value;
                OnSelectionChanged();
            }
        }

        public bool DisplayUnselected
        {
            get { return displayUnselected; }
            set
            {
                if (value == displayUnselected)
                    return;

                displayUnselected = value;
                OnSelectionChanged();
            }
        }

        public event EventHandler SelectionChanged;

        public FlagsList(SmartNumber mainValue)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));

            this.mainValue = mainValue;

            DisplaySelected = true;
            DisplayUnselected = true;
        }

        public void Load(FlagInfoCollection flagInfoCollection)
        {
            mainValue.BitCount = Marshal.SizeOf(flagInfoCollection.UnderlyingType) * 8;

            List<CheckableItem> checkableItems = flagInfoCollection
                .Select(x => new CheckableItem(mainValue, x))
                .ToList();

            foreach (CheckableItem item in checkableItems)
                Items.Add(item);
        }

        public void SelectAllFlags()
        {
            ulong value = 0;

            foreach (CheckableItem item in this)
                value |= item.FlagValue;

            mainValue.SetValue(value);
        }

        public void ClearAllFlags()
        {
            mainValue.Clear();
        }

        protected virtual void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}