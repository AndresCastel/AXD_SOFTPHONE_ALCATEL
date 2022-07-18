using System;
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
using System.Threading;

namespace Axede.WPF.Softphone.Applications.GUI.Splash
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        #region Definiciones

        public static readonly DependencyProperty AvailablePluginsProperty =
        DependencyProperty.Register("AvailablePlugins", typeof(IEnumerable<string>), typeof(SplashScreen),
                                    new UIPropertyMetadata(null));

        public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register("Message", typeof(string), typeof(SplashScreen),
                                    new UIPropertyMetadata(null, OnMessageChanged));

        public static readonly DependencyProperty LicenseeProperty =
        DependencyProperty.Register("Licensee", typeof(string), typeof(SplashScreen), new UIPropertyMetadata(null));



        #endregion

        #region Formulario

        public SplashScreen()
        {
            InitializeComponent();

            Thread.Sleep(1000);
        }

        #endregion


        #region General

        public string Licensee
        {
            get { return (string)this.GetValue(LicenseeProperty); }
            set { this.SetValue(LicenseeProperty, value); }
        }


        public IEnumerable<string> AvailablePlugins
        {
            get { return (IEnumerable<string>)this.GetValue(AvailablePluginsProperty); }
            set { this.SetValue(AvailablePluginsProperty, value); }
        }


        public string Message
        {
            get { return (string)this.GetValue(MessageProperty); }
            set { this.SetValue(MessageProperty, value); }
        }


        public event EventHandler MessageChanged;

        private void RaiseMessageChanged(EventArgs e)
        {
            EventHandler handler = this.MessageChanged;
            if (handler != null) handler(this, e);
        }

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SplashScreen splashScreen = (SplashScreen)d;
            splashScreen.RaiseMessageChanged(EventArgs.Empty);
            //splashScreen.messageTextBlock.Text = splashScreen.Message;
        }

        public void OcultarAnimacion(bool bOcultar = false)
        {

            if (bOcultar == false)
            {
                Loading.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Loading.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #endregion
    }
}
