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
        private readonly MainTitle mainTitle;
        private ProjectViewModel selectedProject;
        private bool isNoTabInfoVisible;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; }

        public ProjectViewModel SelectedProject
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

        public bool IsNoTabInfoVisible
        {
            get { return isNoTabInfoVisible; }
            set
            {
                isNoTabInfoVisible = value;
                OnPropertyChanged();
            }
        }

        public SelectAllFlagsCommand SelectAllFlagsCommand { get; }
        public SelectNoFlagsCommand SelectNoFlagsCommand { get; }
        public CopyCommand CopyCommand { get; }
        public PasteCommand PasteCommand { get; }
        public CreateProjectCommand CreateProjectCommand { get; }
        public CloseCurrentProjectCommand CloseCurrentProjectCommand { get; }
        public DigitCommand DigitCommand { get; }

        public MainWindowViewModel(UserInterface userInterface, StatusInfo statusInfo, OpenedProjects openedProjects)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));

            this.userInterface = userInterface;
            this.statusInfo = statusInfo;
            this.openedProjects = openedProjects;

            // Create commands

            SelectAllFlagsCommand = new SelectAllFlagsCommand(userInterface, openedProjects);
            SelectNoFlagsCommand = new SelectNoFlagsCommand(userInterface, openedProjects);
            CopyCommand = new CopyCommand(userInterface, openedProjects);
            PasteCommand = new PasteCommand(userInterface, openedProjects);
            CreateProjectCommand = new CreateProjectCommand(userInterface, openedProjects);
            CloseCurrentProjectCommand = new CloseCurrentProjectCommand(userInterface, openedProjects);
            DigitCommand = new DigitCommand(userInterface, openedProjects);

            // Initialize everything

            IEnumerable<ProjectViewModel> projects = openedProjects
                .Select(x => new ProjectViewModel(userInterface, statusInfo, openedProjects, x));

            Projects = new ObservableCollection<ProjectViewModel>(projects);
            SelectedProject = Projects.FirstOrDefault();

            mainTitle = new MainTitle(openedProjects);
            mainTitle.Changed += HandleMainTitleChanged;

            Title = mainTitle.ToString();

            this.openedProjects.CurrentProjectChanged += HandleCurrentProjectChanged;
            this.openedProjects.ProjectCreated += HandleProjectCreated;
            this.openedProjects.ProjectClosed += HandleProjectClosed;

            IsNoTabInfoVisible = Projects.Count == 0;
        }

        private void HandleMainTitleChanged(object sender, EventArgs eventArgs)
        {
            Title = mainTitle.ToString();
        }

        private void HandleCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
        {
            SelectedProject = Projects.FirstOrDefault(x => x.ProjectContext == e.NewProject);
        }

        private void HandleProjectCreated(object sender, ProjectCreatedEventArgs e)
        {
            ProjectViewModel projectViewModel = new ProjectViewModel(userInterface, statusInfo, openedProjects, e.NewProject);
            Projects.Add(projectViewModel);

            IsNoTabInfoVisible = Projects.Count == 0;
        }

        private void HandleProjectClosed(object sender, ProjectClosedEventArgs e)
        {
            ProjectViewModel projectToRemove = Projects.First(x => x.ProjectContext == e.ClosedProject);
            Projects.Remove(projectToRemove);

            IsNoTabInfoVisible = Projects.Count == 0;
        }
    }
}