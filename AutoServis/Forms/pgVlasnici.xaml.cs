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
    /// Interaction logic for pgVlasnici.xaml
    /// </summary>
    public partial class pgVlasnici : Page
    {
        string filter = "";
        public pgVlasnici()
        {
            InitializeComponent();
            filter = "";
            UcitajListuVlasnika();
        }
        private void UcitajVlasnika()
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
                txtKontakt.Text = "";
                return;
            }

            DataRowView red = (DataRowView)dgPregled.SelectedItems[0];

            int id = Convert.ToInt32(red[0]);

            Vlasnik vlasnik = Vlasnik.UcitajVlasnika(id);
            txtID.Text = vlasnik.Id.ToString();
            txtIme.Text = vlasnik.ImeVlasnika;
            txtPrezime.Text = vlasnik.PrezimeVlasnika;
            txtJMBG.Text = vlasnik.JMBGVlasnika;
            txtBRLK.Text = vlasnik.BrojLKVlasnika;
            txtGrad.Text = vlasnik.GradVlasnika;
            txtAdresa.Text = vlasnik.AdresaVlasnika;
            txtKontakt.Text = vlasnik.KontaktVlasnika;
        }
        private void UcitajListuVlasnika()
        {
            tbPoruka.Text = "";
            dgPregled.ItemsSource = Vlasnik.ListaVlasnika(filter).DefaultView;
            UcitajVlasnika();
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtIme.Text))
            {
                tbPoruka.Text = "Morate napisati ime vlasnika.";
                return;
            }
            if (String.IsNullOrEmpty(txtKontakt.Text))
            {
                tbPoruka.Text = "Morate napisati kontakt vlasnika.";
                return;
            }
            if (String.IsNullOrEmpty(txtGrad.Text))
            {
                tbPoruka.Text = "Morate napisati grad vlasnika.";
                return;
            }
            tbPoruka.Text = "";

            Vlasnik noviVlasnik = new Vlasnik();

            try
            {
                noviVlasnik.ImeVlasnika = txtIme.Text;
                noviVlasnik.PrezimeVlasnika = txtPrezime.Text;
                noviVlasnik.JMBGVlasnika = txtJMBG.Text;
                noviVlasnik.BrojLKVlasnika = txtBRLK.Text;
                noviVlasnik.GradVlasnika = txtGrad.Text;
                noviVlasnik.AdresaVlasnika = txtAdresa.Text;
                noviVlasnik.KontaktVlasnika = txtKontakt.Text;
            }
            catch (Exception)
            {
                tbPoruka.Text = "Niste uneli ispravne vrednosti.";
                return;
            }

            if (String.IsNullOrEmpty(txtID.Text) != true)
            {
                Vlasnik stariVlasnik = Vlasnik.UcitajVlasnika(Convert.ToInt32(txtID.Text));
                stariVlasnik.Azuriraj(noviVlasnik);
            } else
            {
                if (noviVlasnik.PostojiDuplikat())
                {
                    tbPoruka.Text = "Ovaj vlasnik već postoji u bazi. Ne možete sačuvati duplikat.";
                    return;
                }
                noviVlasnik.Sacuvaj();
            }
            UcitajListuVlasnika();
        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgPregled.Items.Count > 0)
            {
                DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
                int id = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni? Biće obrisani i svi podaci povezani sa vlasnikom.",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    //mora ovako da bismo isli unazad i obrisali sve povezane podatke
                    Vlasnik vlasnik = Vlasnik.UcitajVlasnika(id);
                    foreach (int idVozila in Vlasnik.ListaVozila(id))
                    {
                        Vozilo vozilo = Vozilo.UcitajVozilo(idVozila);
                        foreach (int idNaloga in Vozilo.ListaNaloga(idVozila))
                        {
                            RadniNalog nalog = RadniNalog.UcitajNalog(idNaloga);
                            Garancija.ObrisiSveGarancijeZaRadniNalog(idNaloga);
                            Faktura.ObrisiSveFakture(idNaloga);
                            NaruceniRadovi.ObrisiSveNaruceneRadove(idNaloga);
                            Delovi.ObrisiSveDelove(idNaloga);
                            IzvrseniRadovi.ObrisiSveIzvrseneRadove(idNaloga);
                            nalog.Obrisi();
                        }
                        vozilo.Obrisi();
                    }
                    vlasnik.Obrisi();
                    UcitajListuVlasnika();
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
            UcitajVlasnika();
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
            txtKontakt.Text = "";
            txtIme.Focus();
        }
        private void btnFiltriraj_Click(object sender, RoutedEventArgs e)
        {
            filter = txtPretraga.Text;
            UcitajListuVlasnika();
            txtPretraga.Text = "Pretraga (F3)";
        }
        private void btnOcistiFilter_Click(object sender, RoutedEventArgs e)
        {
            filter = "";
            txtPretraga.Text = "Pretraga (F3)";
            UcitajListuVlasnika();
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
