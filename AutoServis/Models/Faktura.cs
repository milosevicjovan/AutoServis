using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using AutoServis.Interfaces;
using AutoServis.Data;

namespace AutoServis.Models
{
	class Faktura : IData<Faktura>
	{
		private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

		#region privatne promenljive
		private int _id;
		private DateTime _datum;
		private DateTime _valuta;
		private int _brojFiskalnogRacuna;
		private RadniNalog _radniNalog;
		#endregion

		#region getteri i setteri
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public DateTime Datum
		{
			get { return _datum; }
			set { _datum = value; }
		}
		public DateTime Valuta
		{
			get { return _valuta; }
			set { _valuta = value; }
		}
		public int BrojFiskalnogRacuna
		{
			get { return _brojFiskalnogRacuna; }
			set { _brojFiskalnogRacuna = value; }
		}
		public RadniNalog RadniNalog
		{
			get { return _radniNalog; }
			set { _radniNalog = value; }
		}
		#endregion

		#region konstruktor
		public Faktura()
		{

		}
		#endregion

		#region metode
		public void Sacuvaj()
		{
			string sqlUpit = @"insert into tblFaktura(Datum, Valuta, BrojFiskalnogRacuna, RadniNalogID) 
							   values (@datum, @valuta, @brojFisk, @radniNalogId)";

			try
			{
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@datum", Datum);
				cmd.Parameters.AddWithValue("@valuta", Valuta);
				cmd.Parameters.AddWithValue("@brojFisk", BrojFiskalnogRacuna);
				cmd.Parameters.AddWithValue("@radniNalogId", RadniNalog.Id);
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
			AutoServisData.Obrisi("tblFaktura", "FakturaID", Id);
		}
		public void Azuriraj(Faktura novaFaktura)
		{
			try
			{
				string sqlUpit = @"update tblFaktura set Datum=@datum, Valuta=@valuta, BrojFiskalnogRacuna=@brojFisk, 
								   RadniNalogID=@radniNalogId where FakturaID=@id";

				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", Id);
				cmd.Parameters.AddWithValue("@datum", novaFaktura.Datum);
				cmd.Parameters.AddWithValue("@valuta", novaFaktura.Valuta);
				cmd.Parameters.AddWithValue("@brojFisk", novaFaktura.BrojFiskalnogRacuna);
				cmd.Parameters.AddWithValue("@radniNalogId", novaFaktura.RadniNalog.Id);
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
				string sqlUpit = @"select count(*) from tblFaktura where RadniNalogID=@radniNalogId or BrojFiskalnogRacuna=@brojFisk;";
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@radniNalogId", RadniNalog.Id);
				cmd.Parameters.AddWithValue("@brojFisk", BrojFiskalnogRacuna);
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
		public static DataTable ListaFaktura(string filter)
		{
			string sqlUpit = @"SELECT tblFaktura.FakturaID as 'ID', CONVERT(VARCHAR(10), tblFaktura.Datum, 103) as 'Datum',
							   CONVERT(VARCHAR(10), tblFaktura.Valuta, 103) as 'Valuta', 
							   tblFaktura.BrojFiskalnogRacuna as 'Fiskalni račun',
							   tblRadniNalog.RadniNalogID as 'Radni nalog ID',
							   tblMarka.NazivMarke + ' ' + tblmodel.NazivModela as 'Vozilo',
							   tblVlasnik.ImeVlasnika + ' ' + tblVlasnik.PrezimeVlasnika as 'Vlasnik'
							   FROM  tblFaktura join tblRadniNalog on tblFaktura.RadniNalogID=tblRadniNalog.RadniNalogID
							   join tblVozilo on tblRadniNalog.VoziloID = tblVozilo.VoziloID
							   join tblVlasnik on tblVozilo.VlasnikID = tblVlasnik.VlasnikID
							   join tblModel on tblVozilo.ModelID = tblModel.ModelID
							   join tblMarka on tblModel.MarkaID = tblMarka.MarkaID
							   WHERE tblFaktura.FakturaID like '%" + filter + @"%' or 
							   CONVERT(VARCHAR(10), tblFaktura.Datum, 103) like '%" + filter + @"%' or 
							   CONVERT(VARCHAR(10), tblFaktura.Valuta, 103) like '%" + filter + @"%' or 
							   tblFaktura.BrojFiskalnogRacuna like '%" + filter + @"%' or 
							   tblRadniNalog.RadniNalogID like '%" + filter + @"%' or  
							   (tblMarka.NazivMarke + ' ' + tblmodel.NazivModela) like '%" + filter + @"%' or 
							   (tblVlasnik.ImeVlasnika + ' ' + tblVlasnik.PrezimeVlasnika) like '%" + filter + @"%';";
				
			return AutoServisData.UcitajPodatke(sqlUpit);
		}
		public static Faktura UcitajFakturu(int id)
		{
			try
			{
				string sqlUpit = @"select * from tblFaktura where FakturaID=@id";
				Faktura faktura = new Faktura();
				konekcija.Open();
				SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader citac = cmd.ExecuteReader();
				while (citac.Read())
				{
					faktura.Id = Convert.ToInt32(citac["FakturaID"]);
					faktura.Datum = Convert.ToDateTime(citac["Datum"]);
					faktura.Valuta = Convert.ToDateTime(citac["Valuta"]);
					faktura.BrojFiskalnogRacuna = Convert.ToInt32(citac["BrojFiskalnogRacuna"]);
					faktura.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(citac["RadniNalogID"]));
				}
				return faktura;
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
		public static void ObrisiSveFakture(int radniNalogId)
		//brise sve fakture za odredjen radni nalog
		{
			AutoServisData.Obrisi("tblFaktura", "RadniNalogID", radniNalogId);
		}
		#endregion
	}
}
