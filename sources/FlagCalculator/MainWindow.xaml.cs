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
using System.Windows.Input;

namespace DustInTheWind.FlagCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandleKeyDownEvent);
        }

        private void TextBlockValue_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                Clipboard.SetText(TextBlockValue.Text);
        }

        private void HandleKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                string value = ((MainWindowViewModel)DataContext).Value.ToString();
                Clipboard.SetText(value);
            }

            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ulong value;

                bool success = ulong.TryParse(Clipboard.GetText(), out value);

                if (success)
                    ((MainWindowViewModel)DataContext).Value = value;
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                int digit = e.Key - Key.D0;
                ((MainWindowViewModel)DataContext).Value = ((MainWindowViewModel)DataContext).Value * 10 + (ulong)digit;
            }

            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                int digit = e.Key - Key.NumPad0;
                ((MainWindowViewModel)DataContext).Value = ((MainWindowViewModel)DataContext).Value * 10 + (ulong)digit;
            }

            if (e.Key == Key.Escape)
            {
                ((MainWindowViewModel)DataContext).Clear();
            }
        }
    }
}
