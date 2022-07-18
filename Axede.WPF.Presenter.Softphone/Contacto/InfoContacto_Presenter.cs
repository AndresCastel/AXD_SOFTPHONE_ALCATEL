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
    public class InfoContacto_Presenter : Presenter<IinfoContacto_View>
    {
        public InfoContacto_Presenter(IinfoContacto_View view)
            : base(view)
        { }

        public void InsFavoritos()
        {
            ResultadoOperacion oResultadoOperacion = Model.InsertarFavorito(View.oContacto);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                View.EstadoContacto = false;
            }
            else
            {
                View.EstadoContacto = true;
            }
        }

        public void EliminarFavorito()
        {
            ResultadoOperacion oResultadoOperacion = Model.EliminarFavorito(View.oContacto);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                View.EstadoContacto = false;
            }
            else
            {
                View.EstadoContacto = true;
            }
        }

        public void EliminarContacto()
        {
            ResultadoOperacion oResultadoOperacion = Model.EliminarContacto(View.oContacto);

            if (oResultadoOperacion.oEstado == TipoRespuesta.Error)
            {
                View.EstadoContacto = false;
            }
            else
            {
                View.EstadoContacto = true;
            }
        }
    }
}
