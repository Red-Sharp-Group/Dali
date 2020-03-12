using RedSharp.Dali.Common.Interfaces;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using Unity;

namespace RedSharp.Dali.ViewModel
{
    /// <summary>
    /// Class to register all dependencies from the assembly with ViewModels.
    /// </summary>
    public static class EntryPoint
    {
        /// <summary>
        /// Initializes dependencies.
        /// </summary>
        /// <param name="container">DI container.</param>
        /// <remarks>
        /// Find a way to separate this from concrete DI implementation.
        /// </remarks>
        public static void InitilizeContainer(IUnityContainer container)
        {
            //Data providers
            container.RegisterInstance<ISettingsProvider>(new SettingsProvider());

            //ViewModels
            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
        }
    }
}
