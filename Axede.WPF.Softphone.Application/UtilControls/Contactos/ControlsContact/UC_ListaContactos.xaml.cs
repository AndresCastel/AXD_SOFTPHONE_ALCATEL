using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Axede.Utilidades;
using Axede.WPF.Softphone.Applications.BussinesClass.Entities;

namespace Axede.WPF.Softphone.Applications.UtilControls.Contactos.ControlsContact
{
    /// <summary>
    /// Interaction logic for UC_ListaContactos.xaml
    /// </summary>
    public partial class UC_ListaContactos : UserControl, INotifyPropertyChanged
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

        private bool _Mostrarfavoritos;

        public bool Mostrarfavoritos
        {
            get { return _Mostrarfavoritos; }
            set { _Mostrarfavoritos = value; }
        }
        private ObservableCollection<DtoContactos> _ObserGrilla;
        public ObservableCollection<DtoContactos> ObserGrilla
        {
            get { return _ObserGrilla; }
            set { _ObserGrilla = value; }
        }
        private static IProxyCommunicatorService oProxyCommunicatorService { get; set; }
        private string _NomContacto;
        private DtoContactos _dtoContactoSelec;

        public DtoContactos DtoContactoSelec
        {
            get { return _dtoContactoSelec; }
            set { _dtoContactoSelec = value; }
        }
        private DtoContactos _dtoEditarContacto;

        public DtoContactos DtoEditarContacto
        {
            get { return _dtoEditarContacto; }
            set
            {
                _dtoEditarContacto = value;
                NotifyPropertyChanged("DtoEditarContacto");
            }
        }


        public string NomContacto
        {
            get { return _NomContacto; }
            set
            {
                _NomContacto = value;
                NotifyPropertyChanged("NomContacto");
            }
        }


