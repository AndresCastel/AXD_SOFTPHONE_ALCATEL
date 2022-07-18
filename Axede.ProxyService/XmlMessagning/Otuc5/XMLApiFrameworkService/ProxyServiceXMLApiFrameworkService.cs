using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Axede.Utilidades;
using Axede.Utilidades.Enums;

namespace Axede.ProxyService
{
    
    public partial class ProxyService : IProxyService
    {


        public ResultadoOperacion Login_XMLApiFrameworkOTUC5() 
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            oResultadoOperacion.EntidadDatos = false;

            try
            {
                if (_IDSessionAPIFramework == string.Empty)
                {
                    _IDSessionAPIFramework = _XMLApiFrameworkService_OTUC5.login(sXMLApiFrameworkService_OTUC5_User, sXMLApiFrameworkService_OTUC5_Pass);
                }
                oResultadoOperacion.EntidadDatos = true;
            }
            catch (Exception Ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error intentando acceder al servicio XMLMessagingService_OTUC5 :  login()";
                return oResultadoOperacion;
               
            }
           
            return oResultadoOperacion;
        }


       

    }
}
