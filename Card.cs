public class Card
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string SocialSecurityNumber { get; private set; } = null!;
    public int CardNumber { get; private set; }
    public string CardPin { get; private set; } = null!;
    public bool IsAdult { get; private set; }

    public Card(
        string firstName,
        string lastName,
        string socialSecurityNumber,
        int cardNumber,
        string cardPin,
        bool isAdult
    )
    {
        FirstName = firstName;
        LastName = lastName;
        SocialSecurityNumber = socialSecurityNumber;
        CardNumber = cardNumber;
        CardPin = cardPin;
        IsAdult = isAdult;
    }
}
