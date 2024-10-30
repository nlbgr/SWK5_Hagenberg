using System.Data.Common;

namespace Dal.Common {
    public class AdoTemplate {
        public async Task<IEnumerable<T>> Query<T>(string commandText, RowMapper<T> rowMapper) {
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
            command.CommandText = commandText;

            await using DbDataReader reader = await command.ExecuteReaderAsync();

            var items = new List<T>();
            while (await reader.ReadAsync()) {
                items.Add(rowMapper(reader));
            }

            return items;
        } // Connection.DisposeAsync -> Connection.CloseAsync, ... (alle using statements werden freigegeben
    }
}
