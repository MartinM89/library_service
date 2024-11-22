public class Account
{
    private static bool run = true;

    private static List<User> USERS = new List<User>();

    public static void Register() { }

    public static void Login()
    {
        string password = "";

        Console.Clear();
        Console.Write("Username: ");
        string username = Console.ReadLine()!.ToLower();

        Console.Write("Password: ");
        password = HidePassword.Run(password);

        User? user = USERS.Find(u => u.Username.Equals(username) && u.Password.Equals(password));

        if (user != null)
        {
            ChangeColor.TextColorGreen($"Successfully logged in as {username}");
            Console.ReadKey();
        }
        else
        {
            ChangeColor.TextColorRed("Account not found!");
            Console.ReadKey();
        }
    }

    public static void List()
    {
        Console.Clear();
        foreach (User user in USERS)
        {
            Console.WriteLine($"{user.userId} | {user.Username} | {user.Password}");
        }
        Console.ReadKey();
    }
}
