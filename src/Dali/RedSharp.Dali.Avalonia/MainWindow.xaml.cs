using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using RedSharp.Dali.Avalonia.Controls.Windows;

namespace RedSharp.Dali.View
{
    public class MainWindow : DaliWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //ListBox listBox;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}