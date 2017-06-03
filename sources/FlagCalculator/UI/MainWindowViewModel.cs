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
using System.Windows;
using System.Runtime.InteropServices;

namespace DustInTheWind.FlagCalculator.UI
{
    internal sealed class MainWindowViewModel : ViewModelBase
    {
        private readonly FlagNumber flagNumber = new FlagNumber();
        private readonly UserInterface userInterface;

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

        public bool displayOnlySelected;

        public bool DisplayOnlySelected
        {
            get { return displayOnlySelected; }
            set
            {
                if (value == displayOnlySelected)
                    return;

                displayOnlySelected = value;
                OnPropertyChanged();

                if (value)
                    DisplayOnlyUnselected = false;

                UpdateCheckBoxesVisibility();
            }
        }

        public bool displayOnlyUnselected;

        public bool DisplayOnlyUnselected
        {
            get { return displayOnlyUnselected; }
            set
            {
                if (value == displayOnlyUnselected)
                    return;

                displayOnlyUnselected = value;
                OnPropertyChanged();

                if (value)
                    DisplayOnlySelected = false;

                UpdateCheckBoxesVisibility();
            }
        }

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

            userInterface = new UserInterface();

            LoadFlagCollection();

            flagNumber.Value = 0;
        }

        private void HandleFlagNumberValueChanged(object sender, EventArgs e)
        {
            if (FlagItems != null)
                foreach (CheckableItem checkableItem in FlagItems)
                    checkableItem.IsChecked = flagNumber.IsFlagSet(checkableItem.Value);

            Value = flagNumber.Value;
            UpdateCheckBoxesVisibility();
        }

        private void UpdateCheckBoxesVisibility()
        {
            if (FlagItems == null)
                return;

            bool displayAll = !displayOnlySelected && !displayOnlyUnselected;

            FlagItems.ForEach(x =>
            {
                bool display = displayAll || (x.IsChecked && displayOnlySelected) || (!x.IsChecked && displayOnlyUnselected);
                x.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void LoadFlagCollection()
        {
            try
            {
                flagNumber.Clear();

                FlagCollectionProvider flagCollectionProvider = new FlagCollectionProvider();
                FlagCollection flagCollection = flagCollectionProvider.LoadFlagCollection();

                FlagItems = flagCollection
                    .Select(x => ToCheckableItem(x, flagCollection.UnderlyingType))
                    .ToList();

                Title = string.Format("{0} - {1}", TitleBase, flagCollection.Name);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        private CheckableItem ToCheckableItem(FlagInfo flagInfo, Type enumUnderlyingType)
        {
            return new CheckableItem(flagNumber)
            {
                IsChecked = false,
                Value = flagInfo.Value,
                Text = string.Format("{0} ({1}) - {2}", flagInfo.Name, flagInfo.Value, ToBinary(flagInfo.Value, enumUnderlyingType))
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
    }
}
