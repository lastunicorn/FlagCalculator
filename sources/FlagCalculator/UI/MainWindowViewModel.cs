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
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal sealed class MainWindowViewModel : ViewModelBase
    {
        private readonly FlagNumber flagNumber = new FlagNumber();

        private string title;
        private ulong value;
        private const string TitleBase = "Flag Calculator";

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
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public List<CheckableItem> FlagItems { get; private set; }

        public EscapeCommand EscapeCommand { get; private set; }
        public CopyCommand CopyCommand { get; private set; }
        public PasteCommand PasteCommand { get; private set; }
        public DigitCommand DigitCommand { get; private set; }

        public MainWindowViewModel()
        {
            Title = TitleBase;
            EscapeCommand = new EscapeCommand(flagNumber);
            CopyCommand = new CopyCommand(flagNumber);
            PasteCommand = new PasteCommand(flagNumber);
            DigitCommand = new DigitCommand(flagNumber);

            flagNumber.ValueChanged += HandleFlagNumberValueChanged;

            LoadFlagCollection();
        }

        private void HandleFlagNumberValueChanged(object sender, EventArgs e)
        {
            if (FlagItems != null)
                foreach (CheckableItem checkableItem in FlagItems)
                    checkableItem.IsChecked = flagNumber.IsFlagSet(checkableItem.Value);

            Value = flagNumber.Value;
        }

        private void LoadFlagCollection()
        {
            flagNumber.Clear();

            FlagCollectionProvider flagCollectionProvider = new FlagCollectionProvider();
            FlagCollection flagCollection = flagCollectionProvider.LoadFlagCollection();

            FlagItems = flagCollection
                .Select(ToCheckableItem)
                .ToList();

            Title = string.Format("{0} - {1}", TitleBase, flagCollection.Name);
        }

        private CheckableItem ToCheckableItem(FlagInfo flagInfo)
        {
            return new CheckableItem(flagNumber)
            {
                IsChecked = false,
                Value = flagInfo.Value,
                Text = string.Format("{0} ({1})", flagInfo.Name, flagInfo.Value)
            };
        }
    }
}
