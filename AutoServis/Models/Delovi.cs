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
    class Delovi : IData<Delovi>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _redniBroj;
        private string _sifra;
        private string _naziv;
        private string _jedinicaMere;
        private double _kolicina;
        private double _cena;
        private RadniNalog _radniNalog;
        #endregion

        #region getteri i setteri
        public int RedniBroj
        {
            get { return _redniBroj; }
            set { _redniBroj = value; }
        }
        public string Sifra
        {
            get { return _sifra; }
            set { _sifra = value; }
        }
        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }
        public string JedinicaMere
        {
            get { return _jedinicaMere; }
            set { _jedinicaMere = value; }
        }
        public double Kolicina
        {
            get { return _kolicina; }
            set { _kolicina = value; }
        }
        public double Cena
        {
            get { return _cena; }
            set { _cena = value; }
        }
        public RadniNalog RadniNalog
        {
            get { return _radniNalog; }
            set { _radniNalog = value; }
        }
        #endregion

        #region konstruktor
        public Delovi()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblDelovi(RedniBroj, Sifra, Naziv, JedinicaMere, Kolicina, Cena, RadniNalogID)
                               values (@redniBroj, @sifra, @naziv, @jedinicaMere, @kolicina, @cena, @radniNalogId);";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@sifra", Sifra);
                cmd.Parameters.AddWithValue("@naziv", Naziv);
                cmd.Parameters.AddWithValue("@jedinicaMere", JedinicaMere);
                cmd.Parameters.AddWithValue("@kolicina", Kolicina);
                cmd.Parameters.AddWithValue("@cena", Cena);
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
            //moramo da improvizujemo da bismo obrisali samo jedan deo za jedan radni nalog :)
        {
            AutoServisData.Obrisi("tblDelovi", $"RadniNalogID={ RadniNalog.Id } AND RedniBroj", RedniBroj);
            try
            {
                string sqlUpit = @"update tblDelovi set RedniBroj=RedniBroj-1 where RadniNalogID=@id AND RedniBroj>@redniBroj";

                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", RadniNalog.Id);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom ažuriranja rednih brojeva: { e.Message }", "Greška",
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
        public void Azuriraj(Delovi noviDeo)
        {
            try
            {
                string sqlUpit = @"update tblDelovi set Sifra=@sifra, Naziv=@naziv, JedinicaMere=@jedinicaMere, Kolicina=@kolicina,
                                   Cena=@cena where RadniNalogID=@radniNalogId AND RedniBroj=@redniBroj;";

                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@sifra", noviDeo.Sifra);
                cmd.Parameters.AddWithValue("@naziv", noviDeo.Naziv);
                cmd.Parameters.AddWithValue("@jedinicaMere", noviDeo.JedinicaMere);
                cmd.Parameters.AddWithValue("@kolicina", noviDeo.Kolicina);
                cmd.Parameters.AddWithValue("@cena", noviDeo.Cena);
                cmd.Parameters.AddWithValue("@radniNalogId", RadniNalog.Id);
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
                string sqlUpit = @"select count(*) from tblDelovi where Sifra=@sifra AND RedniBroj=@redniBroj AND RadniNalogID=@radniNalogId;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@sifra", Sifra);
                cmd.Parameters.AddWithValue("@radniNalogId", RadniNalog.Id);
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
        }
        public static DataTable ListaDelova(int id)
        //i ovde moramo proslediti argument ID radnog naloga, da bi prikazali samo delove za taj radni nalog
        {
            string sqlUpit = $@"Select RedniBroj as 'Rbr', Sifra as 'Šifra', Naziv as 'Naziv', JedinicaMere as 'Jmr',
                               Cast(Kolicina as numeric(36,3)) as 'Količina', Cast(Cena as numeric(36,2)) as 'Cena'
                               FROM tblDelovi where RadniNalogID={ id };";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }    
        public static Delovi UcitajDeo(int redniBroj, int id)
        //vraca jednu stavku iz narucenih delova sa parametrima redniBroj i id
        {
            try
            {
                string sqlUpit = @"select * from tblDelovi where RedniBroj=@redniBroj AND RadniNalogID=@id";
                Delovi deo = new Delovi();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", redniBroj);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    deo.RedniBroj = Convert.ToInt32(citac["RedniBroj"]);
                    deo.Sifra = citac["Sifra"].ToString();
                    deo.Naziv = citac["Naziv"].ToString();
                    deo.JedinicaMere = citac["JedinicaMere"].ToString();
                    deo.Kolicina = Convert.ToDouble(citac["Kolicina"]);
                    deo.Cena = Convert.ToDouble(citac["Cena"]);
                    deo.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(citac["RadniNalogID"]));
                }
                return deo;
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
        public static void ObrisiSveDelove(int id)
        //brise sve delove za odredjen radni nalog
        {
            AutoServisData.Obrisi("tblDelovi", "RadniNalogID", id);
        }
        #endregion
    }
}
