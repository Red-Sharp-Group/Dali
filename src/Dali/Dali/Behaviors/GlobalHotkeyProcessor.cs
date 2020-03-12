using System;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Xaml.Behaviors;
using RedSharp.Dali.Common.Interop;
using RedSharp.Dali.Common.Data;
using System.Windows.Input;
using RedSharp.Dali.Common.Interfaces;
using RedSharp.Dali.Common.Enums;

namespace RedSharp.Dali.View.Behaviors
{
    /// <summary>
    /// Wraps functionality of <see cref="GlobalHotkeyProvider"/> and makes it possible to 
    /// process it on DataContext of assosiated object.
    /// </summary>
    class GlobalHotkeyProcessor : Behavior<Window>, IDisposable
    {
        /// <summary>
        /// List of all registered providers.
        /// </summary>
        private IList<GlobalHotkeyProvider> _globalHotkeyProviders =
            new List<GlobalHotkeyProvider>();

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.DataContextChanged += OnDataContextChanged;
            AssociatedObject.Loaded += OnWindowLoaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DataContextChanged -= OnDataContextChanged;
            AssociatedObject.Loaded -= OnWindowLoaded;
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;

            Dispose();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            //It possible to get native HWND only after window is loaded.
            RegisterHotkeys();
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Maybe old ones should be freed here?
            if (AssociatedObject.IsLoaded)
                RegisterHotkeys();
        }


        private void OnHotkeyPressed(HotkeyModifier arg1, Key arg2)
        {
            IHotkeyProcessor processor = AssociatedObject.DataContext as IHotkeyProcessor;

            if(processor != null)
                processor.ProcessShortcut(new Shortcut((KeyEnum)arg2, arg1));   
        }

        private void RegisterHotkeys()
        {
            IHotkeyProcessor processor = AssociatedObject.DataContext as IHotkeyProcessor;

            if (processor == null)
                return;

            foreach (Shortcut shortcut in processor.Shortcuts)
            {
                GlobalHotkeyProvider provider = new GlobalHotkeyProvider(AssociatedObject);

                if (provider.Register(shortcut.Modifier, (Key)shortcut.Key))
                {
                    provider.OnHotkeyPressed += OnHotkeyPressed;

                    _globalHotkeyProviders.Add(provider);
                }
            }
        }

        public void Dispose()
        {
            if (_globalHotkeyProviders == null)
                return;

            foreach (GlobalHotkeyProvider hotkeyProvider in _globalHotkeyProviders)
            {
                hotkeyProvider.OnHotkeyPressed -= OnHotkeyPressed;
                hotkeyProvider.Dispose();
            }

            _globalHotkeyProviders = null;
        }
    }
}
