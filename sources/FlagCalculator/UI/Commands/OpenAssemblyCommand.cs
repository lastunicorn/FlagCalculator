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
using Microsoft.Win32;

namespace DustInTheWind.FlagCalculator.UI.Commands
{
    internal class OpenAssemblyCommand : ICommand
    {
        private readonly MainValue mainValue;
        private readonly FlagCollection flagCollection;
        private readonly StatusInfo statusInfo;

        public OpenAssemblyCommand(MainValue mainValue, FlagCollection flagCollection, StatusInfo statusInfo)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));
            if (flagCollection == null) throw new ArgumentNullException(nameof(flagCollection));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.mainValue = mainValue;
            this.flagCollection = flagCollection;
            this.statusInfo = statusInfo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "Dll Files|*.dll|Exe Files|*.exe|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Assembly assembly = Assembly.LoadFrom(openFileDialog.FileName);

                IEnumerable<Type> enumTypes = assembly.GetTypes()
                    .Where(x => x.IsEnum);

                Type enumType = enumTypes.FirstOrDefault();

                if (enumType != null)
                {
                    FlagInfoCollection flagInfoCollection = FlagInfoCollection.FromEnum(enumType);
                    mainValue.BitCount = flagInfoCollection.BitCount;
                    flagCollection.Load(flagInfoCollection, statusInfo);
                }

                MessageBox.Show(openFileDialog.FileName);
            }
        }
    }
}