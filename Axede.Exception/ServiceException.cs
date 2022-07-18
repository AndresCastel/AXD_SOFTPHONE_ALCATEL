using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.Utilidades;
using Axede.Global;

namespace Axede.Exception
{
    public class ServiceException : System.Exception
    {

        public ServiceException(string sMensajeError, System.Exception eException)
            : base(sMensajeError, eException)
        {
            //Escribe en el Log
            TraceHandler.WriteLine(LOG.NombreArchivoLogService, sMensajeError, TipoLog.ERROR);
		}
    }
}
