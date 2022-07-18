using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Axede.Utilidades
{
    [Serializable]
    public class Serializar
    {
        public virtual string ToSerialize(bool IgnoreNamespace)
        {
            if ((this == null))
            {
                throw new ApplicationException("No puede serializarse un objeto nulo.");
            }
            XmlSerializer xs = new XmlSerializer(this.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            StringBuilder sb = new StringBuilder();

            try
            {
                using (StringWriter sw = new StringWriter(sb))
                {
                    if (IgnoreNamespace)
                    {
                        ns.Add("", "");
                    }
                    xs.Serialize(sw, this, ns);
                    sw.Flush();
                }

                return sb.ToString();
            }
            catch (System.Exception Ex)
            {
                throw Ex;
            }

        }
    }
}
