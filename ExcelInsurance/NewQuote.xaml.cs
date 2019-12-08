using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
using ExcelInsurance.Repository.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewQuote.xaml
    /// </summary>
    public partial class NewQuote : Window
    {
        private Quote quote;
        private IQuoteManager quoteManager;
        public NewQuote()
        {
            InitializeComponent();
            quote = new Quote();
            quoteManager = new QuoteManager();
        }

        private void Btn_SaveQuote_Click(object sender, RoutedEventArgs e)
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

            if (validationCheck)
            {
                quote.InsurerFirstName = this.txt_Firstname.Text;
                quote.InsurerLastName = this.txt_Lastname.Text;
                quote.InsurerMiddleName = this.txt_Middlename.Text;
                quote.Email = this.txt_Email.Text;
                quote.Phone = this.txt_PhoneNumber.Text;
                quote.Address = this.txt_Address.Text;
                quote.State = this.txt_State.Text;
                quote.Country = this.txt_Country.Text;
                quote.StartDate = this.date_StartDate.SelectedDate;
                quote.EndDate = this.date_EndDate.SelectedDate;
                quote.BankAccountNumber = this.txt_AccountNumber.Text;
                quote.AddressProofType = ((ComboBoxItem)(this.cb_AddrProofType.SelectedItem)).Tag.ToString();
                quote.Amount = Convert.ToDouble(this.txt_TotalAmount.Text);
                quote.Relation = this.txt_Relation.Text;
                quote.Pin = Convert.ToInt32(this.txt_Pincode.Text);
                quote.Nominee = this.txt_Nominee.Text;
                quote.AgentName = this.txt_AgentName.Text;
                quote.Type = ((ComboBoxItem)(this.cb_QuoteType.SelectedItem)).Tag.ToString();

                if (quoteManager.AddQuote(quote)) 
                {
                    MessageBox.Show("Quote added successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Can not add Quote. Please try later.");
                }
            }
        }
    }
}
