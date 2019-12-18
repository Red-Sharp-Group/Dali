using RedSharp.Dali.Common.GlobalHotkey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RedSharp.Dali.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            base.OnInitialized(e);

            GlobalHotkeyProvider provider = new GlobalHotkeyProvider(Application.Current.MainWindow);

            provider.OnHotkeyPressed += Event;

            provider.Register(HotkeyModifier.Ctrl, InputKeys.R);
        }

        public void Event(HotkeyModifier modifier, InputKeys key)
        {
            MessageBox.Show("Luntic blet");
        }
    }
}
