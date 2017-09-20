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

namespace DustInTheWind.FlagCalculator.UI
{
    internal class MainTitle
    {
        private readonly OpenedProjects openedProjects;

        private readonly string titleBase;

        public event EventHandler Changed;

        public MainTitle(OpenedProjects openedProjects)
        {
            if (openedProjects == null) throw new ArgumentNullException(nameof(openedProjects));

            this.openedProjects = openedProjects;

            titleBase = BuildTitleBase();

            if (openedProjects.CurrentProject != null)
            {
                openedProjects.CurrentProject.Loaded += HandleProjectLoaded;
                openedProjects.CurrentProject.Unloaded += HandleProjectUnloaded;
            }

            this.openedProjects.CurrentProjectChanged += HandleCurrentProjectChanged;
        }

        private void HandleCurrentProjectChanged(object sender, CurrentProjectChangedEventArgs e)
        {
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

            OnChanged();
        }

        private void HandleProjectLoaded(object sender, EventArgs e)
        {
            OnChanged();
        }

        private void HandleProjectUnloaded(object sender, EventArgs eventArgs)
        {
            OnChanged();
        }

        public override string ToString()
        {
            return Generate();
        }

        private string Generate()
        {
            ProjectContext currentProject = openedProjects.CurrentProject;

            if (currentProject?.IsLoaded != true)
                return titleBase;

            FlagsNumber flagNumber = currentProject.FlagsNumber;

            string flagsName = flagNumber.Name;
            string enumTypeName = flagNumber.UnderlyingType.Name;

            return string.Format("{1} ({2}) - {0}", titleBase, flagsName, enumTypeName);
        }

        private static string BuildTitleBase()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            string version = assembly.GetName().Version.ToString(3);
            return $"Flag Calculator {version}";
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}