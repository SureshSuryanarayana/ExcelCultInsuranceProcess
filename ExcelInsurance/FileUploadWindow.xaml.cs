using ExcelInsurance.Repository.Implementations;
using ExcelInsurance.Repository.Interfaces;
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
    /// Interaction logic for FileUploadWindow.xaml
    /// </summary>
    public partial class FileUploadWindow : Window
    {
        int policyId;
        OpenFileDialog dialog;
        IPolicyManager policyManager;
        public FileUploadWindow()
        {
            InitializeComponent();
        }

        public FileUploadWindow(int policyId)
        {
            InitializeComponent();
            this.policyId = policyId;
            policyManager = new PolicyManager();
        }

        private void Btn_UploadFile_Click(object sender, RoutedEventArgs e)
        {
            if (dialog != null && File.Exists(dialog.FileName))
            {
                //Delete file if already exists
                policyManager.DeleteFile(policyId);

                byte[] fileBytes = File.ReadAllBytes(dialog.FileName);
                bool status = policyManager.AddFile(fileBytes, dialog.SafeFileName, policyId);
                if (status)
                {
                    dialog = null;
                    txt_SelectBox.Text = "";
                    MessageBox.Show("File uploaded successfully.");
                    this.Close();
                }
                else {
                    dialog = null;
                    txt_SelectBox.Text = "";
                    MessageBox.Show("Can't upload file now. Please try again later.");
                    this.Close();
                }
                return;
            }

            MessageBox.Show("Please select a file to upload.");
            return;
        }

        private void Btn_SelectFile_Click(object sender, RoutedEventArgs e)
        {
            this.dialog = new OpenFileDialog();
            //dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            bool result = (bool)dialog.ShowDialog();
            if (result)
            {
                var fileInfo = new FileInfo(dialog.FileName);
                double fileSize = fileInfo.Length / 1024;
                int maxSize = Convert.ToInt32(ConfigurationManager.AppSettings["MAX_FILE_SIZE"].ToString());
                if (fileSize > maxSize)
                {
                    MessageBox.Show("File size exceeds limit.");
                    dialog = null;
                    return;
                }
                txt_SelectBox.Text = dialog.SafeFileName;
            }
            return;
        }
    }
}
