using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interop.Native.Enums
{
    /// <summary>
    /// Used in the GetWindowLong and SetWindowLong functions as index value.
    /// In the general idea this is the already predefined zero-based offset 
    /// to the value to be retrieved from the windows structure. 
    /// <para/>
    /// See more: <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlonga"/>
    /// </summary>
    internal enum WindowsLong : int
    {
        /// <summary>
        /// Sets a new extended window style.
        /// </summary>
        GWL_EXSTYLE = -20,

        /// <summary>
        /// Sets a new application instance handle.
        /// </summary>
        GWL_HINSTANCE = -6,

        /// <summary>
        /// Sets a new identifier of the child window.
        /// The window cannot be a top-level window.
        /// </summary>
        GWL_ID = -12,

        /// <summary>
        /// Sets a new window style.
        /// </summary>
        GWL_STYLE = -16,

        /// <summary>
        /// Sets the user data associated with the window. 
        /// This data is intended for use by the application that created the window. 
        /// Its value is initially zero.
        /// </summary>
        GWL_USERDATA = -21,

        /// <summary>
        /// Sets a new address for the window procedure.
        /// You cannot change this attribute if the window does not belong 
        /// to the same process as the calling thread.
        /// </summary>
        GWL_WNDPROC = -4,
    }
}
