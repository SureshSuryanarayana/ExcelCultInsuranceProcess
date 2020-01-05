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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelInsurance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPolicyManager policyManager;
        private IQuoteManager quoteManager;
        public MainWindow()
        {
            InitializeComponent();
            policyManager = new PolicyManager();
            quoteManager = new QuoteManager();

            this.quoteDataGrid.Visibility = Visibility.Collapsed;
            this.btn_AddQuote.Visibility = Visibility.Collapsed;
            this.quoteDataGrid.ItemsSource = null;

            this.policyDataGrid.Visibility = Visibility.Visible;
            this.policyDataGrid.Visibility = Visibility.Visible;
            try
            {
                this.policyDataGrid.ItemsSource = policyManager.GetPolicies("ALL");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            

            App.Current.Properties["EDIT_TYPE"] = "";
            App.Current.Properties["VIEW_TYPE"] = "";

        }

        private void Cb_DivisionSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            try
            {
                string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();

                if (((ComboBoxItem)(this.cb_DataToView.SelectedItem)).Tag.ToString() == "POLICY")
                {

                    this.btn_AddQuote.Visibility = Visibility.Collapsed;
                    this.quoteDataGrid.Visibility = Visibility.Collapsed;
                    this.quoteDataGrid.ItemsSource = null;

                    this.btn_AddPolicy.Visibility = Visibility.Visible;
                    this.policyDataGrid.Visibility = Visibility.Visible;
                    this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter);
                }
                else if (((ComboBoxItem)(this.cb_DataToView.SelectedItem)).Tag.ToString() == "QUOTE")
                {
                    this.policyDataGrid.Visibility = Visibility.Collapsed;
                    this.btn_AddPolicy.Visibility = Visibility.Collapsed;
                    this.policyDataGrid.ItemsSource = null;

                    this.quoteDataGrid.Visibility = Visibility.Visible;
                    this.btn_AddQuote.Visibility = Visibility.Visible;
                    this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes(filter);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cb_DataToView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            try
            {
                string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();

                if (((ComboBoxItem)(this.cb_DataToView.SelectedItem)).Tag.ToString() == "POLICY")
                {

                    this.btn_AddQuote.Visibility = Visibility.Collapsed;
                    this.quoteDataGrid.Visibility = Visibility.Collapsed;
                    this.quoteDataGrid.ItemsSource = null;

                    this.btn_AddPolicy.Visibility = Visibility.Visible;
                    this.policyDataGrid.Visibility = Visibility.Visible;
                    this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter);
                }
                else if (((ComboBoxItem)(this.cb_DataToView.SelectedItem)).Tag.ToString() == "QUOTE")
                {
                    this.policyDataGrid.Visibility = Visibility.Collapsed;
                    this.btn_AddPolicy.Visibility = Visibility.Collapsed;
                    this.policyDataGrid.ItemsSource = null;

                    this.quoteDataGrid.Visibility = Visibility.Visible;
                    this.btn_AddQuote.Visibility = Visibility.Visible;
                    this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes(filter);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_AddPolicy_Click(object sender, RoutedEventArgs e)
        {
            NewPolicy newPolicyWindow = new NewPolicy();
            newPolicyWindow.ShowDialog();
            string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
            this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter);
        }

        private void Btn_AddQuote_Click(object sender, RoutedEventArgs e)
        {
            NewQuote newQuoteWindow = new NewQuote();
            newQuoteWindow.ShowDialog();
            string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
            this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes(filter);
        }

        private void Btn_ViewPolicy_Click(object sender, RoutedEventArgs e)
        {
            dynamic _sender = sender;
            ViewWindow viewWindow = new ViewWindow("POLICY", _sender.DataContext);
            viewWindow.ShowDialog();
        }

        private void Btn_EditPolicy_Click(object sender, RoutedEventArgs e)
        {
            dynamic _sender = sender;
            EditWindow editWindow = new EditWindow("POLICY", _sender.DataContext);
            editWindow.ShowDialog();
            string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
            this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter);
        }

        private void Btn_DeletePolicy_Click(object sender, RoutedEventArgs e)
        {
            dynamic _sender = sender;
            MessageBoxResult confirm = MessageBox.Show("Are you sure, you want to delete?", "Confirm", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes) {
                //Delete
                bool status = policyManager.RemovePolicy(_sender.DataContext.Id);
                if (status)
                {
                    string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
                    this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter);
                    MessageBox.Show("Policy is deleted successfully");
                    return;
                }
                else {
                    MessageBox.Show("Can delete Policy at this moment. Please try later");
                    return;
                }
                
            }
            return;

        }

        private void Btn_ViewQuote_Click(object sender, RoutedEventArgs e)
        {
            dynamic _sender = sender;
            ViewWindow viewWindow = new ViewWindow("QUOTE", _sender.DataContext);
            viewWindow.ShowDialog();
        }

        private void Btn_EditQuote_Click(object sender, RoutedEventArgs e)
        {
            dynamic _sender = sender;
            EditWindow editWindow = new EditWindow("QUOTE", _sender.DataContext);
            editWindow.ShowDialog();
            string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
            this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes(filter);
        }

        private void Btn_DeleteQuote_Click(object sender, RoutedEventArgs e)
        {
            dynamic _sender = sender;
            MessageBoxResult confirm = MessageBox.Show("Are you sure, you want to delete?", "Confirm", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                //Delete
                bool status = quoteManager.RemoveQuote(_sender.DataContext.Id);
                if (status)
                {
                    string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
                    this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes(filter);
                    MessageBox.Show("Quote is deleted successfully");
                    return;
                }
                else
                {
                    MessageBox.Show("Can delete Quote at this moment. Please try later");
                    return;
                }

            }
            return;
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (txt_SearchBox.Text.Length > 0)
            {
                string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
                this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter).Where(x => (x.InsurerFirstName.ToLower().Contains(txt_SearchBox.Text.ToLower()) || x.InsurerLastName.ToLower().Contains(txt_SearchBox.Text.ToLower()) || x.Id.ToString().ToLower().Contains(txt_SearchBox.Text.ToLower())));
            }
            else {
                string filter = ((ComboBoxItem)(this.cb_DivisionSelection.SelectedItem)).Tag.ToString();
                this.policyDataGrid.ItemsSource = policyManager.GetPolicies(filter);
            }
        }
    }
}
