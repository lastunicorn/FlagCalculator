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
using DustInTheWind.FlagCalculator.UI;
using DustInTheWind.FlagCalculator.UI.ViewModels;

namespace DustInTheWind.FlagCalculator.Business
{
    internal class ProjectContext
    {
        private readonly UserInterface userInterface;
        private readonly StatusInfo statusInfo;
        private FlagsNumber flagsNumber;

        public NumericalBaseService NumericalBaseService { get; }

        public FlagsNumber FlagsNumber
        {
            get { return flagsNumber; }
            private set
            {
                FlagsNumber oldValue = flagsNumber;
                flagsNumber = value;

                FlagsNumberChangedEventArgs args = new FlagsNumberChangedEventArgs(oldValue, value);
                OnFlagsNumberChanged(args);
            }
        }

        public FlagCollection FlagCollection { get; }

        public event EventHandler Loaded;
        public event EventHandler<FlagsNumberChangedEventArgs> FlagsNumberChanged;

        public ProjectContext(UserInterface userInterface, StatusInfo statusInfo)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.userInterface = userInterface;
            this.statusInfo = statusInfo;

            NumericalBaseService = new NumericalBaseService();
            FlagsNumber = new FlagsNumber();
            FlagCollection = new FlagCollection(NumericalBaseService);
        }

        public void LoadFlagCollection()
        {
            try
            {
                FlagsNumber.Clear();

                EnumProvider enumProvider = new EnumProvider();
                Type enumType = enumProvider.LoadEnum();

                if (enumType == null)
                    return;

                FlagsNumber = new FlagsNumber(enumType);
                FlagCollection.Load(FlagsNumber, statusInfo);

                FlagsNumber.Clear();
                OnLoaded();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        public void LoadFlagCollection(Type enumType)
        {
            FlagsNumber = new FlagsNumber(enumType);
            FlagCollection.Load(FlagsNumber, statusInfo);

            OnLoaded();
        }

        protected virtual void OnLoaded()
        {
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFlagsNumberChanged(FlagsNumberChangedEventArgs e)
        {
            FlagsNumberChanged?.Invoke(this, e);
        }
    }
}
