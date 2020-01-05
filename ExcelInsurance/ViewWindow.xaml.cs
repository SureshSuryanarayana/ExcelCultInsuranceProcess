using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
using ExcelInsurance.Repository.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExcelInsurance
{
    /// <summary>
    /// Interaction logic for ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        private string _viewType;
        private Policy _policy;
        private Quote _quote;
        private IPolicyManager policyManager;
        public ViewWindow()
        {
            InitializeComponent();
        }
        public ViewWindow(string type, object context)
        {
            InitializeComponent();

            _viewType = type;

            try
            {
                if (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_POLICY"])
                {
                    _policy = (Policy)context;
                    _policy.Type = GetPolicyOrQuoteTypes(_policy.Type);
                    _policy.AddressProofType = _policy.AddressProofType == "PASS" ? "Passport" : "Voter id";
                    this.txtb_header.Text = "Policy ID :" + _policy.Id.ToString();
                    this.DataContext = _policy;

                }
                else if (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_QUOTE"])
                {
                    _quote = (Quote)context;
                    _quote.Type = GetPolicyOrQuoteTypes(_quote.Type);
                    _quote.AddressProofType = _quote.AddressProofType == "PASS" ? "Passport" : "Voter id";
                    this.txtb_header.Text = "Quote ID :" + _quote.Id.ToString();
                    this.btn_DownloadDocument.Visibility = Visibility.Collapsed;
                    this.DataContext = _quote;
                }
                else
                {

                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private string GetPolicyOrQuoteTypes(string type) {
            if (type == "HEALTH")
                return "Health";
            if (type == "TRAVEL")
                return "Travel";
            if (type == "AUTOMOBILE")
                return "Automobile";
            if (type == "RETAIL")
                return "Retail";
            if (type == "REALESTATE")
                return "Real estate";
            return "";
        }

        private void Btn_DownloadDocument_Click(object sender, RoutedEventArgs e)
        {
            policyManager = new PolicyManager();
            try
            {
                var bytes = policyManager.GetFile(_policy.Id);
                if (bytes.Length == 0)
                {
                    MessageBox.Show("There is no file available to download.");
                    return;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                //saveFileDialog.DefaultExt = "jpeg";
                //saveFileDialog.AddExtension = true;
                bool result = (bool)saveFileDialog.ShowDialog();
                if (result)
                {
                    using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                    MessageBox.Show("File saved successfully");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
