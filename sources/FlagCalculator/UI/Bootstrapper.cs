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
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.ViewModels;
using DustInTheWind.FlagCalculator.UI.Views;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class Bootstrapper
    {
        public void Run()
        {
            UserInterface userInterface = new UserInterface();

            try
            {
                StatusInfo statusInfo = new StatusInfo();
                ProjectContext projectContext = new ProjectContext();

                MainWindow mainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(userInterface, statusInfo, projectContext)
                };

                mainWindow.Show();

                ConfigurationEnumProvider enumProvider = new ConfigurationEnumProvider();
                projectContext.LoadFlagCollection(enumProvider);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }
    }
}
