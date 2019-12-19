using ExcelInsurance.Repository.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ViewWindow()
        {
            InitializeComponent();
        }
        public ViewWindow(string type, object context)
        {
            InitializeComponent();

            _viewType = type;

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
                this.txt_Document.Visibility = Visibility.Collapsed;
                this.txtb_uploadDocs.Visibility = Visibility.Collapsed;
                this.DataContext = _quote;
            }
            else {

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

    }
}
