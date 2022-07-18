using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.DataObjects.Entities
{
    public class ParametrosBorrarRegistro
    {

        private string _NombreTabla;
        private string _Campo;
        private int _IdeRegistro;

        /// <summary>
        /// Nombre de la tabla de la cual se elimina el registro.
        /// </summary>
        public string NombreTabla
        {
            get { return _NombreTabla; }
            set { _NombreTabla = value; }
        }

        /// <summary>
        /// Nombre del campo asociado a la tabla, por el cual se realiza el borrado del registro.
        /// </summary>
        public string Campo
        {
            get { return _Campo; }
            set { _Campo = value; }
        }

        /// <summary>
        /// Identificador único del registro a eliminar de la tabla
        /// </summary>
        public int IdeRegistro
        {
            get { return _IdeRegistro; }
            set { _IdeRegistro = value; }
        }

        
       

        
    }
}
