﻿using Axede.BussinesObject.Application;
using Axede.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.Utilidades.Enums;
using Axede.Mensajes;


namespace Axede.Model.SoftPhone
{
    public partial class Model : IModel
    {

        public ResultadoOperacion Login_XMLMessagingOTUC5() 
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();

            oResultadoOperacion = oProxyService.Login_XMLMessagingerviceOTUC5();

            return oResultadoOperacion;
        }

    }
}
