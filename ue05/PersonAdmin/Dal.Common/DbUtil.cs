namespace Dal.Common;

using System.Data.Common;

public static class DbUtil
{
    public static void RegisterAdoProviders()
    {
        // Use new implementation of MS SQL Provider
        DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
        //DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySql.Data.MySqlClient.MySqlClientFactory.Instance);
    }
}
