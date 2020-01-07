using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using RedSharp.Dali.Common.Interop.Native.Enums;

namespace RedSharp.Dali.Common.Interop.Helpers
{
    /// <summary>
    /// Helps to set extended style for the WPF window in safe manner.
    /// </summary>
    public static class WindowsStyleHelper
    {
        private const String InputWindowHasInvalidHandle = "Input window has invalid handle.";

        /// <summary>
        /// Enable mouse input transparency for the given window.
        /// It doesn't work if window isn't layered.
        /// </summary>
        /// <remarks>
        /// Returns true if input transparency is already enabled.
        /// Returns false if window isn't layered.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// If input window is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If input window handle is equal to IntPtr.Zero.
        /// </exception>
        /// <SecurityNote>
        /// This method isn't security critical, because work with window handle going on internally.
        /// </SecurityNote>
        public static bool TryEnableInputTransparency(Window window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            IntPtr handle = InternalWindowsHelper.GetWindowHandle(window);

            if (handle == IntPtr.Zero)
                throw new ArgumentException(InputWindowHasInvalidHandle);

            WindowStylesEx windowStyles = InternalWindowsHelper.GetWindowsExtendedStyle(handle);

            if (!windowStyles.HasFlag(WindowStylesEx.WS_EX_LAYERED))
                return false;

            if (!windowStyles.HasFlag(WindowStylesEx.WS_EX_TRANSPARENT))
            {
                WindowStylesEx newStyle = windowStyles | WindowStylesEx.WS_EX_TRANSPARENT;

                return InternalWindowsHelper.TrySetWindowsExtendedStyle(handle, newStyle);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Disable mouse input transparency for the given window.
        /// </summary>
        /// <remarks>
        /// Returns true if input transparency is already disabled or if window isn't layered.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// If input window is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If input window handle is equal to IntPtr.Zero.
        /// </exception>
        /// <SecurityNote>
        /// This method isn't security critical, because work with window handle going on internally.
        /// </SecurityNote>
        public static bool TryDisableInputTransparency(Window window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            IntPtr handle = InternalWindowsHelper.GetWindowHandle(window);

            if (handle == IntPtr.Zero)
                throw new ArgumentException(InputWindowHasInvalidHandle);

            WindowStylesEx windowStyles = InternalWindowsHelper.GetWindowsExtendedStyle(handle);

            if (!windowStyles.HasFlag(WindowStylesEx.WS_EX_LAYERED))
                return false;

            if (windowStyles.HasFlag(WindowStylesEx.WS_EX_TRANSPARENT))
            {
                WindowStylesEx newStyle = windowStyles & ~WindowStylesEx.WS_EX_TRANSPARENT;

                return InternalWindowsHelper.TrySetWindowsExtendedStyle(handle, newStyle);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks that input window is transparency for the mouse input.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// If input window is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If input window handle is equal to IntPtr.Zero.
        /// </exception>
        /// <SecurityNote>
        /// This method isn't security critical, because work with window handle going on internally.
        /// </SecurityNote>
        public static bool HasInputTransparency(Window window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            IntPtr handle = InternalWindowsHelper.GetWindowHandle(window);

            if(handle == IntPtr.Zero)
                throw new ArgumentException(InputWindowHasInvalidHandle);

            return InternalWindowsHelper.IsContainExtendedStyle(handle, WindowStylesEx.WS_EX_TRANSPARENT);
        }
    }
}
