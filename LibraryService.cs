public class LibraryService
{
    public static void PrintCreateCard()
    {
        // ListLibraryCardManager manager = new ListLibraryCardManager();

        PostgresLibraryCardManager manager = new();

        Console.Write("\nEnter your first name: ");
        string firstName = Console.ReadLine()!.ToUpper().Trim();
        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine()!.ToUpper().Trim();
        Console.Write("Enter your social security numbers (YYYYMMDD-NNNN): ");
        string socialSecurityNumber = Console.ReadLine()!;

        manager.CreateCard(firstName, lastName, socialSecurityNumber);
    }

    public static void PrintUpdatePassword()
    {
        PostgresLibraryCardManager manager = new();

        Console.WriteLine("\nEnter user ID");
        int userId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter new password: ");
        string newPin = Console.ReadLine()!;
        Console.Write("Retype password: ");
        string confirmPin = Console.ReadLine()!;

        manager.UpdatePassword(userId, newPin, confirmPin);
    }

    public static void PrintDeleteCard()
    {
        PostgresLibraryCardManager manager = new();

        Console.Write("\nEnter card ID to delete your card: ");
        int userId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter your password: ");
        string pin = Console.ReadLine()!;

        Random random = new();
        int randomNumber = random.Next(1000, 10000);
        Console.Write($"Enter {randomNumber} to confirm: ");
        int userRandomNumber = int.Parse(Console.ReadLine()!);

        if (!randomNumber.Equals(userRandomNumber))
        {
            Console.WriteLine("Numbers do not match");
            Console.ReadKey();
            return;
        }

        manager.DeleteCard(userId, pin);
    }
}
