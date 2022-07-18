using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Axede.Utilidades;
using Axede.WPF.Softphone.Applications.UtilControls;
using Axede.WPF.Softphone.Applications.UtilControls.ModalMessageBox;

namespace Axede.WPF.Softphone.Applications.GUI.User_Controls
{
    /// <summary>
    /// Interaction logic for Trasferencia_UC.xaml
    /// </summary>
    public partial class Trasferencia_UC : UserControl
    {
        private ClassMethodUtil _classutil;
        public ClassMethodUtil Classutil
        {
            get { return _classutil; }
            set { _classutil = value; }
        }
        public Trasferencia_UC()
        {
            InitializeComponent();
            Classutil = new ClassMethodUtil();
        }

        public MessageBoxResult response;

        private string TransferNumber = string.Empty;

        public string GetTransferNumber()
        {
            if (!string.IsNullOrEmpty(TextBoxTranferNumber.Text))
            {
                TransferNumber = TextBoxTranferNumber.Text;
                response = MessageBoxResult.OK;
            }
            else
            {
                MessageBoxModal.Show(Classutil.ResolveOwnerWindow(), "Por favor Ingrese el Número de Transferencia.", "Información", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel, true);
               response = MessageBoxResult.Cancel;
            }
            return TransferNumber;
        }     

        //public int GetReplaceLineNum()
        //{
        //    if (TextBoxLineNum.Text.Length <= 0)
        //    {
        //        return 0;
        //    }

        //    return int.Parse(TextBoxLineNum.Text);
        //}

        private void TransferCallForm_Load(object sender, EventArgs e)
        {
            TransferNumber = "";
            TextBoxTranferNumber.Text = "";

          //  TextBoxLineNum.Text = "";
        }

        private void ButtonTransfer_Click(object sender, RoutedEventArgs e)
        {
            GetTransferNumber();
            if (response == MessageBoxResult.OK)
            {
                ViewWindow_Modal.CloseModal();
            }
        }

       
    }
}
