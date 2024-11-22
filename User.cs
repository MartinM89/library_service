public class User
{
    public Guid userId;
    public string Username { get; private set; }
    public string Password { get; private set; }

    public User(Guid userId, string username, string password)
    {
        this.userId = userId;
        this.Username = username;
        this.Password = password;
    }
}
