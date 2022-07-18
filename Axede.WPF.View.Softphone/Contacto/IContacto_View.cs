﻿using Axede.BussinesObject.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axede.WPF.View.Softphone
{
    public interface IContacto_View : IView
    {
        List<DtoContactos> CargaGrilla { set; get; }
        Contactos oSearchContact { get; }
        string MuestraMensaje { set; }

    }
}
