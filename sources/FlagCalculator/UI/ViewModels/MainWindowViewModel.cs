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
        private readonly StatusInfo statusInfo;

        private readonly string titleBase;
        private string title;

        private readonly ProjectContext projectContext;
        private bool isHelpPageVisible;
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
        public MainHeaderViewModel MainHeaderViewModel { get; }

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
            statusInfo = new StatusInfo();
            projectContext = new ProjectContext(userInterface, statusInfo);

            // Create view models.

            FlagsViewModel = new FlagsViewModel(projectContext.FlagCollection);
            MainStatusBarViewModel = new MainStatusBarViewModel(projectContext, statusInfo);
            MainHeaderViewModel = new MainHeaderViewModel(projectContext, statusInfo);

            // Create commands

            OpenAssemblyCommand = new OpenAssemblyCommand(projectContext, userInterface);
            EscapeCommand = new EscapeCommand(projectContext);
            SelectAllFlagsCommand = new SelectAllFlagsCommand(projectContext);
            CopyCommand = new CopyCommand(projectContext);
            PasteCommand = new PasteCommand(projectContext);
            DigitCommand = new DigitCommand(projectContext);
            HelpCommand = new HelpCommand(this);
            StatusInfoCommand = new StatusInfoCommand(statusInfo);

            // Initialize everything

            titleBase = BuildTitleBase();
            Title = titleBase;

            isOpenPanelVisible = true;
            isHelpPageVisible = false;

            projectContext.Loaded += HandleProjectLoaded;

            projectContext.LoadFlagCollection();
        }

        private static string BuildTitleBase()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string version = assembly.GetName().Version.ToString(3);
            return string.Format("Flag Calculator {0}", version);
        }

        private void HandleProjectLoaded(object sender, EventArgs eventArgs)
        {
            FlagsNumber flagNumber = projectContext.FlagsNumber;

            Title = string.Format("{1} ({2}) - {0}", titleBase, flagNumber.Name, flagNumber.UnderlyingType.Name);
            IsOpenPanelVisible = false;
        }
    }
}
