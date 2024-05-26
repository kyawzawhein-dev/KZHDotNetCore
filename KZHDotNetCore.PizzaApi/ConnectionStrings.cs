using System.Data.SqlClient;

namespace KZHDotNetCore.PizzaApi;

public static class ConnectionStrings
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "root",
        TrustServerCertificate = true
    };
}
