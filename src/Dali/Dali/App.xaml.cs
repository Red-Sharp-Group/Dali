using RedSharp.Dali.Common.Interfaces.Services;
using RedSharp.Dali.Services;
using RedSharp.Dali.View;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using Unity;

namespace RedSharp.Dali
{
    public partial class App : Application
    {
        private IUnityContainer Container { get; set; }

		/// <summary>
		/// Current application language.
		/// </summary>
		/// <remarks>
		/// If application have no defined resources for given culture - default one will be selected.
		/// </remarks>
		public CultureInfo Language
		{
			get
			{
				return Dispatcher.Thread.CurrentUICulture;
			}
			set
			{
				if (value == null) 
					throw new ArgumentNullException("Language value is null.");

				if (value == Dispatcher.Thread.CurrentUICulture) 
					return;

				Dispatcher.Thread.CurrentUICulture = value;

				ResourceDictionary dict = new ResourceDictionary();

				//TODO: refactor to dictionary after providing localization.
				switch (value.Name)
				{
					case "uk-UA":
						dict.Source = new Uri(string.Format("Resources/Strings/Strings.{0}.xaml", value.Name), 
											  UriKind.Relative);
						break;
					default:
						dict.Source = new Uri("Resources/Strings/Strings.xaml", 
											  UriKind.Relative);
						break;
				}

				ResourceDictionary oldDict = Resources.MergedDictionaries.FirstOrDefault(d => d.Source.OriginalString.StartsWith("Resources/Strings/Strings."));
				if (oldDict != null)
				{
					int ind = Resources.MergedDictionaries.IndexOf(oldDict);
					Resources.MergedDictionaries.Remove(oldDict);
					Resources.MergedDictionaries.Insert(ind, dict);
				}
				else
				{
					Resources.MergedDictionaries.Add(dict);
				}
			}
		}

		protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeContainer();

            MainWindow = Container.Resolve<MainWindow>();
            MainWindow.Show();
        }

        /// <summary>
        /// Point to create DI container and register all dependencies.
        /// </summary>
        private void InitializeContainer()
        {
            Container = new UnityContainer();

            Container.RegisterType<IDialogService, DialogService>();
        }
    }
}
