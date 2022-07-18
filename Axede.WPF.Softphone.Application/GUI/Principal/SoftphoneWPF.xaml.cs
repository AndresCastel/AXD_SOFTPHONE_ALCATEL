﻿//===================================================
//Desarrollado Por		    : Andres Castellanos
//Fecha de Creación		    : 15/Julio/2013
//Lenguaje Programación	    : [C#]
//Producto o sistema	    : SOLUCION SOFTPHONE
//Empresa			        : Axede S.A
//Cliente			        : Axede S.A
//===================================================
//Versión	Descripción
//[1.0.0.0]
//Formulario Principal que contiene las funcionalidades graficas y llamado al core del negocio del softphone
//===================================================
//MODIFICACIONES:
//===================================================
//Ver.	 Fecha		Autor – Empresa 	Descripción

//XX	dd/mm/aaaa	[Nombre Completo]	 [Razón del cambio realizado] 
//===================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Axede.Utilidades;
using System.Net.NetworkInformation;
using Axede.WPF.Softphone.Applications.BussinesClass.Configuracion;
using Axede.WPF.Softphone.Applications.BussinesClass.Enum;
using Axede.WPF.Softphone.Applications.GUI.User_Controls;
using Axede.WPF.Softphone.Applications.PortSIP_Class;
using Axede.WPF.Softphone.Applications.UtilControls;
using Axede.WPF.Softphone.Applications.UtilControls.ModalMessageBox;
using Axede.WPF.Softphone.Applications.UtilControls.PopUp;
using Axede.WPF.View.Softphone;
using Axede.WPF.Presenter.Softphone;
using Axede.BussinesObject.Application;
using Axede.WPF.Softphone.Applications.UtilClass;
using System.Threading;
using Axede.WPF.Softphone.Applications.GUI.Login_Comunication;
using Axede.Encryption;
using System.Windows.Media.Animation;
using Axede.Ping;
using System.Globalization;
using System.IO;

