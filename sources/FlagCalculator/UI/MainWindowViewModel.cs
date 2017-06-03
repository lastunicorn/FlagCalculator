﻿// FlagCalculator
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
using System.Runtime.InteropServices;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI
{
    internal sealed class MainWindowViewModel : ViewModelBase
    {
        private readonly FlagNumber flagNumber = new FlagNumber();
        private readonly UserInterface userInterface;

        private string title;
        private string value;
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

        public string Value
        {
            get { return value; }
            private set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public string NumericalBase
        {
            get { return numericalBase; }
            set
            {
                numericalBase = value;
                OnPropertyChanged();
            }
        }

        private bool displayOnlySelected;

        public bool DisplayOnlySelected
        {
            get { return displayOnlySelected; }
            set
            {
                if (value == displayOnlySelected)
                    return;

                displayOnlySelected = value;
                OnPropertyChanged();

                FlagsViewModel.DisplayOnlySelected = value;
            }
        }

        private bool displayAll;

        public bool DisplayAll
        {
            get { return displayAll; }
            set
            {
                if (value == displayAll)
                    return;

                displayAll = value;
                OnPropertyChanged();

                FlagsViewModel.DisplayAll = value;
            }
        }

        private bool displayOnlyUnselected;
        private string numericalBase;

        public bool DisplayOnlyUnselected
        {
            get { return displayOnlyUnselected; }
            set
            {
                if (value == displayOnlyUnselected)
                    return;

                displayOnlyUnselected = value;
                OnPropertyChanged();

                FlagsViewModel.DisplayOnlyUnselected = value;
            }
        }

        public FlagsViewModel FlagsViewModel { get; }

        public EscapeCommand EscapeCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public DigitCommand DigitCommand { get; }
        public BaseRollCommand BaseRollCommand { get; }

        public string Base { get; set; }

        public MainWindowViewModel()
        {
            userInterface = new UserInterface();

            Title = TitleBase;
            EscapeCommand = new EscapeCommand(flagNumber);
            CopyCommand = new CopyCommand(flagNumber);
            PasteCommand = new PasteCommand(flagNumber);
            DigitCommand = new DigitCommand(flagNumber);
            BaseRollCommand = new BaseRollCommand(flagNumber);

            FlagsViewModel = new FlagsViewModel(flagNumber);
            FlagsViewModel.SelectionChanged += HandleFlagItemsSelectionChanged;

            LoadFlagCollection();

            flagNumber.ValueChanged += HandleFlagNumberValueChanged;
            flagNumber.BaseChanged += HandleFlagNumberBaseChanged;

            FlagsViewModel.DisplayAll = true;

            flagNumber.Value = 0;

            UpdateNumericalBase();
        }

        private void HandleFlagNumberBaseChanged(object sender, EventArgs e)
        {
            UpdateNumericalBase();
            Value = flagNumber.ToString();
        }

        private void UpdateNumericalBase()
        {
            switch (flagNumber.NumericalBase)
            {
                case Business.NumericalBase.Decimal:
                    NumericalBase = "10";
                    break;

                case Business.NumericalBase.Hexadecimal:
                    NumericalBase = "16";
                    break;

                case Business.NumericalBase.Binary:
                    NumericalBase = "2";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleFlagItemsSelectionChanged(object sender, EventArgs e)
        {
            DisplayAll = FlagsViewModel.DisplayAll;
            DisplayOnlySelected = FlagsViewModel.DisplayOnlySelected;
            DisplayOnlyUnselected = FlagsViewModel.DisplayOnlyUnselected;
        }

        private void HandleFlagNumberValueChanged(object sender, EventArgs e)
        {
            Value = flagNumber.ToString();
        }

        private void LoadFlagCollection()
        {
            try
            {
                flagNumber.Clear();

                FlagCollectionProvider flagCollectionProvider = new FlagCollectionProvider();
                FlagInfoCollection flagInfoCollection = flagCollectionProvider.LoadFlagCollection();

                FlagsViewModel.Load(flagInfoCollection);

                flagNumber.BitCount = Marshal.SizeOf(flagInfoCollection.UnderlyingType) * 8;

                Title = string.Format("{0} - {1}", TitleBase, flagInfoCollection.Name);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}
