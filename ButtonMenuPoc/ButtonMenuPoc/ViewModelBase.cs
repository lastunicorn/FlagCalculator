using System.ComponentModel;
using System.Runtime.CompilerServices;
using DustInTheWind.ButtonMenuPoc.Properties;

namespace DustInTheWind.ButtonMenuPoc
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}