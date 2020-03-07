using RedSharp.Dali.Controls.Windows;
using RedSharp.Dali.ViewModel;
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
        public MainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
