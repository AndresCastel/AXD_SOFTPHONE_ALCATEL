using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.DataObjects.Interface;
using System.Data;
using System.Data.Common;
using System.Xml;
using Axede.Utilidades.Enums;
using Axede.Global;
using Axede.Exception;
using Axede.Utilidades;
using Axede.Mensajes;
using Axede.DataObjects.Entities;
using System.Xml.Serialization;
using System.Reflection;
using System.Transactions;
using Axede.BussinesObject.Application;


namespace Axede.DataObjects.Dao.MySQL
{
    public partial class MySQL_CommonDao : BaseDatos, ICommonDao
    {
        #region Definiciones
        #endregion

        #region Consultas_Ix_V8
        public List<DtoContactos> ObtenerContactos(int iIdeUser, string sInitialLetter)
        {
            List<DtoContactos> lstDtoContacto = new List<DtoContactos>();
            DtoContactos oDtoContacto = null;

            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.GetContacts);
                _database.AddInParameter(dbcommand, "iIdeUser", DbType.Int32, iIdeUser);
                _database.AddInParameter(dbcommand, "sInitialLetter", DbType.String, sInitialLetter);
                _database.AddInParameter(dbcommand, "iPage", DbType.Int32, "1");
                _database.AddInParameter(dbcommand, "iPageSize", DbType.Int32, "10");
                _database.ExecuteNonQuery(dbcommand);

                string sExt = string.Empty;

