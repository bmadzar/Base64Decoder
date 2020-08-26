using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Base64Decoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Encoding encoding;

        public MainWindow()
        {
            encoding = Encoding.GetEncoding(1252);
            InitializeComponent();
        }

        private string Base64Encode(string plainText)
        {
            if (plainText is null)
            {
                return "";
            }

            byte[] bytes = encoding.GetBytes(plainText);
            return Convert.ToBase64String(bytes);
        }

        private string Base64Decode(string base64)
        {
            if (base64 is null)
            {
                return "";
            }

            base64 = base64.PadRight((base64.Length + 3) / 4, '=');

            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
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

        private void Base64DataPaste_Click(object sender, RoutedEventArgs e)
        {
            Base64Data.Focus();
            Base64Data.Text = Clipboard.GetText(TextDataFormat.Text);
        }
    }
}
