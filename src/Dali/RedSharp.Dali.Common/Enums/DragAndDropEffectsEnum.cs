using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Enums
{
    /// <summary>
    /// Specifies the effects of a drag-and-drop operation.
    /// </summary>
    [Flags]
    public enum DragAndDropEffectsEnum
    {
        /// <summary>
        /// The drop target does not accept the data.
        /// </summary>
        None = 0,

        /// <summary>
        /// The data is copied to the drop target.
        /// </summary>
        Copy = 1,

        /// <summary>
        /// The data from the drag source is moved to the drop target.
        /// </summary>
        Move = 2,

        /// <summary>
        /// The data from the drag source is linked to the drop target.
        /// </summary>
        Link = 4
    }
}
