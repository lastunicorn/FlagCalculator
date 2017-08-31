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
        private string title;

        private readonly ProjectContext projectContext;
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
        public MainFooterViewModel MainFooterViewModel { get; }
        public MainHeaderViewModel MainHeaderViewModel { get; }

        public OpenAssemblyCommand OpenAssemblyCommand { get; }
        public SelectNoFlagsCommand SelectNoFlagsCommand { get; }
        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public DigitCommand DigitCommand { get; }

        public MainWindowViewModel(UserInterface userInterface, StatusInfo statusInfo, ProjectContext projectContext)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));

            this.projectContext = projectContext;

            // Create view models.

            FlagsViewModel = new FlagsViewModel(projectContext, statusInfo, userInterface);
            MainFooterViewModel = new MainFooterViewModel(projectContext, statusInfo, userInterface);
            MainHeaderViewModel = new MainHeaderViewModel(projectContext, statusInfo, userInterface);

            // Create commands

            OpenAssemblyCommand = new OpenAssemblyCommand(projectContext, userInterface);
            SelectNoFlagsCommand = new SelectNoFlagsCommand(projectContext, userInterface);
            SelectAllFlagsCommand = new SelectAllFlagsCommand(projectContext, userInterface);
            CopyCommand = new CopyCommand(projectContext, userInterface);
            PasteCommand = new PasteCommand(projectContext, userInterface);
            DigitCommand = new DigitCommand(projectContext, userInterface);

            // Initialize everything

            UpdateTitle();
            isOpenPanelVisible = !projectContext.IsLoaded;

            this.projectContext.Loaded += HandleProjectLoaded;
        }

        private static string BuildTitleBase()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string version = assembly.GetName().Version.ToString(3);
            return string.Format("Flag Calculator {0}", version);
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            UpdateTitle();
            IsOpenPanelVisible = !projectContext.IsLoaded;
        }

        private void UpdateTitle()
        {
            if (projectContext.IsLoaded)
            {
                FlagsNumber flagNumber = projectContext.FlagsNumber;

                string titleBase = BuildTitleBase();
                string flagsName = flagNumber.Name;
                string enumTypeName = flagNumber.UnderlyingType.Name;

                Title = string.Format("{1} ({2}) - {0}", titleBase, flagsName, enumTypeName);
            }
            else
            {
                Title = BuildTitleBase();
            }
        }
    }
}