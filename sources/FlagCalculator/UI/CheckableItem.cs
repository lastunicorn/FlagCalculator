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
using DustInTheWind.FlagCalculator.Business;
using DustInTheWind.FlagCalculator.UI.Commands;

namespace DustInTheWind.FlagCalculator.UI
{
    internal class CheckableItem : ViewModelBase
    {
        private bool isChecked;
        private Visibility visibility;
        private ulong value;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged();
            }
        }

        public string Text { get; set; }

        public ulong Value
        {
            get { return value; }
            set
            {
                this.value = value;

                if (FlagCheckedCommand != null)
                    FlagCheckedCommand.Value = value;
            }
        }

        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                OnPropertyChanged();
            }
        }

        public FlagCheckedCommand FlagCheckedCommand { get; }

        public CheckableItem(FlagNumber flagNumber)
        {
            FlagCheckedCommand = new FlagCheckedCommand(flagNumber)
            {
                Value = value
            };
        }

        public override string ToString()
        {
            return Text;
        }
    }
}