public interface ILibraryCardManager
{
    void CreateCard(string firstName, string lastName, string socialSecurityNumber);
    void DeleteCard(int userId, string pin);
    void ShowCard();
    void UpdatePassword(int cardNumber, string newPin, string confirmPin);
}
