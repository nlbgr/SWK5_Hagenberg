using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Dal.Common {
    public class AdoTemplate {
        private readonly IConnectionFactory connectionFactory;

        public AdoTemplate(IConnectionFactory connectionFactory) {
            this.connectionFactory = connectionFactory;
        }

        private void AddParameters(DbCommand command, QueryParameter[] parameters) {
            foreach (var p in parameters){
                DbParameter dbParam = command.CreateParameter();
                dbParam.ParameterName = p.Name;
                dbParam.Value = p.Value;
                command.Parameters.Add(dbParam);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string commandText, RowMapper<T> rowMapper, params QueryParameter[] parameters) {
            await using DbConnection connection = await connectionFactory.CreateConnectionAsync();
            await using DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            AddParameters(command, parameters);

            await using DbDataReader reader = await command.ExecuteReaderAsync();
            var items = new List<T>();

            while (await reader.ReadAsync()) {
                items.Add(rowMapper(reader));
            }

            return items;
        } // Connection.DisposeAsync -> Connection.CloseAsync, ... (alle using statements werden freigegeben

        public async Task<T?> QuerySingleAsnync<T>(string commandText, RowMapper<T> rowMapper, params QueryParameter[] parameters) {
            return (await QueryAsync<T>(commandText, rowMapper, parameters)).SingleOrDefault();
        }
    }
}
