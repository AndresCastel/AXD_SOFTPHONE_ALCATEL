﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Axede.WPF.Softphone.Applications.UtilControls
{
    /// <summary>
    /// Interaction logic for ViewWindow_Modal.xaml
    /// </summary>
    public partial class ViewWindow_Modal : Window
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Constructor por default
        /// </summary>
        public ViewWindow_Modal()
        {
            InitializeComponent();
            Application.Current.Activated += new EventHandler(Current_Activated);
        }

        static void click_cerrar1(object sender, RoutedEventArgs e)
        {
            if (ParentWindow != null)
            {
                if (Cerrar == WinBehavior.Close)
                    ParentWindow.Close();
            }
            else
            {
                if (Cerrar == WinBehavior.Close)
                    GrantParentWindow.Close();
            }
        }

        private static WinBehavior _Cerrar;

        public static WinBehavior Cerrar
        {
            get { return _Cerrar; }
            set { _Cerrar = value; }
        }

        public static void CloseModal()
        {
            click_cerrar1(null, null);

        }

        public enum WinBehavior
        {
            /// <summary>
            /// Cierra el modal
            /// </summary>
            Close,
            /// <summary>
            /// No cierra el modal dependiendo de la respuesta 
            /// </summary>
            DontClose,
        }

        #endregion

        #region PROPIEDADES

        public static double MaxVertical = 0.8;

        public static double MaxHorizontal = 0.8;

        public static Window ParentWindow = null;

        public static Window GrantParentWindow = null;

        private static bool _IsParent = false;

        public static bool IsParent
        {
            get { return ViewWindow_Modal._IsParent; }
            set { ViewWindow_Modal._IsParent = value; }
        }

        public enum MyMessageBoxButton
        {
            // Summary:
            //     The message box displays an OK button.
            OK,
            // 
            // Summary:
            //     The message box displays OK and Cancel buttons.
            OKCancel,
            //
            // Summary:
            //     The message box displays Yes, No, and Cancel buttons.
            YesNoCancel,
            //
            // Summary:
            //     The message box displays Yes and No buttons.
            YesNo,
            //
            // Summary:
            //     Displays a message box without Response buttons or Close button
            None,
            //
            // Summary:
            //     Displays a message box with the X Button only
            NoResponse,
            //
            // Summary:
            //     Displays a message box with a Close Button 
            Close,
            //Summary:
            //    Displays a message box with a Close Button 
            X = 8,
        }

        private MessageBoxResult response;
        private MessageBoxResult Response
        {
            get { return response; }
            set { response = value; }
        }

        private MyMessageBoxButton buttons;
        private MyMessageBoxButton Buttons
        {
            get { return buttons; }
            set
            {
                switch (value)
                {
                    case MyMessageBoxButton.OK:
                        this.Button_OK.Visibility = Visibility.Visible;
                        break;
                    case MyMessageBoxButton.OKCancel:
                        this.Button_OK.Visibility = Visibility.Visible;
                        this.Button_CANCEL.Visibility = Visibility.Visible;
                        this.Button_OK.HorizontalAlignment = HorizontalAlignment.Center;
                        this.Button_CANCEL.HorizontalAlignment = HorizontalAlignment.Right;
                        break;
                    case MyMessageBoxButton.YesNo:
                        this.Button_YES.Visibility = Visibility.Visible;
                        this.Button_NO.Visibility = Visibility.Visible;
                        this.Button_YES.HorizontalAlignment = HorizontalAlignment.Center;
                        this.Button_NO.HorizontalAlignment = HorizontalAlignment.Right;
                        break;
                    case MyMessageBoxButton.YesNoCancel:
                        this.Button_YES.Visibility = Visibility.Visible;
                        this.Button_NO.Visibility = Visibility.Visible;
                        this.Button_CANCEL.Visibility = Visibility.Visible;
                        this.Button_YES.HorizontalAlignment = HorizontalAlignment.Left;
                        this.Button_NO.HorizontalAlignment = HorizontalAlignment.Center;
                        this.Button_CANCEL.HorizontalAlignment = HorizontalAlignment.Right;
                        break;
                    case MyMessageBoxButton.None:
                        this.X.Visibility = Visibility.Collapsed;
                        break;
                    case MyMessageBoxButton.Close:
                        this.X.Visibility = Visibility.Visible;
                        break;
                    case MyMessageBoxButton.X:
                        this.X.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
                this.buttons = value;
            }
        }


        #endregion

        #region PROCEDIMIENTOS DE EVENTO

        private void Grid_MouseLeftButtonDown_drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CANCEL_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.OK;
            Close();
        }

        private void YES_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.Yes;
            Close();
        }

        private void NO_Click(object sender, RoutedEventArgs e)
        {
            this.response = MessageBoxResult.No;
            Close();
        }

        private void win_Deactivated(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double posx = ((System.Windows.SystemParameters.PrimaryScreenWidth - e.NewSize.Width) / 2);
            ((System.Windows.Window)sender).Left = posx;

            double posy = ((System.Windows.SystemParameters.PrimaryScreenHeight - e.NewSize.Height) / 2);
            ((System.Windows.Window)sender).Top = posy;
        }

        #endregion

        #region PROCEDIMIENTOS PRIVADOS

        private void Inicializar()
        {

        }

        private void CargarInformacion()
        {

        }

        private void ValidarInformacion()
        {

        }

        private void GrabarInformacion()
        {

        }

        public static MessageBoxResult Show(object userControl)
        {
            return Show(userControl, "");
        }

        public static MessageBoxResult Show(object userControl, Button CloseButton)
        {
            return Show(userControl, "", CloseButton);
        }

        public static MessageBoxResult Show(object userControl, Hyperlink hyperlink)
        {
            return Show(userControl, "", hyperlink);
        }

        public static MessageBoxResult Show(object userControl, string caption)
        {
            return Show(userControl, caption, MyMessageBoxButton.OK);
        }

        public static MessageBoxResult Show(object userControl, string caption, Button CloseButton)
        {
            return Show(userControl, caption, MyMessageBoxButton.OK, CloseButton);
        }

        public static MessageBoxResult Show(object userControl, string caption, Hyperlink hyperlink)
        {
            return Show(userControl, caption, MyMessageBoxButton.OK, hyperlink);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton)
        {
            return Show(userControl, caption, myMessageBoxButton, ScrollBarVisibility.Hidden, ScrollBarVisibility.Hidden);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, Button CloseButton)
        {
            return Show(userControl, caption, myMessageBoxButton, ScrollBarVisibility.Hidden, ScrollBarVisibility.Hidden, CloseButton);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, Hyperlink hyperlink)
        {
            return Show(userControl, caption, myMessageBoxButton, ScrollBarVisibility.Hidden, ScrollBarVisibility.Hidden, hyperlink);
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, MenuItem menuItem)
        {
            return Show(userControl, caption, myMessageBoxButton, ScrollBarVisibility.Hidden, ScrollBarVisibility.Hidden, menuItem);
        }

        public static void Splash(object userControl, string caption)
        {
            Splash(userControl, caption, false);
        }

        public static void Splash(object userControl, string caption, bool transparency)
        {
            ViewWindow_Modal win = new ViewWindow_Modal();
            win.Title = caption;
            win.labelTitle.Content = caption;
            win.Buttons = MyMessageBoxButton.None;
            win.contentControl1.Content = userControl;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            if (ParentWindow != null) ParentWindow.Close();
            ParentWindow = win;
            if (transparency)
            {
                win.Title = "";
                win.labelTitle.Visibility = Visibility.Collapsed;
                win.stackpanel.Background = Brushes.Transparent;
                win.border.BorderBrush = Brushes.Transparent;
                win.border.Background = Brushes.Transparent;
                win.separador1.Visibility = Visibility.Collapsed;
                win.separador2.Visibility = Visibility.Collapsed;
                win.BitmapEffect = null;
            }
            win.ShowDialog();
        }

        public static void CloseSplash()
        {
            if (ParentWindow != null) ParentWindow.Close();
            ParentWindow = null;
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility ucSBH, ScrollBarVisibility ucSBV)
        {
            MessageBoxResult Response = MessageBoxResult.Cancel;
            bool s = Application.Current.Dispatcher.CheckAccess();
            if (Application.Current.Dispatcher.CheckAccess())
            {
                Response = ShowShadow(userControl, caption, myMessageBoxButton, ucSBH, ucSBV);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Response = ShowShadow(null, caption, myMessageBoxButton, ucSBH, ucSBV);
                    }));
            }
            return Response;
        }

        public static MessageBoxResult ShowShadow(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility ucSBH, ScrollBarVisibility ucSBV)
        {
            ViewWindow_Modal win = new ViewWindow_Modal();
            if (IsParent)
            {
                ParentWindow = win;
            }
            else
            {
                GrantParentWindow = win;
            }
            win.Title = caption;
            win.labelTitle.Content = caption;
            win.Buttons = myMessageBoxButton;
            win.contentControl1.Content = userControl;
            win.sb.VerticalScrollBarVisibility = ucSBV;
            win.sb.HorizontalScrollBarVisibility = ucSBH;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            win.ShowDialog();
            win.contentControl1.Content = null;
            return win.Response;
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, Button CloseButton)
        {
            MessageBoxResult Response = MessageBoxResult.Cancel;
            if (Application.Current.Dispatcher.CheckAccess())
            {
                Response = ShowShadow(userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, CloseButton);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Response = ShowShadow(userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, CloseButton);
                    }));
            }
            return Response;
        }

        public static MessageBoxResult ShowShadow(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, Button CloseButton)
        {
           
            ViewWindow_Modal win = new ViewWindow_Modal();

            //if (IsParent)
            //{
            //win = new ViewWindow_Modal();
            ParentWindow = win;

            //}
            //else {
            //    win = new ViewWindow_Modal();
            //    GrantParentWindow = win;
            //}

            win.Title = caption;
            win.labelTitle.Content = caption;
            win.Buttons = myMessageBoxButton;
            win.contentControl1.Content = userControl;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            CloseButton.Click += new RoutedEventHandler(click_cerrar1);

            //IsParent = true;

            win.ShowDialog();

            //IsParent = false;

            win.contentControl1.Content = null;
            CloseButton.Click -= new RoutedEventHandler(click_cerrar1);
            //ParentWindow = null;
            var x = win.response;
            return win.response;


        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, Hyperlink hyperlink)
        {
            MessageBoxResult Response = MessageBoxResult.Cancel;
            if (Application.Current.Dispatcher.CheckAccess())
            {
                Response = ShowShadow(userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, hyperlink);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Response = ShowShadow(userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, hyperlink);
                    }));
            }
            return Response;
        }

        public static MessageBoxResult ShowShadow(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, Hyperlink hyperlink)
        {
            ViewWindow_Modal win = new ViewWindow_Modal();
            ParentWindow = win;
            win.Title = caption;
            win.labelTitle.Content = caption;
            win.Buttons = myMessageBoxButton;
            win.contentControl1.Content = userControl;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            hyperlink.Click += new RoutedEventHandler(click_cerrar1);
            win.ShowDialog();
            win.contentControl1.Content = null;
            hyperlink.Click -= new RoutedEventHandler(click_cerrar1);
            ParentWindow = null;
            var x = win.response;
            return win.response;
        }

        public static MessageBoxResult Show(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, MenuItem menu)
        {
            MessageBoxResult Response = MessageBoxResult.Cancel;
            if (Application.Current.Dispatcher.CheckAccess())
            {
                Response = ShowShadow(userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, menu);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Response = ShowShadow(userControl, caption, myMessageBoxButton, scrollBarVisibility, scrollBarVisibility_5, menu);
                    }));
            }
            return Response;
        }

        public static MessageBoxResult ShowShadow(object userControl, string caption, MyMessageBoxButton myMessageBoxButton, ScrollBarVisibility scrollBarVisibility, ScrollBarVisibility scrollBarVisibility_5, MenuItem menu)
        {
            ViewWindow_Modal win = new ViewWindow_Modal();
            ParentWindow = win;
            win.Title = caption;
            win.labelTitle.Content = caption;
            win.Buttons = myMessageBoxButton;
            win.contentControl1.Content = userControl;
            win.sb.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * MaxVertical;
            win.sb.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * MaxHorizontal;
            menu.Click += new RoutedEventHandler(click_cerrar1);
            win.ShowDialog();
            win.contentControl1.Content = null;
            menu.Click -= new RoutedEventHandler(click_cerrar1);
            ParentWindow = null;
            var x = win.response;
            return win.response;
        }

        void Current_Activated(object sender, EventArgs e)
        {
            this.Activate();
        }

        #endregion
    }
}
