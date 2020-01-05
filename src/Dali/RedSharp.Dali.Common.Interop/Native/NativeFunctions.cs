using RedSharp.Dali.Common.Interop.Native.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RedSharp.Dali.Common.Interop.Native
{
    /// <summary>
    /// Contains functions from native WinApi.
    /// </summary>
    /// <SecurityNote>
    /// Must be internal, because whole this assembly created for communication 
    /// with the native functionality, so this functionality doesn't needed for other assemblies.
    /// </SecurityNote>
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

        [DllImport(NativeLibrariesNames.User32)]
        internal static extern int GetWindowLong
        (
            [In] IntPtr hwnd,
            [In] WindowsLong index
        );

        [DllImport(NativeLibrariesNames.User32)]
        internal static extern int SetWindowLong
        (
            [In] IntPtr hwnd,
            [In] WindowsLong index,
            [In] int newStyle
        );
    }
}
