﻿using Axede.BussinesObject.Application;
using Axede.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Axede.Utilidades.Enums;
using Axede.Mensajes;


namespace Axede.Model.SoftPhone
{
    public partial class Model : IModel
    {
        public ResultadoOperacion ObtenerListaContactos(Contactos oContacto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            List<DtoContactos> lstContacto = new List<DtoContactos>();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
            oUserInfo = ObtenerUserInfo();

            if (oUserInfo != null)
            {
                oContacto.Ide_User = oUserInfo.Ide_User;
            }

          
                lstContacto = oCommonDao.ObtenerContactos(oContacto.Ide_User, oContacto.Nom_Name);
            }
            catch (Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                string sMensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_ErrorConsultandoBaseDatos);
                oResultadoOperacion.Mensaje = sMensaje;
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = lstContacto;

            return oResultadoOperacion;
        }

        public ResultadoOperacion InsertarContacto(Contactos oContacto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
            oUserInfo = ObtenerUserInfo();

            if (oUserInfo != null)
            {
                oContacto.Ide_User = oUserInfo.Ide_User;
            }

            if (oContacto.Num_Extension == null)
            {
                oContacto.Num_Extension = string.Empty;
            }

            //Validar si el Usuario ya existe con el mismo num de teléfono
            oContacto.Ide_Contact = 0;
            if (ValidarContactoExiste(oContacto))
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Contact_ErrExisteContacto);
                return oResultadoOperacion;
            }
                oCommonDao.InsContactos(oContacto);
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.Message;
                return oResultadoOperacion;
            }

            oResultadoOperacion.oEstado = TipoRespuesta.Exito;
            oResultadoOperacion.Mensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.InsContact_MsInsContact);
            return oResultadoOperacion;
        }

        public ResultadoOperacion ModificarContacto(Contactos oContacto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
            oUserInfo = ObtenerUserInfo();

            if (oUserInfo != null)
            {
                oContacto.Ide_User = oUserInfo.Ide_User;
            }

            if (oContacto.Num_Extension == null)
            {
                oContacto.Num_Extension = string.Empty;
            }

            //Validar si el Usuario ya existe con el mismo num de teléfono
            if (ValidarContactoExiste(oContacto))
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.Contact_ErrExisteContacto);
                return oResultadoOperacion;
            }
                oCommonDao.UpdContactos(oContacto);
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.Message;
                return oResultadoOperacion;
            }

            oResultadoOperacion.oEstado = TipoRespuesta.Exito;
            oResultadoOperacion.Mensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.UpdContact_MsUpdContact);
            return oResultadoOperacion;
        }

        public ResultadoOperacion EliminarContacto(Contactos oContacto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
           
            try
            {
                oCommonDao.DelContactos(oContacto);
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.Message;
                return oResultadoOperacion;
            }

            oResultadoOperacion.oEstado = TipoRespuesta.Exito;

            return oResultadoOperacion;
        }

        public ResultadoOperacion InsertarFavorito(Contactos oContacto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
            oUserInfo = ObtenerUserInfo();
            if (oUserInfo != null)
            {
                oContacto.Ide_User = oUserInfo.Ide_User;
            }

            
           
                oCommonDao.InsRecentContactos(oContacto);
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.Message;
                return oResultadoOperacion;
            }

            oResultadoOperacion.oEstado = TipoRespuesta.Exito;
            return oResultadoOperacion;
        }

        public ResultadoOperacion EliminarFavorito(Contactos oContacto)
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
           
            oUserInfo = ObtenerUserInfo();

            if (oUserInfo != null)
            {
                oContacto.Ide_User = oUserInfo.Ide_User;
            }
           
                oCommonDao.DelRecentUser(oContacto);
            }
            catch (Exception ex)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                oResultadoOperacion.Mensaje = ex.Message;
                return oResultadoOperacion;
            }

            oResultadoOperacion.oEstado = TipoRespuesta.Exito;

            return oResultadoOperacion;
        }

        private DtoUserInfo ObtenerUserInfo()
        {
            DtoUserInfo oUserInfo = new DtoUserInfo();
            string sExt = General.sUserName;
            try
            {
                oUserInfo = oCommonDao.ObtenerInfoUser(sExt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oUserInfo;
        }

        private bool ValidarContactoExiste(Contactos oContacto)
        {
            bool _ContactoExiste = false;
            List<DtoContactos> lstContacto = new List<DtoContactos>();

            try
            {
                lstContacto = oCommonDao.ObtenerContactosxNumTelefono(oContacto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (lstContacto != null && lstContacto.Count > 0)
            {
                if (oContacto.Ide_Contact == 0) // Nuevo Contacto
                {
                    foreach (DtoContactos item in lstContacto)
                    {
                        if (item.Telefono == oContacto.Vlr_PhoneNumber && item.Extension == oContacto.Num_Extension)
                        {
                            return _ContactoExiste = true;
                        }
                    }
                }
                else // Contacto Existente
                {
                    foreach (DtoContactos item in lstContacto)
                    {
                        if (item.Ide_Contact != oContacto.Ide_Contact)
                        {
                            if (item.Telefono == oContacto.Vlr_PhoneNumber && item.Extension == oContacto.Num_Extension)
                            {
                                return _ContactoExiste = true;
                            }
                        }
                    }
                }
            }

            return _ContactoExiste;
        }

        public ResultadoOperacion ObtenerListaContactosFavoritos()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            List<DtoContactos> lstContacto = new List<DtoContactos>();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
            oUserInfo = ObtenerUserInfo();
            lstContacto = oCommonDao.ObtenerContactosFavoritos(General.sUserName);
            }
            catch (Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                string sMensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_ErrorConsultandoBaseDatos);
                oResultadoOperacion.Mensaje = sMensaje;
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = lstContacto;

            return oResultadoOperacion;
        }

        public ResultadoOperacion getContactAndRecentContact()
        {
            ResultadoOperacion oResultadoOperacion = new ResultadoOperacion();
            List<DtoContactos> lstContacto = new List<DtoContactos>();
            DtoUserInfo oUserInfo = new DtoUserInfo();
            try
            {
            oUserInfo = ObtenerUserInfo();
                if (oUserInfo != null)
                {
                    lstContacto = oCommonDao.getContactAndRecentContact(oUserInfo.Ide_User);
                }
            }
            catch (Exception)
            {
                oResultadoOperacion.oEstado = TipoRespuesta.Error;
                string sMensaje = AdministradorMensaje.Instance.GetMensajePorCodigo(Mensajes.CodigoMensaje.General_ErrorConsultandoBaseDatos);
                oResultadoOperacion.Mensaje = sMensaje;
                return oResultadoOperacion;
            }

            oResultadoOperacion.ListaEntidadDatos = lstContacto;

            return oResultadoOperacion;
        }

    }
}