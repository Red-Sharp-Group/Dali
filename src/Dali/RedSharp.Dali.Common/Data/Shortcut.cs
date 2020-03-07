using RedSharp.Dali.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Data
{
    /// <summary>
    /// Represent a shortcut with key and some modifier.
    /// </summary>
    public class Shortcut
    {

        #region Public properties
        /// <summary>
        /// Actual key.
        /// </summary>
        public KeyEnum Key { get; }

        /// <summary>
        /// Shortcut modifier.
        /// </summary>
        public HotkeyModifier Modifier { get; }
        #endregion

        #region Construction
        public Shortcut(KeyEnum key, HotkeyModifier modifier)
        {
            Key = key;
            Modifier = modifier;
        }
        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj is Shortcut shortcut)
                return EqualsPrivate(shortcut);

            return false;
        }

        public bool Equals(Shortcut shortcut)
        {
            if (shortcut == null)
                throw new ArgumentNullException($"{nameof(shortcut)} is null");

            return EqualsPrivate(shortcut);
        }

        private bool EqualsPrivate(Shortcut shortcut)
        {
            return shortcut.Key == Key && shortcut.Modifier == Modifier;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode() ^ Modifier.GetHashCode();
        }

        public static bool operator ==(Shortcut obj1, Shortcut obj2)
        {
            if (obj1 is null && obj2 is null)
            {
                return true;
            }
            else if (!(obj1 is null) && !(obj2 is null))
            {
                return obj1.Equals(obj2);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Shortcut obj1, Shortcut obj2)
        {
            return !(obj1 == obj2);
        }

        #endregion
    }
}
