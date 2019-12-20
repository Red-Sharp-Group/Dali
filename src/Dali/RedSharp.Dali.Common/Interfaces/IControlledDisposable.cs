using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces
{
    /// <summary>
    /// Created for ability to find out already disposed objects.
    /// </summary>
    public interface IControlledDisposable : IDisposable
    {
        /// <summary>
        /// Marks that current instance is released and can't be used anymore.
        /// </summary>
        /// <remarks>
        /// Safe to invoke - all types, which will implement this 
        /// must guarantee do not throw an exception when calling this property.
        /// </remarks>
        bool IsDisposed { get; }
    }
}
