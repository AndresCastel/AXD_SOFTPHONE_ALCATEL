﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows;

namespace Axede.Utilidades
{
    public class Globales
    {

        private static string _NombreAplicacion;

        /// <summary>
        /// Retorna el Nombre de la aplicación del archivo de Configuración
        /// </summary>
        public static string NombreAplicacion
        {
            get {

                if (_NombreAplicacion == null) 
                {
                    _NombreAplicacion = ConfigurationManager.AppSettings["ApplicationName"];
                }
                return _NombreAplicacion; 
            }
            set { _NombreAplicacion = value; }
        }

        public static string SkinBaseWPF
        {
            get { return ConfigurationManager.AppSettings["Skin"]; }

        }


        public static string CodigoAplicacion
        {
            get { return ConfigurationManager.AppSettings["ApplicationCode"]; }

        }

        public static string MensajeLicenciamiento { get; set;}
       

        /// <summary>
        /// Ruta y prefijo de los archivos de log de la aplicación
        /// </summary>
        public static string sRutaLog
        {
            get { return ConfigurationManager.AppSettings["LogFilePath"]; }

        }


        /// <summary>
        /// Establece si está habilitado el logging de la aplicación
        /// </summary>
        public static bool bEnabledTracking
        {
            get
            {
                string sEnabledTracking = ConfigurationManager.AppSettings["EnabledTracking"];
                if (!string.IsNullOrEmpty(sEnabledTracking))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        public static int iRegistrosPagina
        {
            get { return 10; }
        }

        /// <summary>
        /// Número de registros por páginaPop
        /// </summary>
        public static int iRegistrosPaginaPopUp
        {
            get { return 5; }
        }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        public static int iRegistrosPaginaWeb
        {
            get { return 10; }
        }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        public static int iRegistrosPaginaSmall
        {
            get { return 5; }
        }


        /// <summary>
        /// Ruta absoluta del archivo de ayuda de la aplicación 
        /// </summary>
        public static string sRutaArchivoAyuda_GestionCobro
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"Ayuda\CEBPGestionCobro.chm";
            }
        }
    
        /// <summary>
        /// regular expresion para campos decimales
        /// </summary>
        public static string rexDecimal(int PartInt, int PartDecimal)
        {
            //return "^\\d{" + PartInt.ToString() + "}.\\d{" + PartDecimal.ToString() + "}$";
            return "^.\\d{0," + PartInt.ToString() + "}.\\d{0," + PartDecimal.ToString() + "}$";
        }
        /// <summary>
        /// regular expresion para campos numericos
        /// </summary>
        public static string rexNumerico
        {
            get { return "^[0-9]*$"; }
        }
        /// <summary>
        /// regular expresion para campos alfabéticos y espacio
        /// </summary>
        public static string rexAlfabetico
        {           
            get { return "^([a-z]|[A-Z]|Á|É|Í|Ó|Ú|á|é|í|ó|ú|ñ|ü| |)+$"; }
        }

        /// <summary>
        /// regular expresion para campos alfabeticos, numéricos,  espacio
        /// </summary>
        public static string rexAlfaNumerico
        {
           
            get { return "^([a-z]|[A-Z]|[0-9]|Ñ|Á|É|Í|Ó|Ú|á|é|í|ó|ú|ñ|ü| |)+$"; }           
            
        }

        /// <summary>
        /// regular expresion para campos alfabeticos, numéricos,  espacio
        /// </summary>
        public static string rexDireccion
        {

            get { return "^([a-z]|[A-Z]|[0-9]|Ñ|Á|É|Í|Ó|Ú|á|é|í|ó|ú|ñ|ü| |.|#|°|-|)+$"; }

        }

        /// <summary>
        /// regular expresion para campos alfabeticos, numéricos,  sin espacio
        /// </summary>
        public static string rexAlfaNumericoSinEspacios
        {

            get { return "^([a-z]|[A-Z]|[0-9]|Ñ|Á|É|Í|Ó|Ú|á|é|í|ó|ú|ñ|ü|)+$"; }

        }

        /// <summary>
        /// regular expresion para campos alfabeticos, numéricos, guión, punto, espacio
        /// </summary>
        public static string rexAlfaNumericoPuntoGuion
        {

            get { return "^([a-z]|[A-Z]|[0-9]|[//.]|Ñ|Á|É|Í|Ó|Ú|á|é|í|ó|ú|ñ|ü|-| |_|,|)+$"; }
           

        }
        /// <summary>
        /// regular expresion para campos alfanumericos y el guion, espacio
        /// </summary>
        public static string rexAlfaNumericoGuion
        {
            get { return "^([a-z]|[A-Z]|[0-9]|Ñ|Á|É|Í|Ó|Ú|á|é|í|ó|ú|ñ|ü|-| |)+$"; }
        }
        /// <summary>
        /// regular expresion para Emails
        /// </summary>
        public static string rexEmail
        {
            get { return "^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+[//.][a-zA-Z]{2,4}$"; }
        }

        /// <summary>
        /// regular expresion para Emails2
        /// </summary>
        public static string rexEmail2 
        {
            get
            {
                return
                @"^([a-zA-Z0-9_-.]+)@(([[0-9]{1,3}" +
                @".[0-9]{1,3}.[0-9]{1,3}.)|(([a-zA-Z0-9-]+" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$";
            }
        }


        /// <summary>
        /// regular expresion para campos tipo fecha
        /// </summary>
        public static string rexFecha
        {
            get { return "(0[1-9]|[12][0-9]|3[01])[//.](0[1-9]|1[012])[//.](19|20)\\d\\d"; }
            //get { return "^(((((0[1-9])|(1\\d)|(2[0-8]))/((0[1-9])|(1[0-2])))|((31\\/((0[13578])|(1[02])))|((29|30)\\/((0[1,3-9])|(1[0-2])))))\\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\\/02\\/(19|20)(([02468][048])|([13579][26]))))$ "; }
        }

        /// <summary>
        /// regular expresion para campos tipo fecha
        /// </summary>
        public static string rexPassword
        {
            get { return @"(?=^.{6,}$)(?=.*\d)(?=.*\W+)(?![.\n]).*$"; }
        }

        public static string PassPhrase 
        {
            get { return "My P@ss Ax3d3"; }
        }

        public static string LongRegularExpresion(int Long1, int Long2)
        { 
            return "^." + "{" + Long1.ToString() + "," + Long2.ToString() + "}$";
        }

        /// <summary>
        /// limite de días edad cartera
        /// </summary>
        public static int iLimiteDiasEdadCartera
        {
            get { return 1000000; }
        }

        #region Controles Dinamicos
        /// <summary>
        /// alto de los labels
        /// </summary>
        public static int lblHeight
        {
            get { return 16; }
        }
        /// <summary>
        /// ancho de los controles tipo TextBox
        /// </summary>
        public static int txtWidtht
        {
            get { return 250; }
        }
        #endregion

    }
}