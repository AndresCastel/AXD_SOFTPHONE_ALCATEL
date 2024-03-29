﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Axede.Utilidades;
using Axede.Utilidades.Enums;
using Axede.DataObjects.Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EntLibContrib.Data.MySql;
using Axede.BussinesObject.Application;



namespace Axede.DataObjects.Interface
{
    public partial interface ICommonDao
    {

        MySqlDatabase Database { get; }

        List<DtoContactos> ObtenerContactos(int iIdeUser, string sInitialLetter);
        bool InsContactos(Contactos oContactos);
        bool DelContactos(Contactos oContacto);
        bool UpdContactos(Contactos oContacto);
        DtoUserInfo ObtenerInfoUser(string sExt);
        List<DtoContactos> ObtenerContactosxNumTelefono(Contactos oContacto);
        bool InsRecentContactos(Contactos oContactos);
        bool DelRecentUser(Contactos oContacto);
        List<DtoContactos> ObtenerContactosFavoritos(string sExte);
        List<DtoContactos> getContactAndRecentContact(int iIdeUser);
    }
}
