using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoServis.Data
{

    class AutoServisData
        /* Ova klasa sadrzi neke osnovne metode za baratanje Sql operacijama, da bismo izbegli ponavljanje koda
            gde god je to moguce */
    {
        private static SqlConnection konekcija = Konekcija.KreirajKonekciju();
        public static DataTable UcitajPodatke(string sqlUpit)
            //pomocna metoda za ucitavanje podataka koje cemo prikazivati u gridu ili combo boxu
        {
            try
            {
                konekcija.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlUpit, konekcija);
                da.Fill(dt);
                return dt;
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
        public static void Obrisi(string tabela, string idKolona, int idVrednost)
            //pomocna metoda za brisanje podataka iz tabela
        {
            try
            {
                string sqlUpit = $"delete from { tabela } where { idKolona } = @idVrednost;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@idVrednost", idVrednost);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                //ne bi trebalo da prikazuje ako pozivamo brisanje iz svih referenciranih tabela - eventualno za zaposlene, marku, tip itd.
                MessageBox.Show($"Postoje povezani podaci u drugim tabelama!", "Greska",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom brisanja podataka: { e.Message }", "Greška",
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
        public static int BrojRedovaUBazi(string imeTabele, string polje, string uslov)
            //prosledjujemo upit sa ili bez uslova, vraca nam broj redova u bazi
        {
            try
            {
                string sqlUpit = $@"select count(*) from { imeTabele }";
                if (String.IsNullOrEmpty(polje) || String.IsNullOrEmpty(uslov))
                {
                    sqlUpit = $@"select count(*) from { imeTabele };";
                } else
                {
                    sqlUpit = $@"select count(*) from { imeTabele } where { polje } = { uslov }";
                }
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom povlačenja podataka iz baze: { e.Message }", "Greška",
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
        public static string PovuciIzTabele(string imeKolone, string imeTabele, string uslov)
            //metoda ekvivalentna metodi DLookUp() u MsAccess-u i VBA programskom jeziku
        {
            try
            {
                string sqlUpit = $"select { imeKolone } from { imeTabele } where { uslov };";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom povlačenja podataka iz baze: { e.Message }", "Greška",
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
        public static bool IspravnoKorisnickoIme(string unetoKorisnickoIme)
            //ova metoda proverava da li uneto korisnicko ime zaista postoji u bazi
        {
            try
            {
                string sqlUpit = @"select count(*) from tblZaposleni where KorisnickoIme=@korisnickoIme;";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@korisnickoIme", unetoKorisnickoIme);
                int rez = Convert.ToInt32(cmd.ExecuteScalar());
                if (rez>0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom provere korisničkog imena: { e.Message }.", "Greška",
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
        public static bool IspravnaLozinka(string unetoKorisnickoIme, string unetaLozinka)
        //ova metoda proverava da li je uneta ispravna lozinka za uneto korisnicko ime
        
        {
            try
            {
                string sqlUpit = @"select Lozinka from tblZaposleni where KorisnickoIme=@korisnickoIme";
                konekcija.Open();
                SqlCommand cmd = new SqlCommand(sqlUpit, konekcija);
                cmd.Parameters.AddWithValue("@korisnickoIme", unetoKorisnickoIme);
                string dobijenaLozinka = cmd.ExecuteScalar().ToString();
                if (unetaLozinka.Equals(dobijenaLozinka))
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Došlo je do greške prilikom provere lozinke: { e.Message }.", "Greška",
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
    }
}
