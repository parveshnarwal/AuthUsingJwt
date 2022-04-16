namespace AuthUsingJwt
{
    public interface IJwtAuthenticateManager
    {
        string Authenticate(string username, string password);
    }
}
