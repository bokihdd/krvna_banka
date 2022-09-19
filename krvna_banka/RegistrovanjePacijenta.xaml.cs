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
    /// Interaction logic for RegistrovanjePacijenta.xaml
    /// </summary>
    public partial class RegistrovanjePacijenta : Window
    {
        public RegistrovanjePacijenta()
        {
            InitializeComponent();
            fillComboKrvnaGrupaComboBox();
        }

        string IdKG;
        void fillComboKrvnaGrupaComboBox()
        {
            string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";
            string query = "SELECT * FROM krvne_grupe";
            MySqlConnection connection = new MySqlConnection(MyConString);
            connection.Open();
            MySqlCommand cmdSel = new MySqlCommand(query, connection);
            MySqlDataReader dr = cmdSel.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(1);
                KrvnaGrupaComboBox.Items.Add(name);
            }
            connection.Close();
            dr.Close();
        }

        string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";

        private void Window_Closed(object sender, EventArgs e)
        {
            var GlavniOpcioniMeni = new GlavniOpcioniMeni();
            GlavniOpcioniMeni.Show();
        }

        private void RegistrovanjePacijentaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ImeTextBox.Text.Length == 0 || PrezimeTextBox.Text.Length == 0 || KrvnaGrupaComboBox.SelectedIndex == -1 || KolicinaKrviTextBox.Text.Length == 0)
            {
                MessageBox.Show("Popunite sva polja!");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                connection.Open();

                string queryS = "SELECT idKrvneGrupe FROM krvne_grupe where Naziv='" + KrvnaGrupaComboBox.Text + "'";
                MySqlCommand cmdSel = new MySqlCommand(queryS, connection);
                var queryResult = cmdSel.ExecuteReader();
                queryResult.Read();
                IdKG = queryResult.GetString(0);
                queryResult.Close();

                string queryI = " INSERT INTO pacijent (Ime,Prezime,idKrvneGrupe,PotrebnaKolicinaKrvi) VALUES ('" + ImeTextBox.Text + "','" + PrezimeTextBox.Text + "','" + IdKG + "','" + KolicinaKrviTextBox.Text + "')";
                cmdSel = new MySqlCommand(queryI, connection);
                var queryResult1 = cmdSel.ExecuteNonQuery();

                MessageBox.Show("Pacijent uspesno registrovan!");

                ImeTextBox.Text = "";
                PrezimeTextBox.Text = "";
                KrvnaGrupaComboBox.SelectedIndex = -1;
                KolicinaKrviTextBox.Text = "";
            }
        }
    }
}
