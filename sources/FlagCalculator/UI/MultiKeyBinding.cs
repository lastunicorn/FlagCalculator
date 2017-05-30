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