        //private int _CurrentPageIndex = 1;
        //private int _TotalPage = 0;
        //private int _TotalRegistros = 0;
        //private TipoPaginaPaginado _TipoPagina = TipoPaginaPaginado.PrimeraPagina;
        //private string _CampoOrden = "NombreCompleto";
        //private string _OrdenCampo = "Asc";
        private string _Filtro = string.Empty;
        //private UC_InfoGeneralContacto ucInfoGeneral = null;
        public string Filtro
        {
            get { return _Filtro; }
            set { _Filtro = value; }
        }
        //public int CurrentPageIndex
        //{
        //    get { return _CurrentPageIndex; }
        //    set { _CurrentPageIndex = value; }
        //}
        //public int TotalPage
        //{
        //    get { return _TotalPage; }
        //    set { _TotalPage = value; }
        //}
        //public TipoPaginaPaginado TipoPagina
        //{
        //    get { return _TipoPagina; }
        //    set { _TipoPagina = value; }
        //}
        //public string CampoOrden
        //{
        //    get { return _CampoOrden; }
        //    set { _CampoOrden = value; }
        //}
        //public string OrdenCampo
        //{
        //    get { return _OrdenCampo; }
        //    set { _OrdenCampo = value; }
        //}
        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }
        #endregion

        #region Formulario
        public UC_ListaContactos(bool _MostrarCheckBox = false)
        {
            InitializeComponent();
            if (!ApplicationIsInDesignMode)
            {
                if (_MostrarCheckBox)
                {
                    grvContactos.Columns[0].Visibility = Visibility.Visible;
                }
                else
                {
                    grvContactos.Columns[0].Visibility = Visibility.Hidden;
                }
                if (ObserGrilla == null)
                {
                    ObserGrilla = new ObservableCollection<DtoContactos>();
                    grvContactos.ItemsSource = ObserGrilla;
                }
                oProxyCommunicatorService = new ProxyCommunicatorService();

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void ucInfoGeneral_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (((UC_InfoGeneralContacto)sender).CambioEstado)
            {
                LlenarGrillaContactos(Mostrarfavoritos);
            }
        }
        #endregion

        #region Metodos Privados

        private void EstableceAtributosGrilla()
        {
            for (int iRow = 0; iRow < grvContactos.Items.Count; iRow++)
            {
                for (int iCol = 0; iCol < grvContactos.Columns.Count; iCol++)
                {
                    DataGridCell oCell = grvContactos.GetCell(iRow, iCol);
                    if (oCell != null)
                    {

                        if (oCell.Column != null)
                        {
                            DataGridColumn oColumnaBase = oCell.Column;
                            string sortPropertyName = WPFDataGridHelper.GetSortMemberPath(oColumnaBase);
                            if (!string.IsNullOrEmpty(sortPropertyName))
                            {

                                if (CampoOrden == sortPropertyName)
                                {
                                    if (CampoOrden == "Asc")
                                    {
                                        oColumnaBase.SortDirection = ListSortDirection.Descending;
                                    }
                                    else
                                    {
                                        oColumnaBase.SortDirection = ListSortDirection.Ascending;
                                    }
                                    break;
                                }
                            }
                        }

                        Button oButton = WPFDataGridHelper.GetVisualChild<Button>(oCell);
                        if (oButton != null)
                        {
                            string sMensajeToolTip = string.Empty;
                            //switch (oButton.Name)
                            //{
                            //    case "btnConsultarGrilla":
                            //        sMensajeToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_ToolTip_BotonConsultarGrilla);
                            //        break;

                            //    case "btnEditarGrilla":
                            //        sMensajeToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_ToolTip_BotonEditarGrilla);
                            //        break;

                            //    case "btnEliminarGrilla":
                            //        sMensajeToolTip = AdministradorMensaje.Instance.GetMensajePorCodigo(CodigoMensaje.General_ToolTip_BotonEliminaGrilla);
                            //        break;

                            //    default:
                            //        break;
                            //}

                            oButton.ToolTip = sMensajeToolTip;
                        }

                    }
                }
            }

        }

        private void LlenarGrillaContactos(bool _Mostrarfavoritos)
        {
            //if (NomContacto != null)
            //{
            //    PageInfoTO oPageInfoTO = new PageInfoTO();

            //    oPageInfoTO.PageSizeField = 20;
            //    oPageInfoTO.PageField = 1;
            //    oPageInfoTO.InitialLetterField = NomContacto;
            //    oPageInfoTO.UserId = UserSession.Instance.User.UserId;
            //    oPageInfoTO.Token = UserSession.Instance.User.Token;

            //    Axede.Interaxion.Communicator.ContactServerService.ContactTO[] _ContactTO = oProxyCommunicatorService.GetContactPageSize(oPageInfoTO);

            //    DtoContactos oDtoContactos = null;
            //    List<DtoContactos> lstContactos = new List<DtoContactos>();
            //    if (_ContactTO != null)
            //    {
            //        foreach (Axede.Interaxion.Communicator.ContactServerService.ContactTO oContactoTO in _ContactTO)
            //        {
            //            oDtoContactos = new DtoContactos();

            //            oDtoContactos.EstadoPresencia = PresenceState(oContactoTO.PresenceState);
            //          //  oDtoContactos.ToolTipEstado = ToolTipEstadoPrecencia(oContactoTO.PresenceState);
            //            oDtoContactos.Extension = oContactoTO.Extension;
            //            oDtoContactos.NombreCompleto = oContactoTO.Name.ToUpper() + " " + oContactoTO.LastName.ToUpper();
            //            oDtoContactos.Nombre = oContactoTO.Name.ToUpper();
            //            oDtoContactos.Apellido = oContactoTO.LastName.ToUpper();
            //            oDtoContactos.Telefono = oContactoTO.PhoneNumber;
            //            oDtoContactos.TipoTelefono = ObtenerTipoTelefono(oContactoTO.PhoneType);
            //          //  oDtoContactos.TipoTelefonoString = ObtenerToolTipTipoTelf(oContactoTO.PhoneType);
            //            oDtoContactos.Ide_Contact = oContactoTO.IdeContact;
            //            oDtoContactos.RecentContact = oContactoTO.RecentContact;
            //            oDtoContactos.Ide_Contact = oContactoTO.IdeContact;
            //            oDtoContactos.Ide_RecentContact = oContactoTO.IdeRecentContact;
            //            oDtoContactos.Ide_User = oContactoTO.IdeUser;
            //            oDtoContactos.Ide_PhoneType = oContactoTO.PhoneType;
            //            oDtoContactos.Ide_Contact = oContactoTO.ContactType;

            //            lstContactos.Add(oDtoContactos);

            //        }

            //        if (!_Mostrarfavoritos)
            //        {
            //            CargaGrilla = lstContactos;
            //        }
            //        else
            //        {
            //            List<DtoContactos> lstContactoFavorito = (from obj in lstContactos
            //                                                      where obj.RecentContact == true
            //                                                      select obj).ToList();

            //            CargaGrilla = lstContactoFavorito;
            //        }
            //    }
            //}
        }

        private string ObtenerToolTipTipoTelf(ContactServerService.PhoneType phoneType)
        {
            //string sToolTip = string.Empty;

            //if (phoneType == ContactServerService.PhoneType.Office)
            //{
            //    sToolTip = "Oficina";
            //}
            //else if (phoneType == ContactServerService.PhoneType.Home)
            //{
            //    sToolTip = "Casa";
            //}
            //else if (phoneType == ContactServerService.PhoneType.Mobile)
            //{
            //    sToolTip = "Mobil";
            //}

            //return sToolTip;
        }

        private ImageSource ObtenerTipoTelefono(ContactServerService.PhoneType _phoneType)
        {
            ImageSource _ImageTipoTelefono = null;
            if (_phoneType == ContactServerService.PhoneType.Office)
            {
                _ImageTipoTelefono = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/buildings.png"));
            }
            else if (_phoneType == ContactServerService.PhoneType.Home)
            {
                _ImageTipoTelefono = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/Home.png"));
            }
            else if (_phoneType == ContactServerService.PhoneType.Mobile)
            {
                _ImageTipoTelefono = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/mobile_basic_blue.png"));
            }

            return _ImageTipoTelefono;
        }

        private string ToolTipEstadoPrecencia(ContactServerService.PresenceState presenceState)
        {
            string sToolTip = string.Empty;

            if (presenceState == ContactServerService.PresenceState.Available)
            {
                sToolTip = "Disponible";
            }
            else if (presenceState == ContactServerService.PresenceState.Away)
            {
                sToolTip = "Ausente";
            }
            else if (presenceState == ContactServerService.PresenceState.Busy)
            {
                sToolTip = "Ocupado";
            }
            else if (presenceState == ContactServerService.PresenceState.InCall)
            {
                sToolTip = "En Llamada";
            }
            else if (presenceState == ContactServerService.PresenceState.Offline)
            {
                sToolTip = "No Disponible";
            }
            else if (presenceState == ContactServerService.PresenceState.OfflineStatusContact)
            {
                sToolTip = "No Disponible";
            }
            else
            {
                sToolTip = "No Disponible";
            }

            return sToolTip;
        }

        private ImageSource PresenceState(ContactServerService.PresenceState presenceState)
        {
            ImageSource _ImagePresencia = null;

            if (presenceState == ContactServerService.PresenceState.Available)
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/presencia-activa-off.png"));
            }
            else if (presenceState == ContactServerService.PresenceState.Away)
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/presencia-ausente-off.png"));
            }
            else if (presenceState == ContactServerService.PresenceState.Busy)
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/presencia-inactiva-off.png"));
            }
            else if (presenceState == ContactServerService.PresenceState.InCall)
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/presencia-inactiva-off.png"));
            }
            else if (presenceState == ContactServerService.PresenceState.Offline)
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/presencia-desconectado-off.png"));
            }
            else if (presenceState == ContactServerService.PresenceState.OfflineStatusContact)
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/desconectado-personal.png"));
            }
            else
            {
                _ImagePresencia = new BitmapImage(new Uri("pack://application:,,,/Axede.Interaxion.Communicator;component/Themes/Images/desconectado-personal.png"));
            }

            return _ImagePresencia;
        }

        #endregion

        #region Loading Grilla

        private EstadoProceso _EstadoProcesoCargueGrilla;
        public EstadoProceso EstadoProcesoCargueGrilla
        {
            get
            {
                return _EstadoProcesoCargueGrilla;
            }
            set
            {
                _EstadoProcesoCargueGrilla = value;
                OnStateChanged(value);
            }
        }
        protected virtual void OnStateChanged(EstadoProceso newValue)
        {
            if (newValue == EstadoProceso.Disponible)
            {
                this.Dispatcher.BeginInvoke(
                            (Action)(() => UCLoading.Visibility = System.Windows.Visibility.Collapsed));

                this.Dispatcher.BeginInvoke(
                    (Action)(() => grvContactos.Visibility = System.Windows.Visibility.Visible));


            }
            else
            {
                this.Dispatcher.BeginInvoke(
                    (Action)(() => grvContactos.Visibility = System.Windows.Visibility.Collapsed));


                this.Dispatcher.BeginInvoke(
                (Action)(() => UCLoading.Visibility = System.Windows.Visibility.Visible));
            }
        }

        #endregion

        #region Metodos Publicos
        public void ObtenerConatacto(bool _Favorito = false)
        {
            Mostrarfavoritos = _Favorito;
            Task tsk = Task.Factory.StartNew(() =>
            {
                EstadoProcesoCargueGrilla = EstadoProceso.Ocupado;

                Thread.Sleep(1000);

                this.Dispatcher.BeginInvoke(
                (Action)(() =>
                {
                    LlenarGrillaContactos(Mostrarfavoritos);
                }));

            });
            tsk.ContinueWith(t =>
            {

                EstadoProcesoCargueGrilla = EstadoProceso.Disponible;

            }, TaskScheduler.FromCurrentSynchronizationContext());



        }
        #endregion

        #region Controles
        private void btnInfoUser_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu oPopup = new System.Windows.Controls.ContextMenu();
            DependencyObject obj = (DependencyObject)e.OriginalSource;
            Button oBotonBase = (Button)obj;
            oPopup.Style = (Style)System.Windows.Application.Current.FindResource("FilterContexMenu");

            ucInfoGeneral = new UC_InfoGeneralContacto(DtoContactoSelec);
            ucInfoGeneral.PropertyChanged += new PropertyChangedEventHandler(ucInfoGeneral_PropertyChanged);
            oPopup.Items.Add(ucInfoGeneral);

            oPopup.PlacementTarget = oBotonBase;
            oPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
            oPopup.IsOpen = true;
        }

        private void grvContactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DtoContactoSelec = grvContactos.SelectedItem as DtoContactos;
        }

        private void btnEditContact_Click(object sender, RoutedEventArgs e)
        {
            if (DtoContactoSelec.RecentContact)
            {
                DtoEditarContacto = DtoContactoSelec;
            }
            else
            {
                //Mostrar Mensaje de No editar
            }
        }
        #endregion

        #region Cargar Grilla
        private List<DtoContactos> _CargaGrilla;
        public List<DtoContactos> CargaGrilla
        {
            get { return _CargaGrilla; }
            set
            {
                _CargaGrilla = value;
                General.ToObservableCollection<DtoContactos>(_CargaGrilla, ObserGrilla);
                //if (value.Count > 0)
                //{
                //    if (_Filtro.Trim() == string.Empty)
                //    {
                //        _TotalRegistros = Convert.ToInt32(value[0].TotalRegistros);
                //    }
                //    else
                //    {
                //        _TotalRegistros = Convert.ToInt32(value[0].TotalRegistros);
                //    }
                //}
                //else
                //{
                //    _TotalRegistros = 0;
                //}
                ////ActualizarTotalPaginas();
            }
        }
        #endregion
    }
}
