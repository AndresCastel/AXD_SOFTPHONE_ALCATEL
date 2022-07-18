using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.BussinesObject.Application
{
    public class DtoTipoTelefono
    {
        private int _Ide_TipoTelefono;

        public int Ide_TipoTelefono
        {
            get { return _Ide_TipoTelefono; }
            set { _Ide_TipoTelefono = value; }
        }

        private string _NomTipoTelefono;

        public string NomTipoTelefono
        {
            get { return _NomTipoTelefono; }
            set { _NomTipoTelefono = value; }
        }
    }
}
