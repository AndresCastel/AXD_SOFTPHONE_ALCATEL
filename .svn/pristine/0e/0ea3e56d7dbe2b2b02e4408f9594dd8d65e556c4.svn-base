using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Threading;

namespace Axede.WPF.Softphone.Applications.UtilClass
{
    public class ClearTextBox : TextBox
    {

        public static DependencyProperty SearchEventTimeDelayProperty =
    DependencyProperty.Register(
        "SearchEventTimeDelay",
        typeof(Duration),
        typeof(ClearTextBox),
        new FrameworkPropertyMetadata(
            new Duration(new TimeSpan(0, 0, 0, 0, 500)),
            new PropertyChangedCallback(OnSearchEventTimeDelayChanged)));

        public Duration SearchEventTimeDelay
        {
            get { return (Duration)GetValue(SearchEventTimeDelayProperty); }
            set { SetValue(SearchEventTimeDelayProperty, value); }
        }

        static void OnSearchEventTimeDelayChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ClearTextBox stb = o as ClearTextBox;
            if (stb != null)
            {
                stb.searchEventDelayTimer.Interval = ((Duration)e.NewValue).TimeSpan;
                stb.searchEventDelayTimer.Stop();
            }
        }


        private void IconBorder_MouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            if (!IsMouseLeftButtonDown) return;

            if (HasText)
            {

                if (this.Text.Length > 1)
                {
                    this.Text = this.Text.Substring(0, this.Text.Length - 1);
                }
                else
                {
                    this.Text = "";
                }
                this.CaretIndex = this.Text.Length;

            }


            IsMouseLeftButtonDown = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                this.Text = "";
            }
            else if ((e.Key == Key.Return || e.Key == Key.Enter))
            {

            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            HasText = Text.Length != 0;

            searchEventDelayTimer.Stop();
            searchEventDelayTimer.Start();

        }

        private DispatcherTimer searchEventDelayTimer;
        public ClearTextBox()
            : base()
        {
            searchEventDelayTimer = new DispatcherTimer();
            searchEventDelayTimer.Interval = SearchEventTimeDelay.TimeSpan;
            searchEventDelayTimer.Tick += new EventHandler(OnSeachEventDelayTimerTick);
        }


        void OnSeachEventDelayTimerTick(object o, EventArgs e)
        {
            searchEventDelayTimer.Stop();

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Border iconBorder = GetTemplateChild("PART_SearchIconBorder") as Border;
            if (iconBorder != null)
            {
                iconBorder.MouseLeftButtonDown += new MouseButtonEventHandler(IconBorder_MouseLeftButtonDown);
                iconBorder.MouseLeftButtonUp += new MouseButtonEventHandler(IconBorder_MouseLeftButtonUp);
                iconBorder.MouseLeave += new MouseEventHandler(IconBorder_MouseLeave);
            }
        }

        private void IconBorder_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            IsMouseLeftButtonDown = true;
        }



        private void IconBorder_MouseLeave(object obj, MouseEventArgs e)
        {

            IsMouseLeftButtonDown = false;
        }

        private static DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
    DependencyProperty.RegisterReadOnly(
        "IsMouseLeftButtonDown",
        typeof(bool),
        typeof(ClearTextBox),
        new PropertyMetadata());
        public static DependencyProperty IsMouseLeftButtonDownProperty =
            IsMouseLeftButtonDownPropertyKey.DependencyProperty;


        public bool IsMouseLeftButtonDown
        {
            get { return (bool)GetValue(IsMouseLeftButtonDownProperty); }
            private set { SetValue(IsMouseLeftButtonDownPropertyKey, value); }
        }

        public static DependencyProperty LabelTextProperty =


            DependencyProperty.Register(
                "LabelText",
                typeof(string),
                typeof(ClearTextBox));

        public static DependencyProperty LabelTextColorProperty =
            DependencyProperty.Register(
                "LabelTextColor",
                typeof(Brush),
                typeof(ClearTextBox));



        private static DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "HasText",
                typeof(bool),
                typeof(ClearTextBox),
                new PropertyMetadata());
        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        static ClearTextBox()
        {

            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ClearTextBox),
                new FrameworkPropertyMetadata(typeof(ClearTextBox)));
        }


        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public Brush LabelTextColor
        {
            get { return (Brush)GetValue(LabelTextColorProperty); }
            set { SetValue(LabelTextColorProperty, value); }
        }


        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
            private set { SetValue(HasTextPropertyKey, value); }
        }
    }
}
