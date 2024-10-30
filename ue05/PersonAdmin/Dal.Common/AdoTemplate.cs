using System.Data.Common;

namespace Dal.Common {
    public class AdoTemplate {
        private readonly IConnectionFactory connectionFactory;

        public AdoTemplate(IConnectionFactory connectionFactory) {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<T>> Query<T>(string commandText, RowMapper<T> rowMapper) {
            await using DbConnection connection = await connectionFactory.CreateConnectionAsync();
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
