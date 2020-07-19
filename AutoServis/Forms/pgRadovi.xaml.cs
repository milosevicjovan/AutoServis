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
    /// Interaction logic for pgRadovi.xaml
    /// </summary>
    public partial class pgRadovi : Page
    {
        int naruceniRedni;
        int izvrseniRedni;
        int deloviRedni;

        public pgRadovi()
        {
            InitializeComponent();
            cmbRadniNalog.SelectedIndex = 0;
            UcitajRadneNaloge();
            UcitajRadniNalog();
            UcitajListuNarucenih();
            UcitajListuIzvrsenih();
            UcitajListuDelova();
        }
        private void UcitajRadneNaloge()
        {
            string sqlUpit = @"select tblRadniNalog.RadniNalogID, (Convert(varchar(10), tblRadniNalog.RadniNalogID) + '; ' + tblMarka.NazivMarke + ' ' 
                               + tblModel.NazivModela + '; ' + tblVlasnik.ImeVlasnika + ' ' + tblVlasnik.PrezimeVlasnika) as 'Nalog'
                               From tblRadniNalog join tblVozilo on tblRadniNalog.VoziloID=tblVozilo.VoziloID
                                                  join tblVlasnik on tblVozilo.VlasnikID=tblVlasnik.VlasnikID
                                                  join tblModel on tblVozilo.ModelID=tblModel.ModelID
                                                  join tblMarka on tblModel.MarkaID=tblMarka.MarkaID;";

            cmbRadniNalog.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
        }
        private void UcitajRadniNalog()
        {
            if (cmbRadniNalog.SelectedValue != null)
            {
                RadniNalog nalog = RadniNalog.UcitajNalog(Convert.ToInt32(cmbRadniNalog.SelectedValue));
                tbRadniNalog.Text = $@"ID naloga: { nalog.Id }, Vozilo: { nalog.Vozilo.Model.Marka.NazivMarke } { nalog.Vozilo.Model.NazivModela }"
                                    +$@", Vlasnik: { nalog.Vozilo.Vlasnik.ImeVlasnika } { nalog.Vozilo.Vlasnik.PrezimeVlasnika }";
                tbIzvrseni.Text = $@"Izvršeni radovi: { nalog.IznosRadova().ToString("F2") }";
                tbDelovi.Text = $@"Ugrađeni delovi: { nalog.IznosDelova().ToString("F2") }";
            }
        }
        private void cmbRadniNalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRadniNalog.SelectedValue != null)
            {
                UcitajListuNarucenih();
                UcitajListuIzvrsenih();
                UcitajListuDelova();
            }
        }

        #region naruceni
        private void UcitajNaruceni()
        {
            if (dgNaruceni.Items.Count <= 0 || cmbRadniNalog.SelectedValue == null)
            {
                txtOpisNarucenih.Text = "";
                return;
            }
            
            int id = Convert.ToInt32(cmbRadniNalog.SelectedValue);
            DataRowView red = (DataRowView)dgNaruceni.SelectedItems[0];
            int rbr = Convert.ToInt32(red[0]);
            NaruceniRadovi naruceni = NaruceniRadovi.UcitajNaruceneRadove(rbr, id);
            naruceniRedni = rbr;
            txtOpisNarucenih.Text = naruceni.Opis;
        }
        private void UcitajListuNarucenih()
        {
            tbPoruka1.Text = "";
            if (cmbRadniNalog.SelectedValue == null)
            {
                return;
            }
            int id = Convert.ToInt32(cmbRadniNalog.SelectedValue);
            dgNaruceni.ItemsSource = NaruceniRadovi.ListaNarucenihRadova(id).DefaultView;
            UcitajNaruceni();
        }
        private void btnSacuvajNaruceni_Click(object sender, RoutedEventArgs e)
        {
            
            if (cmbRadniNalog.SelectedValue == null)
            {
                tbPoruka1.Text = "Morate izabrati radni nalog.";
                return;
            }

            if (String.IsNullOrEmpty(txtOpisNarucenih.Text))
            {
                tbPoruka1.Text = "Morate uneti opis.";
                return;
            }

            NaruceniRadovi naruceni = new NaruceniRadovi();
            naruceni.RedniBroj = naruceniRedni;
            naruceni.Opis = txtOpisNarucenih.Text;
            naruceni.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(cmbRadniNalog.SelectedValue));

            bool azuriraj = false;

            foreach (DataRowView red in dgNaruceni.ItemsSource)
            {
                if (Convert.ToInt32(red[0]) == naruceni.RedniBroj)
                {
                    azuriraj = true;
                    break;
                }
            }

            if (azuriraj == true)
            {
                NaruceniRadovi stari = NaruceniRadovi.UcitajNaruceneRadove(naruceniRedni, 
                    Convert.ToInt32(cmbRadniNalog.SelectedValue));
                stari.Azuriraj(naruceni);
                UcitajListuNarucenih();
                return;
            }

            naruceni.Sacuvaj();
            UcitajListuNarucenih();
        }
        private void dgNaruceni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcitajNaruceni();
        }
        private void btnNovoNaruceni_Click(object sender, RoutedEventArgs e)
        {
            naruceniRedni = dgNaruceni.Items.Count + 1;
            txtOpisNarucenih.Text = "";
            txtOpisNarucenih.Focus();
        }
        private void btnObrisiNaruceni_Click(object sender, RoutedEventArgs e)
        {
            if (dgNaruceni.Items.Count > 0 && cmbRadniNalog.SelectedValue != null)
            {
                DataRowView red = (DataRowView)dgNaruceni.SelectedItems[0];
                int rbr = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni?",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    NaruceniRadovi rad = NaruceniRadovi.UcitajNaruceneRadove(rbr, Convert.ToInt32(cmbRadniNalog.SelectedValue));
                    rad.Obrisi();

                    UcitajListuNarucenih();
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
        #endregion

        #region izvrseni 

        private void UcitajIzvrseni()
        {
            if (dgIzvrseni.Items.Count <= 0 || cmbRadniNalog.SelectedValue == null)
            {
                txtNazivIzvrsenih.Text = "";
                txtKolicina.Text = "";
                txtCena.Text = "";
                txtJmr.Text = "";
                izvrseniRedni = 1;
                return;
            }

            int id = Convert.ToInt32(cmbRadniNalog.SelectedValue);
            DataRowView red = (DataRowView)dgIzvrseni.SelectedItems[0];
            int rbr = Convert.ToInt32(red[0]);

            IzvrseniRadovi izvrseni = IzvrseniRadovi.UcitajIzvrseneRadove(rbr, id);
            izvrseniRedni = rbr;
            txtNazivIzvrsenih.Text = izvrseni.Naziv;
            txtKolicina.Text = izvrseni.Kolicina.ToString("F3");
            txtJmr.Text = izvrseni.JedinicaMere;
            txtCena.Text = izvrseni.Cena.ToString("F2");
        }
        private void UcitajListuIzvrsenih()
        {
            tbPoruka2.Text = "";
            if (cmbRadniNalog.SelectedValue == null)
            {
                return;
            }
            int id = Convert.ToInt32(cmbRadniNalog.SelectedValue);
            dgIzvrseni.ItemsSource = IzvrseniRadovi.ListaIzvrsenihRadova(id).DefaultView;
            UcitajIzvrseni();
            UcitajRadniNalog();
        }
        private void btnSacuvajIzvrseni_Click(object sender, RoutedEventArgs e)
        {

            if (cmbRadniNalog.SelectedValue == null)
            {
                tbPoruka2.Text = "Morate izabrati radni nalog.";
                return;
            }

            if (String.IsNullOrEmpty(txtNazivIzvrsenih.Text))
            {
                tbPoruka2.Text = "Morate uneti naziv usluge.";
                return;
            }

            if (String.IsNullOrEmpty(txtKolicina.Text))
            {
                tbPoruka2.Text = "Morate uneti količinu.";
                return;
            }

            if (String.IsNullOrEmpty(txtJmr.Text))
            {
                tbPoruka2.Text = "Morate uneti jedinicu mere.";
                return;
            }

            if (String.IsNullOrEmpty(txtCena.Text))
            {
                tbPoruka2.Text = "Morate uneti cenu.";
                return;
            }

            IzvrseniRadovi izvrseni = new IzvrseniRadovi();

            try {

                izvrseni.RedniBroj = izvrseniRedni;
                izvrseni.Naziv = txtNazivIzvrsenih.Text;
                izvrseni.Kolicina = Convert.ToDouble(txtKolicina.Text);
                izvrseni.Cena = Convert.ToDouble(txtCena.Text);
                izvrseni.JedinicaMere = txtJmr.Text;
                izvrseni.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(cmbRadniNalog.SelectedValue));
            }
            catch (Exception)
            {
                tbPoruka2.Text = "Niste uneli ispravne vrednosti.";
                return;
            }
            bool azuriraj = false;

            foreach (DataRowView red in dgIzvrseni.ItemsSource)
            {
                if (Convert.ToInt32(red[0]) == izvrseni.RedniBroj)
                {
                    azuriraj = true;
                    break;
                }
            }

            if (azuriraj == true)
            {
                IzvrseniRadovi stari = IzvrseniRadovi.UcitajIzvrseneRadove(izvrseniRedni,
                    Convert.ToInt32(cmbRadniNalog.SelectedValue));
                stari.Azuriraj(izvrseni);
                UcitajListuIzvrsenih();
                return;
            }

            izvrseni.Sacuvaj();
            UcitajListuIzvrsenih();
        }
        private void dgIzvrseni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcitajIzvrseni();
        }
        private void btnNovoIzvrseni_Click(object sender, RoutedEventArgs e)
        {
            izvrseniRedni = dgIzvrseni.Items.Count + 1;
            txtNazivIzvrsenih.Text = "";
            txtKolicina.Text = "";
            txtCena.Text = "";
            txtJmr.Text = "";
            txtNazivIzvrsenih.Focus();
        }
        private void btnObrisiIzvrseni_Click(object sender, RoutedEventArgs e)
        {
            if (dgIzvrseni.Items.Count > 0 && cmbRadniNalog.SelectedValue != null)
            {
                DataRowView red = (DataRowView)dgIzvrseni.SelectedItems[0];
                int rbr = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni?",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    IzvrseniRadovi rad = IzvrseniRadovi.UcitajIzvrseneRadove(rbr, Convert.ToInt32(cmbRadniNalog.SelectedValue));
                    rad.Obrisi();

                    UcitajListuIzvrsenih();
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

        #endregion

        #region delovi

        private void UcitajDeo()
        {
            if (dgDelovi.Items.Count <= 0 || cmbRadniNalog.SelectedValue == null)
            {
                txtNazivDela.Text = "";
                txtSifra.Text = "";
                txtKolicinaDelovi.Text = "";
                txtCenaDelovi.Text = "";
                txtJmrDelovi.Text = "";
                deloviRedni = 1;
                return;
            }

            int id = Convert.ToInt32(cmbRadniNalog.SelectedValue);
            DataRowView red = (DataRowView)dgDelovi.SelectedItems[0];
            int rbr = Convert.ToInt32(red[0]);

            Delovi deo = Delovi.UcitajDeo(rbr, id);
            deloviRedni = rbr;
            txtNazivDela.Text = deo.Naziv;
            txtSifra.Text = deo.Sifra;
            txtKolicinaDelovi.Text = deo.Kolicina.ToString("F3");
            txtCenaDelovi.Text = deo.Cena.ToString("F2");
            txtJmrDelovi.Text = deo.JedinicaMere;
        }
        private void UcitajListuDelova()
        {
            tbPoruka3.Text = "";
            if (cmbRadniNalog.SelectedValue == null)
            {
                return;
            }
            int id = Convert.ToInt32(cmbRadniNalog.SelectedValue);
            dgDelovi.ItemsSource = Delovi.ListaDelova(id).DefaultView;
            UcitajDeo();
            UcitajRadniNalog();
        }
        private void btnSacuvajDelovi_Click(object sender, RoutedEventArgs e)
        {
            if (cmbRadniNalog.SelectedValue == null)
            {
                tbPoruka3.Text = "Morate izabrati radni nalog.";
                return;
            }
            if (String.IsNullOrEmpty(txtSifra.Text))
            {
                tbPoruka3.Text = "Morate uneti šifru.";
                return;
            }
            if (String.IsNullOrEmpty(txtNazivDela.Text))
            {
                tbPoruka3.Text = "Morate uneti naziv dela.";
                return;
            }
            if (String.IsNullOrEmpty(txtKolicinaDelovi.Text))
            {
                tbPoruka3.Text = "Morate uneti količinu.";
                return;
            }

            if (String.IsNullOrEmpty(txtJmrDelovi.Text))
            {
                tbPoruka3.Text = "Morate uneti jedinicu mere.";
                return;
            }

            if (String.IsNullOrEmpty(txtCenaDelovi.Text))
            {
                tbPoruka3.Text = "Morate uneti cenu.";
                return;
            }

            Delovi deo = new Delovi();

            try {

            deo.RedniBroj = deloviRedni;
            deo.Sifra = txtSifra.Text;
            deo.Naziv = txtNazivDela.Text;
            deo.Kolicina = Convert.ToDouble(txtKolicinaDelovi.Text);
            deo.Cena = Convert.ToDouble(txtCenaDelovi.Text);
            deo.JedinicaMere = txtJmrDelovi.Text;
            deo.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(cmbRadniNalog.SelectedValue));

            }
            catch (Exception)
            {
                tbPoruka3.Text = "Niste uneli ispravne vrednosti.";
                return;
            }

            bool azuriraj = false;

            foreach (DataRowView red in dgDelovi.ItemsSource)
            {
                if (Convert.ToInt32(red[0]) == deo.RedniBroj)
                {
                    azuriraj = true;
                    break;
                }
            }

            if (azuriraj == true)
            {
                Delovi stari = Delovi.UcitajDeo(deloviRedni, 
                    Convert.ToInt32(cmbRadniNalog.SelectedValue));
                stari.Azuriraj(deo);
                UcitajListuDelova();
                return;
            }

            deo.Sacuvaj();
            UcitajListuDelova();
        }
        private void dgDelovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UcitajDeo();
        }
        private void btnNovoDelovi_Click(object sender, RoutedEventArgs e)
        {
            deloviRedni = dgDelovi.Items.Count + 1;
            txtNazivDela.Text = "";
            txtSifra.Text = "";
            txtKolicinaDelovi.Text = "";
            txtCenaDelovi.Text = "";
            txtJmrDelovi.Text = "";
            txtSifra.Focus();
        }
        private void btnObrisiDelovi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDelovi.Items.Count > 0 && cmbRadniNalog.SelectedValue != null)
            {
                DataRowView red = (DataRowView)dgDelovi.SelectedItems[0];
                int rbr = Convert.ToInt32(red[0]);

                try
                {
                    MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni?",
                                                            "Upozorenje",
                                                             MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rez != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    Delovi deo = Delovi.UcitajDeo(rbr, Convert.ToInt32(cmbRadniNalog.SelectedValue));
                    deo.Obrisi();
                    UcitajListuDelova();
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

        #endregion
  
    }
}
