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
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.Business
{
    internal sealed class FlagCollection
    {
        private readonly MainValue mainValue;
        private readonly NumericalBaseService numericalBaseService;
        private bool displaySelected;
        private bool displayUnselected;
        private readonly ObservableCollection<CheckableItem> flags;
        private readonly CollectionViewSource itemsViewSource;

        public bool DisplaySelected
        {
            get { return displaySelected; }
            set
            {
                if (value == displaySelected)
                    return;

                displaySelected = value;
                OnSelectionChanged();

                View.Refresh();
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

                View.Refresh();
            }
        }

        public ICollectionView View => itemsViewSource.View;

        public event EventHandler SelectionChanged;

        public FlagCollection(MainValue mainValue, NumericalBaseService numericalBaseService)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));
            if (numericalBaseService == null) throw new ArgumentNullException(nameof(numericalBaseService));

            this.mainValue = mainValue;
            this.numericalBaseService = numericalBaseService;

            flags = new ObservableCollection<CheckableItem>();

            DisplaySelected = false;
            DisplayUnselected = false;

            itemsViewSource = new CollectionViewSource { Source = flags };
            itemsViewSource.Filter += HandleCollectionViewSourceFilter;

            mainValue.ValueChanged += HandleMainValueChanged;
        }

        private void HandleCollectionViewSourceFilter(object sender, FilterEventArgs e)
        {
            CheckableItem checkableItem = e.Item as CheckableItem;

            bool allOptionsAreUnselected = !DisplaySelected && !DisplayUnselected;

            e.Accepted = allOptionsAreUnselected || checkableItem != null && ((checkableItem.IsChecked && DisplaySelected) || (!checkableItem.IsChecked && DisplayUnselected));
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            foreach (CheckableItem checkableItem in flags)
                checkableItem.IsChecked = mainValue.IsFlagSet(checkableItem.FlagValue.Value);

            View.Refresh();
        }

        public void Load(FlagInfoCollection flagInfoCollection, StatusInfo statusInfo)
        {
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            FlagCheckedCommand flagCheckedCommand = new FlagCheckedCommand(mainValue);
            StatusInfoCommand statusInfoCommand = new StatusInfoCommand(statusInfo);

            List<CheckableItem> checkableItems = flagInfoCollection
                .Select(x => new CheckableItem(numericalBaseService, x, flagInfoCollection.BitCount, flagCheckedCommand, statusInfoCommand))
                .ToList();

            foreach (CheckableItem item in checkableItems)
                flags.Add(item);
        }

        public ulong GetAllFlagsCumulated()
        {
            ulong value = 0;

            foreach (CheckableItem item in flags)
                value |= item.FlagValue.Value;

            return value;
        }

        private void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}