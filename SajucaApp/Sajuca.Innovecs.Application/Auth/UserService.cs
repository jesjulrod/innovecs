namespace Sajuca.Innovecs.Application.Auth
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
           bool result = false;

            if (username.Equals("admin1") && password.Equals("Pa$$WoRd1") ||
               username.Equals("admin2") && password.Equals("Pa$$WoRd2") ||
               username.Equals("admin3") && password.Equals("Pa$$WoRd3"))
            {
                result = true;
            }

            return result;
        }
    }
}