using RedSharp.Dali.Common.Interfaces.ViewModels;
using RedSharp.Dali.Controls.Windows;
using Unity;

namespace RedSharp.Dali.View.Windows
{
    public partial class MainWindow : DaliWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Dependency]
        public IMainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
