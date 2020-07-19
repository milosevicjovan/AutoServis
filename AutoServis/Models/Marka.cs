using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AutoServis.Data;
using AutoServis.Interfaces;
using System.Data.SqlClient;
using System.Windows;

namespace AutoServis.Models
{
    class Marka : IData<Marka>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private string _nazivMarke;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string NazivMarke
        {
            get { return _nazivMarke; }
            set { _nazivMarke = value; }
        }
        #endregion

        #region konstruktor
        public Marka()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblMarka(NazivMarke) values (@marka);";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@marka", NazivMarke);
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
            AutoServisData.Obrisi("tblMarka", "MarkaID", Id);
        }
        public void Azuriraj(Marka novaMarka)
        {
            try
            {
                string sqlUpit = @"update tblMarka set NazivMarke=@marka where MarkaID=@id";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@marka", novaMarka.NazivMarke);
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
                string sqlUpit = @"select count(*) from tblMarka where NazivMarke=@marka;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@marka", NazivMarke);
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
        public static DataTable ListaMarki(string filter)
        {
            string sqlUpit = @"SELECT MarkaID as 'ID marke', NazivMarke as 'Naziv marke'
                               FROM tblMarka WHERE MarkaID like '%" + filter + @"%' or NazivMarke like '%" + filter + @"';";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static Marka UcitajMarku(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblMarka where MarkaID=@id;";
                Marka marka = new Marka();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    marka.Id = Convert.ToInt32(citac["MarkaID"]);
                    marka.NazivMarke = citac["NazivMarke"].ToString();
                }
                return marka;
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
