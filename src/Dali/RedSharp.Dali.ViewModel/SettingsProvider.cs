using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using RedSharp.Dali.Common.Data;
using RedSharp.Dali.Common.Interfaces;

namespace RedSharp.Dali.ViewModel
{
    public class SettingsProvider : ReactiveObject, ISettingsProvider
    {
        private ApplicationSettings _settings;

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

        public SettingsProvider()
        {
            _settings = new ApplicationSettings();
        }

        public SettingsProvider(ApplicationSettings settings)
        {
            _settings = settings;
        }

        public void InitializeSettings(ApplicationSettings settings)
        {
            _settings = settings;

            this.RaisePropertyChanged(nameof(TransparencyShortcut));
            this.RaisePropertyChanged(nameof(CloseTransparentWindowShortcut));
        }
    }
}
