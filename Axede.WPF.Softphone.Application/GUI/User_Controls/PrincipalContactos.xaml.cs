﻿using System;
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
using Axede.BussinesObject.Application;

namespace Axede.WPF.Softphone.Applications.GUI.User_Controls
{
    /// <summary>
    /// Interaction logic for PrincipalContactos.xaml
    /// </summary>
    public partial class PrincipalContactos : UserControl, INotifyPropertyChanged
    {
        #region Definiciones

        private DtoContactos _LlamarNum;
        public DtoContactos LlamarNum
        {
            get { return _LlamarNum; }
            set
            {
                _LlamarNum = value;
                NotifyPropertyChanged("LlamarNum");
            }
        }

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
        private UC_ListaContactos _ucListaContactos;
        public UC_ListaContactos ucListaContactos
        {
            get { return _ucListaContactos; }
            set { _ucListaContactos = value; }
        }
        private UC_AddContacto ucAddContacto = null;
        private bool _MostrarFavorito;
        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }
        public bool MostrarFavorito
        {
            get { return _MostrarFavorito; }
            set { _MostrarFavorito = value; }
        }
        #endregion

        #region Formulario
        public PrincipalContactos()
        {
            try
            {
                
                InitializeComponent();
                MostrarFavorito = true;
                ucListaContactos = new UC_ListaContactos();
                ucAddContacto = new UC_AddContacto();
                ucListaContactos.PropertyChanged += ucListaContactos_PropertyChanged;
                if (!ApplicationIsInDesignMode)
                {
                    txtContact.LabelText = "Buscar Favoritos...";                   
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Inicializar Favoritos         
           
            grdBuscador.Visibility = Visibility.Visible;
            txtContact.Text = string.Empty;
            MostrarFavorito = true;
            sepContactH.Visibility = Visibility.Collapsed;
            sepAddContactH.Visibility = Visibility.Collapsed;
            sepFavoritosH.Visibility = Visibility.Visible;
            ucListaContactos.getContactAndRecentContact();
            ctcContactos.Visibility = Visibility.Visible;
            ctcContactos.Content = ucListaContactos;
        }
        void ucListaContactos_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (txtContact.Text.Length >= 4)
            {
                ctcContactos.Visibility = Visibility.Visible;
                ctcContactos.Content = ucListaContactos;
                ucListaContactos.ObtenerConatacto(MostrarFavorito);
            }

            if (((UC_ListaContactos)sender).DtoEditarContacto != null)
            {
                MostrarAddContact();
                ucAddContacto.EditarContacto(((UC_ListaContactos)sender).DtoEditarContacto);
            }
            if (e.PropertyName == "LlamarNum")
            {
                LlamarNum = ((UC_ListaContactos)sender).LlamarNum;
            }
        }
        #endregion

        #region Metodos Privados
        private void MostrarAddContact()
        {
            txtContact.Focus();
            sepFavoritosH.Visibility = Visibility.Collapsed;
            sepContactH.Visibility = Visibility.Collapsed;
            sepAddContactH.Visibility = Visibility.Visible;
            grdBuscador.Visibility = Visibility.Collapsed;
            ctcContactos.Visibility = Visibility.Visible;
            ctcContactos.Content = null;
            ctcContactos.Margin = new Thickness(6, 2, 8, 20);
            ctcContactos.Content = ucAddContacto;
        }
        #endregion

        #region Eventos Controles

        private void txtContact_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!(Char.IsLetter(ch)) && !(Char.IsNumber(ch)))
                {
                    e.Handled = true;
                }
            }

        }

        private void txtContact_Search(object sender, RoutedEventArgs e)
        {
            if (txtContact.Text.Length >= 4)
            {
                ucListaContactos.NomContacto = txtContact.Text;
            }
            else
            {
                if (MostrarFavorito)
                {
                    ucListaContactos.getContactAndRecentContact();
                    ctcContactos.Visibility = Visibility.Visible;
                    ctcContactos.Content = ucListaContactos;
                }
                else
                {
                    ctcContactos.Visibility = Visibility.Collapsed;
                }   
            }
        }

        private void btnFavoritos_Click(object sender, RoutedEventArgs e)
        {
            MostrarFavorito = true;
            txtContact.LabelText = "Buscar Favoritos...";
            txtContact.Focus();
            sepFavoritosH.Visibility = Visibility.Visible;
            sepContactH.Visibility = Visibility.Collapsed;
            sepAddContactH.Visibility = Visibility.Collapsed;
            grdBuscador.Visibility = Visibility.Visible;
            ucListaContactos.DtoEditarContacto = null;
            ucListaContactos.getContactAndRecentContact();
            ctcContactos.Visibility = Visibility.Visible;
            ctcContactos.Content = ucListaContactos;
            ctcContactos.Margin = new Thickness(6, 2, 8, 0);
            if (txtContact.Text.Length >= 4)
            {
                ucListaContactos.NomContacto = txtContact.Text;
            }
        }

        private void btnContactos_Click(object sender, RoutedEventArgs e)
        {
            MostrarFavorito = false;
            txtContact.LabelText = "Buscar Contactos...";
            txtContact.Focus();
            sepFavoritosH.Visibility = Visibility.Collapsed;
            sepContactH.Visibility = Visibility.Visible;
            sepAddContactH.Visibility = Visibility.Collapsed;
            grdBuscador.Visibility = Visibility.Visible;
            ucListaContactos.DtoEditarContacto = null;
            ctcContactos.Content = null;
            ctcContactos.Margin = new Thickness(6, 2, 8, 0);
            if (txtContact.Text.Length >= 4)
            {
                ucListaContactos.NomContacto = txtContact.Text;
            }
        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            MostrarAddContact();
        }

        private void btnSincronizar_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        
    }
}
