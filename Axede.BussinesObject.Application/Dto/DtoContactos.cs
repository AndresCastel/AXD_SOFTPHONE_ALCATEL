﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Axede.BussinesObject.Application
{
    public class DtoContactos
    {
        private Int32 _TotalRegistros;
        private Int32 _Row;

        private string _ToolTipEstado;

        public string ToolTipEstado
        {
            get { return _ToolTipEstado; }
            set { _ToolTipEstado = value; }
        }

        private int _Ide_PhoneType;

        public int Ide_PhoneType
        {
            get { return _Ide_PhoneType; }
            set { _Ide_PhoneType = value; }
        }

        /// <summary>
        /// Total Registro Consulta Total
        /// </summary>
        public Int32 TotalRegistros
        {
            get { return _TotalRegistros; }
            set { _TotalRegistros = value; }
        }

        /// <summary>
        /// Número de Registro
        /// </summary>
        public Int32 Row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _Apellido;

        public string Apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }

        private string _NombreCompleto;

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private ImageSource _EstadoPresencia;

        public ImageSource EstadoPresencia
        {
            get { return _EstadoPresencia; }
            set { _EstadoPresencia = value; }
        }

        private string _Telefono;

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private string _Extension;

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        private ImageSource _TipoTelefono;

        public ImageSource TipoTelefono
        {
            get { return _TipoTelefono; }
            set { _TipoTelefono = value; }
        }

        private int _Ide_Contact;

        public int Ide_Contact
        {
            get { return _Ide_Contact; }
            set { _Ide_Contact = value; }
        }

        private bool _RecentContact;

        public bool RecentContact
        {
            get { return _RecentContact; }
            set { _RecentContact = value; }
        }

        private int _Ide_RecentContact;

        public int Ide_RecentContact
        {
            get { return _Ide_RecentContact; }
            set { _Ide_RecentContact = value; }
        }

        private int _Ide_User;

        public int Ide_User
        {
            get { return _Ide_User; }
            set { _Ide_User = value; }
        }

        private int _Vlr_UserState;

        public int Vlr_UserState
        {
            get { return _Vlr_UserState; }
            set { _Vlr_UserState = value; }
        }

        private PhoneType _PhoneType;

        public PhoneType PhoneType
        {
            get { return _PhoneType; }
            set { _PhoneType = value; }
        }

        //private ContactType _ContactType;

        //public ContactType ContactType
        //{
        //    get { return _ContactType; }
        //    set { _ContactType = value; }
        //}
    }
}
