using System.Windows.Input;

namespace DustInTheWind.ButtonMenuPoc.MenuModel
{
    internal class MenuItem : IMenuItem
    {
        public string Text { get; set; }
        public string IconPath { get; set; }
        public ICommand Command { get; set; }
    }
}