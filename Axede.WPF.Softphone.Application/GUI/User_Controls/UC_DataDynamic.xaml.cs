﻿using Axede.Mensajes;
using Axede.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UC_DataDynamic.xaml
    /// </summary>
    public partial class UC_DataDynamic : UserControl, INotifyPropertyChanged
    {
        #region Definiciones
        #region INotifyPropertyChanged Members
        private object _obj;

        public object Obj
        {
            get { if (_obj != null) return _obj; else return _obj = new object(); }
            set { _obj = value; }
        }
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
      
       
        private string _CampoOrden = "Campo";
        private string _OrdenCampo = "Desc";
        private string _FiltroPago = string.Empty;

        private object _objectNotification;

        public object ObjectNotification
        {
            get { return _objectNotification; }
            set
            {
                _objectNotification = value;
                NotifyPropertyChanged("ObjectNotification");
            }
        }
        public string Filtro
        {
            get { return _FiltroPago; }
            set { _FiltroPago = value; }
        }
        public string CampoOrden
        {
            get { return _CampoOrden; }
            set { _CampoOrden = value; }
        }
        public string OrdenCampo
        {
            get { return _OrdenCampo; }
            set { _OrdenCampo = value; }
        }
        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }
        private int _pageNumber;

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }
        private List<DtoDatosAdicionales> _lstDatosAdicionalesCargue = new List<DtoDatosAdicionales>();

        public List<DtoDatosAdicionales> LstDatosAdicionalesCargue
        {
            get { return _lstDatosAdicionalesCargue; }
            set { _lstDatosAdicionalesCargue = value; }
        }

        private string _sMensajeSinRegistros = string.Empty;
        private bool _bSinRegistros = false;

        #endregion

        #region Eventos UserControl
        public UC_DataDynamic(List<DtoDatosAdicionales> _lstDatosAdicionales, bool visiblePage = true, bool bHiddenColumnDefault = false)
        {
            InitializeComponent();
            if (!ApplicationIsInDesignMode)
            {
                LstDatosAdicionalesCargue = _lstDatosAdicionales;

                _sMensajeSinRegistros = "Sin Registros";

                if (LstDatosAdicionalesCargue.Count <= 0)
                {
                    _bSinRegistros = true;
                    DtoDatosAdicionales oDtoDatosAdicionales = new DtoDatosAdicionales();
                    oDtoDatosAdicionales.Campo = _sMensajeSinRegistros;
                    oDtoDatosAdicionales.Valor = string.Empty;
                    LstDatosAdicionalesCargue.Add(oDtoDatosAdicionales);
                }

                CargarGrilla(_lstDatosAdicionales);              

                if (!visiblePage)
                {
                    Paginador.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (LstDatosAdicionalesCargue != null)
                    {
                        if (LstDatosAdicionalesCargue.Count > 0)
                        {
                            if (LstDatosAdicionalesCargue.Count > Globales.iRegistrosPaginaSmall)
                            {
                                Paginador.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Paginador.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    else
                    {
                        Paginador.Visibility = Visibility.Collapsed;
                    }
                }


                if (bHiddenColumnDefault == false)
                {
                    grvDatosAdicionales.MaxWidth = this.MaxWidth;
                    grvDatosAdicionales.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Auto);
                }

                if (_bSinRegistros == true)
                {
                    grvDatosAdicionales.Columns[1].Visibility = System.Windows.Visibility.Collapsed;
                }
            }

        }       
        #endregion

        #region General
        private void CargarGrilla(List<DtoDatosAdicionales> _lstDatosAdicionales)
        {
            var records = from emp in _lstDatosAdicionales
                          select emp;
            int showItem = Globales.iRegistrosPaginaPopUp;

            records = records.Skip((PageNumber - 1) * showItem).Take(showItem);
            //records = records.OrderByDescending(p => p.Campo).ToList();

            DtoDatosAdicionales oDtoNewDatos = null;
            List<DtoDatosAdicionales> lstNewDatos = new List<DtoDatosAdicionales>();
            foreach (var item in records)
            {
                oDtoNewDatos = new DtoDatosAdicionales();

                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                if (!item.Campo.Contains(_sMensajeSinRegistros))
                {
                    oDtoNewDatos.Campo = textInfo.ToTitleCase(item.Campo.ToLower()) + " :   ";
                }
                else
                {
                    oDtoNewDatos.Campo = textInfo.ToTitleCase(item.Campo.ToLower());
                }
                oDtoNewDatos.Valor = textInfo.ToTitleCase(item.Valor.ToLower());
                lstNewDatos.Add(oDtoNewDatos);
            }


            grvDatosAdicionales.ItemsSource = lstNewDatos;

        }
        #endregion

        #region Eventos Grilla
        private void grvDatosAdicionales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Obj = grvDatosAdicionales.SelectedItem as object;
            ObjectNotification = Obj;
        }
        #endregion
    }
}