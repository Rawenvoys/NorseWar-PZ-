using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Gra.Helper
{
    public class Methods
    {
        public static SqlConnection GetSqlConnection(string name)
        {
            var sC = new SqlConnection(ConfigurationManager.ConnectionStrings[name].ConnectionString);
            return sC;
        }
    }
}