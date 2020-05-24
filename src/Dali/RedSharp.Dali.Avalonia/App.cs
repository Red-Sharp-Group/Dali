using System;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Logging.Serilog;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace RedSharp.Dali.View
{
    public class App : Application
    {
        public static int Run(string[] args)
        {
            return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        private static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                             .UsePlatformDetect()
                             .LogToDebug();
        }                 

        public override void Initialize()
        {
            LoadStyle("avares://Avalonia.Themes.Default/DefaultTheme.xaml");
            LoadStyle("avares://Avalonia.Themes.Default/Accents/BaseLight.xaml");
            LoadStyle("resm:RedSharp.Dali.Avalonia.Controls.Themes.Styles.xaml?assembly=RedSharp.Dali.Avalonia.Controls");

            /*ResourceInclude include = new ResourceInclude()
            {
                Source = new Uri("resm:RedSharp.Dali.Avalonia.Controls.Themes.Brushes.xaml?assembly=RedSharp.Dali.Avalonia.Controls")
            };
            Resources.MergedDictionaries.Add(include);*/
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
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
   }
}