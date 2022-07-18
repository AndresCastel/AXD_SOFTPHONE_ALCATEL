﻿using Axede.WPF.View.Softphone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axede.WPF.Presenter.Softphone;
using Axede.BussinesObject.Application;
using Axede.Utilidades;
using Axede.Utilidades.Enums;

namespace Axede.WPF.Presenter.Softphone
{
    public class SoftphoneWPF_Presenter : Presenter<ISoftphoneWPF_View>
    {
        public SoftphoneWPF_Presenter(ISoftphoneWPF_View view)
            : base(view)
        { }


        public void Login() 
        {
            Model.Login_XMLMessagingOTUC5();
        }

        public void ObtenerListaContactosFavoritos()
        {
            ResultadoOperacion oResultadoOperacion = Model.ObtenerListaContactosFavoritos();

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.ListContactFav = (List<DtoContactos>)oResultadoOperacion.ListaEntidadDatos;
            }
            else
            {
                View.MuestraMensaje = oResultadoOperacion.Mensaje;
                View.ListContactFav = new List<DtoContactos>();
            }
        }

    }
}
