using Axede.BussinesObject.Application;
using Axede.WPF.Presenter.Softphone;
using Axede.WPF.View.Softphone;
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

namespace Axede.WPF.Softphone.Applications.GUI.User_Controls
{
    /// <summary>
    /// Interaction logic for UC_InfoGeneralContacto.xaml
    /// </summary>
    public partial class UC_InfoGeneralContacto : UserControl, INotifyPropertyChanged, IinfoContacto_View
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
        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }
        private bool _CambioEstado;
        InfoContacto_Presenter _InfoContacto_Presenter = null;
        public bool CambioEstado
        {
            get { return _CambioEstado; }
            set
            {
                _CambioEstado = value;
                NotifyPropertyChanged("CambioEstado");
            }
        }

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

        private DtoContactos _DtoContactoSelec;
        public DtoContactos DtoContactoSelec
        {
            get { return _DtoContactoSelec; }
            set { _DtoContactoSelec = value; }
        }
        private bool _VisibleBotonfavorito;

        public bool VisibleBotonfavorito
        {
            get { return _VisibleBotonfavorito; }
            set { _VisibleBotonfavorito = value; }
        }
        private bool _RecentContact;

        public bool RecentContact
        {
            get { return _RecentContact; }
            set { _RecentContact = value; }
        }
        #endregion

        #region Eventos UserControl
        public UC_InfoGeneralContacto(DtoContactos oDtoContacto, bool _VisibleFavorito = true)
        {
            InitializeComponent();
            _InfoContacto_Presenter = new InfoContacto_Presenter(this);
            if (!ApplicationIsInDesignMode)
            {
                if (oDtoContacto != null)
                {
                    DtoContactoSelec = oDtoContacto;
                    VisibleBotonfavorito = _VisibleFavorito;
                    CargarDatosContacto();
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!VisibleBotonfavorito)
            {
                btnActFavorite.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Metodos Privados
        private void CargarDatosContacto()
        {
            if (DtoContactoSelec.Ide_Contact == 0)
            {
                btnDelContact.Visibility = Visibility.Collapsed;

                if (DtoContactoSelec.RecentContact)
                {
                    ImageSource _ImageFavorito = null;

                    _ImageFavorito = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/Favorites.png"));
                    imgFavorito.ToolTip = "Eliminar Favorito";
                    imgFavorito.Source = _ImageFavorito;

                    RecentContact = DtoContactoSelec.RecentContact;
                }
            }
            else
            {
                btnActFavorite.Visibility = Visibility.Collapsed;

                ImageSource _ImageDelContact = null;

                _ImageDelContact = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/remove-user.png"));
                imgDelContact.Source = _ImageDelContact;
            }

            GenerarInfoContacto(DtoContactoSelec);

        }
        private void GenerarInfoContacto(DtoContactos DtoContactoSelec)
        {
            DtoDatosAdicionales oDtoDatosAdicionales = null;
            List<DtoDatosAdicionales> lstDatosAdicionales = new List<DtoDatosAdicionales>();

            oDtoDatosAdicionales = new DtoDatosAdicionales();
            oDtoDatosAdicionales.Campo = "Teléfono".Trim();
            oDtoDatosAdicionales.Valor = DtoContactoSelec.Telefono == null ? string.Empty : DtoContactoSelec.Telefono;

            lstDatosAdicionales.Add(oDtoDatosAdicionales);

            oDtoDatosAdicionales = new DtoDatosAdicionales();
            oDtoDatosAdicionales.Campo = "Extensión".Trim();
            oDtoDatosAdicionales.Valor = (DtoContactoSelec.Extension == null) ? string.Empty : DtoContactoSelec.Extension;

            lstDatosAdicionales.Add(oDtoDatosAdicionales);

            UC_DataDynamic ucInfoContacto = new UC_DataDynamic(lstDatosAdicionales, false, false);

            ctcContactInfo.Content = ucInfoContacto;

        }
        #endregion

        #region Eventos Control
        private void oExpanderInfo_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void btnActFavorite_Click(object sender, RoutedEventArgs e)
        {
            Contactos oRecentContacto = new Contactos();

            oRecentContacto.Ide_RecentUser = DtoContactoSelec.Ide_User;

            oContacto = oRecentContacto;
            if (RecentContact)
            {
                ImageSource _ImageFavorito = null;

                _ImageFavorito = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/IncludeFavorites.png"));
                imgFavorito.Source = _ImageFavorito;
                _InfoContacto_Presenter.EliminarFavorito();
            }
            else
            {
                ImageSource _ImageFavorito = null;

                _ImageFavorito = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/IncludeFavorites.png"));
                imgFavorito.Source = _ImageFavorito;
                RecentContact = true;

                _InfoContacto_Presenter.InsFavoritos();
            }
            CambioEstado = true;

        }

        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            //Metodo para llamar
            Contactos oRecentContacto = new Contactos();
            if (!string.IsNullOrEmpty(DtoContactoSelec.Telefono) || !string.IsNullOrEmpty(DtoContactoSelec.Extension))
            {
                LlamarNum = DtoContactoSelec;
            }
            else
            {
                LlamarNum = null;
            }
         

        }

        private void btnDelContact_Click(object sender, RoutedEventArgs e)
        {
            Contactos oRecentContacto = new Contactos();
            oRecentContacto.Ide_Contact = DtoContactoSelec.Ide_Contact;
            oContacto = oRecentContacto;
            _InfoContacto_Presenter.EliminarContacto();

            CambioEstado = true;
        }
        #endregion     
        
        private Contactos _oContacto;
        public Contactos oContacto
        {
            get { return _oContacto; }
            set { _oContacto = value; }
        }

        private bool _EstadoContacto;
        public bool EstadoContacto
        {
            get{ return _EstadoContacto; }
            set{ _EstadoContacto = value; }
        }
    }
}
