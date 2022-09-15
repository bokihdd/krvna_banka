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
    /// Interaction logic for RegistrovanjeDonora.xaml
    /// </summary>
    public partial class RegistrovanjeDonora : Window
    {
        public RegistrovanjeDonora()
        {
            InitializeComponent();
            fillComboKrvnaGrupaComboBox();
        }

        string idKG1;
        int UkupnaKolicinaKrviInventar;
        int KolicinaKIzTB;

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

        private void RegistrovanjeDonoraButton_Click(object sender, RoutedEventArgs e)
        {
           //ignorisi ovo
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Ignorisi ovo. Program neradi ako se obrise
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            var GlavniOpcioniMeni = new GlavniOpcioniMeni();
            GlavniOpcioniMeni.Show();
        }

        private void RegistrovanjeDonoraButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (ImeTextBox.Text.Length == 0 || PrezimeTextBox.Text.Length == 0 || DatumDonacijeTextBox.Text.Length == 0 || KrvnaGrupaComboBox.SelectedIndex == -1 || KolicinaKrviTextBox.Text.Length == 0)
            {
                MessageBox.Show("Popunite sva polja!");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                connection.Open();

                string queryS = "select idKrvneGrupe from krvne_grupe where krvne_grupe.Naziv = '" + KrvnaGrupaComboBox.Text + "'";
                MySqlCommand cmdSel = new MySqlCommand(queryS, connection);

                var queryResult = cmdSel.ExecuteReader();
                queryResult.Read();
                idKG1 = queryResult.GetString(0);
                queryResult.Close();






                string query = "insert into donor (Ime, Prezime, DatumDoniranja, idKrvneGrupe, KolicinaKrvi) values ('" + ImeTextBox.Text + "', '" + PrezimeTextBox.Text + "', '" + DatumDonacijeTextBox.Text + "', '" + idKG1 + "', '" + KolicinaKrviTextBox.Text + "') ";
                cmdSel = new MySqlCommand(query, connection);
                var queryResult1 = cmdSel.ExecuteNonQuery();
               



                MessageBox.Show("Donor uspesno registrovan!");

                KolicinaKIzTB = int.Parse(KolicinaKrviTextBox.Text);

                ImeTextBox.Text = "";
                PrezimeTextBox.Text = "";
                DatumDonacijeTextBox.Text = "";
                KrvnaGrupaComboBox.SelectedIndex = -1;
                KolicinaKrviTextBox.Text = "";


                string queryI = "select UkupnaKolicinaKrvi,idKrvneGrupe  from inventar where idKrvneGrupe = '" + idKG1 + "'";
                cmdSel = new MySqlCommand(queryI, connection);


                var queryResultU = cmdSel.ExecuteReader();
                queryResultU.Read();
                UkupnaKolicinaKrviInventar = queryResultU.GetInt32(0);

                queryResultU.Close();

                
                
               

                //int rezultat = int.Parse(UkupnaKolicinaKrviInventar) + int.Parse(KolicinaKIzTB);
                int reazultat2 = UkupnaKolicinaKrviInventar + KolicinaKIzTB;


                string queryU = "update inventar set UkupnaKolicinaKrvi = '"+ reazultat2 + "' where inventar.idKrvneGrupe = '"+ idKG1 + "'";
                cmdSel = new MySqlCommand(queryU, connection);
                var queryResultQ = cmdSel.ExecuteNonQuery();
                connection.Close();

                
            }
        }
    }
}
