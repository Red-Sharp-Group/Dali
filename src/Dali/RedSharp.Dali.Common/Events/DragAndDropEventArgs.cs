using RedSharp.Dali.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Events
{
    /// <summary>
    /// Cross platform representation of drag or drop event occured in application.
    /// </summary>
    public class DragAndDropEventArgs
    {
        /// <summary>
        /// Effects that might be set to handle event.
        /// </summary>
        public DragAndDropEffectsEnum AllowedEffects { get; set; }
        
        /// <summary>
        /// Data to drop. Should not be null.
        /// </summary>
        public IDictionary<DropTypeEnum, object> Data { get; set; }

        /// <summary>
        /// Type of handling the drag and drop request.
        /// </summary>
        public DragAndDropEffectsEnum Effects { get; set; }

        /// <summary>
        /// States of modifiers while dragging. 
        /// </summary>
        public DragAndDropKeyStatesEnum KeyStates { get; set; }

        /// <summary>
        /// Indicates if event should passed next to item that can handle it or it was processed.
        /// </summary>
        public bool Handled { get; set; }
    }
}
