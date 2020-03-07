using System;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Xaml.Behaviors;
using RedSharp.Dali.Common.Interop;
using RedSharp.Dali.Common.Data;
using System.Windows.Input;
using RedSharp.Dali.Common.Interfaces;

namespace RedSharp.Dali.View.Behaviors
{
    class GlobalHotkeyProcessor : Behavior<Window>, IDisposable
    {
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
            RegisterHotkeys();
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (AssociatedObject.IsLoaded)
                RegisterHotkeys();
        }


        private void OnHotkeyPressed(Common.Enums.HotkeyModifier arg1, Key arg2)
        {
            IHotkeyProcessor processor = AssociatedObject.DataContext as IHotkeyProcessor;

            if(processor != null)
                processor.ProcessShortcut(new Shortcut((int)arg2, arg1));   
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
