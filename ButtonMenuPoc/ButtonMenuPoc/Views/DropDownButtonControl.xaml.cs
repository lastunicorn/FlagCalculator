using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DustInTheWind.ButtonMenuPoc.Views
{
    /// <summary>
    /// Interaction logic for DropDownButtonControl.xaml
    /// </summary>
    public partial class DropDownButtonControl : UserControl
    {
        /// <summary>
        /// Gets or sets additional content for the UserControl
        /// </summary>
        public object ButtonContent
        {
            get { return (object)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register("ButtonContent", typeof(object), typeof(DropDownButtonControl), new PropertyMetadata(null));
        
        public double PopupMargin { get; set; }

        public DropDownButtonControl()
        {
            InitializeComponent();

            PopupMargin = 2;
            Popup1.CustomPopupPlacementCallback = GetPopupPlacement;
        }

        private void UserControl1_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private CustomPopupPlacement[] GetPopupPlacement(Size popupSize, Size targetSize, Point offset)
        {
            double x = targetSize.Width - popupSize.Width;
            double y = -popupSize.Height - PopupMargin;

            Point point = new Point(x, y);

            return new[]
            {
                new CustomPopupPlacement(point, PopupPrimaryAxis.None)
            };
        }
    }
}