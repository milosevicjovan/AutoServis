using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using AutoServis.Data;
using AutoServis.Interfaces;
using System.Windows;

namespace AutoServis.Models
{
	class Vozilo : IData<Vozilo>
	{
		private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

		#region privatne promenljive
		private int _id;
		private int _snagaMotora;
		private string _brojMotora;
		private int _godinaProizvodnje;
		private int _zapreminaMotora;
		private string _registarskaOznaka;
		private string _brojSasije;

		private Gorivo _vrstaGoriva;
		private Vlasnik _vlasnik;     
		private TipVozila _tipVozila;
		private Model _model;
		#endregion

		#region getteri i setteri
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}      
		public int SnagaMotora
		{
			get { return _snagaMotora; }
			set { _snagaMotora = value; }
		}      
		public string BrojMotora
		{
			get { return _brojMotora; }
			set { _brojMotora = value; }
		}       
		public int GodinaProizvodnje
		{
			get { return _godinaProizvodnje; }
			set { _godinaProizvodnje = value; }
		}        
		public int ZapreminaMotora
		{
			get { return _zapreminaMotora; }
			set { _zapreminaMotora = value; }
		}        
		public string RegistarskaOznaka
		{
			get { return _registarskaOznaka; }
			set { _registarskaOznaka = value; }
		}      
		public string BrojSasije
		{
			get { return _brojSasije; }
			set { _brojSasije = value; }
		}
		public Gorivo VrstaGoriva
		{
			get { return _vrstaGoriva; }
			set { _vrstaGoriva = value; }
		}
		public Vlasnik Vlasnik
		{
			get { return _vlasnik; }
			set { _vlasnik = value; }
		}       
		public TipVozila TipVozila
		{
			get { return _tipVozila; }
			set { _tipVozila = value; }
		}       
		public Model Model
		{
			get { return _model; }
			set { _model = value; }
		}
		#endregion

		#region konstruktor
		public Vozilo()
		{

		}
		#endregion

		#region metode
		public void Sacuvaj()
		{
			string sqlUpit = @"insert into tblVozilo(VrstaGorivaID, SnagaMotora, BrojMotora, GodinaProizvodnje, ZapreminaMotora,
							   RegOznaka, BrojSasije, VlasnikID, TipVozilaID, ModelID) values (@vrstaGorivaId, @snagaMotora, @brojMotora,
							   @godinaProizvodnje, @zapreminaMotora, @regOznaka, @brojSasije, @vlasnikId, @tipVozilaId, @modelId)";

