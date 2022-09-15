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
using System.Windows.Shapes;

using MySql.Data.MySqlClient;

namespace krvna_banka
{
    /// <summary>
    /// Interaction logic for BrisanjeNaloga.xaml
    /// </summary>
    public partial class BrisanjeNaloga : Window
    {
        public BrisanjeNaloga()
        {
            InitializeComponent();
        }

        string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";
        int kojimeni = 0;

        private void Window_Closed(object sender, EventArgs e)
        {
            if (kojimeni == 0)
            {
                var GlavniOpcioniMeni = new GlavniOpcioniMeni();
                GlavniOpcioniMeni.Show();
            }
            else
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
            }

        }

        private void ObrisiNalogButton_Click(object sender, RoutedEventArgs e)
        {
            string kime = KorisnikInformacije.GetKorisnickoIme<string>("KorisnikI");
            string ksif = KorisnikInformacije.GetSifra<string>("KorisnikS");

            string username = KorisnickoImeTextBox.Text;
            string password = SifraTextBox.Text;




            if (KorisnickoImeTextBox.Text.Length == 0 || SifraTextBox.Text.Length == 0)
            {
                MessageBox.Show("Popunite sva polja!");
            }
            else
            {
                if (KorisnickoImeTextBox.Text == kime && SifraTextBox.Text == ksif)
                {

                    string query = "delete from korisnik where KorisnickoIme = '" + KorisnickoImeTextBox.Text + "' and Sifra = '" + SifraTextBox.Text + "'";
                    MySqlConnection connection = new MySqlConnection(MyConString);
                    connection.Open();

                    MySqlCommand cmdSel = new MySqlCommand(query, connection);
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                    da.Fill(dt);


                    MessageBox.Show("Nalog Obrisan!");
                    connection.Close();

                    kojimeni = 1;

                    this.Close();





                }
                else
                {
                    MessageBox.Show("Pogresno korisnicko ime ili sifra!");
                }
            }
        }
    }
}
