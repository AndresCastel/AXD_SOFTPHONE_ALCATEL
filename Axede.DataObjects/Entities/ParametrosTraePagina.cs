using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Axede.DataObjects.Entities
{  
    public class ParametrosTraePagina
    {
        private string _sNombreVista;
        private int _iRegistrosPagina;
        private int _iPaginaActual;
        private string _sCampoOrden;
        private string _sOrden;
        private int _iTipo;
        private string _sFiltro;

        /// <summary>
        /// Nombre de la vista en BD sobre la cual se va a realizar la consulta.
        /// </summary>
        public string NombreVista
        {
            get { return _sNombreVista; }
            set { _sNombreVista = value; }
        }

        /// <summary>
        /// Número de registros por página
        /// </summary>
        public int RegistrosPagina
        {
            get { return _iRegistrosPagina; }
            set { _iRegistrosPagina = value; }
        }

        /// <summary>
        /// Número de Página Actual
        /// </summary>
        public int PaginaActual
        {
            get { return _iPaginaActual; }
            set { _iPaginaActual = value; }
        }

        /// <summary>
        /// Nombre del campo que hace parte de la vista sobre el cual se va a realizar el ordenamiento.
        /// </summary>
        public string CampoOrden
        {
            get { return _sCampoOrden; }
            set { _sCampoOrden = value; }
        }

        /// <summary>
        /// Corresponde al orden a establecer sobre el campo orden. ASC - DESC
        /// </summary>
        public string Orden
        {
            get { return _sOrden; }
            set { _sOrden = value; }
        }

        /// <summary>
        /// Tipo de página que se va a consultar: 1. Primera Página - 2. Página Anterior - 3. Página Siguiente - 4. Ultima Página - 5. Página Actual
        /// </summary>
        public int Tipo
        {
            get { return _iTipo; }
            set { _iTipo = value; }
        }

        /// <summary>
        /// Filtro sobre la Vista. debe iniciar con AND
        /// </summary>
        public string Filtro
        {
            get { return _sFiltro; }
            set { _sFiltro = value; }
        }

    }
}
