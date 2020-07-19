using System;
using AutoServis.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServis.Interfaces;
using System.Windows;

namespace AutoServis.Models
{
    class TipVozila : IData<TipVozila>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private string _nazivTipaVozila;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string NazivTipaVozila
        {
            get { return _nazivTipaVozila; }
            set { _nazivTipaVozila = value; }
        }
        #endregion

        #region konstruktor
        public TipVozila()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblTipVozila(TipVozila) values (@tipVozila);";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@tipVozila", NazivTipaVozila);
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
            AutoServisData.Obrisi("tblTipVozila", "TipVozilaID", Id);
        }
        public void Azuriraj(TipVozila noviTipVozila)
        {
            try
            {
                string sqlUpit = @"update tblTipVozila set TipVozila=@tipVozila where TipVozilaID=@id";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@tipVozila", noviTipVozila.NazivTipaVozila);
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
                string sqlUpit = @"select count(*) from tblTipVozila where TipVozila=@tipVozila;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@tipVozila", NazivTipaVozila);
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
        public static DataTable ListaTipovaVozila()
        {
            string sqlUpit = @"select TipVozilaID as 'ID', TipVozila as 'Tip vozila' from tblTipVozila;";
            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static TipVozila UcitajTipVozila(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblTipVozila where TipVozilaID=@id;";
                TipVozila tip = new TipVozila();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    tip.Id = Convert.ToInt32(citac["TipVozilaID"]);
                    tip.NazivTipaVozila = citac["TipVozila"].ToString();

                }
                return tip;
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
