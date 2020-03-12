using Microsoft.Xaml.Behaviors;
using RedSharp.Dali.Common.Interop.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RedSharp.Dali.View.Behaviors
{
    /// <summary>
    /// Allows to make window transparent for all input events.
    /// </summary>
    class InputTransparencyBehavior : Behavior<Window>
    {
        /// <summary>
        /// Backing property for <seealso cref="IsInputTransparent"/>.
        /// </summary>
        public static readonly DependencyProperty IsInputTransparentProperty =
            DependencyProperty.Register(nameof(IsInputTransparent), typeof(bool), typeof(InputTransparencyBehavior),
                new PropertyMetadata(true, IsInputTransparentPropertyChanged));


        /// <summary>
        /// Gets or sets value that indicates is window transparent for input events.
        /// </summary>
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
