using System;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Unity;
using RedSharp.Dali.Common.Interfaces.Services;
using RedSharp.Dali.View.Services;

namespace RedSharp.Dali.View
{
    public class App : Application
    {
        internal IUnityContainer Container { get; set; }

        public override void Initialize()
        {
            LoadStyle("avares://Avalonia.Themes.Default/DefaultTheme.xaml");
            LoadStyle("avares://Avalonia.Themes.Default/Accents/BaseLight.xaml");
            LoadStyle("resm:RedSharp.Dali.Avalonia.Controls.Themes.Styles.xaml?assembly=RedSharp.Dali.Avalonia.Controls");
        }

        private void LoadStyle(string uri)
        {
            StyleInclude include = new StyleInclude(new Uri("avares://Themes?assembly=RedSharp.Dali.Avalonia.Controls"))
            {
                Source = new Uri(uri)
            };
            Styles.Add(include);
        }
        
        public override void OnFrameworkInitializationCompleted()
        {  
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.Startup += OnLifetimeStart;

            base.OnFrameworkInitializationCompleted();
        }

        private void OnLifetimeStart(object lifetime, ControlledApplicationLifetimeStartupEventArgs args)
        {
            Container.RegisterType<IDialogService, DialogService>();

            if(lifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.MainWindow = Container.Resolve<MainWindow>();
        }
   }
}