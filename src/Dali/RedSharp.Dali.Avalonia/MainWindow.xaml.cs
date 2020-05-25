using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using RedSharp.Dali.Avalonia.Controls.Windows;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using Unity;

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

        [Dependency]
        public IMainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}