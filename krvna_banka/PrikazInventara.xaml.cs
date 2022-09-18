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
    /// Interaction logic for PrikazInventara.xaml
    /// </summary>
    public partial class PrikazInventara : Window
    {
        public PrikazInventara()
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
            string query = "SELECT krvne_grupe.Naziv,inventar.UkupnaKolicinaKrvi AS Kolicina FROM krvne_grupe,inventar WHERE krvne_grupe.idKrvneGrupe=inventar.idKrvneGrupe";
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
