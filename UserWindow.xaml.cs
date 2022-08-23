using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserAplication
{
    /// <summary>
    /// Lógica interna para UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool nameIsValid = Regex.IsMatch(NameBox.Text, @"^[a-zA-Z ]+$");
            bool jobIsValid = Regex.IsMatch(JobBox.Text, @"^[a-zA-Z ]+$");

            try
            {
                MailAddress email = new MailAddress(EmailBox.Text);

                if(nameIsValid)
                {
                    if(jobIsValid)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show($"Cargo inválido! Tente Novamente");
                    }                    
                }
                else
                {
                    MessageBox.Show($"Nome inválido! Tente Novamente");
                }                             
            }
            catch
            {
                MessageBox.Show($"Email inválido! Tente Novamente");
            }   
        }
    }
}
