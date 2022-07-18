using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Axede.DataObjects.Interface;

namespace Axede.DataObjects.Base
{
    public static class DataAccess
    {

        private static readonly string sConnectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
        private static readonly IDaoFactory oFactoria = DaoFactories.GetFactory(sConnectionStringName);

        public static ICommonDao CommonDao 
        { 
            get 
            {
                return oFactoria.CommonDao;
            } 
        }

    }
}
