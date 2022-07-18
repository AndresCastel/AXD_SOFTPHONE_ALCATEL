using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Axede.WPF.Softphone.Applications.BussinesClass.Configuracion
{
    public class SeccionOXEServerServices : ConfigurationSection
    {

        private static ConfigurationPropertyCollection _propiedades;
        private static ConfigurationProperty _OXEServerServiceConfigurados;

        static SeccionOXEServerServices()
        {
            _OXEServerServiceConfigurados = new ConfigurationProperty
                (
                 "",
                 typeof(ColeccionOXEServerServices),
                 null,
                 ConfigurationPropertyOptions.IsRequired |
                 ConfigurationPropertyOptions.IsDefaultCollection |
                 ConfigurationPropertyOptions.IsKey
                );
            _propiedades = new ConfigurationPropertyCollection();
            _propiedades.Add(_OXEServerServiceConfigurados);
        }



        public ColeccionOXEServerServices OXEServerServiceConfigurados
        {
            get
            {
                return (ColeccionOXEServerServices)base[_OXEServerServiceConfigurados];
            }
        }


        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _propiedades;
            }
        }






    }
}
