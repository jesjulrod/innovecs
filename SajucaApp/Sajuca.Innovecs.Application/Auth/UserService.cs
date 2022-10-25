namespace Sajuca.Innovecs.Application.Auth
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password, string suscription)
        {
            bool result = false;
            switch (suscription)
            {
                case "firstApi":
                    result = username.Equals("admin1") && password.Equals("Pa$$WoRd1");
                    break;
                case "secondApi":
                    result = username.Equals("admin2") && password.Equals("Pa$$WoRd2");
                    break;
                case "thirdApi":
                    result = username.Equals("admin3") && password.Equals("Pa$$WoRd3");
                    break;
            }

            return result;
        }
    }
}
