using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.OracleClient;
using EntLibContrib.Data.MySql;

namespace Axede.DataObjects
{
    public class BaseDatos
    {

       
        private static readonly string sConnectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
        private static readonly string sConnectionString = ConfigurationManager.ConnectionStrings[sConnectionStringName].ConnectionString;
       
        protected MySqlDatabase _database
        {
            get
            {
                MySqlDatabase oDatabase = null;
                oDatabase = new MySqlDatabase(sConnectionString);
            
                return oDatabase;
            }
        }

        public MySqlDatabase Database
        {
            get { return _database; }
        } 

        public BaseDatos()
        {

        }


    }
}
