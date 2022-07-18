using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Axede.WPF.Softphone.Applications.BussinesClass.Configuracion
{
    public class SeccionVideoConferencia : ConfigurationSection
    {
        private static ConfigurationPropertyCollection _propiedades;
        private static ConfigurationProperty _videoconferenciasConfiguradas;

        static SeccionVideoConferencia()
        {
            _videoconferenciasConfiguradas = new ConfigurationProperty
                (
                 "",
                 typeof(ColeccionVideoconferencia),
                 null,
                 ConfigurationPropertyOptions.IsRequired |
                 ConfigurationPropertyOptions.IsDefaultCollection |
                 ConfigurationPropertyOptions.IsKey
                );
            _propiedades = new ConfigurationPropertyCollection();
            _propiedades.Add(_videoconferenciasConfiguradas);
        }

        public ColeccionVideoconferencia videoconferenciasConfiguradas
        {
            get
            {
                return (ColeccionVideoconferencia)base[_videoconferenciasConfiguradas];
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
