using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Common;
using Microsoft.Data.SqlClient;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;

namespace PersonAdmin.Dal.Ado {
    public class AdoPersonDao : IPersonDao {
        public async Task<IEnumerable<Person>> FindAllAsync() {
            (string connectionString, string providerName) =
                ConfigurationUtil.GetConnectionParameters(
                    "PersonDbConnection",
                    "ProviderName"
                );

            DbUtil.RegisterAdoProviders();
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(providerName);

            await using DbConnection? connection = dbFactory.CreateConnection() ?? throw new InvalidOperationException("DbProviderFactory.CreateConnection() returned null");
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();

            await using DbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM PERSON";

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            var items = new List<Person>();
            while (await reader.ReadAsync()){
                items.Add(new Person(
                    id: (int)reader["Id"],
                    firstName: (string)reader["first_name"],
                    lastName: (string)reader["last_name"],
                    dateOfBirth: (DateTime)reader["date_of_birth"]
                    )
                );
            }

            return items;
        } // Connection.DisposeAsync -> Connection.CloseAsync, ... (alle using statements werden freigegeben
    }
}
