using ORM.POCO;
using ServiceStack.OrmLite;
using System.Configuration;
using System.Data;

namespace ORM.Data
{
    public class DbConnection
    {
        /// <summary>
        /// stores actual db connection 
        /// </summary>
        public static IDbConnection _db;

        public static IDbConnection OpenConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

            _db = dbFactory.OpenDbConnection();

            CreateTables();

            return _db;
        }

        private static void CreateTables()
        {
            _db.CreateTableIfNotExists<PLA01>();
            _db.CreateTableIfNotExists<GAM01>();
        }
    }
}
