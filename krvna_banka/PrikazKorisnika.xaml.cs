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
    /// Interaction logic for PrikazKorisnika.xaml
    /// </summary>
    public partial class PrikazKorisnika : Window
    {
        public PrikazKorisnika()
        {
            InitializeComponent();
        }

        string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";

        private void Window_Closed(object sender, EventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
        }

        private void PretraziButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImeTextBox.Text.Length == 0 || PrezimeTextBox.Text.Length == 0)
            {
                MessageBox.Show("Popunite sva polja!");
            }
            else
            {
                string query = "select * from korisnik where Ime = '" + ImeTextBox.Text + "' and Prezime = '" + PrezimeTextBox.Text + "'";
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand cmdSel = new MySqlCommand(query, connection);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
                da.Fill(dt);
                ListaKorisnikaDataGrid.DataContext = dt;
            }
        }

        private void OsveziButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "select * from korisnik";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand cmdSel = new MySqlCommand(query, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            da.Fill(dt);
            ListaKorisnikaDataGrid.DataContext = dt;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "select * from korisnik";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand cmdSel = new MySqlCommand(query, connection);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdSel);
            da.Fill(dt);
            ListaKorisnikaDataGrid.DataContext = dt;
        }
    }
}
