using RedSharp.Dali.Common.Interfaces.Services;
using RedSharp.Dali.Services;
using RedSharp.Dali.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace RedSharp.Dali
{
    public partial class App : Application
    {
        public IUnityContainer Container { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeContainer();

            MainWindow = Container.Resolve<MainWindow>();
            MainWindow.Show();
        }

        private void InitializeContainer()
        {
            Container = new UnityContainer();

            Container.RegisterType<IDialogService, DialogService>();
        }
    }
}
