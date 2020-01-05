using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Interop;

using RedSharp.Dali.Common.Interop.Native;
using RedSharp.Dali.Common.Interop.Native.Enums;

namespace RedSharp.Dali.Common.Interop.Helpers
{
    /// <summary>
    /// Contains semi-safe wrappers under HWND functions.
    /// </summary>
    /// <remarks>
    /// Must be internal because contain a lot of work with IntPtr.
    /// </remarks>
    internal static class InternalWindowsHelper
    {
        /// <summary>
        /// Returns all set extended styles for the input window.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If input window handle is equal to IntPtr.Zero.
        /// </exception>
        /// <SecurityNote>
        /// Security critical, method works with IntPtr directly.
        /// Must be internal.
        /// </SecurityNote>
        [SecurityCritical]
        internal static WindowStylesEx GetWindowsExtendedStyle(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
                throw new ArgumentOutOfRangeException(nameof(hwnd));

            return (WindowStylesEx)NativeFunctions.GetWindowLong(hwnd, WindowsLong.GWL_EXSTYLE);
        }

        /// <summary>
        /// Returns true if successfully set extended styles for the input window.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If input window handle is equal to IntPtr.Zero.
        /// </exception>
        /// <SecurityNote>
        /// Security critical, method works with IntPtr directly.
        /// Must be internal.
        /// </SecurityNote>
        [SecurityCritical]
        internal static bool TrySetWindowsExtendedStyle(IntPtr hwnd, WindowStylesEx windowStyles)
        {
            if (hwnd == IntPtr.Zero)
                throw new ArgumentOutOfRangeException(nameof(hwnd));

            int result = NativeFunctions.SetWindowLong(hwnd, WindowsLong.GWL_EXSTYLE, (int)windowStyles);

            return result != 0;
        }
        /// <summary>
        /// Returns true if input extended style is set for the window.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If input window handle is equal to IntPtr.Zero.
        /// </exception>
        /// <SecurityNote>
        /// Security critical, method works with IntPtr directly.
        /// Must be internal.
        /// </SecurityNote>
        [SecurityCritical]
        internal static bool IsContainExtendedStyle(IntPtr hwnd, WindowStylesEx windowStyle)
        {
            if (hwnd == IntPtr.Zero)
                throw new ArgumentOutOfRangeException(nameof(hwnd));

            WindowStylesEx styles = GetWindowsExtendedStyle(hwnd);

            return styles.HasFlag(windowStyle);
        }

        /// <summary>
        /// Returns windows handle from window class instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If input window is null.
        /// </exception>
        /// <SecurityNote>
        /// Security critical, method works with IntPtr directly.
        /// Must be internal.
        /// </SecurityNote>
        [SecurityCritical]
        internal static IntPtr GetWindowHandle(Window window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            var interopHelper = new WindowInteropHelper(window);

            return interopHelper.Handle;
        }
    }
}
