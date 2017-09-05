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
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModelBase
    {
        private string title;

        private readonly UserInterface userInterface;
        private readonly StatusInfo statusInfo;
        private readonly OpenedProjects openedProjects;
        private TabItem selectedProject;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TabItem> Projects { get; }

        public TabItem SelectedProject
        {
            get { return selectedProject; }
            set
            {
                if (selectedProject == value)
                    return;

                selectedProject = value;
                OnPropertyChanged();

                openedProjects.CurrentProject = value?.ProjectContext;
            }
        }

        public SelectNoFlagsCommand SelectNoFlagsCommand { get; }
        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public CloseCommand CloseCommand { get; }
        public DigitCommand DigitCommand { get; }
        public CreateProjectCommand CreateProjectCommand { get; }

        public MainWindowViewModel(UserInterface userInterface, StatusInfo statusInfo, OpenedProjects openedProjects)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));

            this.userInterface = userInterface;
            this.statusInfo = statusInfo;
            this.openedProjects = openedProjects;

            // Create commands

            SelectNoFlagsCommand = new SelectNoFlagsCommand(userInterface, openedProjects);
            SelectAllFlagsCommand = new SelectAllFlagsCommand(userInterface, openedProjects);
            CopyCommand = new CopyCommand(userInterface, openedProjects);
            PasteCommand = new PasteCommand(userInterface, openedProjects);
            CloseCommand = new CloseCommand(userInterface, openedProjects);
            DigitCommand = new DigitCommand(userInterface, openedProjects);
            CreateProjectCommand = new CreateProjectCommand(userInterface, openedProjects);

            // Initialize everything

            //openedProjects.CreateNew();
            //openedProjects.CreateNew();

            IEnumerable<TabItem> projects = openedProjects
                .Select(x => new TabItem(userInterface, statusInfo, x));

            Projects = new ObservableCollection<TabItem>(projects);
            SelectedProject = Projects.FirstOrDefault();

            UpdateTitle();

            if (openedProjects.CurrentProject != null)
            {
                openedProjects.CurrentProject.Loaded += HandleProjectLoaded;
                openedProjects.CurrentProject.Unloaded += HandleProjectUnloaded;
            }

            this.openedProjects.CurrentProjectChanged += HandleCurrentProjectChanged;
            this.openedProjects.ProjectCreated += HandleProjectCreated;
        }

        private void HandleProjectCreated(object sender, ProjectCreatedEventArgs e)
        {
            TabItem tabItem = new TabItem(userInterface, statusInfo, e.NewProject);
            Projects.Add(tabItem);
        }

        private void HandleCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
        {
            SelectedProject = Projects.FirstOrDefault(x => x.ProjectContext == e.NewProject);

            if (e.OldProject != null)
            {
                e.OldProject.Loaded -= HandleProjectLoaded;
                e.OldProject.Unloaded -= HandleProjectUnloaded;
            }

            if (e.NewProject != null)
            {
                e.NewProject.Loaded += HandleProjectLoaded;
                e.NewProject.Unloaded += HandleProjectUnloaded;
            }

            UpdateTitle();
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
            ProjectContext currentProject = openedProjects.CurrentProject;

            if (currentProject?.IsLoaded == true)
            {
                FlagsNumber flagNumber = currentProject.FlagsNumber;

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