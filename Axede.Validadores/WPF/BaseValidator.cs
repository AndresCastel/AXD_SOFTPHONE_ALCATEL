using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Axede.Utilidades.Enums;
using Microsoft.Windows.Controls;

namespace Axede.Validadores.WPF
{
    public static class ValidacionWPF
    {

        public static bool ValidarContenidosIguales(this object oObject, object oObject2, string sErrorMessaje)
        {
            var converter = new System.Windows.Media.BrushConverter();

            CompareFieldValidator oCompareFieldValidator = new CompareFieldValidator();
            oCompareFieldValidator.ErrorMessage = sErrorMessaje;

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string sInputString = GetInputString(oObject);
            string sInputString1 = GetInputString(oObject2);

            ValidationResult oValidationResult = oCompareFieldValidator.Validate(sInputString, sInputString1, currentCulture);

            SetStyleValidationObject(oObject, oValidationResult.IsValid);
            SetToolTipValidationObject(oObject, oValidationResult.IsValid, sInputString, sErrorMessaje);

            return oValidationResult.IsValid;
        }

        public static bool ValidarRequerido(this object oObject, string sErrorMessaje, string sNormalStyle = "", string sValidationStyle = "")
        {
            var converter = new System.Windows.Media.BrushConverter();

            RequiredFieldValidator oRequiredFieldValidator = new RequiredFieldValidator();
            oRequiredFieldValidator.ErrorMessage = sErrorMessaje;

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string sInputString = GetInputString(oObject);
            if (sInputString == null)
                sInputString = string.Empty;
            ValidationResult oValidationResult = oRequiredFieldValidator.Validate(sInputString, currentCulture);
            SetStyleValidationObject(oObject, oValidationResult.IsValid, sNormalStyle, sValidationStyle);
            SetToolTipValidationObject(oObject, oValidationResult.IsValid, sInputString, sErrorMessaje);

            return oValidationResult.IsValid;
        }
        /// <summary>
        /// valida combos que el primer elemento valido es un Index = 0
        /// </summary>
        /// <param name="oObject"></param>
        /// <param name="sErrorMessaje"></param>
        /// <returns></returns>
        public static bool ValidarRequerido(this object oObject, string sErrorMessaje, TipoLista oTipoLista, string sNormalStyle = "", string sValidationStyle = "")
        {
            var converter = new System.Windows.Media.BrushConverter();

            RequiredFieldValidator oRequiredFieldValidator = new RequiredFieldValidator();
            oRequiredFieldValidator.ErrorMessage = sErrorMessaje;
            ValidationResult oValidationResult = null;

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            if (oTipoLista == TipoLista.Lista)
            {
                string sInputString = string.Empty;
                if (oObject.GetType().ToString() == "System.Windows.Controls.ComboBox")
                {
                    if (((ComboBox)oObject).SelectedValue == null || (int)((ComboBox)oObject).SelectedValue == 0)
                    {
                        sInputString = string.Empty;
                    }
                    else
                    {
                        sInputString = ((ComboBox)oObject).Text;
                    }
                }

                if (sInputString == null)
                    sInputString = string.Empty;
                oValidationResult = oRequiredFieldValidator.Validate(sInputString, currentCulture);
                SetStyleValidationObject(oObject, oValidationResult.IsValid, sNormalStyle, sValidationStyle);
                SetToolTipValidationObject(oObject, oValidationResult.IsValid, sInputString, sErrorMessaje);
            }
            return oValidationResult.IsValid;
        }

        public static bool ValidarExpresionRegular(this object oObject, string sErrorMessaje, string sRegExpresion)
        {
            var converter = new System.Windows.Media.BrushConverter();

            RegexValidationRule oRegExValidator = new RegexValidationRule();
            oRegExValidator.ErrorMessage = sErrorMessaje;
            oRegExValidator.Pattern = sRegExpresion;

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string sInputString = GetInputString(oObject);

            if (sInputString.Contains("\r\n"))
            {
                sInputString = sInputString.Replace("\r\n", " ");
            }

            if (sInputString.Contains("\n"))
            {
                sInputString = sInputString.Replace("\n", " ");
            }

            ValidationResult oValidationResult = oRegExValidator.Validate(sInputString, currentCulture);
            SetStyleValidationObject(oObject, oValidationResult.IsValid);
            SetToolTipValidationObject(oObject, oValidationResult.IsValid, sInputString, sErrorMessaje);


            return oValidationResult.IsValid;
        }

        public static bool ValidarMensajeLibreError(this object oObject, string sErrorMessaje)
        {
            var converter = new System.Windows.Media.BrushConverter();

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string sInputString = GetInputString(oObject);

            ValidationResult oValidationResult = new ValidationResult(false, sErrorMessaje);
            SetStyleValidationObject(oObject, oValidationResult.IsValid);
            SetToolTipValidationObject(oObject, oValidationResult.IsValid, sInputString, sErrorMessaje);


            return oValidationResult.IsValid;
        }


        private static string GetInputString(object oObject)
        {
            string sInputString = string.Empty;

            if (oObject.GetType().ToString() == "System.Windows.Controls.TextBox")
            {
                sInputString = ((TextBox)oObject).Text;
            }
            else if (oObject.GetType().ToString() == "System.Windows.Controls.PasswordBox")
            {
                sInputString = ((PasswordBox)oObject).Password;
            }
            else if (oObject.GetType().ToString() == "System.Windows.Controls.DatePicker")
            {
                sInputString = ((DatePicker)oObject).Text;
            }         
            else if (oObject.GetType().ToString() == "System.Windows.Controls.ComboBox")
            {
                if (((ComboBox)oObject).SelectedIndex == 0)
                {
                    sInputString = string.Empty;
                }
                else
                {
                    sInputString = ((ComboBox)oObject).Text;
                }
            }
            else if (oObject.GetType().ToString() == "Microsoft.Windows.Controls.MaskedTextBox")
            {
                sInputString = ((MaskedTextBox)oObject).Value == null ? string.Empty : ((MaskedTextBox)oObject).Value.ToString();
            }

            return sInputString;
        }

