using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
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
    /// Interaction logic for Donacija.xaml
    /// </summary>
    public partial class Donacija : Window
    {
        public Donacija()
        {
            InitializeComponent();
        }

        string MyConString = "SERVER=127.0.0.1;" + "DATABASE=bankakrvi2;" + "UID=root;" + "PASSWORD=root03102000;";

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var GlavniOpcioniMeni = new GlavniOpcioniMeni();
            GlavniOpcioniMeni.Show();
        }

        private void IzvrsiDonacijuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Ime.Text.Length == 0 || Prezime.Text.Length == 0 || KrvnaGrupa.Text.Length == 0 || KolicinaKrvi.Text.Length == 0 || DatumDonacije.Text.Length == 0)
            {
                MessageBox.Show("Izaberite donora!");
            }
            else
            {

                MySqlConnection connection = new MySqlConnection(MyConString);
                connection.Open();

                string queryS1 = "SELECT idPacijenta FROM pacijent where Ime ='" + Ime.Text + "' and Prezime = '"+Prezime.Text+"'";
                MySqlCommand cmdSel = new MySqlCommand(queryS1, connection);
                var queryResult = cmdSel.ExecuteReader();
                queryResult.Read();
                string IdKG1 = queryResult.GetString(0);
                queryResult.Close();

                string queryS2 = "SELECT idKrvneGrupe FROM krvne_grupe where Naziv ='" + KrvnaGrupa.Text + "'";
                cmdSel = new MySqlCommand(queryS2, connection);
                queryResult = cmdSel.ExecuteReader();
                queryResult.Read();
                string IdKG2 = queryResult.GetString(0);
                queryResult.Close();

                string queryS4 = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = '"+ IdKG2 + "'";
                cmdSel = new MySqlCommand(queryS4, connection);
                queryResult = cmdSel.ExecuteReader();
                queryResult.Read();
                string KolicinaKrviBaza = queryResult.GetString(0);
                queryResult.Close();


                int KolicinaKrviPomocna = Int32.Parse(KolicinaKrvi.Text);
                int KolicinaKrviBazaP = Int32.Parse(KolicinaKrviBaza);

                if (KolicinaKrviBazaP < KolicinaKrviPomocna)
                {
                    MessageBox.Show("Nedovoljna kolicina krvi u inventaru!");
                }
                else
                {
                    string query3 = " INSERT INTO donacije (idPacijenta,idKrvneGrupe,KolicinaKrviDonirano,DatumDonacije) VALUES ('" + IdKG1 + "','" + IdKG2 + "','" + KolicinaKrvi.Text + "','" + DatumDonacije.Text + "')";
                    cmdSel = new MySqlCommand(query3, connection);
                    var queryResult1 = cmdSel.ExecuteNonQuery();

                    MessageBox.Show("Krv uspesno donirana!");
                    int KolicinaKIzTB;
                    KolicinaKIzTB = int.Parse(KolicinaKrvi.Text);

                    Ime.Text = "";
                    Prezime.Text = "";
                    KrvnaGrupa.Text = "";
                    KolicinaKrvi.Text = "";

                    int UkupnaKolicinaKrviInventar;
                    


                    string queryQ = "select UkupnaKolicinaKrvi,idKrvneGrupe  from inventar where idKrvneGrupe = '" + IdKG2 + "'";
                    cmdSel = new MySqlCommand(queryQ, connection);


                    var queryResultU = cmdSel.ExecuteReader();
                    queryResultU.Read();
                    UkupnaKolicinaKrviInventar = queryResultU.GetInt32(0);

                    queryResultU.Close();





                    
                    int reazultat2 = UkupnaKolicinaKrviInventar - KolicinaKIzTB;


                    string queryU = "update inventar set UkupnaKolicinaKrvi = '" + reazultat2 + "' where inventar.idKrvneGrupe = '" + IdKG2 + "'";
                    cmdSel = new MySqlCommand(queryU, connection);
                    var queryResultQ = cmdSel.ExecuteNonQuery();
                    connection.Close();

                    string query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 1";
                    connection = new MySqlConnection(MyConString);
                    connection.Open();
                     cmdSel = new MySqlCommand(query, connection);
                     queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    string KrvnaGrupaP = queryResult.GetString(0);
                    APTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 2";
                    connection = new MySqlConnection(MyConString);
                    connection.Open();
                    cmdSel = new MySqlCommand(query, connection);
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    AMTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 3";
                    connection = new MySqlConnection(MyConString);
                    connection.Open();
                    cmdSel = new MySqlCommand(query, connection);
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    BPTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 4";
                    connection = new MySqlConnection(MyConString);
                    cmdSel = new MySqlCommand(query, connection);
                    connection.Open();
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    BMTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 5";
                    connection = new MySqlConnection(MyConString);
                    cmdSel = new MySqlCommand(query, connection);
                    connection.Open();
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    ABPTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 6";
                    connection = new MySqlConnection(MyConString);
                    connection.Open();
                    cmdSel = new MySqlCommand(query, connection);
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    ABMTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 7";
                    connection = new MySqlConnection(MyConString);
                    connection.Open();
                    cmdSel = new MySqlCommand(query, connection);
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    OPTxt.Text = KrvnaGrupaP;
                    queryResult.Close();
                    connection.Close();

                    query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 8";
                    connection = new MySqlConnection(MyConString);
                    connection.Open();
                    cmdSel = new MySqlCommand(query, connection);
                    queryResult = cmdSel.ExecuteReader();
                    queryResult.Read();
                    KrvnaGrupaP = queryResult.GetString(0);
                    OMTxt.Text = KrvnaGrupaP;
                    queryResult.Close();

                    connection.Close();


                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 1";
            MySqlConnection connection = new MySqlConnection(MyConString);
            connection.Open();
            MySqlCommand cmdSel = new MySqlCommand(query, connection);
            var queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            string KrvnaGrupa = queryResult.GetString(0);
            APTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 2";
            connection = new MySqlConnection(MyConString);
            connection.Open();
            cmdSel = new MySqlCommand(query, connection);
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            AMTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 3";
            connection = new MySqlConnection(MyConString);
            connection.Open();
            cmdSel = new MySqlCommand(query, connection);
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            BPTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 4";
            connection = new MySqlConnection(MyConString);
            cmdSel = new MySqlCommand(query, connection);
            connection.Open();
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            BMTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 5";
            connection = new MySqlConnection(MyConString);
            cmdSel = new MySqlCommand(query, connection);
            connection.Open();
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            ABPTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 6";
            connection = new MySqlConnection(MyConString);
            connection.Open();
            cmdSel = new MySqlCommand(query, connection);
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            ABMTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 7";
            connection = new MySqlConnection(MyConString);
            connection.Open();
            cmdSel = new MySqlCommand(query, connection);
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            OPTxt.Text = KrvnaGrupa;
            queryResult.Close();
            connection.Close();

            query = "select UkupnaKolicinaKrvi from inventar where idKrvneGrupe = 8";
            connection = new MySqlConnection(MyConString);
            connection.Open();
            cmdSel = new MySqlCommand(query, connection);
            queryResult = cmdSel.ExecuteReader();
            queryResult.Read();
            KrvnaGrupa = queryResult.GetString(0);
            OMTxt.Text = KrvnaGrupa;
            queryResult.Close();

            connection.Close();


            string mainconn = ConfigurationManager.ConnectionStrings["mysqlconnection"].ConnectionString;
            MySqlConnection sqlconn = new MySqlConnection(mainconn);
            query = "SELECT pacijent.Ime AS Ime, pacijent.Prezime AS Prezime, krvne_grupe.naziv AS Grupa,pacijent.PotrebnaKolicinaKrvi AS Kolicina FROM pacijent,krvne_grupe WHERE pacijent.idKrvneGrupe = krvne_grupe.idKrvneGrupe";
            sqlconn.Open();
            MySqlCommand sqlcomm = new MySqlCommand(query, sqlconn);
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            PacijentiDataGrid.ItemsSource = dt.DefaultView;
            sqlconn.Close();

            const string FMT = "O";
            DateTime Date = DateTime.Now;
            string strDate = Date.ToString(FMT);
            DateTime now2 = DateTime.ParseExact(strDate, FMT, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            strDate = now2.ToString();

            DatumDonacije.Text=strDate;


        }

        private void PacijentiDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Ime.Text = row_selected["Ime"].ToString();
                Prezime.Text = row_selected["Prezime"].ToString();
                KrvnaGrupa.Text = row_selected["Grupa"].ToString();
                KolicinaKrvi.Text = row_selected["Kolicina"].ToString();
            }
        }
    }
}
