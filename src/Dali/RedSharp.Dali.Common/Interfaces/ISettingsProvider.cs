using RedSharp.Dali.Common.Data;
using System.ComponentModel;

namespace RedSharp.Dali.Common.Interfaces
{
    /// <summary>
    /// Exposes application setting. Proxy to <see cref="ApplicationSettings"/>. 
    /// </summary>
    public interface ISettingsProvider : INotifyPropertyChanged
    {
        /// <summary>
        /// Proxy to <see cref="ApplicationSettings.TransparenceShortcut"/>.
        /// </summary>
        Shortcut TransparencyShortcut { get; set; }

        /// <summary>
        /// Proxy to <see cref="ApplicationSettings.CloseTransparentWindowShortcut"/>.
        /// </summary>
        Shortcut CloseTransparentWindowShortcut { get; set; }

        /// <summary>
        /// Loads setting and fires RaisePropertyChanged for all properties.
        /// </summary>
        /// <param name="settings"></param>
        void InitializeSettings(ApplicationSettings settings);
    }
}
