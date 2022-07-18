﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using Axede.Mensajes;
using Axede.Validadores.WPF;

namespace Axede.WPF.Softphone.Applications.GUI.Login_Comunication
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
   
        public partial class Login : Window
        {

            #region Definiciones

            private string _Password;
            public string PasswordCapture
            {
                get { return _Password; }
                set
                {
                    _Password = value;

                    OnPropertyChanged("PasswordCapture");
                }
            }

            private string _Usuario;
            public string Usuario
            {
                get { return _Usuario; }
                set { _Usuario = value; }
            }

            private string _Clave;
            public string Clave
            {
                get { return _Clave; }
                set { _Clave = value; }
            }

            private string _SIPServe;
            public string SIPServe
            {
                get { return _SIPServe; }
                set { _SIPServe = value; }
            }

            private string _Alias;
            public string Alias
            {
                get { return _Alias; }
                set { _Alias = value; }
            }

            private string _Puerto;
            public string Puerto
            {
                get { return _Puerto; }
                set { _Puerto = value; }
            }



            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion

            #region EventosFormulario

            public void CargarRecursos()
            {
                lblUsuario.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Login_lblUsuario);
                lblClave.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Login_lblClave);
                lblAlias.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Login_lblAlias);
                lblServer.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Login_lblSIPServer);
                lblPuerto.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Login_lblPuerto);

                btnAceptar.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonAceptar);
                btnCancelar.Content = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_TextoBotonCancelar);
               
            }

            public Login()
            {
              
                DataContext = this;
                InitializeComponent();
                string SIPServer = ConfigurationManager.AppSettings.Get("SIPServer");
                string PortSIP = ConfigurationManager.AppSettings.Get("PuertoServer");
                txtServer.Text = SIPServer;
                txtPuerto.Text = PortSIP;
                CargarRecursos();
            }

            private void EssentialWindow_Loaded(object sender, RoutedEventArgs e)
            {
                txtUsuario.Focus();
            }

            private void Window_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                {
                    CerrarFormulario();
                }
            }

            private void StandardWindow_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Return)
                {
                    KeyEventArgs e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource,
                             0, Key.Tab);
                    e1.RoutedEvent = Keyboard.KeyDownEvent;
                    InputManager.Current.ProcessInput(e1);
                }
            }

            private void Window_Closed(object sender, EventArgs e)
            {
                CerrarFormulario();
            }


            #endregion

            #region Privados

            private void CerrarFormulario()
            {
                DisposeObject();
                this.Close();
            }

            private void DisposeObject()
            {

            }

            private bool Validaciones()
            {
                bool bCamposValidos = false;

                bool bUsuarioValido = txtUsuario.ValidarRequerido(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.Login_ErrRequerido_Usuario));
                //bool bClaveValida = txtClave.ValidarRequerido(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.Login_ErrRequerido_Clave));
                bool bServerValido = txtServer.ValidarRequerido(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.Login_ErrRequerido_Server));
                bool bPuertoValida = txtPuerto.ValidarRequerido(AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.Login_ErrRequerido_Puerto));

                if (bUsuarioValido && bServerValido && bPuertoValida) return true;

                return bCamposValidos;
            }

            #endregion

            #region Eventos Botones Principales

            private void btnAceptar_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (Validaciones())
                    {
                        _Usuario = txtUsuario.Text.Trim();
                        _Clave = txtClave.Password.Trim();
                        _SIPServe = txtServer.Text.Trim();
                        _Alias = txtAlias.Text.Trim();
                        _Puerto = txtPuerto.Text.Trim();

                        this.DialogResult = true;
                        CerrarFormulario();

                    }
                }
                catch (System.Exception ex)
                {


                }

            }

            private void btnCancelar_Click(object sender, RoutedEventArgs e)
            {
                this.DialogResult = false;
                CerrarFormulario();
            }

            #endregion

        }
    
}
