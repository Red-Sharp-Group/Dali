using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Enums
{
    /// <summary>
    /// Enum to specify what kind of data will be dropped into application.
    /// </summary>
    public enum DropTypeEnum
    {
        /// <summary>
        /// File system path.
        /// </summary>
        FilePath,
        /// <summary>
        /// Memory stream for bitmap.
        /// </summary>
        Bitmap,
        /// <summary>
        /// Some text. Currently Chrome passes URL as text.
        /// </summary>
        Text
    }
}
