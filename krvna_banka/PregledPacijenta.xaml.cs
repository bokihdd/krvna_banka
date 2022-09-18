using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace krvna_banka
{
    /// <summary>
    /// Interaction logic for PregledPacijenta.xaml
    /// </summary>
    public partial class PregledPacijenta : Window
    {
        public PregledPacijenta()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var GlavniOpcioniMeni = new GlavniOpcioniMeni();
            GlavniOpcioniMeni.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["mysqlconnection"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            string query = "SELECT pacijent.Ime AS Ime, pacijent.Prezime AS Prezime, krvne_grupe.naziv AS Grupa,pacijent.PotrebnaKolicinaKrvi AS Kolicina FROM pacijent,krvne_grupe WHERE pacijent.idKrvneGrupe = krvne_grupe.idKrvneGrupe";
            sqlconn.Open();
            MySqlCommand sqlcomm = new MySqlCommand(query, sqlconn);
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dtGrid.ItemsSource = dt.DefaultView;
            sqlconn.Close();
        }
    }
}
