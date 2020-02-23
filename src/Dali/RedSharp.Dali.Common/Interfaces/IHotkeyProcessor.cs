using RedSharp.Dali.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces
{
    public interface IHotkeyProcessor
    {
        IEnumerable<Shortcut> Shortcuts { get; }

        void ProcessShortcut(Shortcut shortcut);
    }
}
