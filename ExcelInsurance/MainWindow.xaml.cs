using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
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
            this.policyDataGrid.ItemsSource = policyManager.GetPolicies("ALL");

        }

        private void Cb_DivisionSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
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
                this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes();
            }
        }

        private void Cb_DataToView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
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
                this.quoteDataGrid.ItemsSource = quoteManager.GetQuotes();
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

        }
    }
}
