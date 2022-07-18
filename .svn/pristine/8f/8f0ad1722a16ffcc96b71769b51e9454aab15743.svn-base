using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Axede.Validadores.WPF
{
    public class ValidadorWPF
    {
        private ValidationResult a;

        private bool _IsValid;

        public bool IsValid
        {
            get { return _IsValid; }
            set
            {
                _IsValid = value;
                TextBox_Error(ControlEnvio, a);
            }
        }

        private bool _Ban;

        public bool Ban
        {
            get { return _Ban; }
            set { _Ban = value; }
        }


        private bool _ValidateUC;

        public bool ValidateUC
        {
            get { if (!Ban) return _ValidateUC; else return false; }
            set { _ValidateUC = value; }
        }

        private Control _controlEnvio;

        public Control ControlEnvio
        {
            get { return _controlEnvio; }
            set { _controlEnvio = value; }
        }

        private bool _EsRequerido;

        public bool EsRequerido
        {
            get { return _EsRequerido; }
            set
            {
                _EsRequerido = value;
                if (_EsRequerido)
                    ValidacionRequerido();
                else
                    IsValid = true;
            }
        }

        public ValidadorWPF()
        {
            ValidateUC = true;
        }

        #region VALIDACIONES
        public void ValidacionRequerido()
        {
            string sText = string.Empty;

            if (ControlEnvio.GetType().ToString() == "System.Windows.Controls.TextBox")
            {
                sText = ((TextBox)ControlEnvio).Text;
            }
            else if (ControlEnvio.GetType().ToString() == "System.Windows.Controls.PasswordBox")
            {
                sText = ((PasswordBox)ControlEnvio).Password;
            }

            if (string.IsNullOrEmpty(sText))
            {
                a = new ValidationResult(false, MensajeRequerido);
                IsValid = false;
                Ban = true;
            }
            else { IsValid = true; }
        }
        #endregion


        private void TextBox_Error(object sender, ValidationResult e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            if (!IsValid)
            {
                ((Control)sender).ToolTip = e.ErrorContent.ToString();
                ((Control)sender).BorderBrush = (Brush)converter.ConvertFromString("Red");
            }
            else
            {
                ((Control)sender).ToolTip = "";
                ((Control)sender).BorderBrush = (Brush)converter.ConvertFromString("#C6C6C5");
            }

        }

        public void ValidarControl()
        {
            if (EsRequerido)
            {
                ValidacionRequerido();
            }
        }

        //Mensajes
        public string MensajeRequerido = "Campo no puede estar vacio";
    }
}
