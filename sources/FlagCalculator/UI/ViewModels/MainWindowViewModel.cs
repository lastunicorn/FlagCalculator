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
        private SmartNumber value;
        private readonly FlagsList flags;
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

        public SmartNumber Value
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

        public FlagsViewModel FlagsViewModel { get; }
        public MainStatusBarViewModel MainStatusBarViewModel { get; }

        public EscapeCommand EscapeCommand { get; }
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

            value = new SmartNumber();
            flags = new FlagsList(value);
            statusInfo = new StatusInfo();

            FlagsViewModel = new FlagsViewModel(Value, flags);
            MainStatusBarViewModel = new MainStatusBarViewModel(flags, statusInfo);

            EscapeCommand = new EscapeCommand(Value);
            CopyCommand = new CopyCommand(Value);
            PasteCommand = new PasteCommand(Value);
            DigitCommand = new DigitCommand(Value);
            NumericalBaseRollCommand = new NumericalBaseRollCommand(Value);
            HelpCommand = new HelpCommand(this);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            isHelpPageVisible = false;

            LoadFlagCollection();

            Value.ValueChanged += HandleMainValueChanged;
            Value.NumericalBaseChanged += HandleMainValueNumericalBaseChanged;

            Value.Clear();

            UpdateNumericalBase();
        }

        private void HandleMainValueNumericalBaseChanged(object sender, EventArgs e)
        {
            UpdateNumericalBase();
            Value = value;
        }

        private void UpdateNumericalBase()
        {
            NumericalBase = ((int)Value.NumericalBase).ToString(CultureInfo.CurrentCulture);
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            Value = value;
        }

        private void LoadFlagCollection()
        {
            try
            {
                Value.Clear();

                FlagInfoCollectionProvider flagInfoCollectionProvider = new FlagInfoCollectionProvider();
                FlagInfoCollection flagInfoCollection = flagInfoCollectionProvider.LoadFlagCollection();

                flags.Load(flagInfoCollection, statusInfo);

                Title = string.Format("{1} ({2}) - {0}", titleBase, flagInfoCollection.Name, flagInfoCollection.UnderlyingType.Name);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}
