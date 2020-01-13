using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;

        public string ConnectionStringName { get; set; } = "Default";
        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameter)
        {
            var connectioString = _configuration.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectioString))
            {
                var data = await connection.QueryAsync<T>(sql, parameter);

                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameter)
        {
            var connectioString = _configuration.GetConnectionString(ConnectionStringName);

            using (var connection = new SqlConnection(connectioString))
            {
                await connection.ExecuteAsync(sql, parameter);
            }
        }


    }
}
