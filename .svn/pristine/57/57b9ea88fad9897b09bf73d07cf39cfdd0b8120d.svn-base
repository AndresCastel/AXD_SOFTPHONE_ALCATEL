using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Axede.WPF.Softphone.Applications.BussinesClass.Configuracion
{
   public class ConfiguracionVideoconferencia : ConfigurationElement
    {

        [ConfigurationProperty("numeroMarcar", IsRequired = true, IsKey = true)]
       public string NumeroMarcar
        {
            get
            {
                return (string)this["numeroMarcar"];
            }
            set
            {
                this["numeroMarcar"] = value;
            }
        }

        [ConfigurationProperty("nombre", IsRequired = true)]
        public string Nombre
        {
            get
            {
                return (string)this["nombre"];
            }
            set
            {
                this["nombre"] = value;
            }
        }

        [ConfigurationProperty("TipoLlamada", IsRequired = true)]
        public int TipoLlamada
        {  
            get
            {
                return (int)this["TipoLlamada"];
            }
            set
            {
                this["TipoLlamada"] = value;
            }
        }
    }
}

