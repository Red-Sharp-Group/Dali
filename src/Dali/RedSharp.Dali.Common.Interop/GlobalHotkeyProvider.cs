using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Interfaces;
using RedSharp.Dali.Common.Interop.Native;

namespace RedSharp.Dali.Common.Interop
{
    /// <summary>
    /// Creates global HotKeys for application, that can be invoked everywhere from system.
    /// </summary>
    public class GlobalHotkeyProvider : IControlledDisposable
    {
        #region Members
        //=================================================//
        // Members
        
        private HwndSource _source;
        private HotkeyModifier _holdedModifier;
        private Keys _holdedKeyWinForm;
        private Key _holdedKeyWPF;
        private int _identifier;
        private IntPtr _windowHandle;

        private const int WindowsHotkeyMessageCode = 0x0312;

        private static readonly HashSet<int> ActiveIdentifiers;
        private static readonly HashSet<int> BannedIdentifiers;

        #endregion

        #region Public methods
        //=================================================//
        // Public methods

        static GlobalHotkeyProvider()
        {
            ActiveIdentifiers = new HashSet<int>();
            BannedIdentifiers = new HashSet<int>();
        }

        /// <summary>
        /// Invokes when user press chosen combination of keys.
        /// </summary>
        /// <SecurityNote>
        /// I can't guarantee needed thread.
        /// </SecurityNote>
        public event Action<HotkeyModifier, Key> OnHotkeyPressed;

        /// <summary>
        /// Initializes new instance of provider.
        /// </summary>
        /// <param name="window">
        /// Yep, no IntPtr, because i can't guarantee that input IntPtr is HWND.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Input window is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public GlobalHotkeyProvider(Window window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            var interopHelper = new WindowInteropHelper(window);

            _windowHandle = interopHelper.Handle;

            if (_windowHandle == IntPtr.Zero)
                throw new InvalidOperationException("Input Window has invalid handle.");

            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            IsDisposed = false;
            IsRegistered = false;

            _identifier = GetIdentifier();
        }

        /// <inheritdoc/>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Marks that current provider contain registered combination.
        /// </summary>
        public bool IsRegistered { get; private set; }

        /// <summary>
        /// Registers new global HotKey combination.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Current instance has already disposed.
        /// </exception>
        /// <param name="key">Used WPF key enum</param>
        /// <returns>True if operation was success.</returns>
        public bool Register(HotkeyModifier modifier, Key key)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(GlobalHotkeyProvider));

            if (IsRegistered)
                return false;

            _holdedModifier = modifier;
            _holdedKeyWinForm = (Keys)KeyInterop.VirtualKeyFromKey(key);
            _holdedKeyWPF = key;

            return InternalRegister();
        }

        /// <summary>
        /// Registers new global HotKey combination.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Current instance has already disposed.
        /// </exception>
        /// <param name="key">Used WinForm key enum</param>
        /// <returns>True if operation was success.</returns>
        public bool Register(HotkeyModifier modifier, Keys key)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(GlobalHotkeyProvider));

            if (IsRegistered)
                return false;

            _holdedModifier = modifier;
            _holdedKeyWinForm = key;
            _holdedKeyWPF = KeyInterop.KeyFromVirtualKey((int)key);

            return InternalRegister();
        }

        /// <summary>
        /// Remove already registered combination.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Current instance has already disposed.
        /// </exception>
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

        /// <inheritdoc/>
        public void Dispose()
        {
            InternalDispose(true);
        }

        #endregion

        #region Private methods
        //=================================================//
        // Private methods

        /// <summary>
        /// Tried to register HotKey with current identifier.
        /// </summary>
        private bool InternalRegister()
        {
            bool result = NativeFunctions.RegisterHotKey(_windowHandle, _identifier, (int)_holdedModifier, (int)_holdedKeyWinForm);

            if (!result)
            {
                Trace.WriteLine($"Can't register global HotKey with identifier: {_identifier}\n");

                BannedIdentifiers.Add(_identifier);

                Trace.WriteLine($"Identifier: {_identifier} was banned.\n");

                _identifier = GetIdentifier();

                Trace.WriteLine($"Provider get new identifier: {_identifier}\n");
            }

            IsRegistered = result;

            return result;
        }

        /// <summary>
        /// Returns not used and not banned identifier.
        /// </summary>
        private int GetIdentifier()
        {
            int result;

            for (result = 0; result < Int16.MaxValue; result++)
            {
                if (!ActiveIdentifiers.Contains(result) &&
                    !BannedIdentifiers.Contains(result))
                {
                    ActiveIdentifiers.Add(result);

                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Unregister, removes hook and frees identifier.
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
        /// Window hook, in Windows System all windows have the similar functions for listening to system messages.
        /// </summary>
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if(msg == WindowsHotkeyMessageCode)
            {
                if (wParam.ToInt32() == _identifier)
                {
                    OnHotkeyPressed?.Invoke(_holdedModifier, _holdedKeyWPF);
                }
            }

            return IntPtr.Zero;
        }

        #endregion
    }
}
