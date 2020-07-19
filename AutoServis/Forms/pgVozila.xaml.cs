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
using eVehicleRegistrationCOM;

namespace AutoServis.Forms
{
	/// <summary>
	/// Interaction logic for pgVozila.xaml
	/// </summary>
	public partial class pgVozila : Page
	{
		string filter;
		public pgVozila()
		{
			InitializeComponent();
			filter = "";
			UcitajGoriva();
			UcitajVlasnike();
			UcitajTipove();
			UcitajMarke();
			UcitajListuVozila();      
		}
		private void UcitajGoriva()
		{
			string sqlUpit = @"select VrstaGorivaID, VrstaGoriva as 'Gorivo' from tblGorivo;";
			cmbGorivo.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
		}
		private void UcitajVlasnike()
		{
			string sqlUpit = @"select VlasnikID, (ImeVlasnika + ' ' + PrezimeVlasnika) as 'Vlasnik'
							   From tblVlasnik;";
			cmbVlasnik.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
		}
		private void UcitajTipove()
		{
			string sqlUpit = @"select TipVozilaID, TipVozila from tblTipVozila";
			cmbTipVozila.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView;
		}
		private void UcitajMarke()
		{
			string sqlUpit = @"select ModelID, (NazivMarke + ' ' + NazivModela) as 'Vozilo'
							   From tblModel join tblMarka on tblModel.MarkaID=tblMarka.MarkaID;";
			cmbMarka.ItemsSource = AutoServisData.UcitajPodatke(sqlUpit).DefaultView; 
		}
		private void UcitajVozilo()
		{
			if (dgPregled.Items.Count <= 0)
			{
			txtID.Text = "";
			txtSnagaMotora.Text = "";
			txtBrojMotora.Text = "";
			txtGodinaProizvodnje.Text = "";
			txtZapreminaMotora.Text = "";
			txtRegOznaka.Text = "";
			txtBrojSasije.Text = "";
			cmbGorivo.SelectedValue = null;
			cmbVlasnik.SelectedValue = null;
			cmbTipVozila.SelectedValue = null;
			cmbMarka.SelectedValue = null;
			return;
			}
			DataRowView red = (DataRowView)dgPregled.SelectedItems[0];

			int id = Convert.ToInt32(red[0]);
			Vozilo vozilo = Vozilo.UcitajVozilo(id);
			txtID.Text = vozilo.Id.ToString();
			txtSnagaMotora.Text = vozilo.SnagaMotora.ToString();
			txtBrojMotora.Text = vozilo.BrojMotora;
			txtGodinaProizvodnje.Text = vozilo.GodinaProizvodnje.ToString();
			txtZapreminaMotora.Text = vozilo.ZapreminaMotora.ToString();
			txtRegOznaka.Text = vozilo.RegistarskaOznaka;
			txtBrojSasije.Text = vozilo.BrojSasije;
			cmbGorivo.SelectedValue = vozilo.VrstaGoriva.Id;
			cmbVlasnik.SelectedValue = vozilo.Vlasnik.Id;
			cmbTipVozila.SelectedValue = vozilo.TipVozila.Id;
			cmbMarka.SelectedValue = vozilo.Model.Id;          
		}
		private void UcitajListuVozila()
		{
			tbPoruka.Text = "";
			dgPregled.ItemsSource = Vozilo.ListaVozila(filter).DefaultView;
			UcitajVozilo();
		}
		private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
		{
			if (cmbGorivo.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati vrstu goriva.";
				return;
			}
			if (cmbVlasnik.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati vlasnika vozila.";
				return;
			}
			if (cmbTipVozila.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati tip vozila.";
				return;
			}
			if (cmbMarka.SelectedValue == null)
			{
				tbPoruka.Text = "Morate izabrati marku i model.";
				return;
			}

			tbPoruka.Text = "";
			Vozilo novoVozilo = new Vozilo();

			try
			{
				novoVozilo.BrojMotora = txtBrojMotora.Text;

				if (!String.IsNullOrEmpty(txtSnagaMotora.Text))
				{
					novoVozilo.SnagaMotora = Convert.ToInt32(txtSnagaMotora.Text);
				}
				if (!String.IsNullOrEmpty(txtGodinaProizvodnje.Text))
				{
					novoVozilo.GodinaProizvodnje = Convert.ToInt32(txtGodinaProizvodnje.Text);
				}
				if (!String.IsNullOrEmpty(txtZapreminaMotora.Text))
				{
					novoVozilo.ZapreminaMotora = Convert.ToInt32(txtZapreminaMotora.Text);
				}

				novoVozilo.RegistarskaOznaka = txtRegOznaka.Text;
				novoVozilo.BrojSasije = txtBrojSasije.Text;

				novoVozilo.TipVozila = TipVozila.UcitajTipVozila(Convert.ToInt32(cmbTipVozila.SelectedValue));
				novoVozilo.VrstaGoriva = Gorivo.UcitajGorivo(Convert.ToInt32(cmbGorivo.SelectedValue));
				novoVozilo.Vlasnik = Vlasnik.UcitajVlasnika(Convert.ToInt32(cmbVlasnik.SelectedValue));
				novoVozilo.Model = Model.UcitajModel(Convert.ToInt32(cmbMarka.SelectedValue));
			}
			catch (Exception)
			{
				tbPoruka.Text = "Niste uneli ispravne vrednosti.";
				return;
			}

			if (String.IsNullOrEmpty(txtID.Text) != true)
			{
				Vozilo staroVozilo = Vozilo.UcitajVozilo(Convert.ToInt32(txtID.Text));
				staroVozilo.Azuriraj(novoVozilo);
			} else
			{
				if (novoVozilo.PostojiDuplikat())
				{
					tbPoruka.Text = "Ovo vozilo već postoji u bazi. Ne možete sačuvati duplikat.";
					return;
				}
				novoVozilo.Sacuvaj();
			}
			UcitajListuVozila();          
		}
		private void btnObrisi_Click(object sender, RoutedEventArgs e)
		{
			if (dgPregled.Items.Count > 0)
			{
				DataRowView red = (DataRowView)dgPregled.SelectedItems[0];
				int id = Convert.ToInt32(red[0]);

				try
				{
					MessageBoxResult rez = MessageBox.Show(@"Da li ste sigurni? Biće obrisani i svi podaci povezani sa ovim vozilom.",
															"Upozorenje",
															 MessageBoxButton.YesNo, MessageBoxImage.Question);
					if (rez != MessageBoxResult.Yes)
					{
						return;
					}

					Vozilo vozilo = Vozilo.UcitajVozilo(id);

					foreach (int idNaloga in Vozilo.ListaNaloga(id))
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
					UcitajListuVozila();
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
			UcitajVozilo();
		}
		private void btnDodaj_Click(object sender, RoutedEventArgs e)
		{
			txtID.Text = "";
			txtSnagaMotora.Text = "";
			txtBrojMotora.Text = "";
			txtGodinaProizvodnje.Text = "";
			txtZapreminaMotora.Text = "";
			txtRegOznaka.Text = "";
			txtBrojSasije.Text = "";
			cmbGorivo.SelectedValue = null;
			cmbVlasnik.SelectedValue = null;
			cmbTipVozila.SelectedValue = null;
			cmbMarka.SelectedValue = null;
			txtSnagaMotora.Focus();
		}
		private void Grid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F3)
			{
				txtPretraga.Text = "";
				txtPretraga.Focus();
			}
		}
		private void btnOcistiFilter_Click(object sender, RoutedEventArgs e)
		{
			filter = "";
			txtPretraga.Text = "Pretraga (F3)";
			UcitajListuVozila();
		}
		private void btnFiltriraj_Click(object sender, RoutedEventArgs e)
		{
			filter = txtPretraga.Text;
			UcitajListuVozila();
			txtPretraga.Text = "Pretraga (F3)";
		}
		private void btnProcitaj_Click(object sender, RoutedEventArgs e)
		{
			eVehicleRegistrationCOM.Registration vd = new eVehicleRegistrationCOM.Registration();
			try {
				string reader = "";
				int procitano = 0;
				vd.Initialize();
				procitano = vd.GetReaderName(0, out reader);
				string citac = reader;

				if (String.IsNullOrEmpty(citac))
				{
					tbPoruka.Text = "Nema učitanih čitača.";
					return;
				}

				_VEHICLE_DATA vhData = new _VEHICLE_DATA();
				vd.Initialize();
				procitano = vd.SelectReader(citac);
				procitano = vd.ProcessNewCard();
				procitano = vd.ReadVehicleData(out vhData);

				txtSnagaMotora.Text = vhData.maximumNetPower;
				txtBrojMotora.Text = vhData.engineIDNumber;
				txtGodinaProizvodnje.Text = vhData.yearOfProduction;
				txtZapreminaMotora.Text = vhData.engineCapacity;
				txtRegOznaka.Text = vhData.registrationNumberOfVehicle;
				txtBrojSasije.Text = vhData.vehicleIDNumber;
				try {
					cmbGorivo.Text = vhData.typeOfFuel;
				}
				catch (Exception) { }
				try
				{
					cmbTipVozila.Text = vhData.vehicleType;
				}
				catch (Exception) { }
			}
			catch (Exception)
			{
				tbPoruka.Text = "Podaci sa saobraćajne dozvole nisu pročitani. Proverite čitač.";
			}
			finally
			{
				vd.Finalize();
			}
		}
	}
}
