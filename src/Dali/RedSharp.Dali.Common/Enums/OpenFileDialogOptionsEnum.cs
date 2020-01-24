using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Enums
{
    /// <summary>
    /// Defines set of options that might be used to customize open file dialog behavior.
    /// </summary>
    /// <remarks>
    /// Based on Microsoft.Win32.OpenFileDialog. For other implementations missing ones should
    /// be implemented.
    /// </remarks>
    [Flags]
    public enum OpenFileDialogOptionsEnum : ushort
    {
        Multiselect = 1,
        CheckFileExists = 1 << 1,
        CheckPathExists = 1 << 2,
        AddExtension = 1 << 3,
        DereferenceLinks = 1 << 4,
        ReadOnlyCheked = 1 << 5,
        ShowReadOnly = 1 << 6,
        ValidateNames = 1 << 7
    }
}
