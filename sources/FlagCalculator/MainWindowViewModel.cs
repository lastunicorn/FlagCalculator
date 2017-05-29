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
using System.Runtime.CompilerServices;

namespace DustInTheWind.FlagCalculator
{
    internal sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly FlagNumber flagNumber = new FlagNumber();

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ulong Value
        {
            get { return flagNumber.Value; }
            set
            {
                flagNumber.Value = value;
            }
        }

        public List<CheckableItem> FlagItems { get; private set; }

        public MainWindowViewModel()
        {
            Title = "Flag Calculator";

            flagNumber.ValueChanged += HandleFlagNumberValueChanged;

            FlagCollectionProvider flagCollectionProvider = new FlagCollectionProvider();
            FlagCollection flagCollection = flagCollectionProvider.LoadFlagCollection();

            FlagItems = flagCollection
                .Select(x => new CheckableItem(flagNumber)
                {
                    IsChecked = false,
                    Value = x.Value,
                    Text = x.Name
                })
                .ToList();

            Value = 0;

            Title = "Flag Calculator - " + flagCollection.Name;
        }

        private void HandleFlagNumberValueChanged(object sender, EventArgs eventArgs)
        {
            foreach (CheckableItem checkableItem in FlagItems)
                checkableItem.IsChecked = flagNumber.IsFlagSet(checkableItem.Value);

            OnPropertyChanged("Value");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool allowFlagChange = true;
        private string title;

        public void Clear()
        {
            if (!allowFlagChange)
                return;

            allowFlagChange = false;
            try
            {
                flagNumber.Clear();
            }
            finally
            {
                allowFlagChange = true;
            }
        }
    }
}
