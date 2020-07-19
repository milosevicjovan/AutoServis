using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServis.Interfaces;
using AutoServis.Data;
using System.Windows;

namespace AutoServis.Models
{
    class Zaposleni : Interfaces.IData<Zaposleni>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private string _imeZaposlenog;
        private string _prezimeZaposlenog;
        private string _jmbgZaposlenog;
        private string _gradZaposlenog;
        private string _korisnickoIme;
        private string _lozinka;
        private string _brojLKZaposlenog;
        private string _adresaZaposlenog;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string ImeZaposlenog
        {
            get { return _imeZaposlenog; }
            set { _imeZaposlenog = value; }
        }
        public string PrezimeZaposlenog
        {
            get { return _prezimeZaposlenog; }
            set { _prezimeZaposlenog = value; }
        }
        public string JMBGZaposlenog
        {
            get { return _jmbgZaposlenog; }
            set { _jmbgZaposlenog = value; }
        }
        public string GradZaposlenog
        {
            get { return _gradZaposlenog; }
            set { _gradZaposlenog = value; }
        }
        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set { _korisnickoIme = value; }
        }
        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka= value; }
        }
        public string BrojLKZaposlenog
        {
            get { return _brojLKZaposlenog; }
            set { _brojLKZaposlenog = value; }
        }
        public string AdresaZaposlenog
        {
            get { return _adresaZaposlenog; }
            set { _adresaZaposlenog = value; }
        }
        #endregion

        #region konstruktor
        public Zaposleni()
        {

        }
        #endregion

        #region metode
        public void Obrisi()
        {
            AutoServisData.Obrisi("tblZaposleni", "ZaposleniID", Id);
        }
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblZaposleni(ImeZaposlenog, PrezimeZaposlenog, JMBGZaposlenog, AdresaZaposlenog,
                               GradZaposlenog, KorisnickoIme, Lozinka, BrojLKZaposlenog)
                               values (@ime, @prezime, @jmbg, @adresa, @grad, @korisnickoIme, @lozinka, @brojLK)";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@ime", ImeZaposlenog);
                cmd.Parameters.AddWithValue("@prezime", PrezimeZaposlenog);
                cmd.Parameters.AddWithValue("@jmbg", JMBGZaposlenog);
                cmd.Parameters.AddWithValue("@adresa", AdresaZaposlenog);
                cmd.Parameters.AddWithValue("@grad", GradZaposlenog);
                cmd.Parameters.AddWithValue("@korisnickoIme", KorisnickoIme);
                cmd.Parameters.AddWithValue("@lozinka", Lozinka);
                cmd.Parameters.AddWithValue("@brojLK", BrojLKZaposlenog);
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
        public void Azuriraj(Zaposleni noviZaposleni)
        {
            try
            {
                string sqlUpit = @"update tblZaposleni set ImeZaposlenog=@ime, PrezimeZaposlenog=@prezime, JMBGZaposlenog=@jmbg,
                               AdresaZaposlenog=@adresa, GradZaposlenog=@grad, KorisnickoIme=@korisnickoIme, Lozinka=@lozinka, 
                               BrojLKZaposlenog=@brojLK where ZaposleniID=@id;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@ime", noviZaposleni.ImeZaposlenog);
                cmd.Parameters.AddWithValue("@prezime", noviZaposleni.PrezimeZaposlenog);
                cmd.Parameters.AddWithValue("@jmbg", noviZaposleni.JMBGZaposlenog);
                cmd.Parameters.AddWithValue("@adresa", noviZaposleni.AdresaZaposlenog);
                cmd.Parameters.AddWithValue("@grad", noviZaposleni.GradZaposlenog);
                cmd.Parameters.AddWithValue("@korisnickoIme", noviZaposleni.KorisnickoIme);
                cmd.Parameters.AddWithValue("@lozinka", noviZaposleni.Lozinka);
                cmd.Parameters.AddWithValue("@brojLK", noviZaposleni.BrojLKZaposlenog);
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
                string sqlUpit = @"select count(*) from tblZaposleni where ImeZaposlenog=@ime AND JMBGZaposlenog=@jmbg;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@ime", ImeZaposlenog);
                cmd.Parameters.AddWithValue("@jmbg", JMBGZaposlenog);
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
        public static DataTable ListaZaposlenih(string filter)
        {
            string sqlUpit = @"select ZaposleniID as 'ID', ImeZaposlenog as 'Ime', PrezimeZaposlenog as 'Prezime',
                                      JMBGZaposlenog as 'JMBG', AdresaZaposlenog as 'Adresa', GradZaposlenog as 'Grad',
                                      KorisnickoIme as 'Korisničko ime', 
                                      BrojLKZaposlenog as 'Broj LK'
                                      FROM tblZaposleni
                                      where ImeZaposlenog like '%" + filter + @"%' or 
                                      PrezimeZaposlenog like '%" + filter + @"%' or
                                      JMBGZaposlenog like '%" + filter + @"%' or
                                      AdresaZaposlenog like '%" + filter + @"%' or
                                      GradZaposlenog like '%" + filter + @"%' or
                                      KorisnickoIme like '%" + filter + @"%' or
                                      BrojLKZaposlenog like '%" + filter + @"%';";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static Zaposleni UcitajZaposlenog(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblZaposleni where ZaposleniID=@id";
                Zaposleni zaposleni = new Models.Zaposleni();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    zaposleni.Id = Convert.ToInt32(citac["ZaposleniID"]);
                    zaposleni.ImeZaposlenog = citac["ImeZaposlenog"].ToString();
                    zaposleni.PrezimeZaposlenog = citac["PrezimeZaposlenog"].ToString();
                    zaposleni.JMBGZaposlenog = citac["JMBGZaposlenog"].ToString();
                    zaposleni.AdresaZaposlenog = citac["AdresaZaposlenog"].ToString();
                    zaposleni.GradZaposlenog = citac["GradZaposlenog"].ToString();
                    zaposleni.KorisnickoIme = citac["KorisnickoIme"].ToString();
                    zaposleni.Lozinka = citac["Lozinka"].ToString();
                    zaposleni.BrojLKZaposlenog = citac["BrojLKZaposlenog"].ToString();

                }
                return zaposleni;
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
        #endregion
    }
}
