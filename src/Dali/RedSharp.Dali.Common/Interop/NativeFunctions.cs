using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RedSharp.Dali.Common.Interop
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class NativeFunctions
    {

        [DllImport(NativeLibrariesNames.User32)]
        internal static extern bool RegisterHotKey
        (
            [In] IntPtr hWnd,
            [In] int id,
            [In] int fsModifiers,
            [In] int vk
        );

        [DllImport(NativeLibrariesNames.User32)]
        internal static extern bool UnregisterHotKey
        (
            [In] IntPtr hWnd,
            [In] int id
        );
    }
}
