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
	/// Interaction logic for pgRadniNalozi.xaml
	/// </summary>
	public partial class pgRadniNalozi : Page
	{ 
		string filter;
		public pgRadniNalozi()
		{
			InitializeComponent();
			filter = "";
			UcitajListuNaloga();
			UcitajVozila();
			UcitajZaposlene();
		}
		private void UcitajNalog()
		{
			if (dgPregled.Items.Count <= 0)
			{
				txtID.Text = "";
				dtDatumOtvaranja.SelectedDate = null;
				dtDatumZatvaranja.SelectedDate = null;
				cmbVozilo.Text = "";
				cmbZaposleni.Text = "";
				tbTip.Text = "";
				tbVlasnik.Text = "";
				tbGodinaProizvodnje.Text = "";
				tbGorivo.Text = "";
				tbBrojSasije.Text = "";
				tbSnagaMotora.Text = "";
				return;
			}
			DataRowView red = (DataRowView)dgPregled.SelectedItems[0];

			int id = Convert.ToInt32(red[0]);
			RadniNalog nalog = RadniNalog.UcitajNalog(id);
			cmbVozilo.SelectedValue = nalog.Vozilo.Id;
			cmbZaposleni.SelectedValue = nalog.Zaposleni.Id;
			txtID.Text = id.ToString();
			dtDatumOtvaranja.SelectedDate = nalog.DatumOtvaranja;
			dtDatumZatvaranja.SelectedDate = nalog.DatumZatvaranja;

			tbTip.Text = $"Tip vozila: { nalog.Vozilo.TipVozila.NazivTipaVozila }";
			tbVlasnik.Text = $"Vlasnik: { nalog.Vozilo.Vlasnik.ImeVlasnika } { nalog.Vozilo.Vlasnik.PrezimeVlasnika }";
			tbGodinaProizvodnje.Text = $"Godina proizvodnje: { nalog.Vozilo.GodinaProizvodnje.ToString() }";
			tbGorivo.Text = $"Pogonsko gorivo: { nalog.Vozilo.VrstaGoriva.VrstaGoriva }";
			tbBrojSasije.Text = $"Broj šasije: { nalog.Vozilo.BrojSasije }";
			tbSnagaMotora.Text = $"Snaga motora: { nalog.Vozilo.SnagaMotora }";
		}
		private void UcitajVozila()
		{
			string sqlUpit = @"select VoziloID, (NazivMarke + ' ' + nazivmodela + 
							   ' - ' + ImeVlasnika + ' ' + PrezimeVlasnika) as Vozilo
							   from tblVozilo join tblModel on tblVozilo.ModelID=tblModel.ModelID
							   join tblMarka on tblModel.MarkaID=tblMarka.MarkaID
							   join tblVlasnik on tblVozilo.VlasnikID=tblVlasnik.VlasnikID;";

			cmbVozilo.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
		}
		private void UcitajZaposlene()
		{
			string sqlUpit = @"select ZaposleniID, (ImeZaposlenog + ' ' + PrezimeZaposlenog) as 'Zaposleni' From tblZaposleni";
			cmbZaposleni.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
		}
		private void UcitajListuNaloga()
		{
			tbPoruka.Text = "";
			dgPregled.ItemsSource = RadniNalog.ListaNaloga(filter).DefaultView;
			UcitajNalog();
		}
		private void btnObrisi_Click(object sender, RoutedEventArgs e)
		{

			if (dgPregled.Items.Count>0)
			{
				DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
				int id = Convert.ToInt32(red[0]);

				try
				{
					MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni? Biće obrisani i svi podaci povezani na radni nalog.", 
															"Upozorenje",
															 MessageBoxButton.YesNo, MessageBoxImage.Question);
					if (rez != MessageBoxResult.Yes)
					{
						return;
					}

					RadniNalog nalog = RadniNalog.UcitajNalog(id);
					Garancija.ObrisiSveGarancijeZaRadniNalog(id);
					Faktura.ObrisiSveFakture(id);                
					NaruceniRadovi.ObrisiSveNaruceneRadove(id);
					Delovi.ObrisiSveDelove(id);
					IzvrseniRadovi.ObrisiSveIzvrseneRadove(id);

					nalog.Obrisi();
					UcitajListuNaloga();
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
			UcitajNalog();
		}
		private void cmbVozilo_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cmbVozilo.SelectedValue != null)
			{
				int voziloId = Convert.ToInt32(cmbVozilo.SelectedValue.ToString());
				Vozilo vozilo = Vozilo.UcitajVozilo(voziloId);
				tbTip.Text = $"Tip vozila: { vozilo.TipVozila.NazivTipaVozila }";
				tbVlasnik.Text = $"Vlasnik: { vozilo.Vlasnik.ImeVlasnika } { vozilo.Vlasnik.PrezimeVlasnika }";
				tbGodinaProizvodnje.Text = $"Godina proizvodnje: { vozilo.GodinaProizvodnje.ToString() }";
				tbGorivo.Text = $"Pogonsko gorivo: { vozilo.VrstaGoriva.VrstaGoriva }";
				tbBrojSasije.Text = $"Broj šasije: { vozilo.BrojSasije }";
				tbSnagaMotora.Text = $"Snaga motora: { vozilo.SnagaMotora }";
			}

		}
		private void btnDodaj_Click(object sender, RoutedEventArgs e)
		{
			txtID.Text = "";
			tbPoruka.Text = "";
			dtDatumOtvaranja.SelectedDate = null;
			dtDatumZatvaranja.SelectedDate = null;
			cmbVozilo.Text = "";
			cmbZaposleni.Text = "";
			tbTip.Text = "";
			tbVlasnik.Text = "";
			tbGodinaProizvodnje.Text = "";
			tbGorivo.Text = "";
			tbBrojSasije.Text = "";
			tbSnagaMotora.Text = "";
			dtDatumOtvaranja.Focus();
		}
		private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
		{

		  if (dtDatumOtvaranja.SelectedDate == null)
			{
				tbPoruka.Text = "Morate izabrati datum otvaranja radnog naloga.";
				return;
			}

			if (dtDatumZatvaranja.SelectedDate == null)
			{
				tbPoruka.Text = "Morate izabrati datum zatvaranja radnog naloga.";
				return;
			}

			if (cmbVozilo.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati vozilo iz liste.";
				return;
			}

		  if (cmbZaposleni.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati zaposlenog.";
				return;
			}

		  tbPoruka.Text = "";
		  RadniNalog noviNalog = new RadniNalog();
		  noviNalog.DatumOtvaranja = Convert.ToDateTime(dtDatumOtvaranja.SelectedDate);
		  noviNalog.DatumZatvaranja = Convert.ToDateTime(dtDatumZatvaranja.SelectedDate);
				 
		  noviNalog.Zaposleni = Zaposleni.UcitajZaposlenog(Convert.ToInt32(cmbZaposleni.SelectedValue));
		  noviNalog.Vozilo = Vozilo.UcitajVozilo(Convert.ToInt32(cmbVozilo.SelectedValue));

		  if (String.IsNullOrEmpty(txtID.Text)!=true)
			{
				RadniNalog stariNalog = RadniNalog.UcitajNalog(Convert.ToInt32(txtID.Text));
				stariNalog.Azuriraj(noviNalog);
			} else
			{
				if (noviNalog.PostojiDuplikat())
			{
			  tbPoruka.Text = "Ovaj radni nalog već postoji u bazi. Ne možete sačuvati duplikat.";
			  return;
			}
			  noviNalog.Sacuvaj();
			}
		   UcitajListuNaloga();
		}
		private void btnFiltriraj_Click(object sender, RoutedEventArgs e)
		{
			filter = txtPretraga.Text;
			UcitajListuNaloga();
			txtPretraga.Text = "Pretraga (F3)";
		}
		private void btnOcistiFilter_Click(object sender, RoutedEventArgs e)
		{
			filter = "";
			txtPretraga.Text = "Pretraga (F3)";
			UcitajListuNaloga();
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
