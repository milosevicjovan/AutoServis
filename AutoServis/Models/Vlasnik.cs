using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServis.Interfaces;
using AutoServis.Data;
using System.Windows;

namespace AutoServis.Models
{
    class Vlasnik : IData<Vlasnik>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private string _imeVlasnika;
        private string _prezimeVlasnika;
        private string _kontaktVlasnika;
        private string _jmbgVlasnika;
        private string _gradVlasnika;
        private string _brojLKVlasnika;
        private string _adresaVlasnika;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string ImeVlasnika
        {
            get { return _imeVlasnika; }
            set { _imeVlasnika = value; }
        }
        public string PrezimeVlasnika
        {
            get { return _prezimeVlasnika; }
            set { _prezimeVlasnika = value; }
        }
        public string KontaktVlasnika
        {
            get { return _kontaktVlasnika; }
            set { _kontaktVlasnika = value; }
        }
        public string JMBGVlasnika
        {
            get { return _jmbgVlasnika; }
            set { _jmbgVlasnika = value; }
        }
        public string GradVlasnika
        {
            get { return _gradVlasnika; }
            set { _gradVlasnika = value; }
        }
        public string BrojLKVlasnika
        {
            get { return _brojLKVlasnika; }
            set { _brojLKVlasnika = value; }
        }
        public string AdresaVlasnika
        {
            get { return _adresaVlasnika; }
            set { _adresaVlasnika = value; }
        }
        #endregion

        #region konstruktor
        public Vlasnik()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblVlasnik(ImeVlasnika, PrezimeVlasnika, KontaktVlasnika, JMBGVlasnika, AdresaVlasnika,
                               GradVlasnika, BrojLKVlasnika) 
                               values (@ime, @prezime, @kontakt, @jmbg, @adresa, @grad, @brojLK)";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@ime", ImeVlasnika);
                cmd.Parameters.AddWithValue("@prezime", PrezimeVlasnika);
                cmd.Parameters.AddWithValue("@kontakt", KontaktVlasnika);
                cmd.Parameters.AddWithValue("@jmbg", JMBGVlasnika);
                cmd.Parameters.AddWithValue("@adresa", AdresaVlasnika);
                cmd.Parameters.AddWithValue("@grad", GradVlasnika);
                cmd.Parameters.AddWithValue("@brojLK", BrojLKVlasnika);
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
            AutoServisData.Obrisi("tblVlasnik", "VlasnikID", Id);
        }
        public void Azuriraj(Vlasnik noviVlasnik)
        //pozivamo vlasnik.AzurirajVlasnika(noviVlasnik) gde vlasniku dodajemo sve osobine novogVlasnika; MINDFUCK
        {
            try
            {
                string sqlUpit = @"update tblVlasnik set ImeVlasnika=@ime, PrezimeVlasnika=@prezime, KontaktVlasnika=@kontakt,
                                  JMBGVlasnika=@jmbg,
                                  AdresaVlasnika=@adresa, GradVlasnika=@grad, 
                                  BrojLKVlasnika=@brojLK where VlasnikID=@id;";

                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@ime", noviVlasnik.ImeVlasnika);
                cmd.Parameters.AddWithValue("@prezime", noviVlasnik.PrezimeVlasnika);
                cmd.Parameters.AddWithValue("@kontakt", KontaktVlasnika);
                cmd.Parameters.AddWithValue("@jmbg", noviVlasnik.JMBGVlasnika);
                cmd.Parameters.AddWithValue("@adresa", noviVlasnik.AdresaVlasnika);
                cmd.Parameters.AddWithValue("@grad", noviVlasnik.GradVlasnika);
                cmd.Parameters.AddWithValue("@brojLK", noviVlasnik.BrojLKVlasnika);
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
                string sqlUpit = @"select count(*) from tblVlasnik where ImeVlasnika=@ime AND JMBGVlasnika=@jmbg;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@ime", ImeVlasnika);
                cmd.Parameters.AddWithValue("@jmbg", JMBGVlasnika);
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
                if(konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        public static DataTable ListaVlasnika(string filter)
        {
            string sqlUpit = @"select VlasnikID as 'ID', ImeVlasnika as 'Ime', PrezimeVlasnika as 'Prezime', KontaktVlasnika as 'Kontakt',
                                      JMBGVlasnika as 'JMBG', AdresaVlasnika as 'Adresa', GradVlasnika as 'Grad',
                                      BrojLKVlasnika as 'Broj LK'
                                      FROM tblVlasnik
                                      where ImeVlasnika like '%" + filter + @"%' or 
                                      PrezimeVlasnika like '%" + filter + @"%' or
                                      KontaktVlasnika like '%" + filter + @"%' or
                                      JMBGVlasnika like '%" + filter + @"%' or
                                      AdresaVlasnika like '%" + filter + @"%' or
                                      GradVlasnika like '%" + filter + @"%' or
                                      BrojLKVlasnika like '%" + filter + @"%';";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static Vlasnik UcitajVlasnika(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblVlasnik where VlasnikID=@id";
                Vlasnik vlasnik = new Vlasnik();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    vlasnik.Id = Convert.ToInt32(citac["VlasnikID"]);
                    vlasnik.ImeVlasnika = citac["ImeVlasnika"].ToString();
                    vlasnik.PrezimeVlasnika = citac["PrezimeVlasnika"].ToString();
                    vlasnik.KontaktVlasnika = citac["KontaktVlasnika"].ToString();
                    vlasnik.JMBGVlasnika = citac["JMBGVlasnika"].ToString();
                    vlasnik.AdresaVlasnika = citac["AdresaVlasnika"].ToString();
                    vlasnik.GradVlasnika = citac["GradVlasnika"].ToString();
                    vlasnik.BrojLKVlasnika = citac["BrojLKVlasnika"].ToString();
                }
                return vlasnik;
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
        public static List<int> ListaVozila(int id)
        //vraća listu brojeva vozila za vlasnika sa prosledjenim id brojem
        {
            try
            {
                string sqlUpit = @"select VoziloID from tblVozilo where VlasnikID=@id";
                List<int> list = new List<int>();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    list.Add(Convert.ToInt32(citac["VoziloID"]));
                }
                return list;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom povlačenja vozila iz baze: { e.Message }", "Greška",
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
