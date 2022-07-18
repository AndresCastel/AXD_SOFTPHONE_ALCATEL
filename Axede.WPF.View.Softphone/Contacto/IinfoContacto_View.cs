using Axede.BussinesObject.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Axede.WPF.View.Softphone
{
    public interface IinfoContacto_View : IView
    {
        Contactos oContacto { get; set; }
        bool EstadoContacto { get; set; }
    }
}
