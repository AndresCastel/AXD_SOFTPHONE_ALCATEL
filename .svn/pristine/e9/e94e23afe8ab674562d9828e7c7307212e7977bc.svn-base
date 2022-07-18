using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axede.Model.SoftPhone;
using Axede.WPF.View.Softphone;

namespace Axede.WPF.Presenter.Softphone
{
    public class Presenter<T> where T : IView
    {

        protected static IModel Model { get; private set; }

        static Presenter()
        {
            if (Model == null)
            {
                Model = new Axede.Model.SoftPhone.Model();
            }
        }

        public Presenter(T view)
        {
            View = view;
        }

        protected T View { get; private set; }



    }
}
