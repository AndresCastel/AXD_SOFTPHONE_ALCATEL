﻿using Axede.BussinesObject.Application;
using Axede.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.Model.SoftPhone
{
    public partial interface IModel
    {
        ResultadoOperacion ObtenerListaContactos(Contactos oContacto);
        ResultadoOperacion InsertarContacto(Contactos oContacto);
        ResultadoOperacion EliminarContacto(Contactos oContacto);
        ResultadoOperacion ModificarContacto(Contactos oContacto);
        ResultadoOperacion InsertarFavorito(Contactos oContacto);
        ResultadoOperacion EliminarFavorito(Contactos oContacto);
        ResultadoOperacion ObtenerListaContactosFavoritos();
        ResultadoOperacion getContactAndRecentContact();
    }
}
