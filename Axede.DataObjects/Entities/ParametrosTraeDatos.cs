using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.DataObjects.Entities
{
    public class ParametrosTraeDatos
    {

        private string _NombreVista;
        private string _CampoOrden;
        private string _Orden;
        private string _Filtro;
        private int _TopRegistros;
      

        /// <summary>
        /// Nombre de la vista en BD sobre la cual se va a realizar la consulta.
        /// </summary>
        public string NombreVista
        {
            get { return _NombreVista; }
            set { _NombreVista = value; }
        }

        /// <summary>
        /// Nombre del campo que hace parte de la vista sobre el cual se va a realizar el ordenamiento.
        /// </summary>
        public string CampoOrden
        {
            get { return _CampoOrden; }
            set { _CampoOrden = value; }
        }

        /// <summary>
        /// Corresponde al orden a establecer sobre el campo orden. ASC - DESC
        /// </summary>
        public string Orden
        {
            get { return _Orden; }
            set { _Orden = value; }
        }

        /// <summary>
        /// Filtro sobre la Vista. debe iniciar con AND
        /// </summary>
        public string Filtro
        {
            get { return _Filtro; }
            set { _Filtro = value; }
        }

        /// <summary>
        /// Top de registros a consultar
        /// </summary>
        public int TopRegistros
        {
            get { return _TopRegistros; }
            set { _TopRegistros = value; }
        }

    }
}
