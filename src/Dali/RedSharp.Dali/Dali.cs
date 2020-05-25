using System;
using RedSharp.Dali.Common.Interfaces;
using RedSharp.Dali.View;
using Unity;

namespace RedSharp.Dali
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    public class Dali
    {
        /// <summary>
        /// DI container.
        /// </summary>
        private static IUnityContainer Container { get; set; }

        [STAThread]
        public static void Main(string[] args)
        {
            Container = new UnityContainer();
            ViewModel.EntryPoint.InitilizeContainer(Container);

            #if AVALONIA
            
            IApplicationBuilder builder = new DaliApplicationBuilder();
            builder.Configure(args)
                   .WithDIContainer(Container)
                   .Run(args);

            #else
            App app = new App(Container);

            app.Run();
            #endif
        }
    }
}
