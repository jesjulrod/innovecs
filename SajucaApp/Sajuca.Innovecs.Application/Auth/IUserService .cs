namespace Sajuca.Innovecs.Application.Auth
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
