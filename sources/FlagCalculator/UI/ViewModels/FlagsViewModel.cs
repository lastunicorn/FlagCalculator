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
using System.ComponentModel;
using DustInTheWind.FlagCalculator.Business;

namespace DustInTheWind.FlagCalculator.UI.ViewModels
{
    internal class FlagsViewModel : ViewModelBase
    {
        private readonly MainValue mainValue;
        private readonly FlagCollection flagCollection;
        public ICollectionView ItemsView => flagCollection.View;

        public FlagsViewModel(MainValue mainValue, FlagCollection flagCollection)
        {
            if (mainValue == null) throw new ArgumentNullException(nameof(mainValue));
            if (flagCollection == null) throw new ArgumentNullException(nameof(flagCollection));

            this.mainValue = mainValue;
            this.flagCollection = flagCollection;

            mainValue.ValueChanged += HandleMainValueChanged;
        }

        private void HandleMainValueChanged(object sender, EventArgs e)
        {
            flagCollection.UpdateFlags(mainValue);
        }
    }
}