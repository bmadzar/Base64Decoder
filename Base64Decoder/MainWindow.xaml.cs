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

namespace Base64Decoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Encoding encoding;

        public MainWindow()
        {
            encoding = System.Text.Encoding.GetEncoding(1252);
            InitializeComponent();
        }

        private string Base64Encode(string plainText)
        {
            if (plainText is null)
            {
                return "";
            }

            byte[] bytes = encoding.GetBytes(plainText);
            return System.Convert.ToBase64String(bytes);
        }

        private string Base64Decode(string base64)
        {
            if (base64 is null)
            {
                return "";
            }

            base64 = base64.PadRight(4 - (base64.Length % 4), '=');

            try
            {
                byte[] bytes = System.Convert.FromBase64String(base64);
                return encoding.GetString(bytes);
            }
            catch (FormatException)
            {
                return "";
            }
        }

        private void PlainTextData_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!(Base64Data is null) && !Base64Data.IsFocused)
            {
                Base64Data.Text = Base64Encode(textBox.Text);
            }
        }

        private void Base64Data_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!(PlainTextData is null) && !PlainTextData.IsFocused)
            {
                PlainTextData.Text = Base64Decode(textBox.Text);
            }
        }
    }
}
