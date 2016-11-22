using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BL
    {
       
        private DAL.DbConnection dbConn;

        // BL constructor gets connectionString as parameter and creates an instance of DbConnection class
        public BL(string connectionString)
        {
            this.dbConn = new DAL.DbConnection(connectionString);
        }
       
        // Executes "GetRandomWord" stored procedure and returns a random word
        public string GetRandomWord()
        {
            DataTable dt= this.dbConn.ExecSP("[dbo].[GetRandomWord]");
            DataRow dr = dt.Rows[0];
            string value = dr[0].ToString();
            return value;
        }     
    }
}
