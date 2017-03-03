using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace MentorJWcfService.Utilities
{
    partial class MyDataClassesDataContext
    {
        private SqlConnection dBConnection;

        public MyDataClassesDataContext(SqlConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }

        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            }
        }
        public static SqlConnection DBConnection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
        public static MyDataClassesDataContext Context
        {
            get
            {
                return new MyDataClassesDataContext(DBConnection);
            }
        }

    }
    
}