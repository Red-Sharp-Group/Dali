using ControlzEx.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RedSharp.Dali.Controls.Windows
{
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
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DaliWindow), new FrameworkPropertyMetadata(typeof(DaliWindow)));
        }

        #endregion

        #region Depdendency properties

        #region CanClose property
        public static readonly DependencyProperty CanCloseProperty =
                DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }
        #endregion

        #region CanMaximize property
        public static readonly DependencyProperty CanMaximizeProperty =
                DependencyProperty.Register(nameof(CanMaximize), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

        public bool CanMaximize
        {
            get => (bool)GetValue(CanMaximizeProperty);
            set => SetValue(CanMaximizeProperty, value);
        }
        #endregion

        #region CanMinimize property
        public static readonly DependencyProperty CanMinimizeProperty =
                DependencyProperty.Register(nameof(CanMinimize), typeof(bool), typeof(DaliWindow), new PropertyMetadata(true));

        public bool CanMinimize
        {
            get => (bool)GetValue(CanMinimizeProperty);
            set => SetValue(CanMinimizeProperty, value);
        }
        #endregion

        #endregion

        public DaliWindow()
        {
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

        #region Private Methods
        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnMaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

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
