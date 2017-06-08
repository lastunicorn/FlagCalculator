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
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Data;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class FlagsViewModel : ViewModelBase
    {
        private readonly SmartNumber mainValue;

        private bool displaySelected;
        private bool displayUnselected;

        private readonly List<CheckableItem> items;
        private readonly CollectionViewSource itemsViewSource;
        public ICollectionView ItemsView { get; }

        public bool DisplaySelected
        {
            get { return displaySelected; }
            set
            {
                if (value == displaySelected)
                    return;

                displaySelected = value;
                OnSelectionChanged();

                ItemsView.Refresh();
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

                ItemsView.Refresh();
            }
        }

        public event EventHandler SelectionChanged;

        public FlagsViewModel(SmartNumber mainValue)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));

            this.mainValue = mainValue;

            items = new List<CheckableItem>();

            itemsViewSource = new CollectionViewSource();
            itemsViewSource.Source = items;
            itemsViewSource.Filter += HandleCollectionViewSourceFilter;

            ItemsView = itemsViewSource.View;

            DisplaySelected = true;
            DisplayUnselected = true;

            mainValue.ValueChanged += HandleMainValueChanged;
        }

        private void HandleCollectionViewSourceFilter(object sender, FilterEventArgs e)
        {
            CheckableItem checkableItem = e.Item as CheckableItem;

            e.Accepted = checkableItem != null && ((checkableItem.IsChecked && displaySelected) || (!checkableItem.IsChecked && displayUnselected));
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            foreach (CheckableItem checkableItem in items)
                checkableItem.IsChecked = mainValue.IsFlagSet(checkableItem.FlagValue);

            ItemsView.Refresh();
        }

        public void Load(FlagInfoCollection flagInfoCollection)
        {
            mainValue.BitCount = Marshal.SizeOf(flagInfoCollection.UnderlyingType) * 8;

            List<CheckableItem> checkableItems = flagInfoCollection
                .Select(x => new CheckableItem(mainValue, x))
                .ToList();

            items.AddRange(checkableItems);
        }

        protected virtual void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}