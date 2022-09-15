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

using MySql.Data.MySqlClient;

namespace krvna_banka
{
    /// <summary>
    /// Interaction logic for RegistrovanjeKorisnika.xaml
    /// </summary>
    public partial class RegistrovanjeKorisnika : Window
    {
        public RegistrovanjeKorisnika()
        {
            InitializeComponent();
        }

        string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";

        private void Window_Closed(object sender, EventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
        }

        private void RegistrujButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImeTextBox.Text.Length == 0 || PrezimeTextBox.Text.Length == 0 || EmailTextBox.Text.Length == 0 || KorisnickoImeTextBox.Text.Length == 0 || SifraTextBox.Text.Length == 0)
            {
                MessageBox.Show("Popunite sva polja!");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                connection.Open();

                string query = "insert into korisnik (Ime, Prezime, Email, KorisnickoIme, Sifra) values ('" + ImeTextBox.Text + "', '" + PrezimeTextBox.Text + "', '" + EmailTextBox.Text + "', '" + KorisnickoImeTextBox.Text + "', '" + SifraTextBox.Text + "')";
                MySqlCommand cmdSel = new MySqlCommand(query, connection);
                var queryResult1 = cmdSel.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Radnik uspesno registrovan!");

                ImeTextBox.Text = "";
                PrezimeTextBox.Text = "";
                EmailTextBox.Text = "";
                KorisnickoImeTextBox.Text = "";
                SifraTextBox.Text = "";
            }
        }
    }
}
