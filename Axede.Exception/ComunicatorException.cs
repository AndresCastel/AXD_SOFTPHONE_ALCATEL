using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.Utilidades;
using Axede.Global;

namespace Axede.Exception
{
    public class ComunicatorException : System.Exception
    {

        public ComunicatorException(string sAplicacion, string sMensajeError, System.Exception eException)
            : base(sMensajeError, eException)
        {
            //Escribe en el Log
            TraceHandler.WriteLine(LOG.NombreArchivoLogComunicator + @"\" + sAplicacion + "_", sMensajeError, TipoLog.ERROR);
		}
    }
}
