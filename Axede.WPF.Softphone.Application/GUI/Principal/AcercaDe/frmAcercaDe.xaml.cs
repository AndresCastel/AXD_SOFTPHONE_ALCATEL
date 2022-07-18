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
using System.Diagnostics;
using Axede.WPF.CustomWindow;
using Axede.Mensajes;

namespace Axede.WPF.Softphone.Applications.Principal.AcercaDe
{
    /// <summary>
    /// Lógica de interacción para frmAcercaDe.xaml
    /// </summary>
    public partial class frmAcercaDe : StandardWindow
    {

        #region Eventos Formulario

            public frmAcercaDe()
            {
                InitializeComponent();
            }

            public frmAcercaDe(Window parent) : this()
            {
                this.Owner = parent;
            }

            private void StandardWindow_Loaded(object sender, RoutedEventArgs e)
            {
                CargarRecursos();
                btnAceptar.Focus();
            }

            private void StandardWindow_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape) 
                {
                    this.Close();
                }
            }

        #endregion

        #region Eventos Controles

            private void hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
            {
                string uri = e.Uri.AbsoluteUri;
                Process.Start(new ProcessStartInfo(uri));

                e.Handled = true;
            }

            private void okButton_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
            
        #endregion

        #region General

            private void CargarRecursos() 
            { 
                TypeConverterStringToUIElement ocov = new TypeConverterStringToUIElement();
                this.Caption = (UIElement)ocov.ConvertFromString(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.Aplicacion_TituloAcercaDe));

                reserved.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.AcercaDe_DerechosReservados);
                info.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.AcercaDe_DescripcionProducto);
                versionLabel.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.AcercaDe_Version);

                btnAceptar.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_TextoBotonAceptar);
                btnAceptar.ToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_ToolTipBotonAceptar);
            
            }

        #endregion

    }
}
