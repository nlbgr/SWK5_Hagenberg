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

            await using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand("select * from person", connection);
            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            var items = new List<Person>();
            while (reader.Read()){
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
