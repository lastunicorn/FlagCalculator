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
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.ViewModels;
using DustInTheWind.FlagCalculator.UI.Views;
using Microsoft.Win32;

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal class OpenAssemblyCommand : ICommand
    {
        private readonly FlagCollection flagCollection;
        private readonly StatusInfo statusInfo;
        private readonly UserInterface userInterface;

        public OpenAssemblyCommand(FlagCollection flagCollection, StatusInfo statusInfo, UserInterface userInterface)
        {
            if (flagCollection == null) throw new ArgumentNullException(nameof(flagCollection));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));

            this.flagCollection = flagCollection;
            this.statusInfo = statusInfo;
            this.userInterface = userInterface;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = Environment.CurrentDirectory,
                    Filter = "Dll Files|*.dll|Exe Files|*.exe|All Files|*.*"
                };

                if (openFileDialog.ShowDialog() != true)
                    return;

                string assemblyFileName = openFileDialog.FileName;
                LoadAssembly(assemblyFileName);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        private void LoadAssembly(string assemblyFileName)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFileName);
            

            List<Type> enumTypes = assembly.GetTypes()
                .Where(x => x.IsEnum)
                .ToList();

            if (enumTypes.Count == 0)
                return;

            Type enumType = enumTypes.Count == 1
                ? enumTypes[0]
                : ChooseEnumType(enumTypes);

            if (enumType != null)
            {
                FlagInfoCollection flagInfoCollection = new FlagInfoCollection(enumType);
                flagCollection.Load(flagInfoCollection, statusInfo);
            }
        }

        private static Type ChooseEnumType(List<Type> enumTypes)
        {
            EnumSelectorViewModel enumSelectorViewModel = new EnumSelectorViewModel
            {
                List = enumTypes
            };

            EnumSelectorWindow enumSelectorWindow = new EnumSelectorWindow
            {
                DataContext = enumSelectorViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            bool? response = enumSelectorWindow.ShowDialog();

            return response == true
                ? enumSelectorViewModel.SelectedEnumType
                : null;
        }
    }
}