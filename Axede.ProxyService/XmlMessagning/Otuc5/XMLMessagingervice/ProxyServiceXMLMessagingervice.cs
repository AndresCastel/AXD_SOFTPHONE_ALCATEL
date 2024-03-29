﻿using System;
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

        public ResultadoOperacion Login_XMLMessagingerviceOTUC5()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            try
            {
                Login_XMLApiFrameworkOTUC5();
                if (_IDSessionAPIFramework != string.Empty)
                {
                    XMLMessagingService_OTUC5.AlcSessionResult _AlcSessionResult = _XMLMessagingService_OTUC5.login(_IDSessionAPIFramework, "9998","");
                }
                oResultadoOperacion.EntidadDatos = true;
            }
            catch (Exception Ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error intentando acceder al servicio XMLMessagingService_OTUC5 :  GetCounters()";
                return oResultadoOperacion;

            }

            return oResultadoOperacion;
        }
      

        public ResultadoOperacion GetCounters_XMLMessagingOTUC5() 
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();


            try
            {
               // string valor = _XMLMessagingService_OTUC5.getCounters(_IDSessionAPIFramework,
                oResultadoOperacion.EntidadDatos = true;
            }
            catch (Exception Ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = "Error intentando acceder al servicio XMLMessagingService_OTUC5 :  GetCounters()";
                return oResultadoOperacion;

            }

            return oResultadoOperacion;
        }


    }
}
