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
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class OpenedProjects : IEnumerable<ProjectContext>
    {
        private List<ProjectContext> projects = new List<ProjectContext>();
        private ProjectContext currentProject;

        public ProjectContext CurrentProject
        {
            get { return currentProject; }
            set
            {
                if (currentProject == value)
                    return;

                ProjectContext oldProject = currentProject;

                if (oldProject != null)
                {
                    oldProject.Loaded -= HandleProjectLoaded;
                    oldProject.Unloaded -= HandleProjectUnloaded;
                }

                currentProject = value;

                CurrentProjectChangedEventArgs args = new CurrentProjectChangedEventArgs(oldProject, currentProject);
                OnCurrentProjectChanged(args);

                if (currentProject != null)
                {
                    currentProject.Loaded += HandleProjectLoaded;
                    currentProject.Unloaded += HandleProjectUnloaded;
                }
            }
        }

        public event EventHandler<ProjectCreatedEventArgs> ProjectCreated;
        public event EventHandler<CurrentProjectChangedEventArgs> CurrentProjectChanged;
        public event EventHandler CurrentProjectLoaded;
        public event EventHandler CurrentProjectUnloaded;

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            OnCurrentProjectLoaded();
        }

        private void HandleProjectUnloaded(object sender, EventArgs e)
        {
            OnCurrentProjectUnloaded();
        }

        public ProjectContext CreateNew()
        {
            ProjectContext newProject = new ProjectContext();
            AddProject(newProject);

            return newProject;
        }

        public void LoadFrom(IEnumProvider enumProvider)
        {
            ProjectContext newProject = new ProjectContext();
            bool success = newProject.LoadFlagCollection(enumProvider);

            if (success)
                AddProject(newProject);
        }

        private void AddProject(ProjectContext newProject)
        {
            projects.Add(newProject);

            ProjectCreatedEventArgs args = new ProjectCreatedEventArgs(newProject);
            OnProjectCreated(args);
        }

        public IEnumerator<ProjectContext> GetEnumerator()
        {
            return projects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnProjectCreated(ProjectCreatedEventArgs e)
        {
            ProjectCreated?.Invoke(this, e);
        }

        protected virtual void OnCurrentProjectChanged(CurrentProjectChangedEventArgs e)
        {
            CurrentProjectChanged?.Invoke(this, e);
        }

        protected virtual void OnCurrentProjectLoaded()
        {
            CurrentProjectLoaded?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCurrentProjectUnloaded()
        {
            CurrentProjectUnloaded?.Invoke(this, EventArgs.Empty);
        }
    }
}