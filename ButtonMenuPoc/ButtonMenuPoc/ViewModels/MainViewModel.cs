using DustInTheWind.ButtonMenuPoc.Commands;

namespace DustInTheWind.ButtonMenuPoc.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MenuViewModel MenuViewModel { get; }

        public WindowMouseDownCommand WindowMouseDownCommand { get; }
        public WindowLostKeyboardFocusCommand WindowLostKeyboardFocusCommand { get; }
        public WindowLocationChangedCommand WindowLocationChangedCommand { get; }
        public WindowSizeChangedCommand WindowSizeChangedCommand { get; }

        public MainViewModel()
        {
            MenuViewModel = new MenuViewModel();

            WindowMouseDownCommand = new WindowMouseDownCommand(MenuViewModel);
            WindowLostKeyboardFocusCommand = new WindowLostKeyboardFocusCommand(MenuViewModel);
            WindowLocationChangedCommand = new WindowLocationChangedCommand(MenuViewModel);
            WindowSizeChangedCommand = new WindowSizeChangedCommand(MenuViewModel);
        }
    }
}