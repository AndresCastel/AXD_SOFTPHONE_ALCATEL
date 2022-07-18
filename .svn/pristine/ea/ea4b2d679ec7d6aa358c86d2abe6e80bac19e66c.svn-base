using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.IO;
using Axede.Utilidades.Enums;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections;
using System.Net;
using System.Management;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Win32;


namespace Axede.Utilidades
{
    public class General
    {


        public static string sUserName { get; set; }

        public static IList oObjetosPerfilUsuario { get; set; }


        /// <summary>
        /// Convierte un la Lista em un ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(List<T> coll)
        {
            var c = new ObservableCollection<T>();
            foreach (var e in coll) c.Add(e);
            return c;
        }

        /// <summary>
        /// Convierte un la Lista em un ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(List<T> coll, ObservableCollection<T> c)
        {
            c.Clear();
            foreach (var e in coll) c.Add(e);
            return c;
        }


       
        /// <summary>
        /// Retorna el valor correspondiente de una variable existente en los recursos
        /// </summary>
        /// <param name="oTipo"></param>
        /// <param name="sResourceName"></param>
        /// <returns></returns>
        public static string GetResourceManagerMultilingual(Type oTipo,string sResourceName)
        {
            string sCulture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            CultureInfo _CultureInfo = new CultureInfo(sCulture);
            ResourceManager resources = new ResourceManager(oTipo.Namespace + "." + oTipo.Name.Replace("`1",string.Empty), oTipo.Assembly);
            string sValorRecurso = resources.GetString(sResourceName, _CultureInfo);

            return sValorRecurso;
        }

