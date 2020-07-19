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
using AutoServis.Models;
using AutoServis.Data;
using System.Data;

namespace AutoServis.Forms
{
    /// <summary>
    /// Interaction logic for pgFakture.xaml
    /// </summary>
    public partial class pgFakture : Page
    {
        string filter;
        public pgFakture()
        {
            InitializeComponent();
            filter = "";
            UcitajRadneNaloge();
            UcitajListuFaktura();
        }

        private void UcitajListuFaktura()
        {
            tbPoruka.Text = "";
            dgPregled.ItemsSource = Faktura.ListaFaktura(filter).DefaultView;
            UcitajFakturu();
        }
        private void UcitajRadneNaloge()
        {
            string sqlUpit = @"select tblRadniNalog.RadniNalogID, (Convert(varchar(10), tblRadniNalog.RadniNalogID) + '; ' + tblMarka.NazivMarke + ' ' 
                               + tblModel.NazivModela + '-' + tblVlasnik.ImeVlasnika) as 'Nalog'
                               From tblRadniNalog join tblVozilo on tblRadniNalog.VoziloID=tblVozilo.VoziloID
                                                  join tblVlasnik on tblVozilo.VlasnikID=tblVlasnik.VlasnikID
                                                  join tblModel on tblVozilo.ModelID=tblModel.ModelID
                                                  join tblMarka on tblModel.MarkaID=tblMarka.MarkaID;";

