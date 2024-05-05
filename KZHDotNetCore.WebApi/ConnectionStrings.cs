using Microsoft.Data.SqlClient;

namespace KZHDotNetCore.WebApi;

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
