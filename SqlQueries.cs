using Npgsql;

public class SqlQueries
{
    public static void InsertCard(
        string firstName,
        string lastName,
        string pin,
        bool isAdult,
        string socialSecurityNumber
    )
    {
        var connection = DatabaseConnection.OpenConnection();

        using var insertIntoCardsSql = new NpgsqlCommand(
            @"INSERT INTO cards (first_name, last_name, pin, is_adult, social_security_number)
        VALUES (@first_name, @last_name, @pin, @is_adult, @social_security_number)",
            connection
        );

        insertIntoCardsSql.Parameters.AddWithValue("@first_name", firstName);
        insertIntoCardsSql.Parameters.AddWithValue("@last_name", lastName);
        insertIntoCardsSql.Parameters.AddWithValue("@pin", pin);
        insertIntoCardsSql.Parameters.AddWithValue("@is_adult", isAdult);
        insertIntoCardsSql.Parameters.AddWithValue("@social_security_number", socialSecurityNumber);

        insertIntoCardsSql.ExecuteNonQuery();
    }

    public static void UpdatePassword(int cardNumber, string pin)
    {
        var connection = DatabaseConnection.OpenConnection();

        using var updateCardPasswordSql = new NpgsqlCommand(
            @"UPDATE cards SET pin = @pin WHERE card_number = @cardNumber;",
            connection
        );

        updateCardPasswordSql.Parameters.AddWithValue("@cardNumber", cardNumber);
        updateCardPasswordSql.Parameters.AddWithValue("@pin", pin);

        updateCardPasswordSql.ExecuteNonQuery();
    }

    public static void DeleteCard(int cardNumber)
    {
        var connection = DatabaseConnection.OpenConnection();

        using var deleteCardSql = new NpgsqlCommand(
            @"DELETE FROM cards WHERE card_number = @cardNumber;",
            connection
        );

        deleteCardSql.Parameters.AddWithValue("@cardNumber", cardNumber);

        deleteCardSql.ExecuteNonQuery();
    }
}
