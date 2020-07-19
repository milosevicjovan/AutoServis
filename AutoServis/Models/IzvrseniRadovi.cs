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
    class IzvrseniRadovi : IData<IzvrseniRadovi>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _redniBroj;
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
        public IzvrseniRadovi()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblIzvrseniRadovi(RedniBroj, Naziv, JedinicaMere, Kolicina, Cena, RadniNalogID)
                               values (@redniBroj, @naziv, @jedinicaMere, @kolicina, @cena, @radniNalogId);";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
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
            //takodje improvizacija
        {
            AutoServisData.Obrisi("tblIzvrseniRadovi", $"RedniBroj={ RedniBroj } AND RadniNalogID", RadniNalog.Id);

            try
            {
                string sqlUpit = @"update tblIzvrseniRadovi set RedniBroj=RedniBroj-1 where RadniNalogID=@id AND RedniBroj>@redniBroj";

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
        public void Azuriraj(IzvrseniRadovi noviRadovi)
        {
            try
            {
                string sqlUpit = @"update tblIzvrseniRadovi set Naziv=@naziv, JedinicaMere=@jedinicaMere, Kolicina=@kolicina,
                                   Cena=@cena where RadniNalogID=@radniNalogId AND RedniBroj=@redniBroj;";

                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@naziv", noviRadovi.Naziv);
                cmd.Parameters.AddWithValue("@jedinicaMere", noviRadovi.JedinicaMere);
                cmd.Parameters.AddWithValue("@kolicina", noviRadovi.Kolicina);
                cmd.Parameters.AddWithValue("@cena", noviRadovi.Cena);
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
                string sqlUpit = @"select count(*) from tblIzvrseniRadovi where Naziv=@naziv AND RedniBroj=@redniBroj AND RadniNalogID=@radniNalogId;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@naziv", Naziv);
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
        public static DataTable ListaIzvrsenihRadova(int id)
        {
            string sqlUpit = $@"Select RedniBroj as 'Rbr', Naziv as 'Naziv', JedinicaMere as 'Jmr',
                               Cast(Kolicina as numeric(36,3)) as 'Količina', Cast(Cena as numeric(36,2)) as 'Cena'
                               FROM tblIzvrseniRadovi where RadniNalogID={ id };";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static IzvrseniRadovi UcitajIzvrseneRadove(int redniBroj, int id)
        //vraca jednu stavku iz narucenih delova sa parametrima redniBroj i id
        {
            try
            {
                string sqlUpit = @"select * from tblIzvrseniRadovi where RedniBroj=@redniBroj AND RadniNalogID=@id";
                IzvrseniRadovi rad = new IzvrseniRadovi();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", redniBroj);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    rad.RedniBroj = Convert.ToInt32(citac["RedniBroj"]);
                    rad.Naziv = citac["Naziv"].ToString();
                    rad.JedinicaMere = citac["JedinicaMere"].ToString();
                    rad.Kolicina = Convert.ToDouble(citac["Kolicina"]);
                    rad.Cena = Convert.ToDouble(citac["Cena"]);
                    rad.RadniNalog = RadniNalog.UcitajNalog(Convert.ToInt32(citac["RadniNalogID"]));
                }
                return rad;
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
        public static void ObrisiSveIzvrseneRadove(int id)
        //brise sve narucene radove za odredjen radni nalog
        {
            AutoServisData.Obrisi("tblIzvrseniRadovi", "RadniNalogID", id);
        }
        #endregion

    }
}
