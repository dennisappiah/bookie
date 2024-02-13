using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotalAppLibrary.Databases
{
    // repository class that talks the database
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string sql,
                                  U parameters,
                                  string connectionStringName,
                                  bool isStoredProcedure = false)
        {

            string connectionString = _config.GetConnectionString(connectionStringName);

            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sql, parameters, commandType: commandType).ToList();

                return rows;
            }

        }


        public void SaveData<T>(string sqlStatement,
                                T dataparameters,
                                string connectionStringName,
                                   bool isStoredProcedure = false)
        {

            string connectionString = _config.GetConnectionString(connectionStringName);

            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }


            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, dataparameters, commandType: commandType);
            }
        }

    }
}
