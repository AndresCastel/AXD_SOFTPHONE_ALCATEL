using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Axede.ProxyService
{
    [Serializable]
    public partial class ProxyService : IProxyService
    {

        public static string sURL_XMLApiFrameworkService_OTUC5
        {
            get
            {
                string sURL_XMLApiFrameworkService_OTUC5 = ConfigurationManager.AppSettings["URL_XMLApiFrameworkService_OTUC5"];
                if (!string.IsNullOrEmpty(sURL_XMLApiFrameworkService_OTUC5))
                {
                    return sURL_XMLApiFrameworkService_OTUC5;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string sURL_XMLMessagingService_OTUC5
        {
            get
            {
                string sURL_XMLMessagingService_OTUC5 = ConfigurationManager.AppSettings["URL_XMLMessagingService_OTUC5"];
                if (!string.IsNullOrEmpty(sURL_XMLMessagingService_OTUC5))
                {
                    return sURL_XMLMessagingService_OTUC5;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        

        public static string sXMLApiFrameworkService_OTUC5_User
        {
            get
            {
                string sXMLApiFrameworkService_OTUC5_User = ConfigurationManager.AppSettings["XMLApiFrameworkService_OTUC5_User"];
                if (!string.IsNullOrEmpty(sXMLApiFrameworkService_OTUC5_User))
                {
                    return sXMLApiFrameworkService_OTUC5_User;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string sXMLApiFrameworkService_OTUC5_Pass
        {
            get
            {
                string sXMLApiFrameworkService_OTUC5_Pass = ConfigurationManager.AppSettings["XMLApiFrameworkService_OTUC5_Pass"];
                if (!string.IsNullOrEmpty(sXMLApiFrameworkService_OTUC5_Pass))
                {
                    return sXMLApiFrameworkService_OTUC5_Pass;
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        private static XMLApiFrameworkService_OTUC5.XmlApiFrameworkService _XMLApiFrameworkService_OTUC5 { get; set; }
        private static XMLMessagingService_OTUC5.XmlMessagingService _XMLMessagingService_OTUC5 { get; set; }

        private string _IDSessionAPIFramework = string.Empty;
        private string _IDSessionMessagingService = string.Empty;

        static ProxyService()
        {
            _XMLApiFrameworkService_OTUC5 = new XMLApiFrameworkService_OTUC5.XmlApiFrameworkService();
            if (sURL_XMLApiFrameworkService_OTUC5 != string.Empty)
            {
                _XMLApiFrameworkService_OTUC5.Url = sURL_XMLApiFrameworkService_OTUC5;
             
            }

            _XMLMessagingService_OTUC5 = new XMLMessagingService_OTUC5.XmlMessagingService();
            if (sURL_XMLMessagingService_OTUC5 != string.Empty) 
            {

            } _XMLMessagingService_OTUC5.Url = sURL_XMLMessagingService_OTUC5;

        }

    }
}
