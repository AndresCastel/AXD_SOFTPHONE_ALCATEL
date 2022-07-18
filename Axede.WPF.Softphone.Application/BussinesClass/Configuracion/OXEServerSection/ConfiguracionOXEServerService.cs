using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Axede.WPF.Softphone.Applications.BussinesClass.Configuracion
{
    public class ConfiguracionOXEServerService : ConfigurationElement
    {


        [ConfigurationProperty("IP", IsRequired = true, IsKey = true)]
        public string IP
        {
            get
            {
                return (string)this["IP"];
            }
            set
            {
                this["IP"] = value;
            }
        }


        [ConfigurationProperty("Activo", IsRequired = true, IsKey = true)]
        public bool Activo
        {
            get
            {
                return (bool)this["Activo"];
            }
            set
            {
                this["Activo"] = value;
            }
        }

        [ConfigurationProperty("Principal", IsRequired = true, IsKey = true)]
        public bool Principal
        {
            get
            {
                return (bool)this["Principal"];
            }
            set
            {
                this["Principal"] = value;
            }
        }


    }
}
