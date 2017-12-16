using System.Windows;
using System.Windows.Controls;
using DustInTheWind.ButtonMenuPoc.MenuModel;
using MenuItem = DustInTheWind.ButtonMenuPoc.MenuModel.MenuItem;

namespace DustInTheWind.ButtonMenuPoc
{
    public class MenuItemDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null)
            {
                if (item is MenuItem)
                    return element.FindResource("MenuItemDataTemplate") as DataTemplate;

                if (item is MenuItemSeparator)
                    return element.FindResource("MenuItemSeparatorDataTemplate") as DataTemplate;

                if (item is string)
                    return element.FindResource("StringDataTemplate") as DataTemplate;
            }

            return null;
        }
    }
}