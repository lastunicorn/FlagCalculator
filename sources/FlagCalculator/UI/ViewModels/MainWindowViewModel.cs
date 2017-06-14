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
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModelBase
    {
        private readonly UserInterface userInterface;

        private readonly string titleBase;
        private string title;

        private string numericalBase;
        private bool isHelpPageVisible;
        private SmartNumber mainValue;
        private readonly FlagCollection flagCollection;
        private StatusInfo statusInfo;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public SmartNumber MainValue
        {
            get { return mainValue; }
            private set
            {
                this.mainValue = value;
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

        public FlagsViewModel FlagsViewModel { get; }
        public MainStatusBarViewModel MainStatusBarViewModel { get; }

        public EscapeCommand EscapeCommand { get; }
        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public DigitCommand DigitCommand { get; }
        public NumericalBaseRollCommand NumericalBaseRollCommand { get; }
        public HelpCommand HelpCommand { get; }
        public StatusInfoCommand StatusInfoCommand { get; }

        public bool IsHelpPageVisible
        {
            get { return isHelpPageVisible; }
            set
            {
                isHelpPageVisible = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            userInterface = new UserInterface();

            Assembly assembly = Assembly.GetEntryAssembly();
            titleBase = "Flag Calculator " + assembly.GetName().Version.ToString(3);

            Title = titleBase;

            mainValue = new SmartNumber();
            flagCollection = new FlagCollection(mainValue);
            statusInfo = new StatusInfo();

            FlagsViewModel = new FlagsViewModel(MainValue, flagCollection);
            MainStatusBarViewModel = new MainStatusBarViewModel(mainValue, flagCollection, statusInfo);

            EscapeCommand = new EscapeCommand(MainValue);
            SelectAllFlagsCommand = new SelectAllFlagsCommand(mainValue, flagCollection);
            CopyCommand = new CopyCommand(MainValue);
            PasteCommand = new PasteCommand(MainValue);
            DigitCommand = new DigitCommand(MainValue);
            NumericalBaseRollCommand = new NumericalBaseRollCommand(MainValue);
            HelpCommand = new HelpCommand(this);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            isHelpPageVisible = false;

            LoadFlagCollection();

            MainValue.ValueChanged += HandleMainValueChanged;
            MainValue.NumericalBaseChanged += HandleMainValueNumericalBaseChanged;

            MainValue.Clear();

            UpdateNumericalBase();
        }

        private void HandleMainValueNumericalBaseChanged(object sender, EventArgs e)
        {
            UpdateNumericalBase();
            MainValue = mainValue;
        }

        private void UpdateNumericalBase()
        {
            NumericalBase = ((int)MainValue.NumericalBase).ToString(CultureInfo.CurrentCulture);
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            MainValue = mainValue;
        }

        private void LoadFlagCollection()
        {
            try
            {
                MainValue.Clear();

                FlagInfoCollectionProvider flagInfoCollectionProvider = new FlagInfoCollectionProvider();
                FlagInfoCollection flagInfoCollection = flagInfoCollectionProvider.LoadFlagCollection();

                MainValue.BitCount = Marshal.SizeOf(flagInfoCollection.UnderlyingType) * 8;
                flagCollection.Load(flagInfoCollection, statusInfo);

                Title = string.Format("{1} ({2}) - {0}", titleBase, flagInfoCollection.Name, flagInfoCollection.UnderlyingType.Name);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}
