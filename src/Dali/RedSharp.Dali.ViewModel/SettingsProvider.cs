using System;
using ReactiveUI;
using RedSharp.Dali.Common.Data;
using RedSharp.Dali.Common.Interfaces;

namespace RedSharp.Dali.ViewModel
{
    /// <summary>
    /// Implements <see cref="System.ComponentModel.INotifyPropertyChanged"/>
    /// and <see cref="IServiceProvider"/>. Exposes application settings.
    /// </summary>
    public class SettingsProvider : ReactiveObject, ISettingsProvider
    {
        /// <summary>
        /// Actual settings. Used for saving.
        /// </summary>
        private ApplicationSettings _settings;

        /// <inheritdoc/>
        public Shortcut TransparencyShortcut
        {
            get
            {
                return _settings.TransparenceShortcut;
            }
            set
            {
                if (_settings.TransparenceShortcut != value)
                {
                    _settings.TransparenceShortcut = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        ///<inheritdoc/>
        public Shortcut CloseTransparentWindowShortcut 
        {
            get 
            {
                return _settings.CloseTransparentWindowShortcut;
            }
            set
            {
                if (_settings.CloseTransparentWindowShortcut != value)
                {
                    _settings.CloseTransparentWindowShortcut = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Constructs new <see cref="SettingsProvider"/> object with default settings.
        /// </summary>
        public SettingsProvider()
        {
            _settings = new ApplicationSettings();
        }

        /// <summary>
        /// Constructs new <see cref="SettingsProvider"/> object with available settings.
        /// </summary>
        /// <param name="settings"></param>
        public SettingsProvider(ApplicationSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Sets settings and raises all properties.
        /// </summary>
        /// <param name="settings"></param>
        public void InitializeSettings(ApplicationSettings settings)
        {
            _settings = settings;

            this.RaisePropertyChanged(nameof(TransparencyShortcut));
            this.RaisePropertyChanged(nameof(CloseTransparentWindowShortcut));
        }
    }
}
