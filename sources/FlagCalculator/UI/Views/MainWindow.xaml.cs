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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DustInTheWind.FlagCalculator.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            TabItem tabItem = e.Source as TabItem;

            if (tabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
        }


        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            TabItem target = e.Source as TabItem;
            TabItem source = e.Data.GetData(typeof(TabItem)) as TabItem;

            if (!target.Equals(source))
            {
                //var tabControl = target.Parent as TabControl;
                var tabControl = TabControl1;
                int sourceIndex = tabControl.Items.IndexOf(source);
                int targetIndex = tabControl.Items.IndexOf(target);

                tabControl.Items.Remove(source);
                tabControl.Items.Insert(targetIndex, source);

                tabControl.Items.Remove(target);
                tabControl.Items.Insert(sourceIndex, target);
            }
        }
    }
}
