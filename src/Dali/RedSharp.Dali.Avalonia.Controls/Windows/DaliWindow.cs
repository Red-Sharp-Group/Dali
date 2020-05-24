using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using Avalonia.Styling;

namespace RedSharp.Dali.Avalonia.Controls.Windows
{
    /// <summary>
    /// Base Dali window. Handles move, resize and handling toolbar buttons.
    /// </summary>
    public class DaliWindow : Window, IStyleable
    {
        #region Constants

        private const string TitleBarName = "PART_TitleBar";
        private const string CloseButtonName = "PART_CloseButton";
        private const string MinimizeButtonName = "PART_MinimizeButton";
        private const string MaximizeButtonName = "PART_MaximizeButton";

        #endregion

        #region Dependency properties

        #region CanClose property

        /// <summary>
        /// Identifies <see cref="DaliWindow.CanClose"/> property.
        /// </summary>
        public static readonly AvaloniaProperty CanCloseProperty =
                AvaloniaProperty.Register<DaliWindow, bool>(nameof(CanClose), true);

        /// <summary>
        /// Gets or sets value that determines is window can be closed with toolbar close button.
        /// In case of false close button isn't visible.
        /// </summary>
        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }
        #endregion

        #region CanMaximize property
        /// <summary>
        /// Identifies <see cref="DaliWindow.CanMaximize"/> property.
        /// </summary>
        public static readonly AvaloniaProperty CanMaximizeProperty =
                AvaloniaProperty.Register<DaliWindow, bool>(nameof(CanMaximize), true);

        /// <summary>
        /// Gets or sets value that determines is window can be maximized with toolbar maximize button.
        /// In case of false maximize button is not visible and AeroSnap feature is not working.
        /// </summary>
        public bool CanMaximize
        {
            get => (bool)GetValue(CanMaximizeProperty);
            set => SetValue(CanMaximizeProperty, value);
        }
        #endregion

        #region CanMinimize property
        /// <summary>
        /// Identifies <see cref="DaliWindow.CanMinimize"/> property.
        /// </summary>
        public static readonly AvaloniaProperty CanMinimizeProperty =
                AvaloniaProperty.Register<DaliWindow, bool>(nameof(CanMinimize), true);

        /// <summary>
        /// Gets or sets value that determines is window can be minimized with toolbar minimize button.
        /// In case of false minimize button is not visible.
        /// </summary>
        public bool CanMinimize
        {
            get => (bool)GetValue(CanMinimizeProperty);
            set => SetValue(CanMinimizeProperty, value);
        }
        #endregion

        #region IsTitleBarVisible
        /// <summary>
        /// Identifies <see cref="DaliWindow.IsTitleBarVisible"/> property.
        /// </summary>
        public static readonly AvaloniaProperty IsTitleBarVisibleProperty =
                AvaloniaProperty.Register<DaliWindow, bool>(nameof(IsTitleBarVisible), true);

        /// <summary>
        /// Gets or sets value that determines is titlebar visible.
        /// </summary>
        public bool IsTitleBarVisible
        {
            get => (bool)GetValue(IsTitleBarVisibleProperty);
            set => SetValue(IsTitleBarVisibleProperty, value);
        }

        #endregion

        #endregion

        #region Construction

        Type IStyleable.StyleKey => typeof(DaliWindow);
        /// <summary>
        /// Initializes new instance of DaliWindow.
        /// </summary>
        public DaliWindow()
        {
            //I don't understand why style with corresponding type is not applied to window.
            //So I have added this for automatic style application.
            //SetResourceReference(StyleProperty, typeof(DaliWindow));
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Calls <see cref="FrameworkElement.OnApplyTemplate"/> and find required templated parts.
        /// </summary>
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            _titleBar = e.NameScope.Get<IControl>(TitleBarName);
            if(_titleBar != null)
                _titleBar.PointerPressed += OnWindowMove;

            _closeButton = e.NameScope.Get<Button>(CloseButtonName);
            if(_closeButton != null)
                _closeButton.Click += OnCloseButtonClick;

            _minimizeButton = e.NameScope.Get<Button>(MinimizeButtonName);
            if(_minimizeButton != null)
                _minimizeButton.Click += OnMinimizeButtonClick;

            _maximiazeButton = e.NameScope.Get<Button>(MaximizeButtonName);
            if(_maximiazeButton != null)
                _maximiazeButton.Click += OnMaximizeButtonClick;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Handles CloseButton's Click event.
        /// </summary>
        /// <param name="sender">Close button.</param>
        /// <param name="e">Click event args.</param>
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Handles MinimizeButton's Click event.
        /// </summary>
        /// <param name="sender">Minimize button.</param>
        /// <param name="e">Click event args.</param>
        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Handles MaximizeButtons's Click event.
        /// </summary>
        /// <param name="sender">Maximize button.</param>
        /// <param name="e">Click event args.</param>
        private void OnMaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Handler title bar <see cref="UIElement.MouseMove"/> event. Makes window move.
        /// </summary>
        /// <param name="sender">Title bar.</param>
        /// <param name="e">Mouse event args.</param>
        private void OnWindowMove(object sender, PointerPressedEventArgs args)
        {
            PlatformImpl?.BeginMoveDrag(args);
        }

        #endregion

        #region Fields

        private IControl _titleBar;
        private Button _closeButton;
        private Button _minimizeButton;
        private Button _maximiazeButton;

        #endregion
    }
}
