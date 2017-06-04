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
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class FlagsViewModel : ViewModelBase
    {
        private readonly FlagNumber flagNumber;

        public List<CheckableItem> Items { get; }

        private bool displaySelected;

        public bool DisplaySelected
        {
            get { return displaySelected; }
            set
            {
                if (value == displaySelected)
                    return;

                displaySelected = value;
                OnSelectionChanged();

                UpdateCheckBoxesVisibility();
            }
        }

        private bool displayUnselected;

        public bool DisplayUnselected
        {
            get { return displayUnselected; }
            set
            {
                if (value == displayUnselected)
                    return;

                displayUnselected = value;
                OnSelectionChanged();

                UpdateCheckBoxesVisibility();
            }
        }

        public event EventHandler SelectionChanged;

        public FlagsViewModel(FlagNumber flagNumber)
        {
            if (flagNumber == null) throw new ArgumentNullException(nameof(flagNumber));
            this.flagNumber = flagNumber;

            Items = new List<CheckableItem>();

            flagNumber.ValueChanged += HandleFlagNumberValueChanged;
        }

        private void HandleFlagNumberValueChanged(object sender, EventArgs e)
        {
            foreach (CheckableItem checkableItem in Items)
                checkableItem.IsChecked = flagNumber.IsFlagSet(checkableItem.Value);

            UpdateCheckBoxesVisibility();
        }

        public void UpdateCheckBoxesVisibility()
        {
            Items.ForEach(x =>
            {
                bool display = (x.IsChecked && displaySelected) || (!x.IsChecked && displayUnselected);
                x.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        public void Load(FlagInfoCollection flagInfoCollection)
        {
            List<CheckableItem> checkableItems = flagInfoCollection
                .Select(x => ToCheckableItem(x, flagInfoCollection.UnderlyingType))
                .ToList();

            Items.AddRange(checkableItems);
        }

        private CheckableItem ToCheckableItem(FlagInfo flagInfo, Type enumUnderlyingType)
        {
            return new CheckableItem(flagNumber)
            {
                IsChecked = false,
                Value = flagInfo.Value,
                Text = string.Format("{0} ({1})", flagInfo.Name, flagInfo.Value, ToBinary(flagInfo.Value, enumUnderlyingType))
            };
        }

        public static string ToBinary(ulong value, Type enumUnderlyingType)
        {
            int size = Marshal.SizeOf(enumUnderlyingType) * 8;
            List<char> chars = new List<char>(size + (size / 4 - 1));

            for (int i = 0; i < size; i++)
            {
                if (i != 0 && i % 4 == 0)
                    chars.Add(' ');

                bool bit = (value & 1) == 1;
                chars.Add(bit ? '1' : '0');

                value = value >> 1;
            }

            chars.Reverse();

            return string.Join(string.Empty, chars);
        }

        protected virtual void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}