        public static string MayusculaPrimeraLetra(string sNombre)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sNombre);
        }

        /// <summary>
        /// Verifica la existencia de un archivo en una ruta específica
        /// </summary>
        /// <param name="sRuta"></param>
        /// <param name="sNombreArchivo"></param>
        /// <returns></returns>
        public static bool VerificaExistenciaArchivo(string sRuta,string sNombreArchivo) 
        {
            bool bArchivoEncontrado = false;
            string[] Files;
            Files = System.IO.Directory.GetFiles(sRuta);
            System.IO.FileInfo fileInfo = null;
            foreach (string s in Files)
            {
                fileInfo = new System.IO.FileInfo(s);
                if (fileInfo.Name.Trim().ToUpper() == sNombreArchivo.Trim().ToUpper())
                {
                    bArchivoEncontrado = true;
                    break;
                }
            }

            return bArchivoEncontrado;
        }


        /// <summary>
        /// Verifica si el archivo está en uso
        /// </summary>
        /// <param name="sRuta"></param>
        /// <param name="sNombreArchivo"></param>
        /// <returns></returns>
        public static bool VerificaArchivoEnUso(string sRuta, string sNombreArchivo)
        {
            try
            {
                using (var stream = new FileStream(sRuta + "\\" + sNombreArchivo, FileMode.Open, FileAccess.Read)) { }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }


        [DllImport("dwmapi.dll")]
        internal static extern void DwmIsCompositionEnabled(ref bool pfEnabled);

        public static bool ModoAero() 
        {
            bool aeroEnabled = false;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                try
                {
                    DwmIsCompositionEnabled(ref aeroEnabled);
                }
                catch (System.Exception)
                {

                }
            }

            return aeroEnabled;
        }

        /// <summary>
        /// Retorna el numero de lineas de un archivo
        /// </summary>
        /// <param name="sRuta"></param>
        /// <param name="sNombreArchivo"></param>
        /// <returns></returns>
        public static int NumeroLineasArchivo(string sRuta, string sNombreArchivo) 
        {
            int iNumeroLineas = 0;

            iNumeroLineas = File.ReadLines(sRuta + "\\" + sNombreArchivo).Count();

            return iNumeroLineas;
        }


        /// <summary>
        /// Valida que la hora inicio sea menor que la hora fin
        /// </summary>
        /// <param name="dFechaInicio"></param>
        /// <param name="dFechaFin"></param>
        /// <returns></returns>
        public static bool ValidaHoraInicioFin(DateTime dFechaInicio, DateTime dFechaFin)
        {

            bool bRetorno = true;

            if (dFechaInicio.TimeOfDay.TotalSeconds >= dFechaFin.TimeOfDay.TotalSeconds)
            {
                bRetorno = false;
            }

            return bRetorno;
        }

        /// <summary>
        /// Indica si la fecha actual del sistema es mayor que la fecha enviada
        /// </summary>
        /// <param name="oFecha"></param>
        /// <returns></returns>
        public static bool ValidaFechaActualMayor(DateTime oFecha) 
        {
            bool bFechaActualMayor = true;

            DateTime dtFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dtFechaBase = new DateTime(oFecha.Year, oFecha.Month, oFecha.Day);

            if (DateTime.Compare(dtFechaActual, dtFechaBase) < 0)
            {
                return false; // la fecha base es mayor que la fecha actual
            }
            else if (DateTime.Compare(dtFechaActual, dtFechaBase) == 0)
            {
                return false; // las fechas son iguales
            }
            else
            {
                return true; // la fecha actual es mayor que la base
            }

            return bFechaActualMayor;
        }

        /// <summary>
        /// Valida que la Fecha Inicial sea Menor o Igual a la fecha final
        /// </summary>
        /// <param name="oFechaInicio"></param>
        /// <param name="oFechaFin"></param>
        /// <returns></returns>
        public static bool ValidaFechaInicialMenorIgualFinal(DateTime oFechaInicio, DateTime oFechaFin)
        {

            DateTime dtFechaInicial = new DateTime(oFechaInicio.Year, oFechaInicio.Month, oFechaInicio.Day, oFechaInicio.Hour, oFechaInicio.Minute, oFechaInicio.Second);
            DateTime dtFechaFin = new DateTime(oFechaFin.Year, oFechaFin.Month, oFechaFin.Day, oFechaFin.Hour, oFechaFin.Minute, oFechaFin.Second);

            if (DateTime.Compare(dtFechaInicial, dtFechaFin) < 0)
            {
                return true; // la fecha fin es mayor que la fecha actual
            }
            else if (DateTime.Compare(dtFechaInicial, dtFechaFin) == 0)
            {
                return true; // las fechas son iguales
            }

            return false; // la fecha inicial es mayor que la final;
        }

        /// <summary>
        /// Retorna del nombre del día de una fecha específica
        /// </summary>
        /// <param name="oFecha"></param>
        /// <returns></returns>
        public static string ObtenerNombreDiaxFecha(DateTime oFecha)
        {
            string sNombreDia = string.Empty;
            CultureInfo oCultura = CultureInfo.GetCultureInfo("es-ES");

            int day = (int)oFecha.DayOfWeek;

            sNombreDia = oCultura.DateTimeFormat.DayNames[day].Trim().ToUpper();

            return sNombreDia;
        }

        public static void RegistraVBScriptMensajes(Page pagina)
        {
            String strScript;
            strScript = "<SCRIPT language=\"vbscript\"> \n  Function vbMsg(sMensaje,iBotones,sTitulo) \n dim retval \n sMensaje= Replace(sMensaje, \"vbCrLf\", Chr(13)) \n  retval=msgbox(sMensaje,iBotones,sTitulo) \n  End Function \n</SCRIPT>";
            pagina.ClientScript.RegisterStartupScript(pagina.GetType(), "VbScriptMsg", strScript);
        }

        public static Window ResolveOwnerWindow()
        {
            Window owner = null;
            if (System.Windows.Application.Current != null)
            {
                foreach (Window w in System.Windows.Application.Current.Windows)
                {
                    if (w.IsActive)
                    {
                        owner = w;
                        break;
                    }
                }
            }
            return owner;
        }

        public static string CadenaLista<T>(List<T> lstLista, string fieldName, string sSeparador)
        {
            string sCadena = "";
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

            if (fieldName != null)
            {
                if (lstLista.Count > 0)
                {
                    string[] Datos = new string[lstLista.Count];
                    int n = 0;
                    foreach (T item in lstLista)
                    {
                        int i = 0;
                        string[] sCampos = fieldName.Split(delimiterChars);
                        string[] Datos1 = new string[sCampos.Length];
                        foreach (string sfieldName in sCampos)
                        {
                            PropertyInfo pi = item.GetType().GetProperty(sfieldName);
                            Datos1[i++] = pi.GetValue(item, null).ToString();
                        }
                        Datos[n++] = string.Join("-", Datos1);
                    }
                    sCadena = string.Join(sSeparador, Datos);

                }
            }
            return sCadena;
        }

        public static string GetIP()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length-1].ToString();
        }

        public static string GetMachineName() 
        { 
            return System.Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        public static string GetMACAddress()
        {
            string sMACAdrress = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["MacAddress"] != null)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        sMACAdrress = mo["MacAddress"].ToString();
                    }
                }
            }

            return sMACAdrress;
        }

        public static string GetCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        public static byte[] RetornaByteStream(Stream StreamImagen)
        {
            byte[] barrImg = new byte[1];

            MemoryStream ms = new MemoryStream();
            ms = (MemoryStream)StreamImagen;
            barrImg = ms.ToArray();
            ms.Close();
            ms.Dispose();
            ms = null;


            return barrImg;
        }

        public static Stream RetornaStreamByte(byte[] byteImagen)
        {

            Stream StreamFile = null;

            MemoryStream ms = new MemoryStream();
           
            ms.Write(byteImagen, 0, byteImagen.Length);
            ms.Flush();
            ms.Close();
          //  StreamReader reader = new StreamReader(ms, System.Text.Encoding.UTF8);
            StreamFile = (Stream)ms;

            ms.Dispose();
            ms = null;

            return StreamFile;
        }

        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        public static string GetVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        public static string getUniqueID()
        {
            string drive = "C";
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = GetVolumeSerial(drive);
            string cpuID = GetCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        public static bool AnalizarModificacion(object objetoOriginal, object objetoModificado)
        {
            if (objetoOriginal == null)
            {
                return false;
            }

            if (objetoModificado == null)
            {
                return false;
            }

            Type a = null;
            Type b = null;

            if (objetoOriginal != null)
            {
                if (objetoModificado != null)
                {
                    a = objetoOriginal.GetType();
                    b = objetoModificado.GetType();

                    if (a != b)
                    {
                        return false;
                    }

                    if (a != null && objetoModificado != null)
                    {
                        MemoryStream streamOriginal = new MemoryStream();
                        MemoryStream streamModificada = new MemoryStream();

                        XmlTextWriter writer1 = new XmlTextWriter(streamOriginal, Encoding.UTF8);
                        XmlTextWriter writer2 = new XmlTextWriter(streamModificada, Encoding.UTF8);

                        XmlSerializer serializador = new XmlSerializer(objetoOriginal.GetType(), "");

                        try
                        {
                            serializador.Serialize(writer1, objetoOriginal);
                            serializador.Serialize(writer2, objetoModificado);
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        finally
                        {
                            writer1.Close();
                            writer2.Close();
                        }

                        //Documetos y navegadores en memoria
                        try
                        {
                            XmlDocument d1 = new XmlDocument();
                            d1.InnerXml = RemoveUtf8ByteOrderMark(Encoding.UTF8.GetString(streamOriginal.GetBuffer()).Trim());
                            string sOriginal = d1.OuterXml.ToString();
                            XmlDocument d2 = new XmlDocument();
                            d2.InnerXml = RemoveUtf8ByteOrderMark(Encoding.UTF8.GetString(streamModificada.GetBuffer()).Trim());
                            string sModificado = d2.OuterXml.ToString();

                            if (sOriginal.Trim() == sModificado.Trim()) 
                            {
                                return true;
                            }

                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        private static string RemoveUtf8ByteOrderMark(string xml) { string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()); if (xml.StartsWith(byteOrderMarkUtf8)) { xml = xml.Remove(0, byteOrderMarkUtf8.Length); } return xml; }

        public List<T> ConvertXmlToList<T>(XmlDocument oXml)
        {
            T target = Activator.CreateInstance<T>();
            Type _type = target.GetType();

            List<T> lstBaseRetorno = new List<T>();
            Type oTipoListaEntidad = lstBaseRetorno.GetType();
            XmlSerializer serializador = new XmlSerializer(lstBaseRetorno.GetType());

            string nombreEntidad = _type.Name;
            XmlElement root = oXml.DocumentElement;
            string sRoot = root.Name.ToString();
            StringBuilder sbRootHeader = new StringBuilder();
            StringBuilder sbRootFoot = new StringBuilder();

            sbRootHeader.Append("<");
            sbRootHeader.Append(sRoot);
            sbRootHeader.Append(">");

            sbRootFoot.Append("</");
            sbRootFoot.Append(sRoot);
            sbRootFoot.Append(">");

            string sR = oXml.OuterXml.ToString();

            //sR = sR.Replace(sbRootHeader.ToString(), "");
            sR = sR.Remove(1, sbRootHeader.Length);
            sR = sR.Replace(sbRootFoot.ToString(), "");
            sR = @"<?xml version=""1.0"" encoding=""utf-16""?>" + sR;
            sR = sR.Replace(_type.Name.ToString(), "ArrayOf" + _type);
            System.IO.TextReader sr = new System.IO.StringReader(sR);
            try
            {
                lstBaseRetorno = (List<T>)(serializador.Deserialize(sr));
            }
            catch (Exception)
            {

                throw;
            }

            serializador = null;
            sr = null;

            return lstBaseRetorno;
        }

        public static bool ValidarExpresionRegular(string oObject, string sErrorMessaje, string sRegExpresion)
        {
            var converter = new System.Windows.Media.BrushConverter();

            Regex oRegExValidator = new Regex(sRegExpresion);

            System.Windows.Controls.ValidationResult result = new System.Windows.Controls.ValidationResult(true, null);
            string inputString = (oObject ?? string.Empty).ToString();


            if (string.IsNullOrEmpty(inputString))
            {
                result = new System.Windows.Controls.ValidationResult(false, sErrorMessaje);
            }

            if (!oRegExValidator.IsMatch(inputString))
            {
                result = new  System.Windows.Controls.ValidationResult(false, sErrorMessaje);
            }
            else
            {
                result = new System.Windows.Controls.ValidationResult(true, null);
            }

            return result.IsValid;
        }

        public static DateTime GetFirstDayOfNextMonth(DateTime startDate)
        {
            if (startDate.Month == 12) // its end of year , we need to add another year to new date:
            {
                startDate = new DateTime((startDate.Year + 1), 1, 1);
            }
            else
            {
                startDate = new DateTime(startDate.Year, (startDate.Month + 1), 1);
            }
            return startDate;
        }

        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public static DateTime GetLastDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            return GetFirstDayOfWeek(dayInWeek, cultureInfo).AddDays(7).Subtract(new TimeSpan(1, 0, 0, 0, 0));
        }

        public static string IsApplicationInstalled(string p_name)
        {
            string displayName;
            RegistryKey key;

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (key != null)
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null)
                    {
                        if (displayName.Contains(p_name) == true)
                        {
                            return subkey.GetValue("InstallLocation").ToString();
                        }
                    }
                }
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (key != null)
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null)
                    {
                        if (displayName.Contains(p_name) == true)
                        {
                            return subkey.GetValue("InstallLocation").ToString();
                        }
                    }
                }
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            if (key != null)
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null)
                    {
                        if (displayName.Equals(p_name) == true)
                        {
                            return subkey.GetValue("InstallLocation").ToString();
                        }
                    }
                }
            }

            // NOT FOUND
            return string.Empty;
        }
    }
}
