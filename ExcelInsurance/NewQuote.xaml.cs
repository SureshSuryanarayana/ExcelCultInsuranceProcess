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
    /// Interaction logic for NewQuote.xaml
    /// </summary>
    public partial class NewQuote : Window
    {
        private Quote quote;
        private IQuoteManager quoteManager;
        private ICountryManager countryManager;
        public List<Country> countriesList { get; set; }
        public NewQuote()
        {
            InitializeComponent();
            quote = new Quote();
            quoteManager = new QuoteManager();
            countryManager = new CountryManager();
            countriesList = countryManager.GetCountries();
            DataContext = this;
        }

        private void Btn_SaveQuote_Click(object sender, RoutedEventArgs e)
        {
            bool validationCheck = true;

            try
            {
                if (String.IsNullOrEmpty(this.txt_Firstname.Text))
                {
                    this.txt_Firstname.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_Firstname.BorderBrush = Brushes.Black; }
                if (String.IsNullOrEmpty(this.txt_Lastname.Text))
                {
                    this.txt_Lastname.BorderBrush = Brushes.Red;
                    validationCheck = false;
                }
                else { this.txt_Firstname.BorderBrush = Brushes.Black; }
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

                    //double account;
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

                    try
                    {
                        MailAddress mail = new MailAddress(this.txt_Email.Text);
                    }
                    catch (FormatException ex)
                    {
                        this.txt_Email.BorderBrush = Brushes.Red;
                        MessageBox.Show("Please enter valid Email");
                        return;
                    }

                    if (!(rbm.IsChecked == true || rbf.IsChecked == true || rbo.IsChecked == true))
                    {
                        MessageBox.Show("Please select gender");
                        return;
                    }

                    if (rbm.IsChecked == true) { quote.Gender = "MALE"; }
                    else if (rbf.IsChecked == true) { quote.Gender = "FEMALE"; }
                    else quote.Gender = "OTHERS";

                    quote.InsurerFirstName = this.txt_Firstname.Text;
                    quote.InsurerLastName = this.txt_Lastname.Text;
                    //quote.InsurerMiddleName = this.txt_Middlename.Text;
                    quote.Email = this.txt_Email.Text;
                    quote.Phone = this.txt_PhoneNumber.Text;
                    quote.Address = this.txt_Address.Text;
                    quote.State = this.txt_State.Text;
                    quote.Country = ((dynamic)this.cb_Country.SelectedItem).Code;
                    quote.StartDate = this.date_StartDate.SelectedDate;
                    quote.EndDate = this.date_EndDate.SelectedDate;
                    //quote.BankAccountNumber = this.txt_AccountNumber.Text;
                    quote.AddressProofType = ((ComboBoxItem)(this.cb_AddrProofType.SelectedItem)).Tag.ToString();
                    quote.Amount = Convert.ToDouble(this.txt_TotalAmount.Text);
                    quote.Relation = this.txt_Relation.Text;
                    quote.Pin = Convert.ToInt32(this.txt_Pincode.Text);
                    quote.Nominee = this.txt_Nominee.Text;
                    quote.AgentName = this.txt_AgentName.Text;
                    quote.Type = ((ComboBoxItem)(this.cb_QuoteType.SelectedItem)).Tag.ToString();

                    int _quoteId = quoteManager.AddQuote(quote);
                    if (_quoteId > 0)
                    {
                        MessageBox.Show("Policy added successfully.");
                        this.btn_SaveQuote.IsEnabled = false;
                        this.btn_CancelQuote.Content = "Close";
                        this.txt_title.Text = "New policy id : " + _quoteId.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Can not add Quote. Please try later.");
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

        private void Btn_CancelQuote_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
