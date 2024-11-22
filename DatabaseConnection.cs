using Npgsql;

public static class DatabaseConnection
{
    private static NpgsqlConnection? connection;
    private static string connectionString =
        "Host=192.168.0.130:5436;Database=library;Username=postgres;Password=password";

    public static NpgsqlConnection OpenConnection()
    {
        if (connection == null)
        {
            connection = new NpgsqlConnection(connectionString);
        }

        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }

        return connection;
    }

    public static void CloseConnection()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
        }
    }
}