                using (IDataReader dataReader = _database.ExecuteReader(dbcommand))
                {
                    while (dataReader.Read())
                    {
                        oDtoContacto = new DtoContactos();

                        oDtoContacto.Ide_Contact = dataReader["Ide_Contact"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Ide_Contact"]);
                        oDtoContacto.Ide_User = Convert.ToInt32(dataReader["Ide_User"]);
                        oDtoContacto.Nombre = dataReader["Nom_Name"].ToString().ToUpper();
                        oDtoContacto.Apellido = dataReader["Nom_LastName"].ToString().ToUpper();
                        oDtoContacto.NombreCompleto = oDtoContacto.Nombre + " " + oDtoContacto.Apellido;

                        if (dataReader["OwnerRecentUser"].ToString().Equals("0"))
                        {
                            oDtoContacto.RecentContact = false;
                        }
                        else
                        {
                            oDtoContacto.RecentContact = true;
                        }
                        oDtoContacto.Telefono = dataReader["Vlr_PhoneNumber"] == DBNull.Value ? string.Empty : dataReader["Vlr_PhoneNumber"].ToString();
                        oDtoContacto.Ide_PhoneType = dataReader["Ide_PhoneType"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Ide_PhoneType"]);
                        sExt = dataReader["Ide_CommunicationServer"] == DBNull.Value ? string.Empty : dataReader["Ide_CommunicationServer"].ToString();
                        oDtoContacto.Extension = dataReader["Num_Extension"] == DBNull.Value ? sExt : dataReader["Num_Extension"].ToString();
                        oDtoContacto.Vlr_UserState = dataReader["Vlr_UserState"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Vlr_UserState"]);

                        lstDtoContacto.Add(oDtoContacto);
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw new DataBaseMySqlException(Globales.NombreAplicacion, ex.Message, ex);
            }


            return lstDtoContacto;
        }

        public List<DtoContactos> ObtenerContactosFavoritos(string sExte)
        {
            List<DtoContactos> lstDtoContacto = new List<DtoContactos>();
            DtoContactos oDtoContacto = null;

            try
            {
                DbCommand dbcommand1 = _database.GetStoredProcCommand(PROCEDIMIENTOS.GetUserInfo);
                _database.AddInParameter(dbcommand1, "sExt", DbType.String, sExte);
                _database.ExecuteNonQuery(dbcommand1);
                Int32 id_user = 0;
                using (IDataReader dataReader = _database.ExecuteReader(dbcommand1))
                {
                    while (dataReader.Read())
                    {
                        id_user = Convert.ToInt32(dataReader["Ide_User"]);
                    }
                }

                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.getContactAndRecentContact);
                _database.AddInParameter(dbcommand, "ideUser", DbType.Int32, id_user);
                _database.ExecuteNonQuery(dbcommand);

               string sExt = string.Empty;

                using (IDataReader dataReader = _database.ExecuteReader(dbcommand))
                {
                    while (dataReader.Read())
                    {
                        oDtoContacto = new DtoContactos();

                        oDtoContacto.Ide_User = id_user;
                        oDtoContacto.Nombre = dataReader["Nom_Name"].ToString().ToUpper();
                        oDtoContacto.NombreCompleto = dataReader["Nom_Name"].ToString().ToUpper() + " " + dataReader["Nom_LastName"].ToString().ToUpper(); ;
                        oDtoContacto.Telefono = dataReader["Vlr_PhoneNumber"] == DBNull.Value ? string.Empty : dataReader["Vlr_PhoneNumber"].ToString();
                        if (string.IsNullOrEmpty(dataReader["Ide_PhoneType"].ToString()))
                        { oDtoContacto.Ide_PhoneType = 0; }
                        else
                        {
                            oDtoContacto.Ide_PhoneType = dataReader["Ide_PhoneType"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Ide_PhoneType"]);
                        }
                        oDtoContacto.Extension = dataReader["Ide_CommunicationServer"] == DBNull.Value ? sExt : dataReader["Ide_CommunicationServer"].ToString();

                        lstDtoContacto.Add(oDtoContacto);
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw new DataBaseMySqlException(Globales.NombreAplicacion, ex.Message, ex);
            }


            return lstDtoContacto;
        }

        public List<DtoContactos> getContactAndRecentContact(int iIdeUser)
        {
            List<DtoContactos> lstDtoContacto = new List<DtoContactos>();
            DtoContactos oDtoContacto = null;

            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.getContactAndRecentContact);
                _database.AddInParameter(dbcommand, "ideUser", DbType.Int32, iIdeUser);
                _database.ExecuteNonQuery(dbcommand);

                string sExt = string.Empty;

                using (IDataReader dataReader = _database.ExecuteReader(dbcommand))
                {
                    while (dataReader.Read())
                    {
                        oDtoContacto = new DtoContactos();

                        oDtoContacto.Ide_Contact = dataReader["Ide_Contact"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Ide_Contact"]);
                        oDtoContacto.Ide_User = Convert.ToInt32(dataReader["Ide_User"]);
                        oDtoContacto.Nombre = dataReader["Nom_Name"].ToString().ToUpper();
                        oDtoContacto.Apellido = dataReader["Nom_LastName"].ToString().ToUpper();
                        oDtoContacto.NombreCompleto = oDtoContacto.Nombre + " " + oDtoContacto.Apellido;
                        oDtoContacto.Telefono = dataReader["Vlr_PhoneNumber"] == DBNull.Value ? string.Empty : dataReader["Vlr_PhoneNumber"].ToString();
                        if (string.IsNullOrEmpty(dataReader["Ide_PhoneType"].ToString()))
                        {
                            oDtoContacto.Ide_PhoneType = 0;
                        }
                        else
                        {
                            oDtoContacto.Ide_PhoneType = dataReader["Ide_PhoneType"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Ide_PhoneType"]);
                        }
                        oDtoContacto.Extension = dataReader["Num_Extension"] == DBNull.Value ? sExt : dataReader["Num_Extension"].ToString();
                        if (string.IsNullOrEmpty(oDtoContacto.Extension))
                        {
                            oDtoContacto.Extension = dataReader["Ide_CommunicationServer"] == DBNull.Value ? sExt : dataReader["Ide_CommunicationServer"].ToString();
                        }
                        oDtoContacto.RecentContact = dataReader["OwnerRecentUser"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["OwnerRecentUser"]);
                        lstDtoContacto.Add(oDtoContacto);
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw new DataBaseMySqlException(Globales.NombreAplicacion, ex.Message, ex);
            }


            return lstDtoContacto;
        }

        public DtoUserInfo ObtenerInfoUser(string sExt)
        {
            DtoUserInfo oDtoUserInfo = null;
            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.GetUserInfo);
                _database.AddInParameter(dbcommand, "sExt", DbType.String, sExt);
                _database.ExecuteNonQuery(dbcommand);

                using (IDataReader dataReader = _database.ExecuteReader(dbcommand))
                {
                    while (dataReader.Read())
                    {
                        oDtoUserInfo = new DtoUserInfo();

                        oDtoUserInfo.Ide_User = Convert.ToInt32(dataReader["Ide_User"]);
                        oDtoUserInfo.Nom_User = dataReader["Nom_User"].ToString().ToUpper();
                        oDtoUserInfo.Ide_CommunicationServer = dataReader["Ide_CommunicationServer"].ToString().ToUpper();
                        oDtoUserInfo.Nom_Name = dataReader["Nom_Name"].ToString().ToUpper();
                        oDtoUserInfo.Nom_LastName = dataReader["Nom_LastName"].ToString().ToUpper();
                        oDtoUserInfo.Nom_DomainName = dataReader["Nom_DomainName"].ToString().ToUpper();
                        oDtoUserInfo.Vlr_UserState = dataReader["Vlr_UserState"].ToString().ToUpper();
                        oDtoUserInfo.Vlr_Email = dataReader["Vlr_Email"].ToString().ToUpper();
                        oDtoUserInfo.Vlr_AutomaticLogin = dataReader["Vlr_AutomaticLogin"].ToString().ToUpper();
                        oDtoUserInfo.Vlr_ExternalUser = dataReader["Vlr_ExternalUser"].ToString().ToUpper();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new DataBaseMySqlException(Globales.NombreAplicacion, ex.Message, ex);
            }

            return oDtoUserInfo;
        }

        public List<DtoContactos> ObtenerContactosxNumTelefono(Contactos oContacto)
        {
            List<DtoContactos> lstDtoContacto = new List<DtoContactos>();
            DtoContactos oDtoContacto = null;
            string sExt = string.Empty;

            DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.GetContactsByPhone);
            _database.AddInParameter(dbcommand, "vlrPhoneNumber", DbType.String, oContacto.Vlr_PhoneNumber);
            _database.AddInParameter(dbcommand, "ideUser", DbType.Int32, oContacto.Ide_User);
            _database.ExecuteNonQuery(dbcommand);

            using (IDataReader dataReader = _database.ExecuteReader(dbcommand))
            {
                while (dataReader.Read())
                {
                    oDtoContacto = new DtoContactos();

                    oDtoContacto.Ide_Contact = dataReader["Ide_Contact"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Ide_Contact"]);
                    oDtoContacto.Ide_User = Convert.ToInt32(dataReader["Ide_User"]);
                    oDtoContacto.Telefono = dataReader["Vlr_PhoneNumber"] == DBNull.Value ? string.Empty : dataReader["Vlr_PhoneNumber"].ToString();
                    oDtoContacto.Extension = dataReader["Num_Extension"] == DBNull.Value ? string.Empty : dataReader["Num_Extension"].ToString();

                    lstDtoContacto.Add(oDtoContacto);
                }
            }

            return lstDtoContacto;
        }

        public bool InsContactos(Contactos oContactos)
        {
            bool bContactoInsertado = false;
            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.InsContacto);
                _database.AddInParameter(dbcommand, "ideUser", DbType.Int32, oContactos.Ide_User);
                _database.AddInParameter(dbcommand, "nomName", DbType.String, oContactos.Nom_Name);
                _database.AddInParameter(dbcommand, "nomLastName", DbType.String, oContactos.Nom_LastName);
                _database.AddInParameter(dbcommand, "nomGroupName", DbType.String, oContactos.Nom_GroupName);
                _database.AddInParameter(dbcommand, "vlrPhoneNumber", DbType.String, oContactos.Vlr_PhoneNumber);
                _database.AddInParameter(dbcommand, "idePhoneType", DbType.Int32, oContactos.Ide_PhoneType);
                _database.AddInParameter(dbcommand, "numExtension", DbType.String, oContactos.Num_Extension);
                _database.AddInParameter(dbcommand, "nomOrganization", DbType.String, oContactos.Nom_Organization);
                _database.AddInParameter(dbcommand, "desComments", DbType.String, oContactos.Des_Comments);
                _database.ExecuteNonQuery(dbcommand);

                bContactoInsertado = true;
            }
            catch (System.Exception oException)
            {
                StringBuilder sMensaje = new StringBuilder();
                sMensaje.AppendLine("Procedimiento : " + PROCEDIMIENTOS.InsContacto);
                sMensaje.AppendLine("Parametro   : ");
                sMensaje.AppendLine(oContactos.Ide_User.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_Name.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_LastName.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_GroupName.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Vlr_PhoneNumber.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Ide_PhoneType.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Num_Extension.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_Organization.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Des_Comments.ToString());

                throw new DataBaseMySqlException(Globales.NombreAplicacion.ToUpper(), sMensaje.ToString() + oException.Message, oException);
            }

            return bContactoInsertado;
        }

        public bool UpdContactos(Contactos oContactos)
        {
            bool bContactoInsertado = false;
            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.UpdContact);
                _database.AddInParameter(dbcommand, "ideContact", DbType.Int32, oContactos.Ide_Contact);
                _database.AddInParameter(dbcommand, "ideUser", DbType.Int32, oContactos.Ide_User);
                _database.AddInParameter(dbcommand, "nomName", DbType.String, oContactos.Nom_Name);
                _database.AddInParameter(dbcommand, "nomLastName", DbType.String, oContactos.Nom_LastName);
                _database.AddInParameter(dbcommand, "nomGroupName", DbType.String, oContactos.Nom_GroupName);
                _database.AddInParameter(dbcommand, "vlrPhoneNumber", DbType.String, oContactos.Vlr_PhoneNumber);
                _database.AddInParameter(dbcommand, "idePhoneType", DbType.Int32, oContactos.Ide_PhoneType);
                _database.AddInParameter(dbcommand, "numExtension", DbType.String, oContactos.Num_Extension);
                _database.AddInParameter(dbcommand, "nomOrganization", DbType.String, oContactos.Nom_Organization);
                _database.AddInParameter(dbcommand, "desComments", DbType.String, oContactos.Des_Comments);
                _database.ExecuteNonQuery(dbcommand);

                bContactoInsertado = true;
            }
            catch (System.Exception oException)
            {
                StringBuilder sMensaje = new StringBuilder();
                sMensaje.AppendLine("Procedimiento : " + PROCEDIMIENTOS.InsContacto);
                sMensaje.AppendLine("Parametro   : ");
                sMensaje.AppendLine(oContactos.Ide_User.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_Name.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_LastName.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_GroupName.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Vlr_PhoneNumber.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Ide_PhoneType.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Num_Extension.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Nom_Organization.ToString());
                sMensaje.AppendLine(" - ");
                sMensaje.AppendLine(oContactos.Des_Comments.ToString());

                throw new DataBaseMySqlException(Globales.NombreAplicacion.ToUpper(), sMensaje.ToString() + oException.Message, oException);
            }

            return bContactoInsertado;
        }

        public bool DelContactos(Contactos oContactos)
        {
            bool bContactoEliminado = false;
            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.DelContacto);
                _database.AddInParameter(dbcommand, "iIdeContact", DbType.Int32, oContactos.Ide_Contact);
                _database.ExecuteNonQuery(dbcommand);

                bContactoEliminado = true;
            }
            catch (System.Exception oException)
            {
                StringBuilder sMensaje = new StringBuilder();
                sMensaje.AppendLine("Procedimiento : " + PROCEDIMIENTOS.DelContacto);
                sMensaje.AppendLine("Parametro   : ");
                sMensaje.AppendLine(oContactos.Ide_Contact.ToString());

                throw new DataBaseMySqlException(Globales.NombreAplicacion.ToUpper(), sMensaje.ToString() + oException.Message, oException);
            }

            return bContactoEliminado;
        }

        public bool InsRecentContactos(Contactos oContactos)
        {
            bool bContactoInsertado = false;

            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.InsRecentUser);
                _database.AddInParameter(dbcommand, "iIdeUser", DbType.Int32, oContactos.Ide_User);
                _database.AddInParameter(dbcommand, "iIdeRecentUser", DbType.Int32, oContactos.Ide_RecentUser);
                _database.ExecuteNonQuery(dbcommand);

                bContactoInsertado = true;
            }
            catch (System.Exception oException)
            {
                StringBuilder sMensaje = new StringBuilder();
                sMensaje.AppendLine("Procedimiento : " + PROCEDIMIENTOS.InsContacto);
                sMensaje.AppendLine("Parametro   : ");
                sMensaje.AppendLine(oContactos.Ide_User.ToString());
                throw new DataBaseMySqlException(Globales.NombreAplicacion.ToUpper(), sMensaje.ToString() + oException.Message, oException);
            }

            return bContactoInsertado;
        }

        public bool DelRecentUser(Contactos oContactos)
        {
            bool bContactoEliminado = false;
            try
            {
                DbCommand dbcommand = _database.GetStoredProcCommand(PROCEDIMIENTOS.DelRecentUser);
                _database.AddInParameter(dbcommand, "iIde_User", DbType.Int32, oContactos.Ide_User);
                _database.AddInParameter(dbcommand, "iIde_RecentUser", DbType.Int32, oContactos.Ide_RecentUser);
                _database.ExecuteNonQuery(dbcommand);

                bContactoEliminado = true;
            }
            catch (System.Exception oException)
            {
                StringBuilder sMensaje = new StringBuilder();
                sMensaje.AppendLine("Procedimiento : " + PROCEDIMIENTOS.DelContacto);
                sMensaje.AppendLine("Parametro   : ");
                sMensaje.AppendLine(oContactos.Ide_Contact.ToString());

                throw new DataBaseMySqlException(Globales.NombreAplicacion.ToUpper(), sMensaje.ToString() + oException.Message, oException);
            }

            return bContactoEliminado;
        }

        #endregion

        #region Privados

      #endregion
    }
}
