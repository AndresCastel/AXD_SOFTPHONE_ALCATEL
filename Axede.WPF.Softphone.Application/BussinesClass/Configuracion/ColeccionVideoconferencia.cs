﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Axede.WPF.Softphone.Applications.BussinesClass.Configuracion
{
    public class ColeccionVideoconferencia : ConfigurationElementCollection
    {

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "videoconferencia";
            }
        }
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return new ConfigurationPropertyCollection();
            }
        }
        public ConfiguracionVideoconferencia this[int Indice]
        {
            get
            {
                return (ConfiguracionVideoconferencia)base.BaseGet(Indice);
            }
            set
            {
                if (base.BaseGet(Indice) != null) base.BaseRemoveAt(Indice);
                base.BaseAdd(Indice, value);
            }
        }


        public void Add(ConfiguracionVideoconferencia Elemento)
        {
            base.BaseAdd(Elemento);
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfiguracionVideoconferencia();
        }
        protected override object GetElementKey(ConfigurationElement Elemento)
        {
            return ((ConfiguracionVideoconferencia)Elemento).NumeroMarcar;
        }
        public bool ExistElement(string Clave, ref ConfigurationElement Elemento)
        {
            var resultado = from p in base.BaseGetAllKeys().Select
                                ((num, indice) => new { clave = num, indice })
                            where p.clave.Equals(Clave)
                            select p.indice;

            if (resultado.Count() > 0 && this[(int)resultado.First()] != null)
                Elemento = this[(int)resultado.First()];
            else
                Elemento = null;

            return Elemento != null;
        }
        public ConfiguracionVideoconferencia GetElement(Guid Clave)
        {
            var resultado = from p in base.BaseGetAllKeys().Select
                                ((num, indice) => new { clave = num, indice })
                            where p.clave.Equals(Clave)
                            select p.indice;

            if (resultado.Count() > 0 && this[(int)resultado.First()] != null)
                return this[(int)resultado.First()];

            return null;
        }
        public void Remove(ConfiguracionVideoconferencia Elemento)
        {
            base.BaseRemove(Elemento);
        }
        public void RemoveAt(int Indice)
        {
            base.BaseRemoveAt(Indice);
        }

    }
}
