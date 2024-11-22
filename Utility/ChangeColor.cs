public class ChangeColor
{
    public static void TextColorRed(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n" + message);
        Console.ResetColor();
    }

    public static void TextColorGreen(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n" + message);
        Console.ResetColor();
    }
}
