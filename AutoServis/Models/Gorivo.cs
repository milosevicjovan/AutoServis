using AutoServis.Interfaces;
using AutoServis.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoServis.Models
{
    class Gorivo : IData<Gorivo>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private string _vrstaGoriva;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string VrstaGoriva
        {
            get { return _vrstaGoriva; }
            set { _vrstaGoriva = value; }
        }
        #endregion

        #region konstruktor
        public Gorivo()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblGorivo(VrstaGoriva) values (@vrstaGoriva)";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@vrstaGoriva", VrstaGoriva);
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
            AutoServisData.Obrisi("tblGorivo", "VrstaGorivaID", Id);
        }
        public void Azuriraj(Gorivo novaVrstaGoriva)
        {
            try
            {
                string sqlUpit = @"update tblGorivo set VrstaGoriva=@vrstaGoriva where VrstaGorivaID=@id";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@vrstaGoriva", novaVrstaGoriva.VrstaGoriva);
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
                string sqlUpit = @"select count(*) from tblGorivo where VrstaGoriva=@vrstaGoriva;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@vrstaGoriva", VrstaGoriva);
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
        public static DataTable ListaGoriva()
        {
            string sqlUpit = @"select VrstaGorivaID as 'ID', VrstaGoriva as 'Vrsta goriva' from tblGorivo;";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static Gorivo UcitajGorivo(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblGorivo where VrstaGorivaID=@id;";
                Gorivo gorivo = new Gorivo();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    gorivo.Id = Convert.ToInt32(citac["VrstaGorivaID"]);
                    gorivo.VrstaGoriva = citac["VrstaGoriva"].ToString();

                }
                return gorivo;
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
