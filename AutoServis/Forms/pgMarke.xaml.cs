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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using AutoServis.Data;
using AutoServis.Models;

namespace AutoServis.Forms
{
    /// <summary>
    /// Interaction logic for pgMarke.xaml
    /// </summary>
    public partial class pgMarke : Page
    {
        string filter1;
        string filter2;
        public pgMarke()
        {
            InitializeComponent();
            filter1 = "";
            filter2 = "";
            tbPoruka1.Text = "";
            tbPoruka2.Text = "";
            UcitajMarke();
            UcitajListuMarki();
            UcitajListuModela();
        }
        private void UcitajMarku()
        {
            if (dgPregledMarke.Items.Count <= 0)
            {
                txtIDMarke.Text = "";
                txtNazivMarke.Text = "";
                return;
            }
            DataRowView red = (DataRowView)dgPregledMarke.SelectedItems[0];

            int id = Convert.ToInt32(red[0]);
            Marka marka = Marka.UcitajMarku(id);
            txtIDMarke.Text = marka.Id.ToString();
            txtNazivMarke.Text = marka.NazivMarke.ToString();
            
        }
        private void UcitajListuMarki()
        {
            tbPoruka1.Text = "";
            dgPregledMarke.ItemsSource = Marka.ListaMarki(filter1).DefaultView;
            UcitajMarku();
        }
        private void UcitajModel()
        {
            if (dgPregledModela.Items.Count <= 0)
            {
                txtIDModel.Text = "";
                txtNazivModela.Text = "";
                cmbMarka.Text = "";
                return;
            }
            DataRowView red = (DataRowView)dgPregledModela.SelectedItems[0];

            int id = Convert.ToInt32(red[0]);
            Model model = Model.UcitajModel(id);
            txtIDModel.Text = model.Id.ToString();
            txtNazivModela.Text = model.NazivModela.ToString();
            cmbMarka.SelectedValue = model.Marka.Id;
        }
        private void UcitajListuModela()
        {
            tbPoruka2.Text = "";
            dgPregledModela.ItemsSource = Model.ListaModela(filter2).DefaultView;
            UcitajModel();
        }
        private void UcitajMarke()
        {
            string sqlUpit = "select MarkaID, NazivMarke from tblMarka;";
            cmbMarka.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
        }
        private void btnSacuvajMarka_Click(object sender, RoutedEventArgs e)
        {
            
            if (String.IsNullOrEmpty(txtNazivMarke.Text))
            {
                tbPoruka1.Text = "Morate uneti naziv marke vozila.";
                return;
            }

            tbPoruka1.Text = "";

            Marka novaMarka = new Marka();

            novaMarka.NazivMarke = txtNazivMarke.Text;


            if (String.IsNullOrEmpty(txtIDMarke.Text) != true)
            {
                Marka staraMarka = Marka.UcitajMarku(Convert.ToInt32(txtIDMarke.Text));
                staraMarka.Azuriraj(novaMarka);
            }
            else
            {
                if (novaMarka.PostojiDuplikat())
                {
                    tbPoruka1.Text = "Ova marka već postoji u bazi. Ne možete sačuvati duplikat.";
                    return;
                }
                novaMarka.Sacuvaj();
            }
            UcitajListuMarki();
        }
        private void btnNovoMarka_Click(object sender, RoutedEventArgs e)
        {
            txtIDMarke.Text = "";
            txtNazivMarke.Text = "";
            txtNazivMarke.Focus();
        }
        private void btnNovoModel_Click(object sender, RoutedEventArgs e)
        {
            txtIDModel.Text = "";
            txtNazivModela.Text = "";
            cmbMarka.SelectedValue = null;
            cmbMarka.Focus();
        }
        private void dgPregledMarke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcitajMarku();
        }
        private void dgPregledModela_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcitajModel();
        }
        private void btnObrisiMarka_Click(object sender, RoutedEventArgs e)
        {
            if (dgPregledMarke.Items.Count > 0)
            {
                DataRowView red = (DataRowView)dgPregledMarke.SelectedItems[0];
                int id = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni?",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    Marka marka = Marka.UcitajMarku(id);
                    marka.Obrisi();
                    UcitajListuMarki();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Niste izabrali red.", "Greška",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Došlo je do greške prilikom pokušaja brisanja podataka: { ex.Message }.", "Greška",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnObrisiModel_Click(object sender, RoutedEventArgs e)
        {
            if (dgPregledModela.Items.Count > 0)
            {
                DataRowView red = (DataRowView)dgPregledModela.SelectedItems[0];
                int id = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni?",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    Model model = Model.UcitajModel(id);
                    model.Obrisi();
                    UcitajListuModela();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Niste izabrali red.", "Greška",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Došlo je do greške prilikom pokušaja brisanja podataka: { ex.Message }.", "Greška",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btnSacuvajModel_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtNazivModela.Text))
            {
                tbPoruka2.Text = "Morate uneti naziv modela vozila.";
                return;
            }
            if (cmbMarka.SelectedValue == null)
            {
                tbPoruka2.Text = "Morate izabrati marku vozila.";
                return;
            }
            tbPoruka2.Text = "";

            Model noviModel = new Model();

            noviModel.NazivModela = txtNazivModela.Text;
            noviModel.Marka = Marka.UcitajMarku(Convert.ToInt32(cmbMarka.SelectedValue));


            if (String.IsNullOrEmpty(txtIDModel.Text) != true)
            {
                Model stariModel = Model.UcitajModel(Convert.ToInt32(txtIDModel.Text));
                stariModel.Azuriraj(noviModel);
            }
            else
            {
                if (noviModel.PostojiDuplikat())
                {
                    tbPoruka2.Text = "Ova marka već postoji u bazi. Ne možete sačuvati duplikat.";
                    return;
                }
                noviModel.Sacuvaj();
            }
            UcitajListuModela();
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                txtPretragaMarka.Text = "";
                txtPretragaMarka.Focus();
            }
            if (e.Key == Key.F4)
            {
                txtPretragaModel.Text = "";
                txtPretragaModel.Focus();
            }
        }
        private void btnOcistiFilterMarka_Click(object sender, RoutedEventArgs e)
        {
            filter1 = "";
            txtPretragaMarka.Text = "Pretraga (F3)";
            UcitajListuMarki();
        }
        private void btnFiltrirajMarka_Click(object sender, RoutedEventArgs e)
        {
            filter1 = txtPretragaMarka.Text;
            UcitajListuMarki();
            txtPretragaMarka.Text = "Pretraga (F3)";
        }
        private void btnFiltrirajModel_Click(object sender, RoutedEventArgs e)
        {
            filter2 = txtPretragaModel.Text;
            UcitajListuModela();
            txtPretragaModel.Text = "Pretraga (F4)";
        }
        private void btnOcistiFilterModel_Click(object sender, RoutedEventArgs e)
        {
            filter2 = "";
            txtPretragaModel.Text = "Pretraga (F4)";
            UcitajListuModela();
        }
    }
}
