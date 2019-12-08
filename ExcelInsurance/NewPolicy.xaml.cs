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
    /// Interaction logic for NewPolicy.xaml
    /// </summary>
    public partial class NewPolicy : Window
    {
        private Policy policy;
        private IPolicyManager policyManager;
        public NewPolicy()
        {
            InitializeComponent();
            policy = new Policy();
            policyManager = new PolicyManager();
        }

        private void Btn_SavePolicy_Click(object sender, RoutedEventArgs e)
        {
            bool validationCheck = true;

            if (String.IsNullOrEmpty(this.txt_Firstname.Text)) {
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
            if (this.cb_AddrProofType.SelectedIndex == -1) {
                this.cb_AddrProofType.BorderBrush = Brushes.Red;
                validationCheck = false;
            }

            DateTime? sd = this.date_StartDate.SelectedDate;
            DateTime? ed = this.date_EndDate.SelectedDate;
            if (!sd.HasValue) {
                this.date_StartDate.BorderBrush = Brushes.Red;
                validationCheck = false;
            }
            if (!ed.HasValue)
            {
                this.date_EndDate.BorderBrush = Brushes.Red;
                validationCheck = false;
            }

            if (validationCheck) {
                policy.InsurerFirstName = this.txt_Firstname.Text;
                policy.InsurerLastName = this.txt_Lastname.Text;
                policy.InsurerMiddleName = this.txt_Middlename.Text;
                policy.Email = this.txt_Email.Text;
                policy.Phone = this.txt_PhoneNumber.Text;
                policy.Address = this.txt_Address.Text;
                policy.State = this.txt_State.Text;
                policy.Country = this.txt_Country.Text;
                policy.StartDate = this.date_StartDate.SelectedDate;
                policy.EndDate = this.date_EndDate.SelectedDate;
                policy.BankAccountNumber = this.txt_AccountNumber.Text;
                policy.AddressProofType = ((ComboBoxItem)(this.cb_AddrProofType.SelectedItem)).Tag.ToString();
                policy.Amount = Convert.ToDouble(this.txt_TotalAmount.Text);
                policy.DocumentPath = this.txt_Document.Text;
                policy.Relation = this.txt_Relation.Text;
                policy.Pin = Convert.ToInt32(this.txt_Pincode.Text);
                policy.Nominee = this.txt_Nominee.Text;
                policy.AgentName = this.txt_AgentName.Text;
                policy.Type = ((ComboBoxItem)(this.cb_PolicyType.SelectedItem)).Tag.ToString();

                if (policyManager.AddPolicy(policy))
                {
                    MessageBox.Show("Policy added successfully.");
                    this.Close();
                }
                else {
                    MessageBox.Show("Can not add Policy. Please try later.");
                }
            }
        }
    }
}