namespace Axede.WPF.Softphone.Applications.GUI.Principal
{
    /// <summary>
    /// Interaction logic for SoftphoneWPF.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SoftphoneWPF : Window, SIPCallbackEvents, ISoftphoneWPF_View
    {

        #region Propiedades
        private System.Media.SoundPlayer _player;

        Thread myThread;

        public System.Media.SoundPlayer Player
        {
            get { if (_player != null) return _player; else return _player = new System.Media.SoundPlayer(); }
            set { _player = value; }
        }
        private string _UsuarioEnc = null;
        private string _SIPServerEnc = null;
        private bool BanHilo = true;
        private string _AliasEnc = null;
        private string _PuertoEnc = null;
        private string _ClaveEnc = null;
        private bool _BanFileoverPrincipal = true;
        private string _ExtMarcar;
        public string ExtMarcar
        {
            get { return _ExtMarcar; }
            set { _ExtMarcar = value; }
        }

        public string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Contactos.ini";
        public string KeyExternalCall = ConfigurationManager.AppSettings.Get("KeyExternalCall");
        private string _sBaseRegistry = "Software\\\\MicroSoft";
        private string _sRegSubKeyName = "CDDA9601BC8CB56C9662B9F29D6375DCD7610EFD13650AE4";
        private IniFile _ini;
        public IniFile Ini
        {
            get { if (_ini != null) return _ini; else return _ini = new IniFile(filePath); }
            set { _ini = value; }
        }

        public ContextMenu oPopup;

        private Button btn;

        public string MuestraMensaje
        {
            set
            {
                MessageBoxModal.Show(General.ResolveOwnerWindow(), value, "Error", MessageBoxButton.OK, MessageBoxImage.Error, true);
            }
        }

        private List<DtoContactos> _ListContactFav;
        public List<DtoContactos> ListContactFav
        {
            get { return _ListContactFav; }
            set
            {
                CmbFavorities.ItemsSource = null;
                DtoContactos dt = new DtoContactos();
                dt.NombreCompleto = "BORRAR REGISTRO";
                dt.Ide_User = 0;
                value.Insert(0, dt);
                _ListContactFav = value;
                CmbFavorities.ItemsSource = value;
            }
        }

        private List<DtoContactos> _ListContactFavMemoria;
        public List<DtoContactos> ListContactFavMemoria
        {
            get { if (_ListContactFavMemoria != null) return _ListContactFavMemoria; else return _ListContactFav = new List<DtoContactos>(); }
            set
            {
                _ListContactFavMemoria = value;
            }
        }

        private static bool ApplicationIsInDesignMode
        {
            get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _Clave;
        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Alias;
        public string Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }

        private string _SIP_Server;
        public string SIP_Server
        {
            get { return _SIP_Server; }
            set { _SIP_Server = value; }
        }

        private string _Port;
        public string Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        private SoftphoneWPF_Presenter _SoftphoneWPF_Presenter;

        private bool _BanVideoEnlinea = false;
        private bool _BanAutoRespuesta = false;
        private bool _BanConferencia = false;
        private bool _BanLlamadaEspera = false;
        private bool _BanNoDisponible = false;

        private string _NumRedial;

        private ComboBox _cmbFavorities;
        public ComboBox CmbFavorities
        {
            get { if (_cmbFavorities != null) return _cmbFavorities; else return _cmbFavorities = new ComboBox(); }
            set { _cmbFavorities = value; }
        }

        private ClassMethodUtil _classutil;
        public ClassMethodUtil Classutil
        {
            get { return _classutil; }
            set { _classutil = value; }
        }
        private List<ConfiguracionVideoconferencia> _ListSalasConferencia;
        public List<ConfiguracionVideoconferencia> ListSalasConferencia
        {
            get { return _ListSalasConferencia; }
            set { _ListSalasConferencia = value; }
        }

        //FileOver
        private List<ConfiguracionOXEServerService> _ListOXEFileOver;
        public List<ConfiguracionOXEServerService> ListOXEFileOver
        {
            get { if (_ListOXEFileOver != null) return _ListOXEFileOver; else return _ListOXEFileOver = new List<ConfiguracionOXEServerService>(); }
            set { _ListOXEFileOver = value; }
        }

        ConfiguracionOXEServerService OxePrincipal = null;
        ConfiguracionOXEServerService OxeSecundario = null;
        private bool PrimerServidor;
        private bool SegundoServidor;
        private bool bFileOverServices;
        private int iIntentosPingServer;
        private int iTiempoPing;
        

        private static List<ConfiguracionOXEServerService> _ListServidoresOXE;
        public static List<ConfiguracionOXEServerService> ListServidoresOXE
        {
            get { return _ListServidoresOXE; }
            set { _ListServidoresOXE = value; }
        }
       //

        private const int MAX_LINES = 9; // Maximo de Líneas
        private const int LINE_BASE = 1; // Línea por defecto
        private bool _banMute = false; //Bandera para saber si el dispositivo esta en mute            
        private bool _BanLlamadaRespuesta = false; //Bandera para saber si el boton toma la opcion de respuesta o de llamar

        private int _sessionId;
        public int SessionId // Id de la sesion que se esta manejando actualmente
        {
            get { return _sessionId; }
            set
            {
                _sessionId = value;
            }
        }

        private ControlVideo _popcontrol;
        public ControlVideo Popcontrol // Usercontrol que almacena el video local y remoto
        {
            get
            {
                if(_popcontrol!=null){
                    return _popcontrol;
                }
                else { return _popcontrol = new ControlVideo();}
            }
            set { _popcontrol = value; }
        }



        private Session[] _CallSessions = new Session[MAX_LINES]; //Clase para definir sesiones

        private bool _SIPInited = false; //Bandera que valida si hay una sesion inicializada
        private bool _SIPLogined = false; //Bandera que valida si esta logueado
        private int _CurrentlyLine = LINE_BASE; // Línea actual

        private PortSIPCore _core; //Clase core que utiliza por medio de interfaz los metodos de la dll PortSIPCore;
        #endregion

        /// <summary>
        /// Recoge la accion de los botones para enviar tonos y pintar en la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Llamado Hijos Botonera
        void Botonera_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool banCaracterEspecial = false;
            if (sender is Botonera__UC)
            {
                if (Botonera.AccionBoton == BotoneraEnum.ASTERISCO)
                {
                    textBlockDialingNumber.Text = textBlockDialingNumber.Text + "*";
                    _core.sendDtmf(_CallSessions[_CurrentlyLine].getSessionId(), '*');
                    banCaracterEspecial = true;
                }
                else if (Botonera.AccionBoton == BotoneraEnum.NUMERAL)
                {
                    textBlockDialingNumber.Text = textBlockDialingNumber.Text + "#";
                    _core.sendDtmf(_CallSessions[_CurrentlyLine].getSessionId(), '#');
                    banCaracterEspecial = true;
                }
                else { textBlockDialingNumber.Text = textBlockDialingNumber.Text + (int)Botonera.AccionBoton; }
                if (_SIPInited == true && _CallSessions[_CurrentlyLine].getSessionState() == true)
                {
                    if (!banCaracterEspecial)
                    {
                        _core.sendDtmf(_CallSessions[_CurrentlyLine].getSessionId(), Convert.ToChar((((int)Botonera.AccionBoton).ToString())));
                    }
                    banCaracterEspecial = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// Metodo Conectar y Desconectar, si el usuario ya se ha identificado no solicita credenciales nuevamente
        /// </summary>
        /// <param name="UserName" >Usuario de identificacion</param>
        /// <param name="Clave">Clave del usuario de identificacion</param>
        /// <param name="Alias">Nombre Usuario</param>
        /// <param name="SIP_Server">Sip Server</param>
        /// <param name="Port">Puerto Sip Server</param>
        #region Autenticacion

        public  void Conectar()
        {

            try
            {
                if (_SIPInited == true)
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Usted ya esta Logeado", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

                    return;
                }
                if (string.IsNullOrEmpty(this.Alias))
                {
                    textBlockIdentifier.Text = UserName;
                }
                if (UserName.Length <= 0)
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "El usuario está vacio.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }

                if (this.SIP_Server.Length <= 0)
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "El Sip Server está vacio.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }


                int SIPServerPort = 0;
                if (this.Port.Length > 0)
                {
                    SIPServerPort = int.Parse(this.Port);
                    if (SIPServerPort > 65535 || SIPServerPort <= 0)
                    {
                        MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "El puerto del SIPServer esta fuera de Rango.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                        return;
                    }
                }


                int StunServerPort = 0;
                //if (TextBoxStunPort.Text.Length > 0)
                //{
                //    StunServerPort = int.Parse(TextBoxStunPort.Text);
                //    if (StunServerPort > 65535 || StunServerPort <= 0)
                //    {
                //        MessageBox.Show("The Stun server port is out of range.");

                //        return;
                //    }
                //}


                TRANSPORT_TYPE transport = TRANSPORT_TYPE.TRANSPORT_UDP;
                //switch (ComboBoxTransport.SelectedIndex)
                //{
                //    case 0:
                //        transport = TRANSPORT_TYPE.TRANSPORT_UDP;
                //        break;

                //    case 1:

                //        transport = TRANSPORT_TYPE.TRANSPORT_TLS;
                //        break;

                //    case 2:
                //        transport = TRANSPORT_TYPE.TRANSPORT_TCP;
                //        break;
                //}
                //
                // Create the class instance of PortSIP SDK wrapper 
                //

                _core = new PortSIPCore(0, this);
                //Point poi = new Point();
                //poi.X = this.RestoreBounds.X;
                //poi.Y = this.RestoreBounds.Y;

                Popcontrol = new ControlVideo(ref _core);


                //// Popcontrol.Close();
                //Popcontrol.UpdatePosition(poi);
                //Popcontrol._SIPinited = _SIPInited;
                //Popcontrol._CallSessionsG = _CallSessions;
                //Popcontrol._CurrentlyLineG = _CurrentlyLine;
                //Popcontrol._sessionId = SessionId;
                //
                // Create and set the SIP callback handers, this MUST called before
                // _core.initialize();
                //
                _core.createCallbackHandlers();

                string logFilePath = "d:\\"; // The log file path, you can change it - the folder MUST exists
                string agent = "PortSIP VoIP SDK 7.0";
                string stunServer = string.Empty;
                int errorCode = 0;



                // Initialize the SDK
                Boolean state = _core.initialize(transport,
                                 PORTSIP_LOG_LEVEL.PORTSIP_LOG_NONE,
                                 logFilePath,
                                 MAX_LINES,
                                 agent,
                                 stunServer,
                                 StunServerPort,
                                 false,
                                 false,
                                 out errorCode);

                if (state == false)
                {
                    _core.shutdownCallbackHandlers();
                    _core.releaseCallbackHandlers();
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Fallo al inicializar.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

                    return;
                }


                _SIPInited = true;

                loadDevices();


                string password = this.Clave;
                string userDomain = string.Empty;
                string SIPServer = this.SIP_Server;
                string displayName = this.Alias;
                string authName = string.Empty;


                int outboundServerPort = 0;
                string outboundServer = "";

                _core.setAudioCodecParameter(AUDIOCODEC_TYPE.AUDIOCODEC_AMRWB, "mode-set=0; octet-align=0; robust-sorting=0");

                Random rd = new Random();
                int LocalSIPPort = rd.Next(1000, 5000) + 4000; // Generate the random port for SIP

                StringBuilder localIP = new StringBuilder();
                localIP.Length = 64;
                int rt = _core.getLocalIP(0, localIP, 64);
                if (rt != 0)
                {
                    _core.shutdownCallbackHandlers();
                    _core.unInitialize();
                    _core.releaseCallbackHandlers();

                    _SIPInited = false;

                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Problemas al traer la ip Local.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

                    return;
                }


                // Set the SIP user information
                rt = _core.setUserInfo(this.UserName,
                                           displayName,
                                           authName,
                                           password,
                                           localIP.ToString(),
                                           LocalSIPPort,
                                           userDomain,
                                           SIPServer,
                                           SIPServerPort,
                                           outboundServer,
                                           outboundServerPort);
                if (rt != 0)
                {
                    _core.shutdownCallbackHandlers();
                    _core.unInitialize();
                    _core.releaseCallbackHandlers();

                    _SIPInited = false;

                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Almacenar información del usuario erronea.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }


                SetSRTPType();

                string licenseKey = "NTRGNkY1QUM1MERFOUQzNzUyMDhBMEM5NzhEOUVFNzRAOEUxQzM4QTU1QUQ2RUQ2NDFCRjc2Mzk4NDYyNDAxQzlAMkQ5MDFCMjc5MDQ0OEUxOUQ2QTJCNjkyMjZBNzZCMkNAQjg2RTQzNENGNzlENEU3OEYzRjQ2NEU1NTZCQ0M3RjA.";
                _core.setLicenseKey(licenseKey);



                //_core.setLocalVideoWindow(localVideoWindow.Child.Handle);




                SetVideoResolution();
                SetVideoQuality();

                UpdateAudioCodecs();
                UpdateVideoCodecs();

                if (_core.isAudioCodecEmpty() == true)
                {
                    InitDefaultAudioCodecs();
                }

                InitSettings();


                rt = _core.registerServer(90);
                if (rt != 0)
                {
                    _SIPInited = false;

                    _core.shutdownCallbackHandlers();
                    _core.unInitialize();

                    _core.releaseCallbackHandlers();

                    // ListBoxSIPLog.Items.Clear();

                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Fallo en el Registro del servidor.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                }
                else
                {
                    textBlockCallStateInfo.Text = "Conectando...";
                    textBlockRegStatus.Text = "En Línea: " + UserName;
                    General.sUserName = UserName;
                    Popcontrol.InitVideoRemote();
                    textBlockIdentifier.Text = this.Alias;


                }

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.InnerException.ToString());
            }
           

            // ListBoxSIPLog.Items.Add("Registering...");
        }

        public System.IO.StreamWriter file;

         public void FileOverMethod()
        {
            OxePrincipal = new ConfiguracionOXEServerService();
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pingReply;
            foreach (ConfiguracionOXEServerService item in ListOXEFileOver)
            {
                if (item.Principal == true)
                {
                    OxePrincipal = item;
                }
                else { OxeSecundario = item; }
            }
            while (BanHilo)
            {
                try
                {

                    /* Una instancia de la clase PingReply para manejar la respuesta
                     * y usamos el método Send() */
                                   
                    pingReply = ping.Send(OxePrincipal.IP);
                    // Creamos una instancia de la clase ping                 
                    string Directorio = "C://";
                    string NombreArchivo = "LogPing.txt";
                    //Manejo del monitoreo de los ping
                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["MonitoreoPing"]))
                    {
                        if (!File.Exists(Directorio + NombreArchivo))
                        {
                            file = File.CreateText(Directorio + NombreArchivo);
                        }
                        else
                        {
                           
                                file = new System.IO.StreamWriter(Directorio + NombreArchivo, true);
                        }
                        file.WriteLine();
                        file.WriteLine("Envio Ping: " + DateTime.Now + "Estatus del ping: " + pingReply.Status.ToString());
                        //close the file
                        file.Close();
                    }
                    if (pingReply.Status != IPStatus.Success)
                    {
                        iIntentosPingServer = iIntentosPingServer - 1;
                        if (iIntentosPingServer == 0 && _BanFileoverPrincipal == true)
                        {
                            //Log.LogManager LogSoftphone = new Log.LogManager("LogSoftphone");
                            //LogSoftphone.writeErrorLog("Problemas Con el Oxe Principal" + "OXE Caido" + "\r\n" + " - Hora Sistema: " + DateTime.Now.ToString("F", DateTimeFormatInfo.InvariantInfo));                   
                        this.Dispatcher.Invoke(new Action(delegate
                            {
                                deRegisterFromServer();
                                this.SIP_Server = OxeSecundario.IP;                       
                                Conectar();
                            })); 
                            _BanFileoverPrincipal = false;
                        }
                    }
                    else
                    {
                        iIntentosPingServer = Convert.ToInt32(ConfigurationManager.AppSettings["IntentosPingServer"]);
                        
                       if (_BanFileoverPrincipal == true && _SIPInited == false)
                       {
                           this.Dispatcher.Invoke(new Action(delegate
                   {
                           this.SIP_Server = OxePrincipal.IP;
                           if (!_SIPLogined)
                           {
                               Conectar();
                           }
                   }));
                       }
                       else if (_BanFileoverPrincipal == false && _SIPInited == true) 
                       {
                           this.Dispatcher.Invoke(new Action(delegate
                   {
                       deRegisterFromServer();
                       this.SIP_Server = OxePrincipal.IP;
                       Conectar();
                       _BanFileoverPrincipal = true;
                   }));
                       }
                       //if (_BanFileoverPrincipal == false)
                       //{
                       //    deRegisterFromServer();
                       //    this.SIP_Server = OxePrincipal.IP;
                       //    Conectar();
                       //}
                       //_BanFileover = true;
                        
                    }

                  
                    // Escribimos la respuesta en consola
                    //Console.WriteLine("Address: {0}", pingReply.Address);
                    //Console.WriteLine("Time in miliseconds: {0}", pingReply.RoundtripTime);
                    //Console.WriteLine("Status: {0}", pingReply.Status);
                    /* Añadimos un pequeño truco para que no se cierre la consola, es totalmente opcional
                     * consiste en dejar la consola esperando por una línea de entrada */
                   // Console.ReadLine();
                }
                catch (InvalidOperationException Exunhand)
                {
                    //Log.LogManager LogSoftphone = new Log.LogManager("LogSoftphone");
                    //LogSoftphone.writeErrorLog("Problemas Con el Oxe Principal" + Exunhand.Message + "\r\n" + Exunhand.StackTrace + " - Hora Sistema: " + DateTime.Now.ToString("F", DateTimeFormatInfo.InvariantInfo));                   
                  
                }
                catch (Exception ex)
                {
                    //Log.LogManager LogSoftphone = new Log.LogManager("LogSoftphone");
                    //LogSoftphone.writeErrorLog("Problemas pidiendo una Notificacion  de Papeleta al Router" + ex.Message + "\r\n" + ex.StackTrace + " - Hora Sistema: " + DateTime.Now.ToString("F", DateTimeFormatInfo.InvariantInfo));
                  
                }
                iTiempoPing = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutPingServer"]);
                Thread.Sleep(iTiempoPing); 
            }
        }

        private void deRegisterFromServer()
        {
            if (_SIPInited == false)
            {
                return;
            }

            for (int i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getRecvCallState() == true)
                {
                    string reason = " Ocupado";
                    _core.rejectCall(_CallSessions[i].getSessionId(), 486, reason);
                }
                else if (_CallSessions[i].getSessionState() == true)
                {
                    _core.terminateCall(_CallSessions[i].getSessionId());
                }

                _CallSessions[i].reset();
            }

            if (_SIPLogined == true)
            {
                try
                {
                    _core.unRegisterServer();
                }
                catch (Exception ex)
                {                    
                   MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "El canal esta ocupado, busque otro usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel, true);
                   this.Close();
                }
                _SIPLogined = false;
            }


            if (_SIPInited == true)
            {
                //
                // MUST called before _core.unInitliaze();
                //
                _core.shutdownCallbackHandlers();

                _core.unInitialize();

                //
                // MUST called after _core.unInitliaze();
                //
                _core.releaseCallbackHandlers();


                _SIPInited = false;
            }
            //   ComboBoxLines.SelectedIndex = 0;
            _CurrentlyLine = LINE_BASE;


            //ComboBoxSpeakers.Items.Clear();
            //ComboBoxMicrophones.Items.Clear();
            //ComboBoxCameras.Items.Clear();

        }

        #endregion

        /// <summary>
        /// Botones que ejecutan las acciones principales en el softphone. LLamar, Colgar, Mensaje, Video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Botones Principales

        private void BtnColgar_Click(object sender, RoutedEventArgs e)
        {
            _BanLlamadaRespuesta = false;
            _BanVideoEnlinea = false;
            BtnLlamar.ToolTip = "Llamar";
            if (_SIPInited == false || _SIPLogined == false)
            {
                return;
            }
            textBlockDialingNumber.Text = string.Empty;
            if (_CallSessions[_CurrentlyLine].getRecvCallState() == true)
            {
                string reason = "Ocupado";
                _core.rejectCall(_CallSessions[_CurrentlyLine].getSessionId(), 486, reason);
                _CallSessions[_CurrentlyLine].reset();

                textBlockCallStateInfo.Text = "Llamada Rechazada";
                //   ListBoxSIPLog.Items.Add(Text);
                Player.Stop();
                return;
            }

            if (_CallSessions[_CurrentlyLine].getSessionState() == true)
            {
                _core.terminateCall(_CallSessions[_CurrentlyLine].getSessionId());
                _CallSessions[_CurrentlyLine].reset();


                textBlockCallStateInfo.Text = "Colgado";
                Player.Stop();
                this.Dispatcher.Invoke(new Action(delegate
                {
                    lblTimeElapsed.Reset();
                    if (Popcontrol != null)
                    {
                        Popcontrol.Visibility = Visibility.Collapsed;
                    }
                    lblTimeElapsed.Visibility = Visibility.Collapsed;
                    lblTimeElapsed.IsStarted = false;
                }));

                //  ListBoxSIPLog.Items.Add(Text);
            }
        }

        private void BtnMensaje_Click(object sender, RoutedEventArgs e)
        {
            string Mensaj = ConfigurationManager.AppSettings.Get("VoiceMailNumber");
            textBlockDialingNumber.Text = Mensaj;
            Llamar();
            //_SoftphoneWPF_Presenter.Login();
        }

        private void BtnLlamar_Click(object sender, RoutedEventArgs e)
        {
            Llamar();
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            LanzarVideo();
        }

        private void LanzarVideo()
        {
            //if (_PopUpVideo != null)
            //{
            //    _PopUpVideo = null;
            //}
            Point poi = new Point();
            poi.X = this.RestoreBounds.X;
            poi.Y = this.RestoreBounds.Y;
            //_PopUpVideo = new PopUpControl();
            //if (_PopUpVideo != null)
            //{
            //    _PopUpVideo.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
            //    _PopUpVideo.PlacementTarget = this;
            //    _PopUpVideo.AreAnimationsEnabled = true;
            //    _PopUpVideo.IsOpen = true;
            //}

            //Validar el numero de instancias a crear
          
                //Popcontrol = new ControlVideo(poi, ref _core);
                
                //// Popcontrol.Close();
                Popcontrol.UpdatePosition(poi);
                Popcontrol._SIPinited = _SIPInited;
                Popcontrol._CallSessionsG = _CallSessions;
                Popcontrol._CurrentlyLineG = _CurrentlyLine;
                Popcontrol._sessionId = SessionId;
             
                //if (SessionId > 0)
                //{
                //    _BanVideoEnlinea = true;
                //    Popcontrol.SetVideoRemote(SessionId);
                //}

                //Valida si la opcion de videoconferencia esta activa
                //VIDEO_RESOLUTION videoResolution = VIDEO_RESOLUTION.VIDEO_CIF;
                //if (_BanConferencia == true)
                //{
                //    int rt = -1; ;
                //    if (Popcontrol != null)
                //    {
                //        rt = Popcontrol.createConference(videoResolution, false);
                //    }
                //    if (rt == 0)
                //    {
                //        textBlockCallStateInfo.Text = "Conexión a conferencia satisfactoria";
                //        for (int i = LINE_BASE; i < MAX_LINES; ++i)
                //        {
                //            if (_CallSessions[i].getSessionState() == true)
                //            {
                //                joinConference(i);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        textBlockCallStateInfo.Text = "Fallo al relizar conferencia";
                //        _BanConferencia = false;
                //    }
                //}
                //  UpdatePosition();

                //Popcontrol.DragMove();
                Popcontrol.Owner = this;
                Popcontrol.Show();
                if (!_BanConferencia)
                {
                    Popcontrol.SetVideoLocal(true);
                }
                //Popcontrol.WindowState = WindowState.Normal;
                //if (Popcontrol.Visibility == Visibility.Visible)
                //{
                //    Popcontrol.Minimiz();
                //}
                //else
                //{
                //    Popcontrol.Visibility = Visibility.Visible;
                //    Popcontrol.Activate();
                //    Popcontrol.UpdatePosition(poi);
                //}
            
        }

        private void Llamar()
        {

            if (!_BanLlamadaRespuesta)
            {
                if (_SIPInited == false || _SIPLogined == false)
                {
                    return;
                }
                if (textBlockDialingNumber.Text.Length <= 0)
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "El telefono esta vacio.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

                    return;
                }

                if (_CallSessions[_CurrentlyLine].getSessionState() == true || _CallSessions[_CurrentlyLine].getRecvCallState() == true)
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Línea actual está ocupada, por favor, cambie de línea.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }


                string callTo = textBlockDialingNumber.Text;

                UpdateAudioCodecs();
                UpdateVideoCodecs();
                // InitDefaultAudioCodecs();

                if (_core.isAudioCodecEmpty() == true)
                {
                    InitDefaultAudioCodecs();
                }



                //  Usually for 3PCC need to make call without SDP

                Boolean hasSdp = true;
                //if (CheckBoxSDP.IsChecked == true)
                //{
                //    hasSdp = false;
                //}

                _core.setAudioDeviceId(ComboBoxMicrophones.SelectedIndex, ComboBoxSpeakers.SelectedIndex);

                int errorCode = 0;
                SessionId = _core.call(callTo, hasSdp, out errorCode);
                //if (errorCode != 0)
                //{
                //    ListBoxSIPLog.Items.Add("Call failed");
                //    return;
                //}

                if (Popcontrol != null)
                {
                    Popcontrol.Owner = this;
                    Popcontrol._SIPinited = _SIPInited;
                    Popcontrol._CallSessionsG = _CallSessions;
                    Popcontrol._CurrentlyLineG = _CurrentlyLine;
                    Popcontrol._sessionId = SessionId;
                    Popcontrol.SetVideoRemote(SessionId);
                }

                _CallSessions[_CurrentlyLine].setSessionId(SessionId);
                _CallSessions[_CurrentlyLine].setSessionState(true);

                string Text;
                Text = "Llamando...";
                _NumRedial = textBlockDialingNumber.Text;
                textBlockCallStateInfo.Text = Text;
            }
            else
            {
                if (_SIPInited == false || _SIPLogined == false)
                {
                    return;
                }

                if (_CallSessions[_CurrentlyLine].getRecvCallState() == false)
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "La línea actual no tiene una llamada entrante, por favor cambiar de línea", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                    return;
                }

                _CallSessions[_CurrentlyLine].setRecvCallState(false);
                _CallSessions[_CurrentlyLine].setSessionState(true);

                int ses = _CallSessions[_CurrentlyLine].getSessionId();
                SessionId = ses;

                if (Popcontrol != null)
                {
                    //Popcontrol.Owner = this;
                    //// Popcontrol.Close();
                    //// Popcontrol.UpdatePosition(poi);
                    //Popcontrol._SIPinited = _SIPInited;
                    //Popcontrol._CallSessionsG = _CallSessions;
                    //Popcontrol._CurrentlyLineG = _CurrentlyLine;
                    //Popcontrol._sessionId = SessionId;
                    Popcontrol.SetVideoRemote(SessionId);
                }

                int rt = _core.answerCall(_CallSessions[_CurrentlyLine].getSessionId());
                if (rt == 0)
                {
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        lblTimeElapsed.Reset();
                        lblTimeElapsed.Visibility = Visibility.Visible;
                        lblTimeElapsed.IsStarted = true;
                    }));
                    string Text;
                    Text = "Llamada Establecida";
                    textBlockCallStateInfo.Text = Text;
                   Player.Stop();
                    joinConference(_CurrentlyLine);
                }
                else
                {
                    _CallSessions[_CurrentlyLine].reset();

                    string Text;
                    Text = "Fallo Responder Llamada";
                    textBlockCallStateInfo.Text = Text;
                }
            }
        }

        private void DigitarExt()
        {
            string SecWaitSendTones = ConfigurationManager.AppSettings.Get("SecWaitSendTones");
            Thread.Sleep(Convert.ToInt32(SecWaitSendTones));
            if (!string.IsNullOrEmpty(ExtMarcar))
            {
                for (int i = 0; i < ExtMarcar.Length; i++)
                {
                    if (_SIPInited == true && _CallSessions[_CurrentlyLine].getSessionState() == true)
                    {
                        _core.sendDtmf(_CallSessions[_CurrentlyLine].getSessionId(), Convert.ToChar(ExtMarcar[i]));
                    }
                }
            }        
        }

        #endregion

        #region Constructor
        public SoftphoneWPF(string username, string clave, string alias, string sip_server, string port)
        {

            try
            {
                this.UserName = username;
                this.Clave = clave;
                this.Alias = alias;
                this.SIP_Server = sip_server;
                this.Port = port;

                InitializeComponent();

                if (!ApplicationIsInDesignMode)
                {
                    TraerDatosContactoAsignado();
                    _SoftphoneWPF_Presenter = new SoftphoneWPF_Presenter(this);
                    this.Dispatcher.BeginInvoke(
                    (Action)(() => UCLoading.Visibility = System.Windows.Visibility.Visible));

                    textBlockRegStatus.Visibility = Visibility.Hidden;
                    textBlockCallStateInfo.Visibility = Visibility.Hidden;
                    textBlockDialingNumber.Visibility = Visibility.Hidden;
                    textBlockIdentifier.Visibility = System.Windows.Visibility.Hidden;
                    ucPrincipalContact = new PrincipalContactos();
                    ucPrincipalContact.PropertyChanged += ucPrincipalContact_PropertyChanged;
                    ctcContact.Content = ucPrincipalContact;
                    ListSalasConferencia = new List<ConfiguracionVideoconferencia>();
                    ListServidoresOXE = new List<ConfiguracionOXEServerService>();
                    Classutil = new ClassMethodUtil();
                    BtnLlamar.ToolTip = "Llamar";
                    Botonera.PropertyChanged += Botonera_PropertyChanged;
                    ConfiguracionVideoconferencia Video = null;
                    SeccionVideoConferencia seccion = ConfigurationManager.GetSection("videoconferencias") as SeccionVideoConferencia;
                    if (null != seccion)
                    {
                        if (seccion.videoconferenciasConfiguradas.Count > 0)
                        {
                            for (int i = 0; i < seccion.videoconferenciasConfiguradas.Count; i++)
                            {
                                Video = seccion.videoconferenciasConfiguradas[i];
                                if (Video != null)
                                {
                                    ListSalasConferencia.Add(Video);
                                }
                            }
                        }
                    }
                    grvVideoConferencia.ItemsSource = ListSalasConferencia;


                    //TRAEMOS LAS CONFIGURACIONES DE LOS TAGS DEL CONFIG
                    bFileOverServices = Convert.ToBoolean(ConfigurationManager.AppSettings["FileOverService"]);
                    iIntentosPingServer = Convert.ToInt32(ConfigurationManager.AppSettings["IntentosPingServer"]);
                    ConfiguracionOXEServerService OxeFileover = null;
                    SeccionOXEServerServices seccionOXE = ConfigurationManager.GetSection("OXEServerServices") as SeccionOXEServerServices;
                    if (null != seccionOXE)
                    {
                        if (seccionOXE.OXEServerServiceConfigurados.Count > 0)
                        {
                            for (int i = 0; i < seccionOXE.OXEServerServiceConfigurados.Count; i++)
                            {
                                OxeFileover = seccionOXE.OXEServerServiceConfigurados[i];
                                if (OxeFileover != null)
                                {
                                    ListOXEFileOver.Add(OxeFileover);
                                }
                            }
                        }
                    }
                    //Conectar();
                    //FILEOVER SOFTPHONE
                    string FileOver = ConfigurationManager.AppSettings.Get("FileOverService");
                    if (Convert.ToBoolean(FileOver))
                    {

                        //REVISAMOS EL ESTADO DE LA RED CON EL OXE
                        //  Timer tmr = new Timer(FileOverOXEservers, "tick...", 2000, 1000);
                        // FileOverMethod();
                        ThreadStart tr = new ThreadStart(FileOverMethod);
                        myThread = new Thread(tr);
                        myThread.SetApartmentState(System.Threading.ApartmentState.STA);
                        myThread.Name = "MessageRouterBrokerOperacionesThread";
                        myThread.Start();
                        //Thread t = new Thread(new ThreadStart(FileOverMethod));
                        //t.SetApartmentState(System.Threading.ApartmentState.STA);
                        //t.Start();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void FileOverOXEservers()
        {

            bool bFileOverServices = Convert.ToBoolean(ConfigurationManager.AppSettings["FileOverService"]);
            if (bFileOverServices == true)
            {
                if (_ListServidoresOXE != null)
                {
                    //Ordena lista de servidores..el primero en la lista es el activo sin falla y principal
                    var lstServerOrdenada = from srv in _ListServidoresOXE
                                            orderby srv.Principal descending, srv.Activo descending
                                            select srv;

                    _ListServidoresOXE = lstServerOrdenada.ToList();

                    int iIntentosPingServer = Convert.ToInt32(ConfigurationManager.AppSettings["IntentosPingServer"]);
                    if (iIntentosPingServer == 0)
                        iIntentosPingServer = 4;

                    foreach (ConfiguracionOXEServerService iOXEServer in _ListServidoresOXE)
                    {
                        
                            string Host = iOXEServer.IP;

                            Ping.Ping oTestHost = new Ping.Ping();

                            PingResponse responsePing = oTestHost.PingHost(Host, iIntentosPingServer);
                            if (responsePing != null)
                            {
                                if (responsePing.PingResult == PingResponseType.Ok)
                                {
                                    //El servidor está arriba
                                    PrimerServidor = true;
                                    SegundoServidor = false;
                                    //iOXEServer.Activo = true;
                                    break;
                                }
                                else
                                {
                                    PrimerServidor = false;
                                    SegundoServidor = false;
                                    Thread t = new Thread(new ThreadStart(VerificaEstadoServerPrincipal));
                                    t.SetApartmentState(System.Threading.ApartmentState.STA);
                                    t.Name = "Thread ID: " + "VerificaEstadoPrincipal";
                                    t.IsBackground = true;
                                    t.Start();
                                }
                            }
                    
                    }

                }
            }

        }

        private static void VerificaEstadoServerPrincipal()
        {
            Ping.Ping oTestHost = new Ping.Ping();

            int iIntentosPingServer = Convert.ToInt32(ConfigurationManager.AppSettings["IntentosPingServer"]);
            if (iIntentosPingServer == 0)
                iIntentosPingServer = 4;

            IEnumerable<string> ipServerPricipal = (from srv in _ListServidoresOXE
                                                    where srv.Principal == true
                                                    select srv.IP);


            string sServerPrincipal = ipServerPricipal.ToArray()[0].ToString();

            while (true)
            {
                PingResponse responsePing = oTestHost.PingHost(sServerPrincipal, iIntentosPingServer);
                if (responsePing != null)
                {
                    if (responsePing.PingResult == PingResponseType.Ok)
                    {

                        foreach (ConfiguracionOXEServerService iServer in _ListServidoresOXE)
                        {
                            if (iServer.IP == sServerPrincipal)
                            {
                                iServer.Activo = true;
                            }
                            else
                            {
                                iServer.Activo = false;
                            }
                        }

                        break;
                    }
                }
            }

        }

        private void TraerContactosFavoritos()
        {
            _SoftphoneWPF_Presenter.ObtenerListaContactosFavoritos();
        }

        void ucPrincipalContact_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LlamarNum")
            {

                if (!string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.NombreCompleto))
                {
                    if (!string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                    {
                        textBlockDialingNumber.Text = KeyExternalCall + ((PrincipalContactos)sender).LlamarNum.Telefono;
                    }
                    if (!string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Extension))
                    {
                        ExtMarcar = ((PrincipalContactos)sender).LlamarNum.Extension;
                        if(string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                        {
                        textBlockDialingNumber.Text = ExtMarcar;
                        }
                    }
                    else
                    {
                        ExtMarcar = string.Empty;
                    }                   
                    Llamar();
                }
                else
                {
                    MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "El telefono esta vacio.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                }
            }
        }
        #endregion

        /// <summary>
        /// Configuracion y actualizacion de los codecs de video y Audio
        /// </summary>
        #region Configuracion Codecs

        private void UpdateAudioCodecs()
        {
            if (_SIPInited == false)
            {
                return;
            }

            _core.clearAudioCodec();

            //if (checkBoxPCMU.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMU);
            //}


            //if (checkBoxPCMA.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMA);
            //}


            //if (checkBoxG729.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G729);
            //}

            //if (checkBoxILBC.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_ILBC);
            //}


            //if (checkBoxGSM.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_GSM);
            //}


            //if (checkBoxAMR.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_AMR);
            //}

            //if (CheckBoxG722.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G722);
            //}

            //if (CheckBoxSpeex.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_SPEEX);
            //}

            //if (CheckBoxAMRwb.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_AMRWB);
            //}

            //if (CheckBoxSpeexWB.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_SPEEXWB);
            //}

            //if (CheckBoxG7221.IsChecked == true)
            //{
            //    _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G7221);
            //}

            _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMU);
            _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMA);
            _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G729);
            _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_DTMF);

        }

        private void UpdateVideoCodecs()
        {
            //if (_SIPInited == false)
            //{
            //    return;
            //}

            //_core.clearVideoCodec();

            //if (checkBoxH263.IsChecked == true)
            //{
             _core.addVideoCodec(VIDEOCODEC_TYPE.VIDEOCODEC_H264);
            //}

            //if (checkBoxH2631998.IsChecked == true)
            //{
            //    _core.addVideoCodec(VIDEOCODEC_TYPE.VIDEOCODEC_H263_1998);
            //}

            //if (checkBoxH264.IsChecked == true)
            //{
            //_core.addVideoCodec(VIDEOCODEC_TYPE.VIDEOCODEC_H264);
            //}

            //if (checkBoxVP8.IsChecked == true)
            //{
            //    _core.addVideoCodec(VIDEOCODEC_TYPE.VIDEOCODEC_VP8);
            //}

        }

        //Codecs Audio



        //Codecs Video            //private void checkBoxPCMU_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void checkBoxPCMA_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void checkBoxG729_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void checkBoxILBC_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void checkBoxAMR_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void CheckBoxG722_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void CheckBoxSpeex_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void CheckBoxG7221_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void checkBoxGSM_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateAudioCodecs();
        //}

        //private void checkBoxH2631998_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateVideoCodecs();
        //}

        //private void checkBoxH263_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateVideoCodecs();
        //}

        //private void checkBoxH264_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateVideoCodecs();
        //}

        //private void checkBoxVP8_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateVideoCodecs();
        //}

        //private void CheckBoxAMRwb_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateVideoCodecs();
        //}

        //private void CheckBoxSpeexWB_CheckedChanged(object sender, RoutedEventArgs e)
        //{
        //    UpdateVideoCodecs();
        //}

        #endregion

        /// <summary>
        /// Slider de Microfono y speakers asi como validacion de silencio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Volumen

        private void sliderSpeaker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_SIPInited == false)
            {
                return;
            }

            _core.setSpeakerVolume((Int32)e.NewValue);
        }

        private void sliderMicrophone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_SIPInited == false)
            {
                return;
            }

            _core.setMicVolume((Int32)e.NewValue);
        }

        private void BtnMute_Click(object sender, RoutedEventArgs e)
        {
            if (_SIPInited == false || _SIPLogined == false)
            {
                return;
            }

            if (_banMute == true)
            {
                _core.muteMicrophone(false);
                ImgMicro.Source = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/VolumenOn.png"));
                _banMute = false;
            }
            else
            {
                _core.muteMicrophone(true);
                ImgMicro.Source = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/VolumenOff.png"));
                _banMute = true;
            }
        }

        #endregion

        #region TAB Contactos
        private PrincipalContactos ucPrincipalContact = null;
        #endregion

        #region TAB VideoConferencia

        private void BtnConf_Click(object sender, RoutedEventArgs e)
        {
            if (_BanConferencia)
            {
                _BanConferencia = false;
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                b.GradientStops.Add(new GradientStop(Colors.Gray, 0.5));
                b.GradientStops.Add(new GradientStop(Colors.Gray, 1));
                BtnConferencia.Background = b;
            }
            else
            {
                _BanConferencia = true;
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                b.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
                b.GradientStops.Add(new GradientStop(Colors.Green, 1));
                BtnConferencia.Background = b;

            }
            if (_SIPInited == false || _SIPLogined == false)
            {
                _BanConferencia = false;
                return;
            }

            VIDEO_RESOLUTION videoResolution = VIDEO_RESOLUTION.VIDEO_CIF;

            //TODO: Andres Castellanos
            //Validacion deshabilitada y dejada por defecto CIF por el consumo de recursos en el RMX
            //switch (ComboBoxVideoResolution.SelectedIndex)
            //{
            //    case 0:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_QCIF;
            //        break;
            //    case 1:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_CIF;
            //        break;
            //    case 2:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_VGA;
            //        break;
            //    case 3:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_SVGA;
            //        break;
            //    case 4:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_XVGA;
            //        break;
            //    case 5:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_720P;
            //        break;
            //}
            //Point poi = new Point();
            //poi.X = this.RestoreBounds.X;
            //poi.Y = this.RestoreBounds.Y;
            //Popcontrol = new ControlVideo(poi, ref _core);
            //Popcontrol.Owner = this;
            //// Popcontrol.Close();
            //// Popcontrol.UpdatePosition(poi);
            //Popcontrol._SIPinited = _SIPInited;
            //Popcontrol._CallSessionsG = _CallSessions;
            //Popcontrol._CurrentlyLineG = _CurrentlyLine;
            //Popcontrol._sessionId = SessionId;
           
            if (_BanConferencia == true)
            {
                int rt = -1; ;
                
                if (Popcontrol != null)
                {
                    Popcontrol.SetVideoLocal(true);
                    rt = Popcontrol.createConference(videoResolution, false);                   
                }                
                if (rt == 0)
                {
                    textBlockCallStateInfo.Text = "Conexión a conferencia satisfactoria";
                    for (int i = LINE_BASE; i < MAX_LINES; ++i)
                    {
                        if (_CallSessions[i].getSessionState() == true)
                        {
                            joinConference(i);
                        }
                    }
                }
                else
                {
                    textBlockCallStateInfo.Text = "Fallo al realizar conferencia";
                    _BanConferencia = false;
                }
            }
            else
            {
                // Stop conference

                // Before stop the conference, MUST place all lines to hold state

                int[] sessionIds = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };


                for (int i = LINE_BASE; i < MAX_LINES; ++i)
                {
                    if (_CallSessions[i].getSessionState() == true && _CallSessions[i].getHoldState() == false)
                    {

                        sessionIds[i] = _CallSessions[i].getSessionId();

                        // Hold it 
                        _core.hold(sessionIds[i]);
                        _CallSessions[i].setHoldState(true);
                    }
                }

                _core.destroyConference();
                textBlockCallStateInfo.Text = "Liberar de Conferencia";

            }
        }

        private void joinConference(Int32 index)
        {
            if (_SIPInited == false)
            {
                return;
            }
            if (_BanConferencia == false)
            {
                return;
            }

            _core.joinToConference(_CallSessions[index].getSessionId());

            _CallSessions[index].setHoldState(false);
        }

        private void grvVideoConferencia_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void grvVideoConferencia_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCrearSala_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminarSala_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LlamarSala_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionVideoconferencia Seleccionado = grvVideoConferencia.SelectedItem as ConfiguracionVideoconferencia;
            textBlockDialingNumber.Text = string.Empty;
            switch (Seleccionado.TipoLlamada)
            {
                case (int)TipoLlamadaEnum.Extension:
                    textBlockDialingNumber.Text = Seleccionado.NumeroMarcar;
                    break;
                case (int)TipoLlamadaEnum.IP:
                    textBlockDialingNumber.Text = "sip:@" + Seleccionado.NumeroMarcar + ":5060";
                    break;
                case (int)TipoLlamadaEnum.URL:
                    textBlockDialingNumber.Text = Seleccionado.NumeroMarcar;
                    break;
                default:
                    break;
            }
            Llamar();
            LanzarVideo();
        }

        #endregion

        /// <summary>
        /// Carga los dispositivos que esten conectados en el sistema y cambia funcionalidades dependiendo de lo que el usuario utilice
        /// </summary>
        #region TAB Configuracion

        private void loadDevices()
        {
            if (_SIPInited == false)
            {
                return;
            }

            int num = _core.getNumOfPlayoutDevices();
            for (int i = 0; i < num; ++i)
            {
                StringBuilder deviceName = new StringBuilder();
                deviceName.Length = 256;

                if (_core.getPlayoutDeviceName(i, deviceName, 256) == 0)
                {
                    ComboBoxSpeakers.Items.Add(deviceName.ToString());
                }

                ComboBoxSpeakers.SelectedIndex = 0;
            }


            num = _core.getNumOfRecordingDevices();
            for (int i = 0; i < num; ++i)
            {
                StringBuilder deviceName = new StringBuilder();
                deviceName.Length = 256;

                if (_core.getRecordingDeviceName(i, deviceName, 256) == 0)
                {
                    ComboBoxMicrophones.Items.Add(deviceName.ToString());
                }

                ComboBoxMicrophones.SelectedIndex = 0;
            }


            num = _core.getNumOfVideoCaptureDevices();
            for (int i = 0; i < num; ++i)
            {
                StringBuilder uniqueId = new StringBuilder();
                uniqueId.Length = 256;
                StringBuilder deviceName = new StringBuilder();
                deviceName.Length = 256;

                if (_core.getVideoCaptureDeviceName(i, uniqueId, 256, deviceName, 256) == 0)
                {
                    ComboBoxCameras.Items.Add(deviceName.ToString());
                }

                ComboBoxCameras.SelectedIndex = 0;
            }


            int volume = _core.getSpeakerVolume();
            sliderSpeaker.Value = volume;

            volume = _core.getMicVolume();
            sliderMicrophone.Value = volume;

        }

        private void ComboBoxSpeakers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_SIPInited == true)
            {
                _core.setAudioDeviceId(ComboBoxMicrophones.SelectedIndex, ComboBoxSpeakers.SelectedIndex);
            }
        }

        private void ComboBoxMicrophones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_SIPInited == true)
            {
                _core.setAudioDeviceId(ComboBoxMicrophones.SelectedIndex, ComboBoxSpeakers.SelectedIndex);
            }
        }

        private void ComboBoxCameras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_SIPInited)
            {
                _core.setVideoDeviceId(ComboBoxCameras.SelectedIndex);
            }
        }

        private void ComboBoxSRTP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetSRTPType();
        }

        #endregion

        /// <summary>
        /// Eventos del window principal, opciones de drag inicializacion y cerrado, posisionamiento de la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Eventos Window

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



            //  MessageBox.Show("This sample was built base on evaluation PortSIP VoIP SDK, which allows only three minutes conversation. The conversation will be cut off automatically after three minutes, then you can't hearing anything. Feel free contact us at: sales@portsip.com to purchase the official version.", "Information");


            // Create the call sessions array, the PortSIP VoIP SDK allows maximum 100 lines,
            // but we just use 8 lines with this sample, we need a class to save the call sessions information

            int i = 0;
            sliderSpeaker.Value = 0;
            sliderMicrophone.Value = 0;
            for (i = 0; i < MAX_LINES; ++i)
            {
                _CallSessions[i] = new Session();
                _CallSessions[i].reset();
            }

            _SIPInited = false;
            _SIPLogined = false;
            _CurrentlyLine = LINE_BASE;

            ComboBoxLines.Items.Add("Línea 1");
            ComboBoxLines.Items.Add("Línea 2");
            ComboBoxLines.Items.Add("Línea 3");
            ComboBoxLines.Items.Add("Línea 4");
            ComboBoxLines.Items.Add("Línea 5");
            ComboBoxLines.Items.Add("Línea 6");
            ComboBoxLines.Items.Add("Línea 7");
            ComboBoxLines.Items.Add("Línea 8");

            ComboBoxLines.SelectedIndex = 0;

            rddActions.IsImageSource = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/Menu.png"));


            if (!bFileOverServices)
            {
                this.Conectar();
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deRegisterFromServer();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Probar si se puede evitar el click derecho
            try
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {

                }
                else
                {
                    DragMove();
                }
            }
            catch (Exception ex)
            {

                // throw ex;
            }

        }

        void itemMenu_ReseteoLogin_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdatePosition()
        {
            this.Left = this.Left + this.ActualWidth;
            this.Top = this.Top;
        }

        private void _this_LocationChanged(object sender, EventArgs e)
        {
            Point poi = new Point();
            if (Popcontrol != null)
            {
                poi.X = this.RestoreBounds.X;
                poi.Y = this.RestoreBounds.Y;
                Popcontrol.UpdatePosition(poi);
            }
        }

        private void tc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// Opciones del menu, Cerrar, Ayuda, y Acerca de SOFTPHONE AXEDE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Menu

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            BanHilo = false;
            rddActions.IsOpen = false;

            if (_SIPInited == false)
            {
                App.Current.Shutdown();
                return;
            }
            Environment.Exit(Environment.ExitCode);
            deRegisterFromServer();
           
            App.Current.Shutdown();
        }      

        private void rmiReset_Click(object sender, RoutedEventArgs e)
        {
            rddActions.IsOpen = false;

            MessageBoxResult _rsul = MessageBoxModal.Show("¿ Desea resetear las credenciales del Login automático ?", Globales.NombreAplicacion, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (_rsul == MessageBoxResult.Yes)
            {
                ResetLogeoAutomatico();
                if (_SIPInited == false)
                {
                    return;
                }
                deRegisterFromServer();
                textBlockRegStatus.Text = "Offline";
                textBlockIdentifier.Text = string.Empty;
                textBlockCallStateInfo.Text = "Sin Conexión";
                Login _frmLogin = new Login();
                _frmLogin.Owner = this;
                _frmLogin.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                _frmLogin.ShowDialog();
                this.Alias = _frmLogin.txtAlias.Text;
                this.SIP_Server = _frmLogin.txtServer.Text;
                this.Port = _frmLogin.txtPuerto.Text;
                this.Clave = _frmLogin.txtClave.Password;
                this.UserName = _frmLogin.txtUsuario.Text;
                this.Conectar();
                ActualizaRegistroUsuario();
               
            }
        }

        private void ActualizaRegistroUsuario()
        {
            if (!string.IsNullOrEmpty(_userName))
            {
                _UsuarioEnc = Crypto.ActionEncrypt(_userName);
                _ClaveEnc = Crypto.ActionEncrypt(_Clave);
                _SIPServerEnc = Crypto.ActionEncrypt(_SIP_Server);
                _AliasEnc = Crypto.ActionEncrypt(_Alias);
                _PuertoEnc = Crypto.ActionEncrypt(_Port);
                Microsoft.Win32.RegistryKey oReg = default(Microsoft.Win32.RegistryKey);
                oReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_sBaseRegistry, true);
                oReg = oReg.CreateSubKey(_sRegSubKeyName);
                oReg.SetValue("UserSettings", _UsuarioEnc);
                oReg.SetValue("GUID", _ClaveEnc);
                oReg.SetValue("ServerSettings", _SIPServerEnc);
                oReg.SetValue("PortSettings", _PuertoEnc);
                oReg.SetValue("DisplaySettings", _AliasEnc);
                oReg.Close();
            }
        }


        private void ResetLogeoAutomatico()
        {
            string _sBaseRegistry = "Software\\\\MicroSoft";
            string _sRegSubKeyName = "CDDA9601BC8CB56C9662B9F29D6375DCD7610EFD13650AE4";
            Microsoft.Win32.RegistryKey oReg = default(Microsoft.Win32.RegistryKey);
            oReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_sBaseRegistry, true);
            oReg = oReg.CreateSubKey(_sRegSubKeyName);
            oReg.SetValue("UserSettings", string.Empty);
            oReg.SetValue("GUID", string.Empty);
            oReg.SetValue("ServerSettings", string.Empty);
            oReg.SetValue("PortSettings", string.Empty);
            oReg.SetValue("DisplaySettings", string.Empty);
            oReg.Close();
        }

        #endregion

        /// <summary>
        /// Inicializacion de codecs resoluciones y validaciones de configuracion por defecto
        /// </summary>
        #region Resolucion Valores Defecto

        private void InitSettings()
        {
            if (_SIPInited == false)
            {
                return;
            }
            string dtmfsobre2833 = ConfigurationManager.AppSettings.Get("DTMFsobre2833");
            _core.enableDTMFOfRFC2833(Convert.ToInt32(dtmfsobre2833)); // Use DTMF as RTP event - RFC2833
            _core.setDtmfSamples(160);

            //      _core.enableDtmfOfInfo(); // Use DTMF as SIP INFO method

            //if (checkBoxAEC.IsChecked == true)
            //{
            _core.enableAEC(true);
            //}
            //else
            //{
            //    _core.enableAEC(false);
            //}

            //if (checkBoxVAD.IsChecked == true)
            //{
            //    _core.enableVAD(true);
            //}
            //else
            //{
            _core.enableVAD(false);
            //  }

            //if (checkBoxCNG.IsChecked == true)
            //{
            //    _core.enableCNG(true);
            //}
            //else
            //{
            _core.enableCNG(false);
            //  }

            //if (checkBoxAGC.IsChecked == true)
            //{
            //    _core.enableAGC(true);
            //}
            //else
            //{
            _core.enableAGC(false);
            //  }
        }

        private void InitDefaultAudioCodecs()
        {
            if (_SIPInited == false)
            {
                return;
            }


            _core.clearAudioCodec();

            // Default we just using PCMU, PCMA, G729
            _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMU);
            //  _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_PCMA);
            _core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_G729);

            //_core.addAudioCodec(AUDIOCODEC_TYPE.AUDIOCODEC_DTMF);  // for DTMF as RTP Event - RFC2833

        }

        private void SetSRTPType()
        {
            if (_SIPInited == false)
            {
                return;
            }

            SRTP_POLICY SRTPPolicy = SRTP_POLICY.SRTP_POLICY_NONE;

            //switch (ComboBoxSRTP.SelectedIndex)
            //{
            //    case 0:
            //        SRTPPolicy = SRTP_POLICY.SRTP_POLICY_NONE;
            //        break;

            //    case 1:
            //        SRTPPolicy = SRTP_POLICY.SRTP_POLICY_PREFER;
            //        break;

            //    case 2:
            //        SRTPPolicy = SRTP_POLICY.SRTP_POLICY_FORCE;
            //        break;
            //}

            _core.setSrtpPolicy(SRTPPolicy);
        }

        private void SetVideoResolution()
        {
            if (_SIPInited == false)
            {
                return;
            }

            VIDEO_RESOLUTION videoResolution = VIDEO_RESOLUTION.VIDEO_CIF;

            //switch (ComboBoxVideoResolution.SelectedIndex)
            //{
            //    case 0:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_QCIF;
            //        break;
            //    case 1:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_CIF;
            //        break;
            //    case 2:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_VGA;
            //        break;
            //    case 3:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_SVGA;
            //        break;
            //    case 4:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_XVGA;
            //        break;
            //    case 5:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_720P;
            //        break;
            //    case 6:
            //        videoResolution = VIDEO_RESOLUTION.VIDEO_QVGA;
            //        break;
            //}

            _core.setVideoResolution(videoResolution);
        }

        private void SetVideoQuality()
        {
            if (_SIPInited == false)
            {
                return;
            }

            //    _core.setVideoBitrate((Int32)sliderVideoQualityLevel.Value);
        }

        #endregion

        /// <summary>
        /// Llamada en espera, Conferencia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Configuracion General Llamadas

        private void BtnLlamadaEspera_Click(object sender, RoutedEventArgs e)
        {
            if (!_BanVideoEnlinea)
            {
                if (_BanLlamadaEspera)
                {
                    _BanLlamadaEspera = false;
                    LinearGradientBrush b = new LinearGradientBrush();
                    ColorConverter cc = new ColorConverter();
                    b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                    b.GradientStops.Add(new GradientStop(Colors.Gray, 0.5));
                    b.GradientStops.Add(new GradientStop(Colors.Gray, 1));
                    BtnLlamadaEspera.Background = b;
                }
                else
                {
                    _BanLlamadaEspera = true;
                    LinearGradientBrush b = new LinearGradientBrush();
                    ColorConverter cc = new ColorConverter();
                    b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                    b.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
                    b.GradientStops.Add(new GradientStop(Colors.Green, 1));
                    BtnLlamadaEspera.Background = b;

                }
                if (_BanLlamadaEspera)
                {
                    if (_SIPInited == false || _SIPLogined == false)
                    {
                        return;
                    }

                    if (_CallSessions[_CurrentlyLine].getSessionState() == false)
                    {
                        return;
                    }


                    if (_CallSessions[_CurrentlyLine].getHoldState() == true)
                    {
                        return;
                    }


                    string Text;


                    int rt = _core.hold(_CallSessions[_CurrentlyLine].getSessionId());
                    if (rt != 0)
                    {
                        Text = "Línea " + _CurrentlyLine.ToString();
                        Text = Text + ": Fallo en Espera";
                        textBlockCallStateInfo.Text = Text;

                        return;
                    }


                    _CallSessions[_CurrentlyLine].setHoldState(true);

                    Text = "Línea " + _CurrentlyLine.ToString();
                    Text = Text + ": En Espera";
                    textBlockCallStateInfo.Text = Text;
                }
                else
                {
                    if (_SIPInited == false || _SIPLogined == false)
                    {
                        return;
                    }

                    if (_CallSessions[_CurrentlyLine].getSessionState() == false)
                    {
                        return;
                    }


                    if (_CallSessions[_CurrentlyLine].getHoldState() == false)
                    {
                        return;
                    }


                    string Text;
                    int rt = _core.unHold(_CallSessions[_CurrentlyLine].getSessionId());
                    if (rt != 0)
                    {
                        _CallSessions[_CurrentlyLine].setHoldState(false);

                        Text = "Línea " + _CurrentlyLine.ToString();
                        Text = Text + ": Fallo Llamada Recuperada.";
                        textBlockCallStateInfo.Text = Text;

                        return;
                    }

                    _CallSessions[_CurrentlyLine].setHoldState(false);

                    Text = "Line " + _CurrentlyLine.ToString();
                    Text = Text + ": Llamada Recuperada";
                    textBlockCallStateInfo.Text = Text;
                }
            }
            else
            { MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No es posible pasar a una VideoLlamada a espera.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true); }
        }

        private void ComboBoxLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SIPInited == false || _SIPLogined == false)
            {
                ComboBoxLines.SelectedIndex = 0;
                return;
            }

            if (_CurrentlyLine == (ComboBoxLines.SelectedIndex + LINE_BASE))
            {
                return;
            }

            if (_BanConferencia == true)
            {
                _CurrentlyLine = ComboBoxLines.SelectedIndex + LINE_BASE;
                return;
            }

            // To switch the line, must hold currently line first
            if (_CallSessions[_CurrentlyLine].getSessionState() == true && _CallSessions[_CurrentlyLine].getHoldState() == false)
            {
                _core.hold(_CallSessions[_CurrentlyLine].getSessionId());
                _CallSessions[_CurrentlyLine].setHoldState(true);

                string Text = "Línea " + _CurrentlyLine.ToString();
                Text = Text + ": Espera";
                textBlockCallStateInfo.Text = Text;
            }



            _CurrentlyLine = ComboBoxLines.SelectedIndex + LINE_BASE;


            // If target line was in hold state, then un-hold it
            if (_CallSessions[_CurrentlyLine].getSessionState() == true && _CallSessions[_CurrentlyLine].getHoldState() == true)
            {
                _core.unHold(_CallSessions[_CurrentlyLine].getSessionId());
                _CallSessions[_CurrentlyLine].setHoldState(false);

                string Text = "Línea " + _CurrentlyLine.ToString();
                Text = Text + ": Recuperada - Llamada Establecida";
                textBlockCallStateInfo.Text = Text;
            }
        }

        #endregion

        /// <summary>
        /// Inicializacion y llamado a los metodos del core
        /// </summary>
        #region Metodos CORE
        private void ButtonReject_Click(object sender, RoutedEventArgs e)
        {
            if (_SIPInited == false || _SIPLogined == false)
            {
                return;
            }

            if (_CallSessions[_CurrentlyLine].getRecvCallState() == true)
            {
                string reason = "Ocupado";
                _core.rejectCall(_CallSessions[_CurrentlyLine].getSessionId(), 486, reason);
                _CallSessions[_CurrentlyLine].reset();

                string Text = "Línea " + _CurrentlyLine.ToString();
                textBlockCallStateInfo.Text = Text + "Llamada Rechazada";
                //   ListBoxSIPLog.Items.Add(Text);

                return;
            }
        }

        private void ButtonTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (_SIPInited == false || _SIPLogined == false)
            {
                return;
            }

            if (_CallSessions[_CurrentlyLine].getSessionState() == false)
            {
                MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Necesita establecer una llamada Primero.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
                return;
            }

            string referTo = string.Empty;
            Trasferencia_UC TransferDlg = new Trasferencia_UC();
            // ViewWindow_Modal.Cerrar = ViewWindow_Modal.WinBehavior.DontClose;
            MessageBoxResult Response = ViewWindow_Modal.Show(TransferDlg, "Trasferencia", ViewWindow_Modal.MyMessageBoxButton.X);
            if (TransferDlg.response == MessageBoxResult.OK)
            {
                referTo = TransferDlg.TextBoxTranferNumber.Text;
            }

            //if (referTo.Length <= 0)
            //{
            //    MessageBox.Show("El numero de Transferencia esta vacio");
            //    return;
            //}
            if (!string.IsNullOrEmpty(referTo))
            {
                int rt = _core.refer(_CallSessions[_CurrentlyLine].getSessionId(), referTo);
                if (rt != 0)
                {
                    string Text = "Línea " + _CurrentlyLine.ToString();
                    textBlockCallStateInfo.Text = Text + ": Fallo al Transferir";
                    // ListBoxSIPLog.Items.Add(Text);
                }
                else
                {
                    string Text = "Línea " + _CurrentlyLine.ToString();
                    textBlockCallStateInfo.Text = Text + ": Transferencia";
                    //ListBoxSIPLog.Items.Add(Text);
                }
            }
        }

        private void BtnAA_Click(object sender, RoutedEventArgs e)
        {
            if (_BanAutoRespuesta)
            {
                _BanAutoRespuesta = false;
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                b.GradientStops.Add(new GradientStop(Colors.Gray, 0.5));
                b.GradientStops.Add(new GradientStop(Colors.Gray, 1));
                BtnAA.Background = b;
            }
            else
            {
                _BanAutoRespuesta = true;
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                b.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
                b.GradientStops.Add(new GradientStop(Colors.Green, 1));
                BtnAA.Background = b;

            }
        }

        private void BtnNoMolestar_Click(object sender, RoutedEventArgs e)
        {
            if (_BanNoDisponible)
            {
                _BanNoDisponible = false;
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                b.GradientStops.Add(new GradientStop(Colors.Gray, 0.5));
                b.GradientStops.Add(new GradientStop(Colors.Gray, 1));
                BtnDND.Background = b;
            }
            else
            {
                _BanNoDisponible = true;
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.Black, 0));
                b.GradientStops.Add(new GradientStop(Colors.Green, 0.5));
                b.GradientStops.Add(new GradientStop(Colors.Green, 1));
                BtnDND.Background = b;

            }
        }
        public Int32 onRegisterSuccess(Int32 callbackObject, Int32 statusCode, String statusText)
        {
            // use the Invoke method to modify the control.

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                General.sUserName = this.UserName;
                textBlockCallStateInfo.Text = "Registro Exitoso";
                this.Dispatcher.BeginInvoke(
        (Action)(() => UCLoading.Visibility = System.Windows.Visibility.Collapsed));
                textBlockRegStatus.Visibility = Visibility.Visible;
                textBlockCallStateInfo.Visibility = Visibility.Visible;
                textBlockDialingNumber.Visibility = Visibility.Visible;
                textBlockIdentifier.Visibility = System.Windows.Visibility.Visible;
            }));

            _SIPLogined = true;


            return 0;
        }


        public Int32 onRegisterFailure(Int32 callbackObject, Int32 statusCode, String statusText)
        {

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                General.sUserName = this.UserName;
                textBlockCallStateInfo.Text = "Registro Exitoso";
                this.Dispatcher.BeginInvoke(
        (Action)(() => UCLoading.Visibility = System.Windows.Visibility.Collapsed));
                textBlockRegStatus.Visibility = Visibility.Visible;
                textBlockCallStateInfo.Visibility = Visibility.Visible;
                textBlockDialingNumber.Visibility = Visibility.Visible;
                textBlockIdentifier.Visibility = System.Windows.Visibility.Visible;
            }));

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = "Fallo al Registrarse";
            }));


            _SIPLogined = false;

            return 0;
        }


        public Int32 onInviteIncoming(Int32 callbackObject,
                                          Int32 sessionId,
                                          String caller,
                                          String callerDisplayName,
                                          String callee,
                                          String calleeDisplayName,
                                          String audioCodecName,
                                          String videoCodecName,
                                          Boolean hasVideo)
        {

            int i = 0;
            _BanLlamadaRespuesta = true;
            BtnLlamar.Dispatcher.Invoke(new Action(delegate
            {
                BtnLlamar.ToolTip = "Contestar";
            }));

            Player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Themes\Sonidos\TelephoneRing.wav";
           // Player.SoundLocation = "pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Sonidos/TelephoneRing.wav";
            Player.Play();

            bool state = false;
            string Text = string.Empty;

            if (hasVideo == true)
            {
               // Popcontrol._core = _core;
            }
            else
            {
                // This incoming call hasn't the video SDP
            }

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionState() == false && _CallSessions[i].getRecvCallState() == false)
                {
                    state = true;
                    _CallSessions[i].setRecvCallState(true);
                    break;
                }
            }

            if (state == false)
            {
                string reason = "Ocupado";
                _core.rejectCall(sessionId, 486, reason);

                return 0;
            }


            // For DND(Do not disturb
            Boolean DND = false;
            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                if (_BanNoDisponible == true)
                {
                    DND = true;
                }
            }));


            if (DND == true)
            {
                string reason = "Ocupado";
                _core.rejectCall(sessionId, 486, reason);
                _CallSessions[i].reset();

                Text = "Línea " + i.ToString();
                Text = Text + ": Llamada Rechazada por DND";


                textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
                {
                    textBlockCallStateInfo.Text = Text;
                }));

                Player.Stop();
                return 0;
            }

            _CallSessions[i].setSessionId(sessionId);

            bool needIgnoreAutoAnswer = false;
            int j = 0;

            for (j = LINE_BASE; j < MAX_LINES; ++j)
            {
                if (_CallSessions[j].getSessionState() == true)
                {
                    needIgnoreAutoAnswer = true;
                    break;
                }
            }


            Boolean AA = false;

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                if (_BanAutoRespuesta == true)
                {
                    AA = true;
                }
            }));

            if (needIgnoreAutoAnswer == false && AA == true)
            {
                _CallSessions[i].setRecvCallState(false);
                _CallSessions[i].setSessionState(true);

                textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
                {
                    if (_BanAutoRespuesta == true)
                    {
                        if (Popcontrol != null)
                        {
                            Popcontrol.SetVideoRemote(_CallSessions[i].getSessionId());
                        }
                        _core.answerCall(_CallSessions[i].getSessionId());
                    }
                }));



                Text = "Línea " + i.ToString();
                Text = Text + ": Llamada Contestada por Auto Respuesta";

                Player.Stop();
                textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
                {
                    textBlockCallStateInfo.Text = Text;
                }));

                return 0;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate
               {
                   if ((this.WindowState == System.Windows.WindowState.Minimized) || (this.IsActive == false))
                   {
                       if (this.WindowState == System.Windows.WindowState.Minimized)
                       {
                           this.WindowState = System.Windows.WindowState.Normal;
                       }
                       FlashWindow.Flash(this, 3);
                       this.Activate();
                   }
               }));

            Text = "Línea " + i.ToString();
            Text = Text + " Llamada entrante de ";
            Text = Text + callerDisplayName;


            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            //  You should write your own code to play the wav file here for alert the incoming call(incoming tone);

            return 0;

        }

        public Int32 onInviteTrying(Int32 callbackObject, Int32 sessionId, String caller, String callee)
        {
            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            string Text = "Llamada Saliente...";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));

            return 0;

        }

        public Int32 onInviteRinging(Int32 callbackObject,
                                            Int32 sessionId,
                                            Boolean hasEarlyMedia,
                                            Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            if (hasEarlyMedia == false)
            {
                Player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + @"\Themes\Sonidos\TelephoneRing.wav";
                Player.Play();
            }

            string Text = "Llamando...";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            return 0;
        }


        public Int32 onInviteAnswered(Int32 callbackObject,
                                             Int32 sessionId,
                                             Boolean hasVideo,
                                             Int32 statusCode,
                                             String statusText,
                                             String audioCodecName,
                                             String videoCodecName)
        {

            if (hasVideo == true)
            {
                // This incoming call has video SDP
            }
            else
            {
                // This incoming call hasn't the video SDP
            }


            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            _CallSessions[i].setSessionState(true);

            this.Dispatcher.Invoke(new Action(delegate
            {
                lblTimeElapsed.Reset();
                lblTimeElapsed.Visibility = Visibility.Visible;
                lblTimeElapsed.IsStarted = true;
            }));
            string Text = "Llamada Establecida";
            Player.Stop();
            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
                joinConference(i);
            }));
            //    
            DigitarExt();


            return 0;
        }


        public Int32 onInviteFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            _CallSessions[i].reset();


            if (statusText.Contains("Not Found"))
            { statusText = " Número no encontrado"; }
            if (statusText.Contains("Address Incomplete"))
            { statusText = " Número Incompleto"; }
            if (statusText.Contains("Busy Here"))
            { statusText = " Número Ocupado"; }
            string Text = "Fallo";
            Text = Text + statusText;

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));



            //  the error reason is statusText
            //  the error code is statusCode

            return 0;
        }


        public Int32 onInviteClosed(Int32 callbackObject, Int32 sessionId)
        {

            int i = 0;
            _BanLlamadaRespuesta = false;
            _BanVideoEnlinea = false;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            _CallSessions[i].reset();


            string Text = "Colgado";
            Player.Stop();
            this.Dispatcher.Invoke(new Action(delegate
            {
                if (Popcontrol != null)
                {
                    Popcontrol.Visibility = Visibility.Collapsed;
                }
                lblTimeElapsed.Reset();
                lblTimeElapsed.Visibility = Visibility.Collapsed;
                lblTimeElapsed.IsStarted = false;
            }));

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            return 0;
        }



        public Int32 onInviteUpdated(Int32 callbackObject,
                                            Int32 sessionId,
                                            Boolean hasVideo,
                                            String audioCodecName,
                                            String videoCodecName
                                            )
        {


            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            string Text = "Línea " + i.ToString();
            Text = Text + ": Llamada Actualizada";


            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));

            return 0;

        }


        public Int32 onInviteUASConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {

            return 0;
        }


        public Int32 onInviteUACConnected(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {

            return 0;
        }


        public Int32 onInviteBeginingForward(Int32 callbackObject, String forwardingTo)
        {

            string Text = "Llamada enviada a: ";
            Text = Text + forwardingTo;

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            return 0;
        }



        public Int32 onRemoteHold(Int32 callbackObject, Int32 sessionId)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            string Text = "Línea " + i.ToString();
            Text = Text + ": Espera Remota";


            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));

            return 0;
        }


        public Int32 onRemoteUnHold(Int32 callbackObject, Int32 sessionId)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            string Text = "Línea " + i.ToString();
            Text = Text + ": Recuperar Espera Remota";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));

            return 0;
        }


        public Int32 onTransferTrying(Int32 callbackObject, Int32 sessionId, String referTo)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            // for example, if A and B is established call, A transfer B to C, the transfer is trying,
            // B will got this transferTring event, and use referTo to know C ( C is "referTo" in this case)

            string Text = "Línea " + i.ToString();
            Text = Text + ": Tratando de Transferir";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            return 0;
        }

        public Int32 onTransferRinging(Int32 callbackObject, Int32 sessionId, Boolean hasVideo)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            string Text = "Línea " + i.ToString();
            Text = Text + ": Esperando Respuesta Transferencia";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            // Use hasVideo to check does this transfer call has video.
            // if hasVideo is true, then it's have video, if hasVideo is false, means has no video.


            return 0;
        }


        public Int32 onPASVTransferSuccess(Int32 callbackObject, Int32 sessionId, Boolean hasVideo)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }

            string Text = "Línea " + i.ToString();
            textBlockCallStateInfo.Text = Text + ": Transferencia Exitosa";

            //ListBoxSIPLog.Dispatcher.Invoke(new Action(delegate
            //{
            //    _core.setRemoteVideoWindow(sessionId, remoteVideoWindow.Child.Handle);
            //    ListBoxSIPLog.Items.Add(Text);
            //}));

            return 0;
        }

        public Int32 onPASVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            //  statusText is error reason
            //  statusCode is error code

            string Text = "Línea " + i.ToString();
            textBlockCallStateInfo.Text = Text + ": Fallo al Transferir";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));

            return 0;
        }



        public Int32 onACTVTransferSuccess(Int32 callbackObject, Int32 sessionId)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            string Text = "Línea " + i.ToString();
            Text = Text + ": Transferencia Exitosa, Colgado";
            this.Dispatcher.Invoke(new Action(delegate
            {
                if (Popcontrol != null)
                {
                    Popcontrol.Visibility = Visibility.Collapsed;
                }
                lblTimeElapsed.Reset();
                lblTimeElapsed.Visibility = Visibility.Collapsed;
                lblTimeElapsed.IsStarted = false;
            }));

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            // The ACTIVE Transfer success, then reset currently call.
            _CallSessions[i].reset();

            return 0;
        }

        public Int32 onACTVTransferFailure(Int32 callbackObject, Int32 sessionId, Int32 statusCode, String statusText)
        {
            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            string Text = "Línea " + i.ToString();
            Text = Text + ": Fallo al transferir";

            textBlockCallStateInfo.Dispatcher.Invoke(new Action(delegate
            {
                textBlockCallStateInfo.Text = Text;
            }));


            //  statusText is error reason
            //  statusCode is error code

            return 0;
        }




        public Int32 onRecvPagerMessage(Int32 callbackObject, String from, String fromDisplayName, StringBuilder message)
        {

            string Text = "Received SIP pager message from ";
            Text += from;
            Text += " :";
            Text += message.ToString();

            MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), Text, "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);

            return 0;
        }

        public Int32 onSendPagerMessageSuccess(Int32 callbackObject,
                                                      String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName
                                                     )
        {


            return 0;
        }



        public Int32 onSendPagerMessageFailure(Int32 callbackObject,
                                                       String caller,
                                                      String callerDisplayName,
                                                      String callee,
                                                      String calleeDisplayName,
                                                      Int32 statusCode,
                                                      String statusText
                                                     )
        {
            return 0;
        }






        public Int32 onArrivedSignaling(Int32 callbackObject, Int32 sessionId, StringBuilder signaling)
        {
            // This event will be fired when the SDK received a SIP message
            // you can use signaling to access the SIP message.

            return 0;
        }

        public Int32 onSentSignaling(Int32 callbackObject, StringBuilder signaling)
        {
            // This event will be fired when the SDK sent a SIP message
            // you can use signaling to access the SIP message.

            return 0;
        }


        public Int32 onWaitingVoiceMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {

            string Text = messageAccount;
            Text += " has voice message.";


            //ListBoxSIPLog.Dispatcher.Invoke(new Action(delegate
            //{
            //    ListBoxSIPLog.Items.Add(Text);
            //}));

            // You can use these parameters to check the voice message count

            //  urgentNewMessageCount;
            //  urgentOldMessageCount;
            //  newMessageCount;
            //  oldMessageCount;

            return 0;
        }


        public Int32 onWaitingFaxMessage(Int32 callbackObject,
                                                  String messageAccount,
                                                  Int32 urgentNewMessageCount,
                                                  Int32 urgentOldMessageCount,
                                                  Int32 newMessageCount,
                                                  Int32 oldMessageCount)
        {
            string Text = messageAccount;
            Text += " has FAX message.";


            //ListBoxSIPLog.Dispatcher.Invoke(new Action(delegate
            //{
            //    ListBoxSIPLog.Items.Add(Text);
            //}));



            // You can use these parameters to check the FAX message count

            //  urgentNewMessageCount;
            //  urgentOldMessageCount;
            //  newMessageCount;
            //  oldMessageCount;



            return 0;
        }


        public Int32 onRecvDtmfTone(Int32 callbackObject, Int32 sessionId, Int32 tone)
        {

            int i = 0;
            bool state = false;

            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            string DTMFTone = tone.ToString();
            if (DTMFTone == "10")
            {
                DTMFTone = "*";
            }
            else if (DTMFTone == "11")
            {
                DTMFTone = "#";
            }

            string Text = "Received DTMF Tone: ";
            Text += DTMFTone;
            Text += " on line ";
            Text += i.ToString();

            //ListBoxSIPLog.Dispatcher.Invoke(new Action(delegate
            //{
            //    ListBoxSIPLog.Items.Add(Text);
            //}));


            return 0;
        }


        public Int32 onPresenceRecvSubscribe(Int32 callbackObject,
                                                    Int32 subscribeId,
                                                    String from,
                                                    String fromDisplayName,
                                                    String subject)
        {


            return 0;
        }


        public Int32 onPresenceOnline(Int32 callbackObject, String from, String fromDisplayName, String stateText)
        {

            return 0;
        }

        public Int32 onPresenceOffline(Int32 callbackObject, String from, String fromDisplayName)
        {


            return 0;
        }

        public Int32 onRecvOptions(Int32 callbackObject, StringBuilder optionsMessage)
        {
            /*
                        string text = "Received an OPTIONS message: ";
                        text += optionsMessage.ToString();
                        MessageBox.Show(text);
            */


            return 0;
        }

        public Int32 onRecvInfo(Int32 callbackObject, Int32 sessionId, StringBuilder infoMessage)
        {
            /*          
                      int i = 0;
                      bool state = false;
                      for (i = LINE_BASE; i < MAX_LINES; ++i)
                      {
                          if (_CallSessions[i].getSessionId() == sessionId)
                          {
                              state = true;
                              break;
                          }
                      }

                      if (state == false)
                      {
                          return 0;
                      }


                      string text = "Received a INFO message on line ";
                      text += i.ToString();
                      text += ": ";
                      text += infoMessage.ToString();

                      MessageBox.Show(text);
              */

            return 0;
        }


        public Int32 onRecvMessage(Int32 callbackObject, Int32 sessionId, StringBuilder message)
        {
            /*          
                      int i = 0;
                      bool state = false;
                      for (i = LINE_BASE; i < MAX_LINES; ++i)
                      {
                          if (_CallSessions[i].getSessionId() == sessionId)
                          {
                              state = true;
                              break;
                          }
                      }

                      if (state == false)
                      {
                          return 0;
                      }


                      string text = "Received a MESSAGE message on line ";
                      text += i.ToString();
                      text += ": ";
                      text += message.ToString();

                      MessageBox.Show(text);
              */

            return 0;
        }



        public Int32 onRecvBinaryMessage(Int32 callbackObject,
                                        Int32 sessionId,
                                        StringBuilder message,
                                        byte[] messageBody,
                                        Int32 length)
        {

            int i = 0;
            bool state = false;
            for (i = LINE_BASE; i < MAX_LINES; ++i)
            {
                if (_CallSessions[i].getSessionId() == sessionId)
                {
                    state = true;
                    break;
                }
            }

            if (state == false)
            {
                return 0;
            }


            string text = "Received a binary MESSAGE message on line ";
            text += i.ToString();


            MessageBox.Show(text, "Received a binary MESSAGE message");

            return 0;
        }


        public Int32 onRecvBinaryPagerMessage(Int32 callbackObject,
                                              StringBuilder from,
                                              StringBuilder fromDisplayName,
                                              byte[] messageBody,
                                              Int32 length)
        {
            string text = "Received a binary pager message(out of dialog) from ";
            text += from;

            MessageBox.Show(text, "Received a binary pager MESSAGE message");

            return 0;
        }



        public Int32 onReceivedRtpPacket(IntPtr callbackObject,
                                  Int32 sessionId,
                                  Boolean isAudio,
                                  byte[] RTPPacket,
                                  Int32 packetSize)
        {
            /*
    !!! IMPORTANT !!!

    Don’t call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
    other code which will spend long time, you should post a message to main thread(main window) or other thread,
    let the thread to call SDK API functions or other code.

*/

            return 0;
        }

        public Int32 onSendingRtpPacket(IntPtr callbackObject,
                                  Int32 sessionId,
                                  Boolean isAudio,
                                  byte[] RTPPacket,
                                  Int32 packetSize)
        {

            /*
    !!! IMPORTANT !!!

    Don’t call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
    other code which will spend long time, you should post a message to main thread(main window) or other thread,
    let the thread to call SDK API functions or other code.
            
*/

            return 0;
        }



        public Int32 onAudioRawCallback(IntPtr callbackObject,
                                           Int32 sessionId,
                                           Int32 callbackType,
                                           [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] data,
                                           Int32 dataLength,
                                           Int32 samplingFreqHz)
        {


            /*
                !!! IMPORTANT !!!

                Don’t call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

            */

            // The data parameter is audio stream as PCM format, 16bit, Mono.
            // the dataLength parameter is audio steam data length.




            //
            // IMPORTANT: the data length is stored in dataLength parameter!!!
            //

            AUDIOSTREAM_CALLBACK_MODE type = (AUDIOSTREAM_CALLBACK_MODE)callbackType;

            if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_LOCAL_MIX)
            {
                // The callback data is mixed from local record device - microphone
                // The sessionId is CALLBACK_SESSION_ID.PORTSIP_LOCAL_MIX_ID

            }
            else if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_REMOTE_MIX)
            {
                // The callback data is mixed from local record device - microphone
                // The sessionId is CALLBACK_SESSION_ID.PORTSIP_REMOTE_MIX_ID
            }
            else if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_LOCAL_PER_CHANNEL)
            {
                // The callback data is from local record device of each session, use the sessionId to identifying the session.
            }
            else if (type == AUDIOSTREAM_CALLBACK_MODE.AUDIOSTREAM_REMOTE_PER_CHANNEL)
            {
                // The callback data is received from remote side of each session, use the sessionId to identifying the session.
            }




            return 0;
        }

        public Int32 onVideoRawCallback(IntPtr callbackObject,
                                                Int32 sessionId,
                                                Int32 callbackType,
                                                Int32 width,
                                                Int32 height,
                                                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] byte[] data,
                                                Int32 dataLength)
        {
            /*
                !!! IMPORTANT !!!

                Don’t call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

                The video data format is YUV420.
            */



            //
            // IMPORTANT: the data length is stored in dataLength parameter!!!
            //



            VIDEOSTREAM_CALLBACK_MODE type = (VIDEOSTREAM_CALLBACK_MODE)callbackType;

            if (type == VIDEOSTREAM_CALLBACK_MODE.VIDEOSTREAM_LOCAL)
            {

            }

            else if (type == VIDEOSTREAM_CALLBACK_MODE.VIDEOSTREAM_REMOTE)
            {

            }


            return 0;

        }


        public Int32 onPlayAviFileFinished(IntPtr callbackObject, Int32 sessionId)
        {
            // when the file is play finished, this callback event will be fired.

            /*
                !!! IMPORTANT !!!

                Don’t call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

            */


            return 0;
        }


        public Int32 onPlayWaveFileFinished(IntPtr callbackObject, Int32 sessionId, String fileName)
        {
            // when the file is play finished, this callback event will be fired.

            /*
                !!! IMPORTANT !!!

                Don’t call any PortSIP SDK API functions in here directly. If you want to call the PortSIP API functions or 
                other code which will spend long time, you should post a message to main thread(main window) or other thread,
                let the thread to call SDK API functions or other code.

            */


            return 0;
        }
        #endregion


        private void _this_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!ucPrincipalContact.ctcContactos.IsFocused && !ucPrincipalContact.txtContact.IsFocused)
            {
                switch (e.Key)
                {

                    case Key.NumPad0:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "0";
                        break;
                    case Key.NumPad1:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "1";
                        break;
                    case Key.NumPad2:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "2";
                        break;
                    case Key.NumPad3:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "3";
                        break;
                    case Key.NumPad4:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "4";
                        break;
                    case Key.NumPad5:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "5";
                        break;
                    case Key.NumPad6:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "6";
                        break;
                    case Key.NumPad7:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "7";
                        break;
                    case Key.NumPad8:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "8";
                        break;
                    case Key.NumPad9:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "9";
                        break;
                    case Key.Multiply:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "*";
                        break;
                    case Key.D3:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "3";
                        break;
                    case Key.D0:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "0";
                        break;
                    case Key.D1:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "1";
                        break;
                    case Key.D2:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "2";
                        break;
                    case Key.D4:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "4";
                        break;
                    case Key.D5:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "5";
                        break;
                    case Key.D6:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "6";
                        break;
                    case Key.D7:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "7";
                        break;
                    case Key.D8:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "8";
                        break;
                    case Key.D9:
                        textBlockDialingNumber.Text = textBlockDialingNumber.Text + "9";
                        break;
                    default:
                        break;
                }
                if (e.Key == Key.D3 && e.IsToggled == true)
                {
                    textBlockDialingNumber.Text = textBlockDialingNumber.Text + "#";
                }
                //switch (e.Key)
                //{
                //    case (Key.Back | Key.LeftCtrl):
                //        e.SuppressKeyPress = true;
                //        TextBox textbox = (TextBox)sender;
                //        int i;
                //        if (textbox.SelectionStart.Equals(0))
                //        {
                //            return;
                //        }
                //        int space = textbox.Text.LastIndexOf(' ', textbox.SelectionStart - 1);
                //        int line = textbox.Text.LastIndexOf("\r\n", textbox.SelectionStart - 1);
                //        if (space > line)
                //        {
                //            i = space;
                //        }
                //        else
                //        {
                //            i = line;
                //        }
                //        if (i > -1)
                //        {
                //            while (textbox.Text.Substring(i - 1, 1).Equals(' '))
                //            {
                //                if (i.Equals(0))
                //                {
                //                    break;
                //                }
                //                i--;
                //            }
                //            textbox.Text = textbox.Text.Substring(0, i) + textbox.Text.Substring(textbox.SelectionStart);
                //            textbox.SelectionStart = i;
                //        }
                //        else if (i.Equals(-1))
                //        {
                //            textbox.Text = textbox.Text.Substring(textbox.SelectionStart);
                //        }
                //        break;
                //}
                if (e.Key == Key.Enter)
                {
                    Llamar();
                }
            }
        }

        private void BtnLlamarAsignarFav_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;           
            switch (btn.Name)
            {
                case "BtnAsignarFav1":
                    if (!string.IsNullOrEmpty(txtFav1.Text))
                    {
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav1.Tag).Telefono))
                        {
                            textBlockDialingNumber.Text = KeyExternalCall + ((DtoContactos)txtFav1.Tag).Telefono;
                        }
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav1.Tag).Extension))
                        {
                            ExtMarcar = ((DtoContactos)txtFav1.Tag).Extension;
                            if (sender is PrincipalContactos)
                            {
                                if (((PrincipalContactos)sender).LlamarNum != null)
                                {
                                    if (string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                                    {
                                        textBlockDialingNumber.Text = ExtMarcar;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ExtMarcar = string.Empty;
                        }
                    }
                    else { MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No hay un favorito Asignado a este espacio", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true); return; }
                    break;
                case "BtnAsignarFav2":
                    if (!string.IsNullOrEmpty(txtFav2.Text))
                    {
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav2.Tag).Telefono))
                        {
                            textBlockDialingNumber.Text = KeyExternalCall + ((DtoContactos)txtFav2.Tag).Telefono;
                        }
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav2.Tag).Extension))
                        {
                            ExtMarcar = ((DtoContactos)txtFav2.Tag).Extension;
                            if (sender is PrincipalContactos)
                            {
                                if (((PrincipalContactos)sender).LlamarNum != null)
                                {
                                    if (string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                                    {
                                        textBlockDialingNumber.Text = ExtMarcar;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ExtMarcar = string.Empty;
                        }
                    }
                    else { MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No hay un favorito Asignado a este espacio", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true); return; }
                    break;
                case "BtnAsignarFav3":
                    if (!string.IsNullOrEmpty(txtFav3.Text))
                    {
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav3.Tag).Telefono))
                        {
                            textBlockDialingNumber.Text = KeyExternalCall + ((DtoContactos)txtFav3.Tag).Telefono;
                        }
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav5.Tag).Extension))
                        {
                            ExtMarcar = ((DtoContactos)txtFav3.Tag).Extension;
                            if (sender is PrincipalContactos)
                            {
                                if (((PrincipalContactos)sender).LlamarNum != null)
                                {
                                    if (string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                                    {
                                        textBlockDialingNumber.Text = ExtMarcar;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ExtMarcar = string.Empty;
                        }
                    }
                    else { MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No hay un favorito Asignado a este espacio", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true); return; }
                    break;
                case "BtnAsignarFav4":
                    if (!string.IsNullOrEmpty(txtFav4.Text))
                    {
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav4.Tag).Telefono))
                        {
                            textBlockDialingNumber.Text = KeyExternalCall + ((DtoContactos)txtFav4.Tag).Telefono;
                        }
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav4.Tag).Extension))
                        {
                            ExtMarcar = ((DtoContactos)txtFav4.Tag).Extension;
                            if (sender is PrincipalContactos)
                            {
                                if (((PrincipalContactos)sender).LlamarNum != null)
                                {
                                    if (string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                                    {
                                        textBlockDialingNumber.Text = ExtMarcar;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ExtMarcar = string.Empty;
                        }
                    }
                    else { MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No hay un favorito Asignado a este espacio", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true); return; }
                    break;
                case "BtnAsignarFav5":
                    if (!string.IsNullOrEmpty(txtFav5.Text))
                    {
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav5.Tag).Telefono))
                        {
                            textBlockDialingNumber.Text = KeyExternalCall + ((DtoContactos)txtFav5.Tag).Telefono;
                        }
                        if (!string.IsNullOrEmpty(((DtoContactos)txtFav5.Tag).Extension))
                        {
                            ExtMarcar = ((DtoContactos)txtFav5.Tag).Extension;
                            if (sender is PrincipalContactos)
                            {
                                if (((PrincipalContactos)sender).LlamarNum != null)
                                {
                                    if (string.IsNullOrEmpty(((PrincipalContactos)sender).LlamarNum.Telefono))
                                    {
                                        textBlockDialingNumber.Text = ExtMarcar;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ExtMarcar = string.Empty;
                        }
                    }
                    else { MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No hay un favorito Asignado a este espacio", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true); return; }
                    break;
                default:
                    break;
            }
            Llamar();
        }

        private void BtnAsignarFav1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn = sender as Button;
            oPopup = new System.Windows.Controls.ContextMenu();
            oPopup.Style = (Style)System.Windows.Application.Current.FindResource("FilterContexMenu");
            CmbFavorities = new ComboBox();
            CmbFavorities.SelectionChanged += CmbFavorities_SelectionChanged;
            TraerContactosFavoritos();
            CmbFavorities.DisplayMemberPath = "NombreCompleto";
            CmbFavorities.SelectedValuePath = "Ide_User";
            CmbFavorities.Height = 20;
            CmbFavorities.Width = 150;

            switch (btn.Name)
            {
                case "BtnAsignarFav1":
                    oPopup.PlacementTarget = BtnAsignarFav1;
                    break;
                case "BtnAsignarFav2":
                    oPopup.PlacementTarget = BtnAsignarFav2;
                    break;
                case "BtnAsignarFav3":
                    oPopup.PlacementTarget = BtnAsignarFav3;
                    break;
                case "BtnAsignarFav4":
                    oPopup.PlacementTarget = BtnAsignarFav4;
                    break;
                case "BtnAsignarFav5":
                    oPopup.PlacementTarget = BtnAsignarFav5;
                    break;
                default:
                    break;
            }
            oPopup.Items.Add(CmbFavorities);

            oPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            oPopup.IsOpen = true;
            //   oPopup.Style = (Style)System.Windows.Application.Current.FindResource("FilterContexMenu");

        }

        void CmbFavorities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (btn.Name)
            {
                case "BtnAsignarFav1":
                    if (CmbFavorities.SelectedIndex == 0)
                    {
                        txtFav1.Text = string.Empty;
                    }
                    else
                    {
                        txtFav1.Text = ((DtoContactos)CmbFavorities.SelectedItem).NombreCompleto;
                        txtFav1.Tag = (DtoContactos)CmbFavorities.SelectedItem;
                    }
                    break;
                case "BtnAsignarFav2":
                    if (CmbFavorities.SelectedIndex == 0)
                    {
                        txtFav2.Text = string.Empty;
                    }
                    else
                    {
                        txtFav2.Text = ((DtoContactos)CmbFavorities.SelectedItem).NombreCompleto;
                        txtFav2.Tag = (DtoContactos)CmbFavorities.SelectedItem;
                        //   ListContactFavMemoria.Insert(2, ((DtoContactos)CmbFavorities.SelectedItem));
                    }
                    break;
                case "BtnAsignarFav3":
                    if (CmbFavorities.SelectedIndex == 0)
                    {
                        txtFav3.Text = string.Empty;
                    }
                    else
                    {
                        txtFav3.Text = ((DtoContactos)CmbFavorities.SelectedItem).NombreCompleto;
                        txtFav3.Tag = (DtoContactos)CmbFavorities.SelectedItem;
                    }
                    break;
                case "BtnAsignarFav4":
                    if (CmbFavorities.SelectedIndex == 0)
                    {
                        txtFav4.Text = string.Empty;
                    }
                    else
                    {
                        txtFav4.Text = ((DtoContactos)CmbFavorities.SelectedItem).NombreCompleto;
                        txtFav4.Tag = (DtoContactos)CmbFavorities.SelectedItem;
                    }
                    break;
                case "BtnAsignarFav5":
                    if (CmbFavorities.SelectedIndex == 0)
                    {
                        txtFav5.Text = string.Empty;
                    }
                    else
                    {
                        txtFav5.Text = ((DtoContactos)CmbFavorities.SelectedItem).NombreCompleto;
                        txtFav5.Tag = (DtoContactos)CmbFavorities.SelectedItem;
                    }

                    break;
                default:
                    break;
            }
            GuardarDatosContactoAsignado();
            oPopup.IsOpen = false;
        }

        private void GuardarDatosContactoAsignado()
        {
            if (!string.IsNullOrEmpty(txtFav1.Text))
            {
                Ini.IniWriteValue("Info Contacto 1", "NombreCompleto", ((DtoContactos)txtFav1.Tag).NombreCompleto);
                Ini.IniWriteValue("Info Contacto 1", "Telefono", ((DtoContactos)txtFav1.Tag).Telefono);
                Ini.IniWriteValue("Info Contacto 1", "Extension", ((DtoContactos)txtFav1.Tag).Extension);
                Ini.IniWriteValue("Info Contacto 1", "PhoneType", Convert.ToString(((DtoContactos)txtFav1.Tag).PhoneType));
            }
            else
            {
                Ini.IniWriteValue("Info Contacto 1", "NombreCompleto", string.Empty);
                Ini.IniWriteValue("Info Contacto 1", "Telefono", string.Empty);
                Ini.IniWriteValue("Info Contacto 1", "Extension", string.Empty);
                Ini.IniWriteValue("Info Contacto 1", "PhoneType", string.Empty);
            }
            if (!string.IsNullOrEmpty(txtFav2.Text))
            {
                Ini.IniWriteValue("Info Contacto 2", "NombreCompleto", ((DtoContactos)txtFav2.Tag).NombreCompleto);
                Ini.IniWriteValue("Info Contacto 2", "Telefono", ((DtoContactos)txtFav2.Tag).Telefono);
                Ini.IniWriteValue("Info Contacto 2", "Extension", ((DtoContactos)txtFav2.Tag).Extension);
                Ini.IniWriteValue("Info Contacto 2", "PhoneType", Convert.ToString(((DtoContactos)txtFav2.Tag).PhoneType));
            }
            else
            {
                Ini.IniWriteValue("Info Contacto 2", "NombreCompleto", string.Empty);
                Ini.IniWriteValue("Info Contacto 2", "Telefono", string.Empty);
                Ini.IniWriteValue("Info Contacto 2", "Extension", string.Empty);
                Ini.IniWriteValue("Info Contacto 2", "PhoneType", string.Empty);
            }
            if (!string.IsNullOrEmpty(txtFav3.Text))
            {
                Ini.IniWriteValue("Info Contacto 3", "NombreCompleto", ((DtoContactos)txtFav3.Tag).NombreCompleto);
                Ini.IniWriteValue("Info Contacto 3", "Telefono", ((DtoContactos)txtFav3.Tag).Telefono);
                Ini.IniWriteValue("Info Contacto 3", "Extension", ((DtoContactos)txtFav3.Tag).Extension);
                Ini.IniWriteValue("Info Contacto 3", "PhoneType", Convert.ToString(((DtoContactos)txtFav3.Tag).PhoneType));
            }
            else
            {
                Ini.IniWriteValue("Info Contacto 3", "NombreCompleto", string.Empty);
                Ini.IniWriteValue("Info Contacto 3", "Telefono", string.Empty);
                Ini.IniWriteValue("Info Contacto 3", "Extension", string.Empty);
                Ini.IniWriteValue("Info Contacto 3", "PhoneType", string.Empty);
            }
            if (!string.IsNullOrEmpty(txtFav4.Text))
            {
                Ini.IniWriteValue("Info Contacto 4", "NombreCompleto", ((DtoContactos)txtFav4.Tag).NombreCompleto);
                Ini.IniWriteValue("Info Contacto 4", "Telefono", ((DtoContactos)txtFav4.Tag).Telefono);
                Ini.IniWriteValue("Info Contacto 4", "Extension", ((DtoContactos)txtFav4.Tag).Extension);
                Ini.IniWriteValue("Info Contacto 4", "PhoneType", Convert.ToString(((DtoContactos)txtFav4.Tag).PhoneType));
            }
            else
            {
                Ini.IniWriteValue("Info Contacto 4", "NombreCompleto", string.Empty);
                Ini.IniWriteValue("Info Contacto 4", "Telefono", string.Empty);
                Ini.IniWriteValue("Info Contacto 4", "Extension", string.Empty);
                Ini.IniWriteValue("Info Contacto 4", "PhoneType", string.Empty);
            }
            if (!string.IsNullOrEmpty(txtFav5.Text))
            {
                Ini.IniWriteValue("Info Contacto 5", "NombreCompleto", ((DtoContactos)txtFav5.Tag).NombreCompleto);
                Ini.IniWriteValue("Info Contacto 5", "Telefono", ((DtoContactos)txtFav5.Tag).Telefono);
                Ini.IniWriteValue("Info Contacto 5", "Extension", ((DtoContactos)txtFav5.Tag).Extension);
                Ini.IniWriteValue("Info Contacto 5", "PhoneType", Convert.ToString(((DtoContactos)txtFav5.Tag).PhoneType));
            }
            else
            {
                Ini.IniWriteValue("Info Contacto 5", "NombreCompleto", string.Empty);
                Ini.IniWriteValue("Info Contacto 5", "Telefono", string.Empty);
                Ini.IniWriteValue("Info Contacto 5", "Extension", string.Empty);
                Ini.IniWriteValue("Info Contacto 5", "PhoneType", string.Empty);
            }
        }

        private void TraerDatosContactoAsignado()
        {
            DtoContactos dto = new DtoContactos();
            dto.NombreCompleto = Ini.IniReadValue("Info Contacto 1", "NombreCompleto");
            dto.Telefono = Ini.IniReadValue("Info Contacto 1", "Telefono");
            dto.Extension = Ini.IniReadValue("Info Contacto 1", "Extension");
            // dto.PhoneType = Ini.IniReadValue("Info Contacto 1", "PhoneType");
            if (!string.IsNullOrEmpty(dto.NombreCompleto))
            {
                txtFav1.Tag = dto;
                txtFav1.Text = dto.NombreCompleto;
            }
            dto = new DtoContactos();
            dto.NombreCompleto = Ini.IniReadValue("Info Contacto 2", "NombreCompleto");
            dto.Telefono = Ini.IniReadValue("Info Contacto 2", "Telefono");
            dto.Extension = Ini.IniReadValue("Info Contacto 2", "Extension");
            //    Ini.IniReadValue("Info Contacto 2", "PhoneType");
            if (!string.IsNullOrEmpty(dto.NombreCompleto))
            {
                txtFav2.Tag = dto;
                txtFav2.Text = dto.NombreCompleto;
            }
            dto = new DtoContactos();
            dto.NombreCompleto = Ini.IniReadValue("Info Contacto 3", "NombreCompleto");
            dto.Telefono = Ini.IniReadValue("Info Contacto 3", "Telefono");
            dto.Extension = Ini.IniReadValue("Info Contacto 3", "Extension");
            //Ini.IniReadValue("Info Contacto 3", "PhoneType");
            if (!string.IsNullOrEmpty(dto.NombreCompleto))
            {
                txtFav3.Tag = dto;
                txtFav3.Text = dto.NombreCompleto;
            }
            dto = new DtoContactos();
            dto.NombreCompleto = Ini.IniReadValue("Info Contacto 4", "NombreCompleto");
            dto.Telefono = Ini.IniReadValue("Info Contacto 4", "Telefono");
            dto.Extension = Ini.IniReadValue("Info Contacto 4", "Extension");
            //Ini.IniReadValue("Info Contacto 4", "PhoneType");
            if (!string.IsNullOrEmpty(dto.NombreCompleto))
            {
                txtFav4.Tag = dto;
                txtFav4.Text = dto.NombreCompleto;
            }
            dto = new DtoContactos();
            dto.NombreCompleto = Ini.IniReadValue("Info Contacto 5", "NombreCompleto");
            dto.Telefono = Ini.IniReadValue("Info Contacto 5", "Telefono");
            dto.Extension = Ini.IniReadValue("Info Contacto 5", "Extension");
            //Ini.IniReadValue("Info Contacto 5", "PhoneType"); 
            if (!string.IsNullOrEmpty(dto.NombreCompleto))
            {
                txtFav5.Tag = dto;
                txtFav5.Text = dto.NombreCompleto;
            }
        }

        private void ButtonRedial_Click(object sender, RoutedEventArgs e)
        {
            //CambioColorContacto(true);
            if (!string.IsNullOrEmpty(_NumRedial))
            {
                textBlockDialingNumber.Text = _NumRedial;
                Llamar();
            }
            else
            {
                MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "No hay un número anterior marcado.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
            }
        }

        private void _this_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private Storyboard _sb;
        public Storyboard sb
        {
            get { if (_sb != null) return _sb; else return _sb = new Storyboard(); }
            set { _sb = value; }
        }

        public void CambioColorContacto(bool Animacion)
        {
            if (Animacion)
            {
                ColorAnimation Animation = new ColorAnimation();
                Animation.From = (Colors.White);
                Animation.To = (Color)ColorConverter.ConvertFromString("#FF0000");
                Animation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                Animation.RepeatBehavior = RepeatBehavior.Forever;
                Animation.AutoReverse = true;
                BtnMensaje.Background = Brushes.Red;
                sb.Children.Add(Animation);
                Storyboard.SetTarget(Animation, BtnMensaje);
                Storyboard.SetTargetProperty(Animation, new PropertyPath("Background.Color"));
                Storyboard.SetTargetProperty(Animation, new PropertyPath("Background.Color"));
                imgMessage.Source = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/emailRed.png"));
                //TagPrueba.ToolTip = "Seleccione Un Contacto";
                Animacion = true;

                sb.Begin();
            }
            else
            {
                sb.Pause();
                LinearGradientBrush b = new LinearGradientBrush();
                ColorConverter cc = new ColorConverter();
                b.GradientStops.Add(new GradientStop(Colors.White, 0));
                b.GradientStops.Add(new GradientStop(Colors.CadetBlue, 1));
                b.GradientStops.Add(new GradientStop(Colors.AliceBlue, 0.3));
                imgMessage.Source = new BitmapImage(new Uri("pack://application:,,,/Axede.WPF.Softphone.Applications;component/Themes/Images/email.png"));
                BtnMensaje.Background = b;
                BtnMensaje.ToolTip = String.Empty;
                Animacion = false;
            }
        }

        private void MinimizarMenuItem_Click(object sender, RoutedEventArgs e)
        {
             this.WindowState = WindowState.Minimized;  
        }

        private void _this_Closed(object sender, EventArgs e)
        {
            BanHilo = false;
            rddActions.IsOpen = false;

            if (_SIPInited == false)
            {
                App.Current.Shutdown();
                return;
            }
            Environment.Exit(Environment.ExitCode);
            deRegisterFromServer();

            App.Current.Shutdown();
        }

    }
}