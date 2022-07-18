using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.BussinesObject.Application
{
    public class DtoDatosAdicionales
    {
        private string _campo;

        public string Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        private string _valor;

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        
    }
}
