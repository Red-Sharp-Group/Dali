using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Enums
{
    /// <summary>
    /// Enum that indicates result selected in MessageBox.
    /// Created not to add dependecny to WPF MessageBoxResult.
    /// </summary>
    public enum MessageBoxResult
    {
        Ok,
        Cancel,
        None,
        Yes,
        No
    }
}
