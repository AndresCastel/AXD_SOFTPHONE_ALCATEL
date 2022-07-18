using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Axede.WPF.Softphone.Applications.BussinesClass.Enum;

namespace Axede.WPF.Softphone.Applications.GUI.User_Controls
{
    /// <summary>
    /// Interaction logic for Botonera__UC.xaml
    /// </summary>
    public partial class Botonera__UC : UserControl, INotifyPropertyChanged
    {
        #region Definiciones
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Evento de acuerdo a la interfáz.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Método que permite hacer el raise del evento de cambio de propiedad.
        /// </summary>
        /// <param name="info">Propiedad que está cambiando</param>
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        private BotoneraEnum _AccionBoton;
        public BotoneraEnum AccionBoton
        {
            get { return _AccionBoton; }
            set { _AccionBoton = value;
            NotifyPropertyChanged("AccionBoton");
            }
        }

        #endregion

        public Botonera__UC()
        {
            InitializeComponent();
        }

        private void BtnAccion_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
	            {
                    case "Btn1":
                        AccionBoton = BotoneraEnum.UNO;
                        break;
                    case "Btn2":
                        AccionBoton = BotoneraEnum.DOS;
                        break;
                    case "Btn3":
                        AccionBoton = BotoneraEnum.TRES;
                        break;
                    case "Btn4":
                        AccionBoton = BotoneraEnum.CUATRO;
                        break;
                    case "Btn5":
                        AccionBoton = BotoneraEnum.CINCO;
                        break;
                    case "Btn6":
                        AccionBoton = BotoneraEnum.SEIS;
                        break;
                    case "Btn7":
                        AccionBoton = BotoneraEnum.SIETE;
                        break;
                    case "Btn8":
                        AccionBoton = BotoneraEnum.OCHO;
                        break;
                    case "Btn9":
                        AccionBoton = BotoneraEnum.NUEVE;
                        break;
                    case "Btn0":
                        AccionBoton = BotoneraEnum.CERO;
                        break;
                    case "BtnX":
                        AccionBoton = BotoneraEnum.ASTERISCO;
                        break;
                    case "BtnNum":
                        AccionBoton = BotoneraEnum.NUMERAL;
                        break;
                    default:
                        break;
	            }
          
        }
    }
}
