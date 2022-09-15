using System;
using System.Collections.Generic;
using System.Data;
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

using MySql.Data.MySqlClient;

namespace krvna_banka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void UlogujSeButton_Click(object sender, RoutedEventArgs e)
        {
            if (KorisnickoIme.Text.Length == 0 || Sifra.Text.Length == 0)
            {
                MessageBox.Show("Popunite sva polja!");
            }
            else
            {
                string username = KorisnickoIme.Text;
                string password = Sifra.Text;

                try
                {
                    MySqlConnection connection = new MySqlConnection(MyConString);
                    connection.Open();
                    string query = "select * from korisnik where KorisnickoIme = '" + KorisnickoIme.Text + "' and Sifra = '" + Sifra.Text + "'";
                    MySqlDataAdapter sda = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        KorisnikInformacije.setKorisnickoIme("KorisnikI", KorisnickoIme.Text);
                        KorisnikInformacije.setSifra("KorisnikS", Sifra.Text);

                        GlavniOpcioniMeni prozor = new GlavniOpcioniMeni();
                        this.Visibility = Visibility.Hidden;
                        prozor.Show();
                    }
                    else
                    {
                        MessageBox.Show("Pogresno korisnicko ime ili sifra!");
                    }
                    connection.Close();
                }
                catch
                {
                    MessageBox.Show("Error!");
                }
            }
        }

        private void RegistrujSeButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrovanjeKorisnika RegistrovanjeKorisnika = new RegistrovanjeKorisnika();
            RegistrovanjeKorisnika.Show();
            this.Hide();
        }

        private void PrikazKorisnikaButton_Click(object sender, RoutedEventArgs e)
        {
            PrikazKorisnika PrikazKorisnika = new PrikazKorisnika();
            PrikazKorisnika.Show();
            this.Hide();
        }

        private void Izadji_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