			try
			{
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@vrstaGorivaId", VrstaGoriva.Id);
				cmd.Parameters.AddWithValue("@snagaMotora", SnagaMotora);
				cmd.Parameters.AddWithValue("@brojMotora", BrojMotora);
				cmd.Parameters.AddWithValue("@godinaProizvodnje", GodinaProizvodnje);
				cmd.Parameters.AddWithValue("@zapreminaMotora", ZapreminaMotora);
				cmd.Parameters.AddWithValue("@regOznaka", RegistarskaOznaka);
				cmd.Parameters.AddWithValue("@brojSasije", BrojSasije);
				cmd.Parameters.AddWithValue("@vlasnikId", Vlasnik.Id);
				cmd.Parameters.AddWithValue("@tipVozilaId", TipVozila.Id);
				cmd.Parameters.AddWithValue("@modelId", Model.Id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				MessageBox.Show($"Došlo je do greške: { e.Message }. Podaci nisu sačuvani.", "Greška",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				if (konekcija != null)
				{
					konekcija.Close();
				}
			}
		}
		public void Obrisi()
		{
			AutoServisData.Obrisi("tblVozilo", "VoziloID", Id);
		}
		public void Azuriraj(Vozilo novoVozilo)
		{
			try
			{
				string sqlUpit = @"update tblVozilo set VrstaGorivaID=@vrstaGorivaId, SnagaMotora=@snagaMotora, 
								   BrojMotora=@brojMotora, GodinaProizvodnje=@godinaProizvodnje, ZapreminaMotora=@zapreminaMotora,
								   RegOznaka=@regOznaka, BrojSasije=@brojSasije, VlasnikID=@vlasnikId, TipVozilaID=@tipVozilaId,
								   ModelID=@modelId where VoziloID=@id";

				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", Id);
				cmd.Parameters.AddWithValue("@vrstaGorivaId", novoVozilo.VrstaGoriva.Id);
				cmd.Parameters.AddWithValue("@snagaMotora", novoVozilo.SnagaMotora);
				cmd.Parameters.AddWithValue("@brojMotora", novoVozilo.BrojMotora);
				cmd.Parameters.AddWithValue("@godinaProizvodnje", novoVozilo.GodinaProizvodnje);
				cmd.Parameters.AddWithValue("@zapreminaMotora", novoVozilo.ZapreminaMotora);
				cmd.Parameters.AddWithValue("@regOznaka", novoVozilo.RegistarskaOznaka);
				cmd.Parameters.AddWithValue("@brojSasije", novoVozilo.BrojSasije);
				cmd.Parameters.AddWithValue("@vlasnikId", novoVozilo.Vlasnik.Id);
				cmd.Parameters.AddWithValue("@tipVozilaId", novoVozilo.TipVozila.Id);
				cmd.Parameters.AddWithValue("@modelId", novoVozilo.Model.Id);
				cmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				MessageBox.Show($"Došlo je do greške: { e.Message }. Podaci nisu ažurirani.", "Greška",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				if (konekcija != null)
				{
					konekcija.Close();
				}
			}
		}
		public bool PostojiDuplikat()
		{
			try
			{
				string sqlUpit = @"select count(*) from tblVozilo where ModelID=@modelId
								  AND BrojSasije=@brojSasije AND VrstaGorivaID=@vrstaGorivaId AND SnagaMotora=@snagaMotora
								  AND BrojMotora=@brojMotora AND GodinaProizvodnje=@godinaProizvodnje
								  AND ZapreminaMotora=@zapreminaMotora;";
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@vrstaGorivaId", VrstaGoriva.Id);
				cmd.Parameters.AddWithValue("@snagaMotora", SnagaMotora);
				cmd.Parameters.AddWithValue("@brojMotora", BrojMotora);
				cmd.Parameters.AddWithValue("@godinaProizvodnje", GodinaProizvodnje);
				cmd.Parameters.AddWithValue("@zapreminaMotora", ZapreminaMotora);
				cmd.Parameters.AddWithValue("@brojSasije", BrojSasije);
				cmd.Parameters.AddWithValue("@tipVozilaId", TipVozila.Id);
				cmd.Parameters.AddWithValue("@modelId", Model.Id);
				int rez = Convert.ToInt32(cmd.ExecuteScalar());
				if (rez > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show($"Došlo je do greške prilikom provere duplikata u bazi: { e.Message }", "Greška",
				MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
			finally
			{
				if (konekcija != null)
				{
					konekcija.Close();
				}
			}
		}
		public static DataTable ListaVozila(string filter)
		{
			string sqlUpit = @"SELECT tblVozilo.voziloId as 'ID', tblMarka.NazivMarke + ' ' + tblModel.NazivModela as 'Vozilo',
							   tblVlasnik.ImeVlasnika + ' ' + tblvlasnik.PrezimeVlasnika as 'Vlasnik',
							   tblGorivo.VrstaGoriva as 'Pogonsko gorivo', tblTipVozila.TipVozila as 'Tip vozila', tblVozilo.RegOznaka as 'Registarska oznaka',
							   tblVozilo.SnagaMotora as 'Snaga motora', tblVozilo.BrojMotora as 'Broj motora',
							   tblVozilo.GodinaProizvodnje as 'Godina proizvodnje', tblVozilo.ZapreminaMotora as 'Zapremina motora',
							   tblVozilo.BrojSasije as 'Broj šasije'
							   FROM tblVozilo join tblVlasnik on tblVozilo.VlasnikID=tblVlasnik.VlasnikID
							   join tblModel on tblVozilo.ModelID = tblModel.ModelID
							   join tblMarka on tblModel.MarkaID = tblmarka.MarkaID
							   join tblTipVozila on tblVozilo.TipVozilaID=tblTipVozila.TipVozilaID
							   join tblGorivo on tblVozilo.VrstaGorivaID=tblGorivo.VrstaGorivaID
							   where tblVozilo.VoziloID like '%" + filter + @"%' or 
							   (tblMarka.NazivMarke + ' ' + tblModel.NazivModela) like '%" + filter + @"%' 
							   or (tblVlasnik.ImeVlasnika + ' ' + tblVlasnik.PrezimeVlasnika) like '%" + filter + @"%' 
							   or tblGorivo.VrstaGoriva like '%" + filter + @"%' 
							   or tblTipVozila.TipVozila like '%" + filter + @"%' 
							   or tblVozilo.RegOznaka like '%" + filter + @"%' 
							   or tblVozilo.SnagaMotora like '%" + filter + @"%' 
							   or tblVozilo.BrojMotora like '%" + filter + @"%' 
							   or tblVozilo.GodinaProizvodnje like '%" + filter + @"%' 
							   or tblVozilo.ZapreminaMotora like '%" + filter + @"%' 
							   or tblVozilo.BrojSasije like '%" + filter + @"%';";

			return AutoServisData.UcitajPodatke(sqlUpit);
		}
		public static Vozilo UcitajVozilo(int id)
		{
			try
			{
				string sqlUpit = @"select * from tblVozilo where VoziloID=@id";
				Vozilo vozilo = new Vozilo();
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader citac = cmd.ExecuteReader();
				while (citac.Read())
				{
					vozilo.Id = Convert.ToInt32(citac["VoziloID"]);
					vozilo.SnagaMotora = Convert.ToInt32(citac["SnagaMotora"]);
					vozilo.BrojMotora = citac["BrojMotora"].ToString();
					vozilo.GodinaProizvodnje = Convert.ToInt32(citac["GodinaProizvodnje"]);
					vozilo.ZapreminaMotora = Convert.ToInt32(citac["ZapreminaMotora"]);
					vozilo.RegistarskaOznaka = citac["RegOznaka"].ToString();
					vozilo.BrojSasije = citac["BrojSasije"].ToString();

					vozilo.VrstaGoriva = Gorivo.UcitajGorivo(Convert.ToInt32(citac["VrstaGorivaID"]));
					vozilo.Vlasnik = Vlasnik.UcitajVlasnika(Convert.ToInt32(citac["VlasnikID"]));
					vozilo.TipVozila = TipVozila.UcitajTipVozila(Convert.ToInt32(citac["TipVozilaID"]));
					vozilo.Model = Model.UcitajModel(Convert.ToInt32(citac["ModelID"]));
				}
				return vozilo;
			}
			catch (Exception e)
			{
				MessageBox.Show($"Došlo je do greške: { e.Message }. Podaci nisu učitani.", "Greška",
				MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			}
			finally
			{
				if (konekcija != null)
				{
					konekcija.Close();
				}
			}
		}
		public static List<int> ListaNaloga(int id)
			//vraća listu brojeva radnih naloga za određeno vozilo,
			//da bismo mogli da prođemo kroz listu i obrišemo sve podatke za njih
		{
			try
			{
				string sqlUpit = @"select RadniNalogID from tblRadniNalog where VoziloID=@id";
				List<int> list = new List<int>();
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader citac = cmd.ExecuteReader();
				while (citac.Read())
				{
					list.Add(Convert.ToInt32(citac["RadniNalogID"]));
				}
				return list;
			}
			catch (Exception e)
			{
				MessageBox.Show($"Došlo je do greške prilikom povlačenja naloga iz baze: { e.Message }", "Greška",
				MessageBoxButton.OK, MessageBoxImage.Error);
				return null;
			}
			finally
			{
				if (konekcija != null)
				{
					konekcija.Close();
				}
			}
		}
		#endregion
	}
}
