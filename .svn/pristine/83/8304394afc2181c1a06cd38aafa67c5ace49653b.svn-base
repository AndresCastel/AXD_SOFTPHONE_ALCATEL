using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.DataObjects.Base;
using Axede.DataObjects.Interface;

namespace Axede.DataObjects.Dao.MySQL
{
    public class MySQLDaoFactory : IDaoFactory
    {
        public ICommonDao CommonDao
        {
            get
            {
                return new MySQL_CommonDao();
            }
        }
    }
}
