using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Dialogs;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using RedSharp.Dali.Common.Interfaces;
using Unity;

namespace RedSharp.Dali.View
{
    public class DaliApplicationBuilder : IApplicationBuilder
    {
        private AppBuilder _builder;
        private ClassicDesktopStyleApplicationLifetime _lifetime;

        public IApplicationBuilder Configure(string[] args)
        {
            _lifetime = new ClassicDesktopStyleApplicationLifetime() { ShutdownMode = ShutdownMode.OnMainWindowClose };
            _builder = AppBuilder.Configure<App>();

            _builder.UsePlatformDetect()
                    .UseReactiveUI()
                    .UseManagedSystemDialogs()
                    .LogToDebug()
                    .SetupWithLifetime(_lifetime);

            return this;
        }

        public IApplicationBuilder WithDIContainer<T>(T container)
        {
            if(typeof(T) == typeof(IUnityContainer))
            {
                (_builder.Instance as App).Container = container as IUnityContainer;
                return this;
            }

            throw new NotSupportedException();
        }

        public int Run(string[] args)
        {
            _lifetime.Start(args);
            return 0;
        }
    }
}