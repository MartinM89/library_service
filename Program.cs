namespace LoginSystem;

class Program
{
    private static bool run = true;

    static void Main(string[] args)
    {
        PostgresLibraryCardManager.LibraryCardManager(); // Keep

        while (run)
        {
            Console.Clear();
            Console.WriteLine("[C]reate Library Card");
            Console.WriteLine("[L]ogin");
            Console.WriteLine("[A]ccounts list");
            Console.WriteLine("[U]pdate password");
            Console.WriteLine("[D]elete card");
            Console.WriteLine("[E]xit");

            Console.Write("\nEnter choice: ");
            char userChoice = char.Parse(Console.ReadLine()!.ToUpper());

            switch (userChoice)
            {
                case 'C':
                    LibraryService.PrintCreateCard();
                    break;

                case 'L':
                    Account.Login();
                    break;

                case 'U':
                    LibraryService.PrintUpdatePassword();
                    break;

                case 'D':
                    LibraryService.PrintDeleteCard();
                    break;

                case 'A':
                    ListLibraryCardManager.ListLibrayCard();
                    break;

                case 'E':
                    run = false;
                    break;

                default:
                    Console.WriteLine($"{userChoice} not available");
                    break;
            }
        }
    }
}
