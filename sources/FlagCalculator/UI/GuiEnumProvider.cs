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
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.ViewModels;
using DustInTheWind.FlagCalculator.UI.Views;
using Microsoft.Win32;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class GuiEnumProvider : IEnumProvider
    {
        public IEnumerable<Type> LoadEnum()
        {
            string assemblyFileName = AskUserForAssembly();

            if (assemblyFileName == null)
                return null;

            Assembly assembly = Assembly.LoadFrom(assemblyFileName);

            List<Type> enumTypes = assembly.GetTypes()
                .Where(x => x.IsEnum)
                .ToList();

            return enumTypes.Count == 0
                ? null
                : new[] { ChooseEnumType(enumTypes) };
        }

        private static string AskUserForAssembly()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Dll Files|*.dll|Exe Files|*.exe|All Files|*.*"
            };

            return openFileDialog.ShowDialog() != true
                ? null
                : openFileDialog.FileName;
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