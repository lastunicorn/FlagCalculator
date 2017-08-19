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

namespace DustInTheWind.FlagCalculator.Business
{
    internal class ProjectContext
    {
        private readonly UserInterface userInterface;
        private FlagsNumber flagsNumber;
        private bool displaySelected;
        private bool displayUnselected;

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

        public bool DisplaySelected
        {
            get { return displaySelected; }
            set
            {
                if (value == displaySelected)
                    return;

                displaySelected = value;
                displayUnselected = false;

                OnDisplaySelectedChanged();
            }
        }

        public bool DisplayUnselected
        {
            get { return displayUnselected; }
            set
            {
                if (value == displayUnselected)
                    return;

                displayUnselected = value;
                displaySelected = false;

                OnDisplaySelectedChanged();
            }
        }

        public event EventHandler Loaded;
        public event EventHandler<FlagsNumberChangedEventArgs> FlagsNumberChanged;
        public event EventHandler DisplaySelectedChanged;

        public ProjectContext(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));

            this.userInterface = userInterface;

            DisplaySelected = false;
            DisplayUnselected = false;

            NumericalBaseService = new NumericalBaseService();
            FlagsNumber = new FlagsNumber();
        }

        public void LoadFlagCollection()
        {
            try
            {
                EnumProvider enumProvider = new EnumProvider();
                Type enumType = enumProvider.LoadEnum();

                if (enumType == null)
                    return;

                FlagsNumber = new FlagsNumber(enumType);

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

        protected virtual void OnDisplaySelectedChanged()
        {
            DisplaySelectedChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
