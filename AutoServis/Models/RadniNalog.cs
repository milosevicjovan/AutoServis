using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServis.Data;
using AutoServis.Interfaces;
using System.Windows;


namespace AutoServis.Models
{
    class RadniNalog : IData<RadniNalog>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private DateTime _datumOtvaranja;
        private DateTime _datumZatvaranja;
        private Vozilo _vozilo;
        private Zaposleni _zaposleni;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime DatumOtvaranja
        {
            get { return _datumOtvaranja; }
            set { _datumOtvaranja = value; }
        }
        public DateTime DatumZatvaranja
        {
            get { return _datumZatvaranja; }
            set { _datumZatvaranja = value; }
        }
        public Vozilo Vozilo
        {
            get { return _vozilo; }
            set { _vozilo = value; }
        }
        public Zaposleni Zaposleni
        {
            get { return _zaposleni; }
            set { _zaposleni = value; }
        }
        #endregion

        #region konstruktor
        public RadniNalog()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblRadniNalog (DatumOtvaranja, DatumZatvaranja, VoziloID, ZaposleniID) 
                               values (@datumOtvaranja, @datumZatvaranja, @voziloId, @zaposleniId)";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@datumOtvaranja", DatumOtvaranja);
                cmd.Parameters.AddWithValue("@datumZatvaranja", DatumZatvaranja);
                cmd.Parameters.AddWithValue("@voziloId", Vozilo.Id);
                cmd.Parameters.AddWithValue("@zaposleniId", Zaposleni.Id);
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
            AutoServisData.Obrisi("tblRadniNalog", "RadniNalogID", Id);
        }
        public void Azuriraj(RadniNalog noviRadniNalog)
        {
            try
            {
                string sqlUpit = @"update tblRadniNalog set DatumOtvaranja=@datumOtvaranja, DatumZatvaranja=@datumZatvaranja,
                                   VoziloID=@voziloId, ZaposleniID=@zaposleniId where RadniNalogID=@id";

                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@datumOtvaranja", noviRadniNalog.DatumOtvaranja);
                cmd.Parameters.AddWithValue("@datumZatvaranja", noviRadniNalog.DatumZatvaranja);
                cmd.Parameters.AddWithValue("@voziloId", noviRadniNalog.Vozilo.Id);
                cmd.Parameters.AddWithValue("@zaposleniId", noviRadniNalog.Zaposleni.Id);
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
                string sqlUpit = @"select count(*) from tblRadniNalog where DatumOtvaranja=@datumOtvaranja AND 
                                   DatumZatvaranja=@datumZatvaranja AND VoziloID=@voziloId AND ZaposleniID=@zaposleniId;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@datumOtvaranja", DatumOtvaranja);
                cmd.Parameters.AddWithValue("@datumZatvaranja", DatumZatvaranja);
                cmd.Parameters.AddWithValue("@voziloId", Vozilo.Id);
                cmd.Parameters.AddWithValue("@zaposleniId", Zaposleni.Id);
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
        public int BrojDelova()
        {
            return AutoServisData.BrojRedovaUBazi("tblDelovi", "RadniNalogID", Id.ToString());
        }
        public int BrojRadova()
        {
            return AutoServisData.BrojRedovaUBazi("tblIzvrseniRadovi", "RadniNalogID", Id.ToString());
        }
        public double IznosDelova()
        {
            if (BrojDelova() <= 0)
            {
                return 0;
            }

            try
            {
                string sqlUpit = @"select Sum(Kolicina*Cena) from tblDelovi where RadniNalogID=@id;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                double rez = Convert.ToDouble(cmd.ExecuteScalar());
                return rez;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom računanja iznosa delova: { e.Message }", "Greška",
                MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        public double IznosRadova()
        {
            if (BrojRadova() <= 0)
            {
                return 0;
            }
            try
            {
                string sqlUpit = @"select Sum(Kolicina*Cena) from tblIzvrseniRadovi where RadniNalogID=@id;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                double rez = Convert.ToDouble(cmd.ExecuteScalar());
                return rez;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom računanja iznosa radova: { e.Message }", "Greška",
                MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        public static DataTable ListaNaloga(string filter)
        {
            string sqlUpit = @"SELECT RadniNalogID as 'ID', CONVERT(VARCHAR(10), DatumOtvaranja, 103) as 'Datum otvaranja', 
                              CONVERT(VARCHAR(10), DatumZatvaranja, 103) as 'Datum zatvaranja',
                              (NazivMarke + ' ' + NazivModela) as 'Vozilo', (ImeVlasnika + ' ' + PrezimeVlasnika) as 'Vlasnik',
                              (ImeZaposlenog + ' ' + PrezimeZaposlenog) as 'Zaposleni'
                              FROM tblRadniNalog join tblVozilo on tblRadniNalog.VoziloID=tblVozilo.VoziloID
                              join tblZaposleni on tblRadniNalog.ZaposleniID=tblZaposleni.ZaposleniID
                              join tblModel on tblVozilo.ModelID=tblModel.ModelID
                              join tblMarka on tblModel.MarkaID=tblMarka.MarkaID
                              join tblVlasnik on tblVozilo.VlasnikID=tblVlasnik.VlasnikID
                              WHERE RadniNalogID like '%" + filter + @"%' or CONVERT(VARCHAR(10), DatumOtvaranja, 103) like '%" + filter + @"%' 
                              or CONVERT(VARCHAR(10), DatumZatvaranja, 103) like '%" + filter + @"%'
                              or (NazivMarke + ' ' + NazivModela) like '%" + filter + @"%' 
                              or (ImeVlasnika + ' ' + PrezimeVlasnika) like '%" + filter + @"%'
                              or (ImeZaposlenog + ' ' + PrezimeZaposlenog) like '%" + filter + @"%';";

            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static RadniNalog UcitajNalog(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblRadniNalog where RadniNalogID=@id";
                RadniNalog nalog = new RadniNalog();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    nalog.Id = Convert.ToInt32(citac["RadniNalogID"]);
                    nalog.DatumOtvaranja = Convert.ToDateTime(citac["DatumOtvaranja"]);
                    nalog.DatumZatvaranja = Convert.ToDateTime(citac["DatumZatvaranja"]);

                    nalog.Vozilo = Vozilo.UcitajVozilo(Convert.ToInt32(citac["VoziloID"]));
                    nalog.Zaposleni = Zaposleni.UcitajZaposlenog(Convert.ToInt32(citac["ZaposleniID"]));
                }
                return nalog;
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
        public static void ObrisiSveRadneNaloge(int voziloId)
        //brise sve radne naloge za odredjeno vozilo
        {
            AutoServisData.Obrisi("tblRadniNalog", "VoziloID", voziloId);
        }
        #endregion 
    }
}
