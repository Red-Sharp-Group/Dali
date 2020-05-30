using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Interfaces.Services;

namespace RedSharp.Dali.View.Services
{
    /// <summary>
    /// Wraps <see cref="Dispatcher"/> for operation dispatching.
    /// </summary>
    class DispatcherService : IDispatcher
    {
        /// <summary>
        /// Object that actually do the work.
        /// </summary>
        private readonly Dispatcher _nativeDispatcher;

        /// <summary>
        /// Mappings between framework independent and WPF dispatcher priority.
        /// </summary>
        private IReadOnlyDictionary<DaliDispatcherPriorityEnum, DispatcherPriority> _enumMapper =
            new ReadOnlyDictionary<DaliDispatcherPriorityEnum, DispatcherPriority>(
                new Dictionary<DaliDispatcherPriorityEnum, DispatcherPriority>
                {
                    { DaliDispatcherPriorityEnum.Canceled, DispatcherPriority.Inactive },
                    { DaliDispatcherPriorityEnum.Idle, DispatcherPriority.ContextIdle },
                    { DaliDispatcherPriorityEnum.Background, DispatcherPriority.Background },
                    { DaliDispatcherPriorityEnum.Render, DispatcherPriority.Render },
                    { DaliDispatcherPriorityEnum.Foreground, DispatcherPriority.Normal }
                });

        /// <summary>
        /// Constructs the <see cref="DialogService"/> instanse.
        /// </summary>
        /// <param name="dispatcher">WPF dispatcher.</param>
        public DispatcherService(Dispatcher dispatcher)
        {
            _nativeDispatcher = dispatcher;
        }

        ///<inheritdoc/>
        public void BeginInvoke(Action action, DaliDispatcherPriorityEnum priority)
        {
            if (action == null)
                throw new ArgumentNullException($"{nameof(action)} parameter is null");

            _nativeDispatcher.BeginInvoke(action, _enumMapper[priority]);
        }

        ///<inheritdoc/>
        public void Invoke(Action action)
        {
            if (action == null)
                throw new ArgumentNullException($"{nameof(action)} parameter is null");

            _nativeDispatcher.Invoke(action);
        }
    }
}