        private static void SetToolTipValidationObject(object oObject, bool oIsValid, string sInputString, string sErrorMessaje)
        {
            if (oIsValid)
            {
                if (oObject.GetType().ToString() != "System.Windows.Controls.PasswordBox")
                {
                    ((Control)oObject).ToolTip = sInputString;
                }
                else
                {
                    ((Control)oObject).ToolTip = "**********";
                }

            }
            else
            {
                ToolTip oToolTip = new ToolTip();
                oToolTip.Content = "  " + sErrorMessaje;

                oToolTip.Style = (Style)Application.Current.FindResource("TextBoxValidationStyle_ToolTip");

                if (oObject.GetType().ToString() == "System.Windows.Controls.TextBox")
                {
                    ((TextBox)oObject).ToolTip = oToolTip;
                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.PasswordBox")
                {
                    ((PasswordBox)oObject).ToolTip = oToolTip;
                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.ComboBox")
                {
                    ((ComboBox)oObject).ToolTip = oToolTip;
                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.DatePicker")
                {
                    ((DatePicker)oObject).ToolTip = oToolTip;
                }
              
            }
        }

        private static void SetStyleValidationObject(object oObject, bool oIsValid, string sNormalStyle = "", string sValidationStyle = "")
        {
            Style oStyle = new Style();


            if (!oIsValid)
            {
                if (oObject.GetType().ToString() == "System.Windows.Controls.TextBox")
                {
                    if (sValidationStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("TextBoxValidationStyle");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sValidationStyle.Trim());
                    }
                    ((TextBox)oObject).Style = oStyle;

                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.PasswordBox")
                {
                    if (sValidationStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("SmartPasswordBoxValidateStyle");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sValidationStyle.Trim());
                    }
                    ((PasswordBox)oObject).Style = oStyle;
                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.ComboBox")
                {
                    if (sValidationStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("ComboBoxValidationStyle");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sValidationStyle.Trim());
                    }
                    ((ComboBox)oObject).Style = oStyle;

                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.DatePicker")
                {
                    if (sValidationStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("DatePickerValidationStyle");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sValidationStyle.Trim());
                    }
                    ((DatePicker)oObject).Style = oStyle;
                }
            
                else if (oObject.GetType().ToString() == "Microsoft.Windows.Controls.MaskedTextBox")
                {
                    if (sValidationStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("MaskedTextBoxValidationStyle");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sValidationStyle.Trim());
                    }
                    ((MaskedTextBox)oObject).Style = oStyle;
                }
            }
            else
            {
                if (oObject.GetType().ToString() == "System.Windows.Controls.TextBox")
                {
                    if (sNormalStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("TextBoxValidationStyle_Normal");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sNormalStyle.Trim());
                    }
                    ((TextBox)oObject).Style = oStyle;
                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.PasswordBox")
                {
                    if (sNormalStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("SmartPasswordBoxStyle");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sNormalStyle.Trim());
                    }
                    ((PasswordBox)oObject).Style = oStyle;
                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.ComboBox")
                {
                    if (sNormalStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("ComboBoxValidationStyle_Normal");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sNormalStyle.Trim());
                    }
                    ((ComboBox)oObject).Style = oStyle;

                }
                else if (oObject.GetType().ToString() == "System.Windows.Controls.DatePicker")
                {
                    if (sNormalStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("DatePickerValidationStyle_Normal");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sNormalStyle.Trim());
                    }
                    ((DatePicker)oObject).Style = oStyle;
                }
             
                else if (oObject.GetType().ToString() == "System.Windows.Controls.MaskedTextBox")
                {
                    if (sNormalStyle.Trim() == string.Empty)
                    {
                        oStyle = (Style)Application.Current.FindResource("MaskedValidationStyle_Normal");
                    }
                    else
                    {
                        oStyle = (Style)Application.Current.FindResource(sNormalStyle.Trim());
                    }
                    ((MaskedTextBox)oObject).Style = oStyle;
                }
            }


        }

    }

    public class RegexValidationRule : ValidationRule
    {

        private string _pattern;
        private Regex _regex;
        private string _errorMessage;

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                _regex = new Regex(_pattern, RegexOptions.IgnoreCase);
            }
        }


        public RegexValidationRule()
        {
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo ultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value == null || !_regex.Match(value.ToString()).Success)
            {
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return new ValidationResult(true, null);
            }


        }
    }

    public class RequiredFieldValidator : ValidationRule
    {

        private string _errorMessage;


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value,
           CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();

            if (string.IsNullOrEmpty(inputString))
            {
                result = new ValidationResult(false, this.ErrorMessage);
            }

            return result;
        }



    }

    public class CompareFieldValidator : ValidationRule
    {

        private string _errorMessage;


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public ValidationResult Validate(object value, object value1, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            string inputString = (value ?? string.Empty).ToString().Trim();
            string inputString1 = (value1 ?? string.Empty).ToString().Trim();

            if (inputString != inputString1)
            {
                result = result = new ValidationResult(false, this.ErrorMessage);
            }

            return result;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            return result;
        }



    }
}
