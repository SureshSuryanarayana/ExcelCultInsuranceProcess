using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
using ExcelInsurance.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for NewPolicy.xaml
    /// </summary>
    public partial class NewPolicy : Window
    {
        private Policy policy;
        public List<Country> countriesList { get; set; }
        private IPolicyManager policyManager;
        private ICountryManager countryManager;
        public NewPolicy()
        {
            InitializeComponent();
            policy = new Policy();
            policyManager = new PolicyManager();
            countryManager = new CountryManager();
            countriesList = countryManager.GetCountries();
            DataContext = this;
        }

        private void Btn_SavePolicy_Click(object sender, RoutedEventArgs e)
        {
            bool validationCheck = true;
            try
            {
                if (String.IsNullOrEmpty(this.txt_Firstname.Text))
                {
                    this.txt_Firstname.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else
                {
                    this.txt_Firstname.BorderBrush = Brushes.Black;
                }
                if (String.IsNullOrEmpty(this.txt_Lastname.Text))
                {
                    this.txt_Lastname.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else
                {
                    this.txt_Lastname.BorderBrush = Brushes.Black;
                }
                if (String.IsNullOrEmpty(this.txt_TotalAmount.Text))
                {
                    this.txt_TotalAmount.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_TotalAmount.BorderBrush = Brushes.Black; }
                if (String.IsNullOrEmpty(this.txt_Address.Text))
                {
                    this.txt_Address.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_Address.BorderBrush = Brushes.Black; }
                if (String.IsNullOrEmpty(this.txt_State.Text))
                {
                    this.txt_State.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_State.BorderBrush = Brushes.Black; }
                if (this.cb_Country.SelectedItem == null)
                {
                    this.cb_Country.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.cb_Country.BorderBrush = Brushes.Black; }
                //if (String.IsNullOrEmpty(this.txt_AccountNumber.Text))
                //{
                //    this.txt_AccountNumber.BorderBrush = Brushes.Red;
                //    validationCheck = false;
                //}
                //else { this.txt_AccountNumber.BorderBrush = Brushes.Black; }
                if (String.IsNullOrEmpty(this.txt_Email.Text))
                {
                    this.txt_Email.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_Email.BorderBrush = Brushes.Black; }
                if (String.IsNullOrEmpty(this.txt_PhoneNumber.Text))
                {
                    this.txt_PhoneNumber.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_PhoneNumber.BorderBrush = Brushes.Black; }
                //Id proof check, date checks
                if (this.cb_AddrProofType.SelectedIndex == -1)
                {
                    this.cb_AddrProofType.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.cb_AddrProofType.BorderBrush = Brushes.Black; }

                DateTime? sd = this.date_StartDate.SelectedDate;
                DateTime? ed = this.date_EndDate.SelectedDate;
                if (!sd.HasValue)
                {
                    this.date_StartDate.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.date_StartDate.BorderBrush = Brushes.Black; }
                if (!ed.HasValue)
                {
                    this.date_EndDate.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.date_EndDate.BorderBrush = Brushes.Black; }

                
                if (validationCheck)
                {

                    try
                    {
                        MailAddress mail = new MailAddress(this.txt_Email.Text);
                    }
                    catch (FormatException ex) {
                        this.txt_Email.BorderBrush = Brushes.Red;
                        MessageBox.Show("Please enter valid Email");
                        return;
                    }

                    double totalAmount;
                    if (!double.TryParse(this.txt_TotalAmount.Text, out totalAmount))
                    {
                        this.txt_TotalAmount.BorderBrush = Brushes.Red;
                        MessageBox.Show("Please enter valid amount");
                        return;
                    }

                    double pin;
                    if (!double.TryParse(this.txt_Pincode.Text, out pin))
                    {
                        MessageBox.Show("Please enter valid pin number");
                        return;
                    }

                    double account;
                    //if (!double.TryParse(this.txt_AccountNumber.Text, out account))
                    //{
                    //    this.txt_AccountNumber.BorderBrush = Brushes.Red;
                    //    MessageBox.Show("Please enter valid account number");
                    //    return;
                    //}

                    double phone;
                    if (!double.TryParse(this.txt_PhoneNumber.Text, out phone) || this.txt_PhoneNumber.Text.Length != 10)
                    {
                        this.txt_PhoneNumber.BorderBrush = Brushes.Red;
                        MessageBox.Show("Please enter valid phone number");
                        return;
                    }

                    if (!(rbm.IsChecked == true || rbf.IsChecked == true || rbo.IsChecked == true))
                    {
                        MessageBox.Show("Please select gender");
                        return;
                    }

                    if (rbm.IsChecked == true) { policy.Gender = "MALE"; }
                    else if (rbf.IsChecked == true) { policy.Gender = "FEMALE"; }
                    else policy.Gender = "OTHERS";

                    policy.InsurerFirstName = this.txt_Firstname.Text;
                    policy.InsurerLastName = this.txt_Lastname.Text;
                    //policy.InsurerMiddleName = this.txt_Middlename.Text;
                    policy.Email = this.txt_Email.Text;
                    policy.Phone = this.txt_PhoneNumber.Text;
                    policy.Address = this.txt_Address.Text;
                    policy.State = this.txt_State.Text;
                    policy.Country = ((dynamic)this.cb_Country.SelectedItem).Code;
                    policy.StartDate = this.date_StartDate.SelectedDate;
                    policy.EndDate = this.date_EndDate.SelectedDate;
                    //policy.BankAccountNumber = this.txt_AccountNumber.Text;
                    policy.AddressProofType = ((ComboBoxItem)(this.cb_AddrProofType.SelectedItem)).Tag.ToString();
                    policy.Amount = Convert.ToDouble(this.txt_TotalAmount.Text);
                    policy.Relation = this.txt_Relation.Text;
                    policy.Pin = Convert.ToInt32(this.txt_Pincode.Text);
                    policy.Nominee = this.txt_Nominee.Text;
                    policy.AgentName = this.txt_AgentName.Text;
                    policy.Type = ((ComboBoxItem)(this.cb_PolicyType.SelectedItem)).Tag.ToString();
                    int _policyId = policyManager.AddPolicy(policy);
                    if (_policyId > 0)
                    {
                        MessageBox.Show("Policy added successfully.");
                        this.btn_SavePolicy.IsEnabled = false;
                        this.btn_CancelPolicy.Content = "Close";
                        this.txt_title.Text = "New policy id : " + _policyId.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Can not add Policy. Please try later.");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all mandatory fields");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Btn_CancelPolicy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
