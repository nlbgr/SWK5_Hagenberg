using System.Data.Common;

namespace Dal.Common {
    public interface IConnectionFactory {
        string ConnectionString { get; }
        string ProviderName { get; }

        Task<DbConnection> CreateConnectionAsync();
    }
}
