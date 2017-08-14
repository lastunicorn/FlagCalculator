using System;
using DustInTheWind.FlagCalculator.UI;

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
