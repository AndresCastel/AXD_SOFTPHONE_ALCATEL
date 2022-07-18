using Axede.WPF.View.Softphone;
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
    public class Contacto_Presenter : Presenter<IContacto_View>
    {
        public Contacto_Presenter(IContacto_View view)
            : base(view)
        { }

        public void ObtenerListaContactos()
        {
            ResultadoOperacion oResultadoOperacion = Model.ObtenerListaContactos(View.oSearchContact);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.CargaGrilla = (List<DtoContactos>)oResultadoOperacion.ListaEntidadDatos;
            }
            else
            {
                View.MuestraMensaje = oResultadoOperacion.Mensaje;
                View.CargaGrilla = new List<DtoContactos>();
            }
        }

        public void getContactAndRecentContact()
        {
            ResultadoOperacion oResultadoOperacion = Model.getContactAndRecentContact();

            if (oResultadoOperacion.oEstado == TipoRespuesta.Exito)
            {
                View.CargaGrilla = (List<DtoContactos>)oResultadoOperacion.ListaEntidadDatos;
            }
            else
            {
                View.MuestraMensaje = oResultadoOperacion.Mensaje;
                View.CargaGrilla = new List<DtoContactos>();
            }
        }

        public bool InsertarContacto()
        {
            bool _Msjerror = false;
            ResultadoOperacion oResultadoOperacion = Model.InsertarContacto(View.oSearchContact);
            View.MuestraMensaje = oResultadoOperacion.Mensaje;
            if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                _Msjerror = true;
            }
            return _Msjerror;
        }

        public bool ModificarContacto()
        {
            bool _Msjerror = false;
            ResultadoOperacion oResultadoOperacion = Model.ModificarContacto(View.oSearchContact);
            View.MuestraMensaje = oResultadoOperacion.Mensaje;
            if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                _Msjerror = true;
            }
            return _Msjerror;
        }

    }
}
