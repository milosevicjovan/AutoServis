using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using AutoServis.Data;
using AutoServis.Interfaces;

namespace AutoServis.Models
{
	class Garancija : IData<Garancija>
	{
		private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

		#region privatne promenljive
		private int _id;
		private string _opis;
		private int _rokVazenja;
		private Faktura _faktura;
		#endregion

		#region getteri i setteri
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public Faktura Faktura
		{
			get { return _faktura; }
			set { _faktura = value; }
		}
		public string Opis
		{
			get { return _opis; }
			set { _opis = value; }
		}
		public int RokVazenja
		{
			get { return _rokVazenja; }
			set { _rokVazenja = value; }
		}
		#endregion

		#region konstruktor
		public Garancija()
		{

		}
		#endregion

		#region metode
		public void Sacuvaj()
		{
			string sqlUpit = @"insert into tblGarancija(FakturaID, Opis, RokVazenja) values (@fakturaId, @opis, @rokVazenja);";

			try
			{
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@fakturaId", Faktura.Id);
				cmd.Parameters.AddWithValue("@opis", Opis);
				cmd.Parameters.AddWithValue("@rokVazenja", RokVazenja);
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
			AutoServisData.Obrisi("tblGarancija", "GarancijaID", Id);
		}
		public void Azuriraj(Garancija novaGarancija)
		{
			try
			{
				string sqlUpit = @"update tblGarancija set FakturaID=@fakturaId, Opis=@opis, RokVazenja=@rokVazenja
								   where GarancijaID=@id;";

				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", Id);
				cmd.Parameters.AddWithValue("@fakturaId", novaGarancija.Faktura.Id);
				cmd.Parameters.AddWithValue("@opis", novaGarancija.Opis);
				cmd.Parameters.AddWithValue("@rokVazenja", novaGarancija.RokVazenja);
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
				string sqlUpit = @"select count(*) from tblGarancija where FakturaID=@fakturaId and Opis=@opis and RokVazenja=@rokVazenja;";
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@fakturaId", Faktura.Id);
				cmd.Parameters.AddWithValue("@opis", Opis);
				cmd.Parameters.AddWithValue("@rokVazenja", RokVazenja);
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
		public static DataTable ListaGarancija(int id, string filter)
			//prikazuje listu garancija za odredjenu fakturu
		{
			string sqlUpit = @"SELECT tblGarancija.GarancijaID as 'ID garancije',tblFaktura.FakturaID as 'ID fakture',
							  tblVlasnik.ImeVlasnika + ' ' + tblVlasnik.PrezimeVlasnika as 'Kupac',
							  tblGarancija.Opis, 
							  CONVERT(VARCHAR(10), (DATEADD(month, tblGarancija.RokVazenja, tblFaktura.Datum)), 103) as 'Važi do datuma:'
							  FROM tblGarancija join tblFaktura on tblGarancija.FakturaID = tblFaktura.FakturaID
							  join tblRadniNalog on tblFaktura.RadniNalogID = tblRadniNalog.RadniNalogID
							  join tblVozilo on tblRadniNalog.VoziloID = tblVozilo.VoziloID
							  join tblVlasnik on tblVozilo.VlasnikID = tblVlasnik.VlasnikID
							  where tblFaktura.FakturaID=" + id + @" AND
							  (tblGarancija.GarancijaID like '%" + filter + @"%' OR tblFaktura.FakturaID like '%" + filter + @"%'
							  OR (tblVlasnik.ImeVlasnika + ' ' + tblVlasnik.PrezimeVlasnika) like '%" + filter + @"%'
							  OR tblGarancija.Opis like '%" + filter + @"%' 
							  OR (DATEADD(month, tblGarancija.RokVazenja, tblFaktura.Datum)) like '%" + filter + @"%'
							  OR tblGarancija.RokVazenja like '%" + filter + @"%');";

			return AutoServisData.UcitajPodatke(sqlUpit);
		}
		public static Garancija UcitajGaranciju(int id)
		{
			try
			{
				string sqlUpit = @"select * from tblGarancija where GarancijaID=@id";
				Garancija garancija = new Garancija();
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader citac = cmd.ExecuteReader();
				while (citac.Read())
				{
					garancija.Id = Convert.ToInt32(citac["GarancijaID"]);
					garancija.Opis = citac["Opis"].ToString();
					garancija.RokVazenja = Convert.ToInt32(citac["RokVazenja"]);
					garancija.Faktura = Faktura.UcitajFakturu(Convert.ToInt32(citac["FakturaID"]));
				}
				return garancija;
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
		public static void ObrisiSveGarancije(int fakturaId)
		//brise sve garancije za odredjenu fakturu
		{
			AutoServisData.Obrisi("tblGarancija", "FakturaID", fakturaId);
		}
		public static void ObrisiSveGarancijeZaRadniNalog(int radniNalogId)
			//nabudzeno je al trebalo bi da radi
			//ideja je da se prvo izvuku sve garancije koje imaju referencu na neki radni nalog
			//skladiste se u listu garancija jedna po jedna
			//zatim se prolazi kroz listu i brisu se jedna po jedna da slucajno ne bismo obrisali sve iz tabele
			//RADI!!!! NE DIRATI NISTA!!!
		{
			try
			{
				string sqlUpit = @"select tblGarancija.GarancijaID, tblGarancija.FakturaID, tblGarancija.Opis, tblGarancija.RokVazenja
								   from tblGarancija join tblFaktura on tblFaktura.FakturaID=tblGarancija.FakturaID
								   join tblRadniNalog on tblFaktura.RadniNalogID=tblRadniNalog.RadniNalogID
								   where tblRadniNalog.RadniNalogID=@id;";

				Garancija garancija;
				List<Garancija> listaGarancija = new List<Garancija>();
				
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", radniNalogId);
				SqlDataReader citac = cmd.ExecuteReader();
				while (citac.Read())
				{
					garancija = new Garancija();
					garancija.Id = Convert.ToInt32(citac["GarancijaID"]);
					garancija.Opis = citac["Opis"].ToString();
					garancija.RokVazenja = Convert.ToInt32(citac["RokVazenja"]);
					garancija.Faktura = Faktura.UcitajFakturu(Convert.ToInt32(citac["FakturaID"]));
					listaGarancija.Add(garancija);
				}

				foreach(Garancija tempGarancija in listaGarancija)
				{
					AutoServisData.Obrisi("tblGarancija", "GarancijaID", tempGarancija.Id);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show($"Došlo je do greške prilikom brisanja garancije za radni nalog: { e.Message }.", "Greška",
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
		#endregion
	}
}
