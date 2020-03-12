using RedSharp.Dali.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces
{
    /// <summary>
    /// Identifies object that can work with hotkey.
    /// </summary>
    public interface IHotkeyProcessor
    {
        /// <summary>
        /// Gets shortcuts to register.
        /// </summary>
        IEnumerable<Shortcut> Shortcuts { get; }

        /// <summary>
        /// Processes fired shortcut.
        /// </summary>
        /// <param name="shortcut">Shortcut to process.</param>
        void ProcessShortcut(Shortcut shortcut);
    }
}
