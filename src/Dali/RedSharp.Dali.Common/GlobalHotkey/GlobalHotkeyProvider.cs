using RedSharp.Dali.Common.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

//TODO documentation
//TODO regions

namespace RedSharp.Dali.Common.GlobalHotkey
{
    /// <summary>
    /// TODO
    /// </summary>
    public class GlobalHotkeyProvider : IDisposable
    {
        private HwndSource _source;
        private HotkeyModifier _holdedModifier;
        private InputKeys _holdedKey;
        private int _identifier;
        private IntPtr _windowHandle;

        private const int MessageWindowsHotkey = 0x0312;

        private static readonly HashSet<int> ActiveIdentifiers;

        static GlobalHotkeyProvider()
        {
            ActiveIdentifiers = new HashSet<int>();
        }

        /// <summary>
        /// TODO
        /// </summary>
        public event Action<HotkeyModifier, InputKeys> OnHotkeyPressed;

        /// <summary>
        /// TODO
        /// </summary>
        public GlobalHotkeyProvider(Window window)
        {
            if (window is null)
                throw new ArgumentNullException();

            var interopHelper = new WindowInteropHelper(window);

            _windowHandle = interopHelper.Handle;

            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            IsDisposed = false;
            IsRegistered = false;

            for (_identifier = 0; _identifier < Int16.MaxValue; _identifier++)
            {
                if (!ActiveIdentifiers.Contains(_identifier))
                {
                    ActiveIdentifiers.Add(_identifier);

                    break;
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// TODO
        /// </summary>
        public bool IsRegistered { get; private set; }

        /// <summary>
        /// TODO
        /// </summary>
        public void Register(HotkeyModifier modifier, InputKeys key)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(GlobalHotkeyProvider));

            _holdedModifier = modifier;
            _holdedKey = key;

            bool result = NativeFunctions.RegisterHotKey(_windowHandle, _identifier, (int)_holdedModifier, (int)_holdedKey);

            if (!result)
                Trace.WriteLine("Can't register hotkey");

            IsRegistered = true;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void Unregister()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(GlobalHotkeyProvider));

            if (!IsRegistered)
                return;

            NativeFunctions.UnregisterHotKey(_windowHandle, _identifier);

            IsRegistered = false;
        }

        ~GlobalHotkeyProvider()
        {
            InternalDispose(false);
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void Dispose()
        {
            InternalDispose(true);
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void InternalDispose(bool calledManually)
        {
            if (IsDisposed)
                return;
            
            _source.RemoveHook(HwndHook);

            if (IsRegistered)
                Unregister();

            ActiveIdentifiers.Remove(_identifier);

            IsDisposed = true;

            if (calledManually)
                GC.SuppressFinalize(this);
        }

        /// <summary>
        /// TODO
        /// </summary>
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == MessageWindowsHotkey)
            {
                //int value = (lParam.ToInt32() >> 16) & 0xFFFF;

                //if (value == (int)_holdedKey)
                if (wParam.ToInt32() == _identifier)
                {
                    OnHotkeyPressed?.Invoke(_holdedModifier, _holdedKey);
                }
            }

            return IntPtr.Zero;
        }
    }
}
