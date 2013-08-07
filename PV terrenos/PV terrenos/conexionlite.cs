using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Configuration;

namespace PV_terrenos
{
    class conexionlite
    {
        private SQLiteConnection sqCon;
        private SQLiteCommand sqCmd;

        public conexionlite() 
        {
            sqCon = new SQLiteConnection(ConfigurationManager.AppSettings["rutaDB"].ToString());
            sqCon.Open();

        }
        public void ejecutar(string sql)
        {
            sqCmd = new SQLiteCommand(sql, sqCon);

            sqCmd.ExecuteNonQuery();
        }
        ~conexionlite()
        {
            sqCon.Close();
        }
    }
}
