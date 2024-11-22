using Npgsql;

public class ListLibraryCardManager : ILibraryCardManager
{
    public static int cardNumber = 1;
    private static readonly List<Card> LIBRARYCARDS = [];

    public void CreateCard(string firstName, string lastName, string socialSecurityNumber)
    {
        DateTime today = DateTime.Today;
        DateTime isEighteen = today.AddYears(-18);
        bool isAdult = false;
        string pin;

        if (!firstName.All(char.IsLetter))
        {
            return;
        }

        if (!lastName.All(char.IsLetter))
        {
            return;
        }

        if (!DateTime.TryParse(socialSecurityNumber.Substring(0, 10), out DateTime birthDate))
        {
            Console.WriteLine($"Social1: {socialSecurityNumber}"); // Debugging
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Social2: {socialSecurityNumber}"); // Debugging
        Console.ReadKey();

        if (!socialSecurityNumber.Substring(12).All(char.IsDigit))
        {
            return;
        }
        Console.WriteLine("Stuck here too"); // Debugging

        if (birthDate < isEighteen)
        {
            isAdult = true;
        }

        pin = socialSecurityNumber.Substring(11);

        Card newLibraryCard = new Card(
            firstName,
            lastName,
            socialSecurityNumber,
            cardNumber++,
            pin,
            isAdult
        );

        LIBRARYCARDS.Add(newLibraryCard);
    }

    public static void ListLibrayCard() // Debugging
    {
        foreach (Card card in LIBRARYCARDS)
        {
            Console.WriteLine($"First Name {card.FirstName}");
            Console.WriteLine($"Last Name {card.LastName}");
            Console.WriteLine($"Social Number {card.SocialSecurityNumber}");
            Console.WriteLine($"Card Number {card.CardNumber}");
            Console.WriteLine($"Pin {card.CardPin}");
            Console.WriteLine($"Adult {card.IsAdult}");
        }
        Console.ReadKey();
    }

    public void UpdatePassword(int cardNumber, string newPin, string confirmPin) { }

    public void DeleteCard(int userId, string newPin) { }

    public void ShowCard() { }

    public static bool CheckUsernameAndPasswordExists(string passwordUsername)
    {
        return LIBRARYCARDS.Any(get =>
            get.FirstName.Substring(0, 3) == passwordUsername.Substring(4, 3)
            && get.LastName.Substring(0, 3) == passwordUsername.Substring(7)
            && get.CardPin == passwordUsername.Substring(0, 4)
        );
    }
}

public class PostgresLibraryCardManager : ILibraryCardManager
{
    public static void LibraryCardManager()
    {
        var connection = DatabaseConnection.OpenConnection();

        string? createLibraryTables =
            @"CREATE TABLE IF NOT EXISTS cards (
                card_number SERIAL PRIMARY KEY NOT NULL,
                first_name VARCHAR(255) NOT NULL,
                last_name VARCHAR(255) NOT NULL,
                pin VARCHAR(4) NOT NULL,
                is_adult BOOLEAN NOT NULL,
                social_security_number VARCHAR(15) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS accounts (
                username VARCHAR(6) NOT NULL,
                password VARCHAR(4) NOT NULL,
                id SERIAL NOT NULL REFERENCES cards (card_number)
            );

            CREATE TABLE IF NOT EXISTS books (
                isbn VARCHAR(13) PRIMARY KEY NOT NULL,
                title VARCHAR(255) NOT NULL,
                author VARCHAR(255) NOT NULL,
                genre  VARCHAR(255) NOT NULL,
                published_date DATE NOT NULL,
                copies INT DEFAULT 1 NOT NULL
            );

            CREATE TABLE IF NOT EXISTS borrowed_books (
                isbn VARCHAR(13) NOT NULL REFERENCES books (isbn),
                card_number INT NOT NULL REFERENCES cards (card_number),
                borrow_date DATE DEFAULT CURRENT_DATE NOT NULL,
                return_date DATE NOT NULL
            );";

        using NpgsqlCommand createTableCardsCmd = new(createLibraryTables, connection);
        createTableCardsCmd.ExecuteNonQuery();

        connection.Close();
    }

    public void CreateCard(string firstName, string lastName, string socialSecurityNumber)
    {
        DateTime isEighteen = DateTime.Today.AddYears(-18);
        bool isAdult = false;
        string pin;

        if (!firstName.All(char.IsLetter))
        {
            Console.WriteLine("Input Invalid. Name must be only characters.");
            Console.ReadKey();
            return;
        }

        if (!lastName.All(char.IsLetter))
        {
            Console.WriteLine("Input Invalid. Name must be only characters.");
            Console.ReadKey();
            return;
        }

        if (!DateTime.TryParse(socialSecurityNumber.Substring(0, 10), out DateTime birthDate))
        {
            Console.WriteLine("Input Invalid. Use the correct format: (YYYY-MM-DD-NNNN)");
            Console.ReadKey();
            return;
        }

        if (!socialSecurityNumber.Substring(12).All(char.IsDigit))
        {
            return;
        }

        if (birthDate < isEighteen)
        {
            isAdult = true;
        }

        pin = socialSecurityNumber.Substring(11);

        SqlQueries.InsertCard(firstName, lastName, pin, isAdult, socialSecurityNumber);
    }

    public void DeleteCard(int userId, string newPin)
    {
        if (!newPin.All(char.IsDigit))
        {
            Console.WriteLine("Invalid Input. Pin can only be numbers");
            Console.ReadKey();
            return;
        }

        if (!newPin.Length.Equals(4))
        {
            Console.WriteLine("Invalid Input. Pin must be 4 long");
            Console.ReadKey();
            return;
        }

        SqlQueries.DeleteCard(userId);
    }

    public void ShowCard()
    {
        throw new NotImplementedException();
    }

    public void UpdatePassword(int cardNumber, string newPin, string confirmPin)
    {
        if (!newPin.All(char.IsDigit))
        {
            Console.WriteLine("Input Invalid. Only 4 numbers");
            Console.ReadKey();
            return;
        }

        if (!newPin.Equals(confirmPin))
        {
            Console.WriteLine("Password does not match!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Password updated"); // Debugging
        Console.ReadKey();
        SqlQueries.UpdatePassword(cardNumber, newPin);
    }
}
