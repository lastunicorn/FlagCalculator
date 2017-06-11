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
using System.ComponentModel;
using System.Windows.Data;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class FlagsViewModel : ViewModelBase
    {
        private readonly SmartNumber mainValue;
        
        private readonly FlagsList flags;
        private readonly CollectionViewSource itemsViewSource;
        public ICollectionView ItemsView { get; }
        
        public FlagsViewModel(SmartNumber mainValue, FlagsList flags)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));

            this.mainValue = mainValue;

            this.flags = flags;

            itemsViewSource = new CollectionViewSource();
            itemsViewSource.Source = this.flags;
            itemsViewSource.Filter += HandleCollectionViewSourceFilter;

            ItemsView = itemsViewSource.View;
            
            this.flags.SelectionChanged += HandleFlagsSelectionChanged;

            mainValue.ValueChanged += HandleMainValueChanged;
        }

        private void HandleFlagsSelectionChanged(object sender, EventArgs eventArgs)
        {
            ItemsView.Refresh();
        }

        private void HandleCollectionViewSourceFilter(object sender, FilterEventArgs e)
        {
            CheckableItem checkableItem = e.Item as CheckableItem;

            e.Accepted = checkableItem != null && ((checkableItem.IsChecked && flags.DisplaySelected) || (!checkableItem.IsChecked && flags.DisplayUnselected));
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            foreach (CheckableItem checkableItem in flags)
                checkableItem.IsChecked = mainValue.IsFlagSet(checkableItem.FlagValue);

            ItemsView.Refresh();
        }
    }
}