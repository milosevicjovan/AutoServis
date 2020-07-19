using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServis.Interfaces;
using System.Windows;
using AutoServis.Data;

namespace AutoServis.Models
{
    class Model : IData<Model>
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();

        #region privatne promenljive
        private int _id;
        private string _nazivModela;
        private Marka _marka;
        #endregion

        #region getteri i setteri
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string NazivModela
        {
            get { return _nazivModela; }
            set { _nazivModela = value; }
        }
        public Marka Marka
        {
            get { return _marka; }
            set { _marka = value; }
        }
        #endregion

        #region konstruktor
        public Model()
        {

        }
        #endregion

        #region metode
        public void Sacuvaj()
        {
            string sqlUpit = @"insert into tblModel(NazivModela,MarkaID) values (@model,@markaId);";

            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@model", NazivModela);
                cmd.Parameters.AddWithValue("@markaId", Marka.Id);
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
           AutoServisData.Obrisi("tblModel", "ModelID", Id);
        }
        public void Azuriraj(Model noviModel)
        {
            try
            {
                string sqlUpit = @"update tblModel set NazivModela=@model, MarkaID=@markaID where ModelID=@id";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@model", noviModel.NazivModela);
                cmd.Parameters.AddWithValue("@markaID", noviModel.Marka.Id);
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
                string sqlUpit = @"select count(*) from tblModel where NazivModela=@model AND MarkaID=@markaID;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@model", NazivModela);
                cmd.Parameters.AddWithValue("@markaID", Marka.Id);
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
        public static DataTable ListaModela(string filter)
        {
            string sqlUpit = @"SELECT tblModel.ModelID as 'ID', tblMarka.NazivMarke as 'Marka', tblModel.NazivModela as 'Model'
                               FROM tblMarka join tblModel on tblMarka.MarkaID = tblModel.MarkaID
                               WHERE tblMarka.MarkaID like '%" + filter + @"%' or tblMarka.NazivMarke like '%" + filter + @"'
                               OR tblModel.NazivModela like '%" + filter + @"%' or (tblMarka.NazivMarke + ' ' + tblModel.NazivModela) like '%" + filter + @"%' 
                               ORDER BY tblModel.ModelID,tblMarka.NazivMarke,tblModel.NazivModela;";

            return AutoServisData.UcitajPodatke(sqlUpit);
        }
        public static Model UcitajModel(int id)
        {
            try
            {
                string sqlUpit = @"select * from tblModel where ModelID=@id;";
                Model model = new Model();
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader citac = cmd.ExecuteReader();
                while (citac.Read())
                {
                    model.Id = Convert.ToInt32(citac["ModelID"]);
                    model.NazivModela = citac["NazivModela"].ToString();
                    model.Marka = Marka.UcitajMarku(Convert.ToInt32(citac["MarkaID"]));
                }
                return model;
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
