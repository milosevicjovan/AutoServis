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
    class NaruceniRadovi : IData<NaruceniRadovi>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _redniBroj;
        private string _opis;
        private RadniNalog _radniNalog;
        #endregion

        #region getteri i setteri
        public int RedniBroj
        {
            get { return _redniBroj; }
            set { _redniBroj = value; }
        }
        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }
        public RadniNalog RadniNalog
        {
            get { return _radniNalog; }
            set { _radniNalog = value; }
        }
        #endregion

        #region konstruktor
        public NaruceniRadovi()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblNaruceniRadovi (RedniBroj, Opis, RadniNalogID) values (@redniBroj, @opis, @radniNalogId)";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@opis", Opis);
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
            //improvizacija metode obrisi :))))))
        {
            AutoServisData.Obrisi("tblNaruceniRadovi", $"RedniBroj={ RedniBroj } AND RadniNalogID", RadniNalog.Id);

            try
            {
                string sqlUpit = @"update tblNaruceniRadovi set RedniBroj=RedniBroj-1 where RadniNalogID=@id AND RedniBroj>@redniBroj";

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
        public void Azuriraj(NaruceniRadovi noviRadovi)
        {
            try
            {
                string sqlUpit = @"update tblNaruceniRadovi set Opis=@opis where RedniBroj=@redniBroj AND RadniNalogID=@id";

                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@opis", noviRadovi.Opis);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@id", RadniNalog.Id);
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
                string sqlUpit = @"select count(*) from tblRadniNalog where Opis=@opis AND RedniBroj=@redniBroj AND RadniNalogID=@id;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@opis", Opis);
                cmd.Parameters.AddWithValue("@redniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@id", RadniNalog.Id);
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
        public static DataTable ListaNarucenihRadova(int RadniNalogID)
            //i ovde moramo proslediti argument ID radnog naloga, da bi prikazali samo radove za taj radni nalog
        {
            string sqlUpit = $@"Select RedniBroj as 'Rbr', Opis as 'Opis radova'
                               FROM tblNaruceniRadovi where RadniNalogID={ RadniNalogID };";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static NaruceniRadovi UcitajNaruceneRadove(int redniBroj, int id)
            //vraca jednu stavku iz narucenih radova sa parametrima redniBroj i id
        {
            try
            {
                string sqlUpit = @"select * from tblNaruceniRadovi where RedniBroj=@redniBroj AND RadniNalogID=@id";
                NaruceniRadovi rad = new NaruceniRadovi();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@redniBroj", redniBroj);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    rad.RedniBroj = Convert.ToInt32(citac["RedniBroj"]);
                    rad.Opis = citac["Opis"].ToString();
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
        public static void ObrisiSveNaruceneRadove(int radniNalogId)
            //brise sve narucene radove za odredjen radni nalog
        {
            AutoServisData.Obrisi("tblNaruceniRadovi", "RadniNalogID", radniNalogId);
        }
        #endregion

    }
}
