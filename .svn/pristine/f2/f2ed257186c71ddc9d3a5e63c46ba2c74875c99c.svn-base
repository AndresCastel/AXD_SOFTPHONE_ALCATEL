// ===================================================
// Desarrollado Por		    : CARLOS ALFONSO MOLINA JIMENEZ
// Fecha de Creación		: 26/ENE/2012
// Lenguaje Programación	: [C#]
// Producto o sistema	    : CEBP LIBRANZAS
// Empresa			        : Axede S.A
// Cliente			        : Axede S.A
// ===================================================
// Versión	Descripción
// [1.0.0.0]
// Clase encargada escribir mensajes en un archivo de log.
// ===================================================
// MODIFICACIONES:
// ===================================================
// Ver.	 Fecha		Autor – Empresa 	Descripción
// ---	 -------------	----------------------	----------------------------------------
// XX	dd/mm/aaaa	[Nombre Completo]	 [Razón del cambio realizado] 
// ===================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Axede.Utilidades
{
    public class TraceHandler
    {
        private static object syncLock = new object();

        private static void WriteLogFile(string sFileName,string message,TipoLog oTipoLog)
        {
            if (oTipoLog == TipoLog.ERROR)
            {
                message = "****** ERROR ******" +  DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss").ToString() + " **********" + "\r\n"
                          + message + "\r\n"
                          + "*************************************************";
            }
            else if (oTipoLog != TipoLog.PLANO)
            {
                message = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss").ToString() + " - " + message;
            }

            string directoryFullPath = Path.GetDirectoryName(sFileName);

            if (!Directory.Exists(directoryFullPath))
            {
                Directory.CreateDirectory(directoryFullPath);
            }
            if (oTipoLog == TipoLog.PLANO)
            {
                if (File.Exists(sFileName) == true)
                {
                    if (General.VerificaArchivoEnUso(directoryFullPath, Path.GetFileName(sFileName)) == true)
                    {
                        throw new Exception("Archivo en uso");
                    }
                }
            }
            lock (syncLock)
            {
                if (oTipoLog != TipoLog.PLANO)
                {
                    if (Globales.bEnabledTracking == true)
                    {
                        string sNameFile = sFileName;
                        if (oTipoLog != TipoLog.TRAZA)
                        {
                            sNameFile = sNameFile + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".log";
                        }
                        else 
                        {
                            sNameFile = sNameFile + ".log";
                        }
                        // create a writer and open the file
                        System.IO.TextWriter tw = new System.IO.StreamWriter(sNameFile, true);

                        // write a line of text to the file
                        tw.WriteLine(message);

                        // close the stream
                        tw.Close();
                    }
                }
                else 
                {
                    string sNameFile = sFileName;

                    // create a writer and open the file
                    System.IO.TextWriter tw = new System.IO.StreamWriter(sNameFile, true);

                    // write a line of text to the file
                    tw.WriteLine(message);

                    // close the stream
                    tw.Close();
                }
            }


        }

        /// <summary>
        /// Escribe una linea en el archivo del log
        /// </summary>
        /// <param name="sFileName">Nombre del Archivo de Log...</param>
        /// <param name="message">Mensaje a escribir en el Log...</param>
        /// <param name="oTipoLog">Tipo de Log</param>
        public static void WriteLine(string sFileName,string message, TipoLog oTipoLog)
        {
            WriteLogFile(sFileName, message, oTipoLog);
        }


    }

    /// <summary>
    /// Tipo de Log que define la estructura del mensaje en el archivo de log.
    /// </summary>
    public enum TipoLog 
    { 
        ERROR,
        TRAZA,
        PLANO
    }
}
