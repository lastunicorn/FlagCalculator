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
    internal class FlagsViewModel : ViewModelBase
    {
        private readonly ProjectContext projectContext;
        private readonly StatusInfo statusInfo;

        private readonly ObservableCollection<CheckableItem> flags;

        public ICollectionView ItemsView { get; }

        public FlagsViewModel(ProjectContext projectContext, StatusInfo statusInfo)
        {
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.projectContext = projectContext;
            this.statusInfo = statusInfo;

            flags = new ObservableCollection<CheckableItem>();

            ItemsView = CollectionViewSource.GetDefaultView(flags);

            ItemsView.Filter = Filter;
            ItemsView.Refresh();

            projectContext.FlagsNumberChanged += HandleProjectContextFlagsNumberChanged;
            projectContext.DisplaySelectedChanged += HandleProjectContextDisplaySelectedChanged;
        }

        private bool Filter(object o)
        {
            CheckableItem checkableItem = o as CheckableItem;

            bool displaySelected = projectContext.DisplaySelected;
            bool displayUnselected = projectContext.DisplayUnselected;

            bool allOptionsAreUnselected = !displaySelected && !displayUnselected;

            return allOptionsAreUnselected || checkableItem != null && ((checkableItem.IsChecked && displaySelected) || (!checkableItem.IsChecked && displayUnselected));
        }
        
        private void HandleProjectContextFlagsNumberChanged(object sender, FlagsNumberChangedEventArgs e)
        {
            if (e.OldValue != null)
                e.OldValue.ValueChanged -= HandleMainValueChanged;

            if (e.NewValue != null)
            {
                UpdateFlagsList(e.NewValue);
                e.NewValue.ValueChanged += HandleMainValueChanged;
            }
        }

        private void UpdateFlagsList(FlagsNumber flagsNumber)
        {
            flags.Clear();

            StatusInfoCommand statusInfoCommand = new StatusInfoCommand(statusInfo);

            IEnumerable<CheckableItem> checkableItems = flagsNumber
                .Select(x => new CheckableItem(projectContext.NumericalBaseService, x, statusInfoCommand));

            foreach (CheckableItem item in checkableItems)
                flags.Add(item);
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            ItemsView.Refresh();
        }

        private void HandleProjectContextDisplaySelectedChanged(object sender, EventArgs eventArgs)
        {
            ItemsView.Refresh();
        }
    }
}