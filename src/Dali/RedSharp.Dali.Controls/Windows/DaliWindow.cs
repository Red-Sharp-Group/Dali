using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace RedSharp.Dali.Controls.Windows
{
    public class DaliWindow : Window
    {
        static DaliWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DaliWindow), new FrameworkPropertyMetadata(typeof(DaliWindow)));
        }

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

    }
}
