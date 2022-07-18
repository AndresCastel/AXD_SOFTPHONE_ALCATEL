using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axede.DataObjects.Base
{
    public class DaoFactories
    {
        public static IDaoFactory GetFactory(string dataProvider)
        {
            // Return the requested DaoFactory
            switch (dataProvider)
            {
                case "MySQL":
                    return new Axede.DataObjects.Dao.MySQL.MySQLDaoFactory();

                default:
                    return new Axede.DataObjects.Dao.MySQL.MySQLDaoFactory();
            }
        }
    }
}
