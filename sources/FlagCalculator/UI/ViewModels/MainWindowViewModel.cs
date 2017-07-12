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

        private bool isHelpPageVisible;
        private MainValue mainValue;
        private readonly FlagCollection flagCollection;
        private readonly StatusInfo statusInfo;
        private NumericalBaseService numericalBaseService;
        private bool isOpenPanelVisible;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public MainValue MainValue
        {
            get { return mainValue; }
            private set
            {
                mainValue = value;
                OnPropertyChanged();
            }
        }

        public bool IsOpenPanelVisible
        {
            get { return isOpenPanelVisible; }
            set
            {
                isOpenPanelVisible = value;
                OnPropertyChanged();
            }
        }

        public FlagsViewModel FlagsViewModel { get; }
        public MainStatusBarViewModel MainStatusBarViewModel { get; }
        public MainValueViewModel MainValueViewModel { get; }

        public OpenAssemblyCommand OpenAssemblyCommand { get; }
        public EscapeCommand EscapeCommand { get; }
        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public DigitCommand DigitCommand { get; }
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
            // Create business services.

            userInterface = new UserInterface();
            numericalBaseService = new NumericalBaseService();
            mainValue = new MainValue(numericalBaseService);
            flagCollection = new FlagCollection(mainValue, numericalBaseService);
            statusInfo = new StatusInfo();

            // Create view models.

            FlagsViewModel = new FlagsViewModel(flagCollection);
            MainStatusBarViewModel = new MainStatusBarViewModel(mainValue, flagCollection, statusInfo);
            MainValueViewModel = new MainValueViewModel(mainValue, numericalBaseService, statusInfo);

            // Create commands

            OpenAssemblyCommand = new OpenAssemblyCommand(flagCollection, statusInfo, userInterface);
            EscapeCommand = new EscapeCommand(MainValue);
            SelectAllFlagsCommand = new SelectAllFlagsCommand(mainValue, flagCollection);
            CopyCommand = new CopyCommand(MainValue);
            PasteCommand = new PasteCommand(MainValue);
            DigitCommand = new DigitCommand(MainValue);
            HelpCommand = new HelpCommand(this);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            // Initialize everything

            titleBase = BuildTitleBase();
            Title = titleBase;

            isOpenPanelVisible = true;
            isHelpPageVisible = false;

            flagCollection.Loaded += HandleFlagCollectionLoaded;

            LoadFlagCollection();
        }

        private static string BuildTitleBase()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string version = assembly.GetName().Version.ToString(3);
            return string.Format("Flag Calculator {0}", version);
        }

        private void HandleFlagCollectionLoaded(object sender, EventArgs eventArgs)
        {
            FlagInfoCollection flagInfoCollection = flagCollection.FlagInfoCollection;
            MainValue.BitCount = flagInfoCollection.BitCount;

            Title = string.Format("{1} ({2}) - {0}", titleBase, flagInfoCollection.Name, flagInfoCollection.UnderlyingTypeName);

            IsOpenPanelVisible = false;

            MainValue.Clear();
        }

        private void LoadFlagCollection()
        {
            try
            {
                MainValue.Clear();

                FlagInfoCollectionProvider flagInfoCollectionProvider = new FlagInfoCollectionProvider();
                FlagInfoCollection flagInfoCollection = flagInfoCollectionProvider.LoadFlagCollection();
                
                if (flagInfoCollection == null)
                    return;

                flagCollection.Load(flagInfoCollection, statusInfo);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}
