using RedSharp.Dali.Common.Interop.Helpers;
using RedSharp.Dali.Controls.Windows;
using RedSharp.Dali.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RedSharp.Dali.View
{
    public partial class TransparentWindow : Window
    {
        public static readonly DependencyProperty IsInputTransparentProperty =
            DependencyProperty.Register(nameof(IsInputTransparent), typeof(bool), typeof(TransparentWindow), 
                new PropertyMetadata(true, IsInputTransparentPropertyChanged));

        private static void IsInputTransparentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TransparentWindow window = sender as TransparentWindow;

            if (window != null)
            {
                if (window.IsInputTransparent)
                {
                    WindowsStyleHelper.TryEnableInputTransparency(window);
                }
                else
                {
                    WindowsStyleHelper.TryDisableInputTransparency(window);
                }
            }
        }

        public TransparentWindow()
        {
            InitializeComponent();

            SetBinding(IsInputTransparentProperty, nameof(TransparentWindowViewModel.IsTransparent));
        }

        public bool IsInputTransparent
        {
            get => (bool)GetValue(IsInputTransparentProperty);
            set => SetValue(IsInputTransparentProperty, value);
        }
    }
}