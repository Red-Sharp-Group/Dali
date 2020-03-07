using RedSharp.Dali.Common.Interop.Helpers;
using RedSharp.Dali.ViewModel;
using System.Windows;

namespace RedSharp.Dali.View.Windows
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