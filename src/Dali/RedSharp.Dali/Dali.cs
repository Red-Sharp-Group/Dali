using System;
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
            App.Run(args);
            #else
            App app = new App(Container);

            app.Run();
            #endif
        }
    }
}
