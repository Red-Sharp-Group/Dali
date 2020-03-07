using Microsoft.Xaml.Behaviors;
using RedSharp.Dali.Common.Interop.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RedSharp.Dali.View.Behaviors
{
    class InputTransparencyBehavior : Behavior<Window>
    {
        private static string IsInputTransparentPropertyName = "IsInputTransparent";

        public static readonly DependencyProperty IsInputTransparentProperty =
            DependencyProperty.Register(IsInputTransparentPropertyName, typeof(bool), typeof(InputTransparencyBehavior),
                new PropertyMetadata(true, IsInputTransparentPropertyChanged));

        public bool IsInputTransparent
        {
            get => (bool)GetValue(IsInputTransparentProperty);
            set => SetValue(IsInputTransparentProperty, value);
        }

        private static void IsInputTransparentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            InputTransparencyBehavior behavior = sender as InputTransparencyBehavior;

            if ((bool)args.NewValue)
            {
                WindowsStyleHelper.TryEnableInputTransparency(behavior.AssociatedObject);
            }
            else
            {
                WindowsStyleHelper.TryDisableInputTransparency(behavior.AssociatedObject);
            }
        }
    }
}
