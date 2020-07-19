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
using AutoServis.Data;
using AutoServis.Models;

namespace AutoServis.Forms
{
    /// <summary>
    /// Interaction logic for pgZaposleni.xaml
    /// </summary>
    public partial class pgZaposleni : Page
    {
        string filter;
        public pgZaposleni()
        {
            InitializeComponent();
            filter = "";
            UcitajListuZaposlenih();
        }
        private void UcitajZaposlenog()
        {
            if (dgPregled.Items.Count <= 0)
            {
                txtID.Text = "";
                txtIme.Text = "";
                txtPrezime.Text = "";
                txtJMBG.Text = "";
                txtBRLK.Text = "";
                txtGrad.Text = "";
                txtAdresa.Text = "";
                txtKorisnickoIme.Text = "";
                psbLozinka.Password = "";
                tbBrojNaloga.Text = "";
                return;
            }

            DataRowView red = (DataRowView)dgPregled.SelectedItems[0];

            int id = Convert.ToInt32(red[0]);

            Zaposleni zaposleni = Zaposleni.UcitajZaposlenog(id);
            txtID.Text = zaposleni.Id.ToString();
            txtIme.Text = zaposleni.ImeZaposlenog;
            txtPrezime.Text = zaposleni.PrezimeZaposlenog;
            txtJMBG.Text = zaposleni.JMBGZaposlenog;
            txtBRLK.Text = zaposleni.BrojLKZaposlenog;
            txtGrad.Text = zaposleni.GradZaposlenog;
            txtAdresa.Text = zaposleni.AdresaZaposlenog;
            txtKorisnickoIme.Text = zaposleni.KorisnickoIme;
            psbLozinka.Password = zaposleni.Lozinka;
            tbBrojNaloga.Text = $@"Broj radnih naloga: { AutoServisData.BrojRedovaUBazi("tblRadniNalog", "ZaposleniID", zaposleni.Id.ToString()) }";
        }
        private void UcitajListuZaposlenih()
        {
            tbPoruka.Text = "";
            dgPregled.ItemsSource = Zaposleni.ListaZaposlenih(filter).DefaultView;
            UcitajZaposlenog();
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {

            #region validacija
            if (String.IsNullOrEmpty(txtIme.Text))
            {
                tbPoruka.Text = "Morate uneti ime zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(txtPrezime.Text))
            {
                tbPoruka.Text = "Morate uneti prezime zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(txtJMBG.Text))
            {
                tbPoruka.Text = "Morate uneti JMBG zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(txtBRLK.Text))
            {
                tbPoruka.Text = "Morate uneti broj lične karte zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(txtAdresa.Text))
            {
                tbPoruka.Text = "Morate uneti adresu zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(txtGrad.Text))
            {
                tbPoruka.Text = "Morate uneti grad zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(txtKorisnickoIme.Text))
            {
                tbPoruka.Text = "Morate uneti korisničko ime zaposlenog.";
                return;
            }
            if (String.IsNullOrEmpty(psbLozinka.Password))
            {
                tbPoruka.Text = "Morate uneti lozinku zaposlenog.";
                return;
            }
            #endregion

            tbPoruka.Text = "";

            Zaposleni noviZaposleni = new Zaposleni();

            try
            {
                noviZaposleni.ImeZaposlenog = txtIme.Text;
                noviZaposleni.PrezimeZaposlenog = txtPrezime.Text;
                noviZaposleni.JMBGZaposlenog = txtJMBG.Text;
                noviZaposleni.BrojLKZaposlenog = txtBRLK.Text;
                noviZaposleni.GradZaposlenog = txtGrad.Text;
                noviZaposleni.AdresaZaposlenog = txtAdresa.Text;
                noviZaposleni.KorisnickoIme = txtKorisnickoIme.Text;
                noviZaposleni.Lozinka = psbLozinka.Password;
            }
            catch (Exception)
            {
                tbPoruka.Text = "Niste uneli ispravne vrednosti.";
                return;
            }

            if (String.IsNullOrEmpty(txtID.Text) != true)
            {
                Zaposleni stariZaposleni = Zaposleni.UcitajZaposlenog(Convert.ToInt32(txtID.Text));
                stariZaposleni.Azuriraj(noviZaposleni);
            }
            else
            {
                if (noviZaposleni.PostojiDuplikat())
                {
                    tbPoruka.Text = "Ovaj zaposleni već postoji u bazi. Ne možete sačuvati duplikat.";
                    return;
                }
                noviZaposleni.Sacuvaj();
            }
            UcitajListuZaposlenih();
        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgPregled.Items.Count > 0)
            {
                DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
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

                    Zaposleni zaposleni = Zaposleni.UcitajZaposlenog(id);                    
                    zaposleni.Obrisi();
                    UcitajListuZaposlenih();
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
        private void dgPregled_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcitajZaposlenog();
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = "";
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtJMBG.Text = "";
            txtBRLK.Text = "";
            txtGrad.Text = "";
            txtAdresa.Text = "";
            txtKorisnickoIme.Text = "";
            psbLozinka.Password = "";
            txtIme.Focus();
        }
        private void btnFiltriraj_Click(object sender, RoutedEventArgs e)
        {
            filter = txtPretraga.Text;
            UcitajListuZaposlenih();
            txtPretraga.Text = "Pretraga (F3)";
        }
        private void btnOcistiFilter_Click(object sender, RoutedEventArgs e)
        {
            filter = "";
            txtPretraga.Text = "Pretraga (F3)";
            UcitajListuZaposlenih();
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                txtPretraga.Text = "";
                txtPretraga.Focus();
            }
        }
    }
}
