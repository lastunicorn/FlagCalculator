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

using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Markup;

namespace DustInTheWind.FlagCalculator.UI
{
    [ContentProperty("Keys")]
    public class MultiKeyBinding : InputBinding
    {
        public ModifierKeys Modifiers;
        public List<Key> Keys = new List<Key>();

        private InputGesture _gesture;

        public override InputGesture Gesture
        {
            get
            {
                if (_gesture == null) _gesture = new MultiKeyGesture { Parent = this };
                return _gesture;
            }
            set { throw new InvalidOperationException(); }
        }

        private class MultiKeyGesture : InputGesture
        {
            public MultiKeyBinding Parent { get; set; }

            public override bool Matches(object target, InputEventArgs e)
            {
                bool match =
                    e is KeyEventArgs &&
                    Parent.Modifiers == Keyboard.Modifiers &&
                    Parent.Keys.Contains(((KeyEventArgs)e).Key);

                // Pass actual key as CommandParameter
                if (match) Parent.CommandParameter = ((KeyEventArgs)e).Key;

                return match;
            }
        }
    }
}