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

        public NumericalBaseService NumericalBaseService { get; }
        public MainValue MainValue { get; }
        public FlagCollection FlagCollection { get; }
        
        public ProjectContext(UserInterface userInterface, StatusInfo statusInfo)
        {
            if (userInterface == null) throw new ArgumentNullException(nameof(userInterface));
            if (statusInfo == null) throw new ArgumentNullException(nameof(statusInfo));

            this.userInterface = userInterface;
            this.statusInfo = statusInfo;

            NumericalBaseService = new NumericalBaseService();
            MainValue = new MainValue(NumericalBaseService);
            FlagCollection = new FlagCollection(MainValue, NumericalBaseService);

            FlagCollection.Loaded += HandleFlagCollectionLoaded;
        }

        public void LoadFlagCollection()
        {
            try
            {
                MainValue.Clear();

                FlagInfoCollectionProvider flagInfoCollectionProvider = new FlagInfoCollectionProvider();
                FlagInfoCollection flagInfoCollection = flagInfoCollectionProvider.LoadFlagCollection();

                if (flagInfoCollection == null)
                    return;

                FlagCollection.Load(flagInfoCollection, statusInfo);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex);
            }
        }

        private void HandleFlagCollectionLoaded(object sender, EventArgs eventArgs)
        {
            FlagInfoCollection flagInfoCollection = FlagCollection.FlagInfoCollection;
            MainValue.BitCount = flagInfoCollection.BitCount;
            MainValue.Clear();
        }
    }
}
