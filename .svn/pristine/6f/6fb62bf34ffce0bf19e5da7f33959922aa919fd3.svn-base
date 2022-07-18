using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.Utilidades;
using Axede.Global;

namespace Axede.Exception
{
    public class ApplicationXMAXException : System.Exception
    {

        public ApplicationXMAXException(string sMensajeError, System.Exception eException)
            : base(sMensajeError, eException)
        {
            //Escribe en el Log
            TraceHandler.WriteLine(LOG.NombreArchivoLogApp, sMensajeError, TipoLog.ERROR);
        }

    }
}
