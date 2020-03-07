using RedSharp.Dali.Common.Interfaces;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using Unity;

namespace RedSharp.Dali.ViewModel
{
    public static class EntryPoint
    {
        public static void InitilizeContainer(IUnityContainer container)
        {
            //Data providers
            container.RegisterInstance<ISettingsProvider>(new SettingsProvider());

            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();
        }
    }
}
