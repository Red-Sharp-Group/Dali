using ControlzEx.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RedSharp.Dali.Controls.Windows
{
    /// <summary>
    /// Base Dali window. Handles move, resize and handling toolbar buttons.
    /// </summary>
    [TemplatePart(Name = DaliWindow.TitleBarName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = DaliWindow.CloseButtonName, Type = typeof(Button))]
    [TemplatePart(Name = DaliWindow.MinimizeButtonName, Type = typeof(Button))]
    [TemplatePart(Name = DaliWindow.MaximizeButtonName, Type = typeof(Button))]
    public class DaliWindow : Window
    {
        #region Constants

        private const string TitleBarName = "PART_TitleBar";
        private const string CloseButtonName = "PART_CloseButton";
        private const string MinimizeButtonName = "PART_MinimizeButton";
        private const string MaximizeButtonName = "PART_MaximizeButton";

        #endregion

        #region Static
        static DaliWindow()
        {
            //Should work to automatically use styles for this type.
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DaliWindow), new FrameworkPropertyMetadata(typeof(DaliWindow)));
        }

        #endregion

        #region Dependency properties

        #region CanClose property

        /// <summary>
        /// Identifies <see cref="DaliWindow.CanClose"/> property.
        /// </summary>
        public static readonly DependencyProperty CanCloseProperty =
                DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

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
        public static readonly DependencyProperty CanMaximizeProperty =
                DependencyProperty.Register(nameof(CanMaximize), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

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
        public static readonly DependencyProperty CanMinimizeProperty =
                DependencyProperty.Register(nameof(CanMinimize), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

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
        public static readonly DependencyProperty IsTitleBarVisibleProperty =
                DependencyProperty.Register(nameof(IsTitleBarVisible), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

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
        /// <summary>
        /// Initializes new instance of DaliWindow.
        /// </summary>
        public DaliWindow()
        {
            //I don't understand why style with corresponding type is not applied to window.
            //So I have added this for automatic style application.
            SetResourceReference(StyleProperty, typeof(DaliWindow));

            //WindowChromeBehavior is set here, in code to prevent setting in every derived class in XAML.
            WindowChromeBehavior behavior = new WindowChromeBehavior();

            BindingOperations.SetBinding(behavior,
                                         WindowChromeBehavior.EnableMaxRestoreProperty,
                                         new Binding()
                                         {
                                             Source = this,
                                             Path = new PropertyPath(nameof(CanMaximize))
                                         });

            Microsoft.Xaml.Behaviors.Interaction.GetBehaviors(this).Add(behavior);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Calls <see cref="FrameworkElement.OnApplyTemplate"/> and find required templated parts.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _titleBar = Template.FindName(TitleBarName, this) as FrameworkElement;
            if(_titleBar != null)
                _titleBar.MouseMove += OnWindowMove;

            _closeButton = Template.FindName(CloseButtonName, this) as Button;
            if(_closeButton != null)
                _closeButton.Click += OnCloseButtonClick;

            _minimizeButton = Template.FindName(MinimizeButtonName, this) as Button;
            if(_minimizeButton != null)
                _minimizeButton.Click += OnMinimizeButtonClick;

            _maximiazeButton = Template.FindName(MaximizeButtonName, this) as Button;
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
        private void OnWindowMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        #endregion

        #region Fields

        private FrameworkElement _titleBar;
        private Button _closeButton;
        private Button _minimizeButton;
        private Button _maximiazeButton;

        #endregion
    }
}
