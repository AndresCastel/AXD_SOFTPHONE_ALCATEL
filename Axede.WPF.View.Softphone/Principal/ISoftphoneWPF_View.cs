﻿using Axede.BussinesObject.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axede.WPF.View.Softphone
{
    public interface ISoftphoneWPF_View : IView
    {
        List<DtoContactos> ListContactFav { set; get; }      

        string MuestraMensaje { set; }
    }
}
