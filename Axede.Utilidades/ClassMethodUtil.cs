using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Axede.Utilidades
{
    public class ClassMethodUtil
    {
        public Window ResolveOwnerWindow()
        {
            Window owner = null;
            if (System.Windows.Application.Current != null)
            {
                foreach (Window w in System.Windows.Application.Current.Windows)
                {
                    if (w.IsActive)
                    {
                        owner = w;
                        break;
                    }
                }
            }
            return owner;
        }
    }
}
