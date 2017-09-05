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

namespace DustInTheWind.FlagCalculator.Business
{
    internal class ProjectContext
    {
        private FlagsNumber flagsNumber;
        private bool displaySelected;
        private bool displayUnselected;

        public NumericalBaseService NumericalBaseService { get; }

        public bool IsLoaded { get; private set; }

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
        public event EventHandler Unloaded;
        public event EventHandler<FlagsNumberChangedEventArgs> FlagsNumberChanged;
        public event EventHandler DisplaySelectedChanged;

        public ProjectContext()
        {
            DisplaySelected = false;
            DisplayUnselected = false;

            NumericalBaseService = new NumericalBaseService();
            FlagsNumber = new FlagsNumber();
        }

        public bool LoadFlagCollection(Type enumType)
        {
            if (enumType == null)
                return false;

            FlagsNumber = new FlagsNumber(enumType);

            IsLoaded = true;
            OnLoaded();

            return true;
        }

        public void Unload()
        {
            FlagsNumber = new FlagsNumber();
            NumericalBaseService.NumericalBase = NumericalBase.Decimal;
            DisplaySelected = false;
            DisplayUnselected = false;

            IsLoaded = false;
            OnUnloaded();
        }

        public void ToggleDisplaySelected()
        {
            DisplaySelected = !DisplaySelected;
        }

        public void ToggleDisplayUnselected()
        {
            DisplayUnselected = !DisplayUnselected;
        }

        protected virtual void OnLoaded()
        {
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUnloaded()
        {
            Unloaded?.Invoke(this, EventArgs.Empty);
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