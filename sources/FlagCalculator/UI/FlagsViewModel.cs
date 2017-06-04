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
                .Select(x => new CheckableItem(flagNumber, x))
                .ToList();

            Items.AddRange(checkableItems);
        }

        protected virtual void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}