            cmbRadniNalog.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
        }
        private void UcitajFakturu()
        {
            if (dgPregled.Items.Count <= 0)
            {
                txtID.Text = "";
                dtDatum.SelectedDate = null;
                dtValuta.SelectedDate = null;
                txtBrojFiskalnogRacuna.Text = "";
                cmbRadniNalog.Text = "";
                tbRadniNalogID.Text = "";
                tbVlasnik.Text = "";
                tbVozilo.Text = "";
                tbIznosDelova.Text = "";
                tbIznosRadova.Text = "";
                tbUkupno.Text = "";
                return;
            }
            DataRowView red = (DataRowView)dgPregled.SelectedItems[0];

            int id = Convert.ToInt32(red[0]);

            Faktura faktura = Faktura.UcitajFakturu(id);
            cmbRadniNalog.SelectedValue = faktura.RadniNalog.Id;
            txtID.Text = id.ToString();
            dtDatum.SelectedDate = faktura.Datum;
            dtValuta.SelectedDate = faktura.Valuta;
            txtBrojFiskalnogRacuna.Text = faktura.BrojFiskalnogRacuna.ToString();

            tbRadniNalogID.Text = $@"ID radnog naloga: { faktura.RadniNalog.Id }";
            tbVlasnik.Text = $@"Vlasnik: { faktura.RadniNalog.Vozilo.Vlasnik.ImeVlasnika } { faktura.RadniNalog.Vozilo.Vlasnik.PrezimeVlasnika }";
            tbVozilo.Text = $@"Vozilo: { faktura.RadniNalog.Vozilo.Model.Marka.NazivMarke } { faktura.RadniNalog.Vozilo.Model.NazivModela }";
            tbIznosDelova.Text = $@"Iznos delova: { faktura.RadniNalog.IznosDelova().ToString("F2") }";
            tbIznosRadova.Text = $@"Iznos radova: { faktura.RadniNalog.IznosRadova().ToString("F2") }";
            tbUkupno.Text = $@"Ukupan iznos: { (faktura.RadniNalog.IznosRadova() + faktura.RadniNalog.IznosDelova()).ToString("F2") }";
            dgRadovi.ItemsSource = IzvrseniRadovi.ListaIzvrsenihRadova(faktura.RadniNalog.Id).DefaultView;
            dgDelovi.ItemsSource = Delovi.ListaDelova(faktura.RadniNalog.Id).DefaultView;

        }
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgPregled.Items.Count > 0)
            {
                DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
                int id = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni? Biće obrisani i sve garancije povezane na fakturu.",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    Faktura faktura = Faktura.UcitajFakturu(id);
                    Garancija.ObrisiSveGarancije(id);

                    faktura.Obrisi();
                    UcitajListuFaktura();
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
            UcitajFakturu();
        }
        private void cmbRadniNalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRadniNalog.SelectedValue != null)
            {
                RadniNalog nalog = RadniNalog.UcitajNalog(Convert.ToInt32(cmbRadniNalog.SelectedValue));
                tbRadniNalogID.Text = $@"ID radnog naloga: { nalog.Id }";
                tbVlasnik.Text = $@"Vlasnik: { nalog.Vozilo.Vlasnik.ImeVlasnika } { nalog.Vozilo.Vlasnik.PrezimeVlasnika }";
                tbVozilo.Text = $@"Vozilo: { nalog.Vozilo.Model.Marka.NazivMarke } { nalog.Vozilo.Model.NazivModela }";

                tbIznosDelova.Text = $@"Iznos delova: { nalog.IznosDelova().ToString("F2") }";
                tbIznosRadova.Text = $@"Iznos radova: { nalog.IznosRadova().ToString("F2") }";
                tbUkupno.Text = $@"Ukupan iznos: { (nalog.IznosRadova() + nalog.IznosDelova()).ToString("F2") }";

                dgRadovi.ItemsSource = IzvrseniRadovi.ListaIzvrsenihRadova(nalog.Id).DefaultView;
                dgDelovi.ItemsSource = Delovi.ListaDelova(nalog.Id).DefaultView;
            }
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = "";
            dtDatum.SelectedDate = null;
            dtValuta.SelectedDate = null;
            txtBrojFiskalnogRacuna.Text = "";
            cmbRadniNalog.Text = "";
            tbRadniNalogID.Text = "";
            tbVlasnik.Text = "";
            tbVozilo.Text = "";
            tbIznosDelova.Text = "";
            tbIznosRadova.Text = "";
            tbUkupno.Text = "";
            dtDatum.Focus();
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                txtPretraga.Text = "";
                txtPretraga.Focus();
            }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (dtDatum.SelectedDate == null)
            {
                tbPoruka.Text = "Morate izabrati datum.";
                return;
            }
            if(dtValuta.SelectedDate == null)
            {
                tbPoruka.Text = "Morate izabrati valutu.";
                return;
            }
            if (String.IsNullOrEmpty(txtBrojFiskalnogRacuna.Text))
            {
                tbPoruka.Text = "Morate uneti broj fiskalnog računa.";
                return;
            }
            if (cmbRadniNalog.SelectedValue == null)
            {
                tbPoruka.Text = "Morate izabrati radni nalog.";
                return;
            }
            
            tbPoruka.Text = "";

            Faktura novaFaktura = new Faktura();

            try
            {
            novaFaktura.Datum = Convert.ToDateTime(dtDatum.SelectedDate);
            novaFaktura.Valuta = Convert.ToDateTime(dtValuta.SelectedDate);
            novaFaktura.BrojFiskalnogRacuna = Convert.ToInt32(txtBrojFiskalnogRacuna.Text);
            novaFaktura.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(cmbRadniNalog.SelectedValue));
            }
            catch (Exception)
            {
                tbPoruka.Text = "Niste uneli ispravne vrednosti.";
                return;
            }

            if ((novaFaktura.RadniNalog.BrojDelova() + novaFaktura.RadniNalog.BrojRadova())<=0)
            {
                tbPoruka.Text = "Ne možete sačuvati fakturu kojoj je iznos radova i delova 0.";
                return;
            }

            if (String.IsNullOrEmpty(txtID.Text) != true)
            {
                Faktura staraFaktura = Faktura.UcitajFakturu(Convert.ToInt32(txtID.Text));
                staraFaktura.Azuriraj(novaFaktura);
            } else
            {
                if (novaFaktura.PostojiDuplikat())
                {
                    tbPoruka.Text = "Ova faktura već postoji u bazi. Ne možete sačuvati duplikat.";
                    return;
                }
                novaFaktura.Sacuvaj();
            }
            UcitajListuFaktura();
        }
        private void btnFiltriraj_Click(object sender, RoutedEventArgs e)
        {
            filter = txtPretraga.Text;
            UcitajListuFaktura();
            txtPretraga.Text = "Pretraga (F3)";
        }
        private void btnOcistiFilter_Click(object sender, RoutedEventArgs e)
        {
            filter = "";
            txtPretraga.Text = "Pretraga (F3)";
            UcitajListuFaktura();
        }
    }
}
