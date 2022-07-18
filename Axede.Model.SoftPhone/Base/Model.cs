﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.DataObjects.Interface;
using Axede.DataObjects.Base;
using Axede.ProxyService;

namespace Axede.Model.SoftPhone
{
    public partial class Model : IModel
    {
        private static readonly ICommonDao oCommonDao = DataAccess.CommonDao;
        private static IProxyService oProxyService { get; set; }

        static Model()
        {

            oProxyService = new Axede.ProxyService.ProxyService();
        }

    }
}
