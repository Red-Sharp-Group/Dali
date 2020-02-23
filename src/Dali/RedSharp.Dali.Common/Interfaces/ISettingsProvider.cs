using RedSharp.Dali.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces
{
    public interface ISettingsProvider
    {
        Shortcut TransparencyShortcut { get; set; }
        Shortcut CloseTransparentWindowShortcut { get; set; }

        void InitializeSettings(ApplicationSettings settings);
    }
}
