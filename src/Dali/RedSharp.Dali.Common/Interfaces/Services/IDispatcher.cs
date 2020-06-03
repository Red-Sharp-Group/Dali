using System;
using RedSharp.Dali.Common.Enums;

namespace RedSharp.Dali.Common.Interfaces.Services
{
    //Maybe we should add possibilty to receive return value for both sync and async commands.
    //Also IAwaiter object might be introduced to receive notifications about finishing or cancelling
    //operation.

    /// <summary>
    /// Wraps platform detendent dispather to be able to work with it in cross platform assemblies.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Synchronously executes operation on main, UI, thread.
        /// </summary>
        /// <param name="action">Action to be executed.</param>
        void Invoke(Action action);

        /// <summary>
        /// Executes operation on UI thread asynchronously with some priority.
        /// </summary>
        /// <param name="action">Action to be executed.</param>
        /// <param name="priority">Priority to execute operations. Actions with higher priority will be executed first.
        /// Look at <see cref="DaliDispatcherPriorityEnum"/> for more details.</param>
        void BeginInvoke(Action action, DaliDispatcherPriorityEnum priority);

    }
}
