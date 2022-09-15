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
using System.Configuration;
using System.Data;


namespace krvna_banka
{
    /// <summary>
    /// Interaction logic for PregledDonora.xaml
    /// </summary>
    public partial class PregledDonora : Window
    {
        public PregledDonora()
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
            string query = "SELECT donor.Ime,donor.Prezime,krvne_grupe.naziv AS Grupa,donor.KolicinaKrvi AS Kolicina,donor.DatumDoniranja AS Datum FROM donor, krvne_grupe WHERE donor.idKrvneGrupe = krvne_grupe.idKrvneGrupe";
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
