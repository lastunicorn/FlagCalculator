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
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal sealed class FlagCollection
    {
        private FlagsNumber flagsNumber;
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

        public FlagCollection(NumericalBaseService numericalBaseService)
        {
            if (numericalBaseService == null) throw new ArgumentNullException(nameof(numericalBaseService));

            this.numericalBaseService = numericalBaseService;

            flags = new ObservableCollection<CheckableItem>();

            DisplaySelected = false;
            DisplayUnselected = false;

            itemsViewSource = new CollectionViewSource { Source = flags };
            itemsViewSource.Filter += HandleCollectionViewSourceFilter;
        }

        private void HandleCollectionViewSourceFilter(object sender, FilterEventArgs e)
        {
            CheckableItem checkableItem = e.Item as CheckableItem;

            bool allOptionsAreUnselected = !DisplaySelected && !DisplayUnselected;

            e.Accepted = allOptionsAreUnselected || checkableItem != null && ((checkableItem.IsChecked && DisplaySelected) || (!checkableItem.IsChecked && DisplayUnselected));
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            View.Refresh();
        }

        public void Load(FlagsNumber flagsNumber, StatusInfo statusInfo)
        {
            if (flagsNumber == null) throw new ArgumentNullException(nameof(flagsNumber));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            if (this.flagsNumber != null)
                this.flagsNumber.ValueChanged -= HandleMainValueChanged;

            StatusInfoCommand statusInfoCommand = new StatusInfoCommand(statusInfo);

            List<CheckableItem> checkableItems = flagsNumber
                .Select(x => new CheckableItem(numericalBaseService, x, flagsNumber.BitCount, statusInfoCommand))
                .ToList();

            foreach (CheckableItem item in checkableItems)
                flags.Add(item);

            this.flagsNumber = flagsNumber;
            this.flagsNumber.ValueChanged += HandleMainValueChanged;
        }

        private void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}