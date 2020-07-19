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
	/// Interaction logic for pgGarancije.xaml
	/// </summary>
	public partial class pgGarancije : Page
	{
		string filter;
		public pgGarancije()
		{
			InitializeComponent();
			filter = "";
			cmbFaktura.SelectedIndex = 0;
			UcitajFakture();
			UcitajListuGarancija();
		}
		private void UcitajFakturu()
		{
			int id = Convert.ToInt32(cmbFaktura.SelectedValue);
			Faktura faktura = Faktura.UcitajFakturu(id);
			tbFaktura.Text = $@"ID fakture: { faktura.Id }, Datum: { faktura.Datum.ToShortDateString() }, Valuta: { faktura.Datum.ToShortDateString() }";
			tbRadniNalogID.Text = $@"ID radnog naloga: { faktura.RadniNalog.Id }";
			tbVlasnik.Text = $@"Vlasnik: { faktura.RadniNalog.Vozilo.Vlasnik.ImeVlasnika } { faktura.RadniNalog.Vozilo.Vlasnik.PrezimeVlasnika }";
			tbVozilo.Text = $@"Vozilo: { faktura.RadniNalog.Vozilo.Model.Marka.NazivMarke } { faktura.RadniNalog.Vozilo.Model.NazivModela }";
			tbIznosFakture.Text = $@"Iznos fakture: { (faktura.RadniNalog.IznosDelova() + faktura.RadniNalog.IznosRadova()).ToString("F2") }";
		}
		private void UcitajFakture()
		{
			string sqlUpit = @"select tblFaktura.FakturaID, (Convert(varchar(10), tblFaktura.FakturaID) + ' - ' + tblVlasnik.ImeVlasnika
							   + ' ' + tblVlasnik.PrezimeVlasnika + '; ' + tblMarka.NazivMarke + ' ' + tblModel.NazivModela) as 'Faktura'
							   from tblFaktura join tblRadniNalog on tblFaktura.RadniNalogID=tblRadniNalog.RadniNalogID
											   join tblVozilo on tblRadniNalog.VoziloID=tblVozilo.VoziloID
											   join tblVlasnik on tblVozilo.VlasnikID=tblVlasnik.VlasnikID
											   join tblModel on tblVozilo.ModelID=tblModel.ModelID
											   join tblMarka on tblModel.MarkaID=tblMarka.MarkaID;";

			cmbFaktura.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
		}
		private void UcitajGaranciju()
		{
			if (dgPregled.Items.Count <= 0 || cmbFaktura.SelectedValue == null)
			{
				txtID.Text = "";
				txtOpis.Text = "";
				txtRokVazenja.Text = "";
				return;
			}

			DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
			int garancijaID = Convert.ToInt32(red[0]);
			Garancija garancija = Garancija.UcitajGaranciju(garancijaID);
			txtID.Text = garancija.Id.ToString();
			txtOpis.Text = garancija.Opis.ToString();
			txtRokVazenja.Text = garancija.RokVazenja.ToString();
		}
		private void UcitajListuGarancija()
		{
			tbPoruka.Text = "";
			if (cmbFaktura.SelectedValue == null)
			{
				return;
			}
			int id = Convert.ToInt32(cmbFaktura.SelectedValue);
			dgPregled.ItemsSource = Garancija.ListaGarancija(id,filter).DefaultView;
			UcitajGaranciju();
		}
		private void btnObrisi_Click(object sender, RoutedEventArgs e)
		{
			if (dgPregled.Items.Count > 0 && cmbFaktura.SelectedValue != null)
			{
				DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
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

					Garancija garancija = Garancija.UcitajGaranciju(Convert.ToInt32(Convert.ToInt32(txtID.Text)));
					garancija.Obrisi();

					UcitajListuGarancija();
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
		private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
		{
			if (cmbFaktura.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati fakturu.";
				return;
			}
			if (String.IsNullOrEmpty(txtOpis.Text))
			{
				tbPoruka.Text = "Morate uneti opis.";
				return;
			}
			if (String.IsNullOrEmpty(txtRokVazenja.Text))
			{
				tbPoruka.Text = "Morate uneti rok važenja garancije.";
				return;
			}
			tbPoruka.Text = "";
			Garancija novaGarancija = new Garancija();
			try {
				novaGarancija.Opis = txtOpis.Text;
				novaGarancija.RokVazenja = Convert.ToInt32(txtRokVazenja.Text);
				novaGarancija.Faktura = Faktura.UcitajFakturu(Convert.ToInt32(cmbFaktura.SelectedValue));
				}
			catch (Exception)
			{
				tbPoruka.Text = "Niste uneli ispravne vrednosti.";
				return;
			}

			if (String.IsNullOrEmpty(txtID.Text) != true)
			{
				Garancija staraGarancija = Garancija.UcitajGaranciju(Convert.ToInt32(txtID.Text));
				staraGarancija.Azuriraj(novaGarancija);
			} else
			{
				if (novaGarancija.PostojiDuplikat())
				{
					tbPoruka.Text = "Ova garancija već postoji u bazi. Ne možete sačuvati duplikat.";
					return;
				}
				novaGarancija.Sacuvaj();
			}
			UcitajListuGarancija();
		   
		  
		}
		private void dgPregled_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UcitajGaranciju();
		}
		private void cmbFaktura_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UcitajListuGarancija();
			UcitajFakturu();
		}
		private void btnFiltriraj_Click(object sender, RoutedEventArgs e)
		{
			filter = txtPretraga.Text;
			UcitajListuGarancija();
			txtPretraga.Text = "Pretraga (F3)";
		}
		private void btnOcistiFilter_Click(object sender, RoutedEventArgs e)
		{
			filter = "";
			txtPretraga.Text = "Pretraga (F3)";
			UcitajListuGarancija();
		}
		private void Grid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F3)
			{
				txtPretraga.Text = "";
				txtPretraga.Focus();
			}
		}
		private void btnDodaj_Click(object sender, RoutedEventArgs e)
		{
			filter = "";
			UcitajListuGarancija();
			txtID.Text = "";
			txtOpis.Text = "";
			txtRokVazenja.Text = "";
			txtOpis.Focus();
		}
	}
}
