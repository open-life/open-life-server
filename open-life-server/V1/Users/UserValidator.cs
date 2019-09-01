using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace open_life_server.V1.Users
{
    public interface IUserValidator
    {
        bool Valid(User user);
        string GetInvalidMessage(User user);
    }

    public class UserValidator : IUserValidator
    {
        public bool Valid(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrWhiteSpace(user.Name))
                return false;

            try
            {
                var unused = new System.Net.Mail.MailAddress(user.Email);
            }
            catch
            {
                return false;
            }

            if (!string.IsNullOrEmpty(user.ImageUrl) && !string.IsNullOrWhiteSpace(user.ImageUrl))
            {
                if (!Uri.TryCreate(user.ImageUrl, UriKind.Absolute, out var uriResult) || !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    return false;
            }

            return true;
        }

        public string GetInvalidMessage(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrWhiteSpace(user.Name))
                return "Name is not valid.";

            try
            {
                var unused = new System.Net.Mail.MailAddress(user.Email);
            }
            catch
            {
                return "Email address is not valid.";
            }

            return "Image url is not valid.";
        }
    }
}
