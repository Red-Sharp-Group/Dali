using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Enums
{
    /// <summary>
    /// List of modify buttons for register global HotKeys.
    /// </summary>
    /// <SecurityNote>
    /// Do not change values.
    /// </SecurityNote>
    public enum HotkeyModifier
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        Win = 8
    }
}
