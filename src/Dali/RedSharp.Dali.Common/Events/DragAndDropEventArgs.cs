using RedSharp.Dali.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Events
{
    public class DragAndDropEventArgs
    {
        public DragAndDropEffectsEnum AllowedEffects { get; set; }
        public IDictionary<DropTypeEnum, object> Data { get; set; }
        public DragAndDropEffectsEnum Effects { get; set; }
        public DragAndDropKeyStatesEnum KeyStates { get; set; }
        public bool Handled { get; set; }
    }
}
