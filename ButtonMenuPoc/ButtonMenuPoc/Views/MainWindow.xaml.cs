using System.Windows;
using DustInTheWind.ButtonMenuPoc.ViewModels;

namespace DustInTheWind.ButtonMenuPoc.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}