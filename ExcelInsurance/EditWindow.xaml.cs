using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private string _viewType;
        private Policy _policy;
        private Quote _quote;
        private IPolicyManager policyManager;
        private IQuoteManager quoteManager;
        public EditWindow()
        {
            InitializeComponent();
            policyManager = new PolicyManager();
            quoteManager = new QuoteManager();
        }

        public EditWindow(string type, object context)
        {
            InitializeComponent();
            policyManager = new PolicyManager();
            quoteManager = new QuoteManager();

            _viewType = type;

            if (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_POLICY"])
            {
                _policy = (Policy)context;
                txtb_header.Text = "Editing Policy : " + _policy.Id.ToString();
                txt_Firstname.Text = _policy.InsurerFirstName;
                txt_Lastname.Text = _policy.InsurerLastName;
                txt_Middlename.Text = _policy.InsurerMiddleName;
                txt_Email.Text = _policy.Email;
                txt_PhoneNumber.Text = _policy.Phone;
                txt_TotalAmount.Text = _policy.Amount.ToString();
                txt_Address.Text = _policy.Address;
                txt_State.Text = _policy.State;
                txt_Country.Text = _policy.Country;
                txt_Pincode.Text = _policy.Pin.ToString();
                txt_AccountNumber.Text = _policy.BankAccountNumber;
                txt_AgentName.Text = _policy.AgentName;
                txt_Document.Text = _policy.DocumentPath;
                txt_Nominee.Text = _policy.Nominee;
                txt_Relation.Text = _policy.Relation;
                date_StartDate.SelectedDate = _policy.StartDate;
                date_EndDate.SelectedDate = _policy.EndDate;

            }
            else if (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_QUOTE"]) {
                btn_UploadFile.Visibility = Visibility.Hidden;
                _quote = (Quote)context;
                txtb_header.Text = "Editing Quote : " + _quote.Id.ToString(); 
                txt_Firstname.Text = _quote.InsurerFirstName;
                txt_Lastname.Text = _quote.InsurerLastName;
                txt_Middlename.Text = _quote.InsurerMiddleName;
                txt_Email.Text = _quote.Email;
                txt_PhoneNumber.Text = _quote.Phone;
                txt_TotalAmount.Text = _quote.Amount.ToString();
                txt_Address.Text = _quote.Address;
                txt_State.Text = _quote.State;
                txt_Country.Text = _quote.Country;
                txt_Pincode.Text = _quote.Pin.ToString();
                txt_AccountNumber.Text = _quote.BankAccountNumber;
                txt_AgentName.Text = _quote.AgentName;
                txt_Nominee.Text = _quote.Nominee;
                txt_Relation.Text = _quote.Relation;

                date_StartDate.SelectedDate = _quote.StartDate;
                date_EndDate.SelectedDate = _quote.EndDate;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_POLICY"])
            {
                cb_PolicyType.SelectedItem = cb_PolicyType.Items.Cast<ComboBoxItem>().Single(t => t.Tag.Equals(_policy.Type));
                cb_AddrProofType.SelectedItem = cb_AddrProofType.Items.Cast<ComboBoxItem>().Single(t => t.Tag.Equals(_policy.AddressProofType));
            }
            else if (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_QUOTE"])
            {
                cb_PolicyType.SelectedItem = cb_PolicyType.Items.Cast<ComboBoxItem>().Single(t => t.Tag.Equals(_quote.Type));
                cb_AddrProofType.SelectedItem = cb_AddrProofType.Items.Cast<ComboBoxItem>().Single(t => t.Tag.Equals(_quote.AddressProofType));
            }
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            bool validationCheck = true;

            if (String.IsNullOrEmpty(this.txt_Firstname.Text))
            {
                this.txt_Firstname.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_Lastname.Text))
            {
                this.txt_Lastname.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_TotalAmount.Text))
            {
                this.txt_TotalAmount.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_Address.Text))
            {
                this.txt_Address.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_State.Text))
            {
                this.txt_State.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_Country.Text))
            {
                this.txt_Country.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_AccountNumber.Text))
            {
                this.txt_AccountNumber.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_Email.Text))
            {
                this.txt_Email.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (String.IsNullOrEmpty(this.txt_PhoneNumber.Text))
            {
                this.txt_PhoneNumber.BorderBrush = Brushes.Red;
                validationCheck = false;
            }

            //Id proof check, date checks
            if (this.cb_AddrProofType.SelectedIndex == -1)
            {
                this.cb_AddrProofType.BorderBrush = Brushes.Red;
                validationCheck = false;
            }

            DateTime? sd = this.date_StartDate.SelectedDate;
            DateTime? ed = this.date_EndDate.SelectedDate;
            if (!sd.HasValue)
            {
                this.date_StartDate.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (!ed.HasValue)
            {
                this.date_EndDate.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (validationCheck && (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_POLICY"].ToString()))
            {
                _policy.InsurerFirstName = this.txt_Firstname.Text;
                _policy.InsurerLastName = this.txt_Lastname.Text;
                _policy.InsurerMiddleName = this.txt_Middlename.Text;
                _policy.Email = this.txt_Email.Text;
                _policy.Phone = this.txt_PhoneNumber.Text;
                _policy.Address = this.txt_Address.Text;
                _policy.State = this.txt_State.Text;
                _policy.Country = this.txt_Country.Text;
                _policy.StartDate = this.date_StartDate.SelectedDate;
                _policy.EndDate = this.date_EndDate.SelectedDate;
                _policy.BankAccountNumber = this.txt_AccountNumber.Text;
                _policy.AddressProofType = ((ComboBoxItem)(this.cb_AddrProofType.SelectedItem)).Tag.ToString();
                _policy.Amount = Convert.ToDouble(this.txt_TotalAmount.Text);
                _policy.DocumentPath = this.txt_Document.Text;
                _policy.Relation = this.txt_Relation.Text;
                _policy.Pin = Convert.ToInt32(this.txt_Pincode.Text);
                _policy.Nominee = this.txt_Nominee.Text;
                _policy.AgentName = this.txt_AgentName.Text;
                _policy.Type = ((ComboBoxItem)(this.cb_PolicyType.SelectedItem)).Tag.ToString();

                if (policyManager.UpdatePolicy(_policy))
                {
                    MessageBox.Show("Policy updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Can not update Policy. Please try later.");
                }
            }
            else if (validationCheck && (_viewType == ConfigurationManager.AppSettings["VIEW_TYPE_QUOTE"].ToString())) {
                _quote.InsurerFirstName = this.txt_Firstname.Text;
                _quote.InsurerLastName = this.txt_Lastname.Text;
                _quote.InsurerMiddleName = this.txt_Middlename.Text;
                _quote.Email = this.txt_Email.Text;
                _quote.Phone = this.txt_PhoneNumber.Text;
                _quote.Address = this.txt_Address.Text;
                _quote.State = this.txt_State.Text;
                _quote.Country = this.txt_Country.Text;
                _quote.StartDate = this.date_StartDate.SelectedDate;
                _quote.EndDate = this.date_EndDate.SelectedDate;
                _quote.BankAccountNumber = this.txt_AccountNumber.Text;
                _quote.AddressProofType = ((ComboBoxItem)(this.cb_AddrProofType.SelectedItem)).Tag.ToString();
                _quote.Amount = Convert.ToDouble(this.txt_TotalAmount.Text);
                _quote.Relation = this.txt_Relation.Text;
                _quote.Pin = Convert.ToInt32(this.txt_Pincode.Text);
                _quote.Nominee = this.txt_Nominee.Text;
                _quote.AgentName = this.txt_AgentName.Text;
                _quote.Type = ((ComboBoxItem)(this.cb_PolicyType.SelectedItem)).Tag.ToString();

                if (quoteManager.UpdateQuote(_quote))
                {
                    MessageBox.Show("Quote updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Can not update Quote. Please try later.");
                }
            }
        }

        private void Btn_UploadFile_Click(object sender, RoutedEventArgs e)
        {
            FileUploadWindow fileUpload = new FileUploadWindow(_policy.Id);
            fileUpload.Show();
        }

        
    }
}
