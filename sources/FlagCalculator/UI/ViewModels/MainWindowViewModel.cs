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
using System.Collections.ObjectModel;
using System.Reflection;
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModelBase
    {
        private string title;

        private readonly ProjectContext projectContext;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public SelectNoFlagsCommand SelectNoFlagsCommand { get; }
        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public CloseCommand CloseCommand { get; }
        public DigitCommand DigitCommand { get; }

        public ProjectViewModel ProjectViewModel { get; set; }

        public ObservableCollection<TabItem> Projects { get; }

        public MainWindowViewModel(UserInterface userInterface, StatusInfo statusInfo, ProjectContext projectContext)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (projectContext == null) throw new ArgumentNullException(nameof(projectContext));

            this.projectContext = projectContext;

            // Create view models.

            ProjectViewModel = new ProjectViewModel(userInterface, statusInfo, projectContext);

            // Create commands

            SelectNoFlagsCommand = new SelectNoFlagsCommand(projectContext, userInterface);
            SelectAllFlagsCommand = new SelectAllFlagsCommand(projectContext, userInterface);
            CopyCommand = new CopyCommand(projectContext, userInterface);
            PasteCommand = new PasteCommand(projectContext, userInterface);
            CloseCommand = new CloseCommand(projectContext, userInterface);
            DigitCommand = new DigitCommand(projectContext, userInterface);

            // Initialize everything

            Projects = new ObservableCollection<TabItem>
            {
                new TabItem(userInterface, statusInfo, new ProjectContext()),
                new TabItem(userInterface, statusInfo, new ProjectContext())
            };

            UpdateTitle();

            this.projectContext.Loaded += HandleProjectLoaded;
            this.projectContext.Unloaded += HandleProjectUnloaded;
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            UpdateTitle();
        }

        private void HandleProjectUnloaded(object sender, EventArgs eventArgs)
        {
            UpdateTitle();
        }

        private static string BuildTitleBase()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string version = assembly.GetName().Version.ToString(3);
            return string.Format("Flag Calculator {0}", version);
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