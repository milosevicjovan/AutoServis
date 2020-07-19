using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServis
{
    class Konekcija
    {
        public static SqlConnection KreirajKonekciju()
        {
            SqlConnectionStringBuilder ccnsb = new SqlConnectionStringBuilder();
            ccnsb.DataSource = @"Naziv_Servera"; //Uneti naziv servera
            ccnsb.InitialCatalog = "AutoServis";
            ccnsb.IntegratedSecurity = true;

            string con = ccnsb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }      

    }
}
