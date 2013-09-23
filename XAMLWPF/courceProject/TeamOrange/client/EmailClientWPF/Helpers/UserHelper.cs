using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClientWPF.Helpers
{
    public static class UserHelper
    {
        private const int TokenLength = 50;
        private const string TokenChars = "qwertyuiopasdfghjklmnbvcxzQWERTYUIOPLKJHGFDSAZXCVBNM";

        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 20;
        private const string ValidUsernameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_.@";

        private const int AuthenticationCodeLength = 40;
        private const int MinPasswordLength = 4;
        private const int MaxPasswordLength = 20;

        public static bool ValidateUsername(string username)
        {
            if (username.Length < MinUsernameLength || MaxUsernameLength < username.Length)
            {
                throw new FormatException("Username must be betwean 6 and 20 chars long.");
            }
            if (username.Any(ch => !ValidUsernameChars.Contains(ch)))
            {
                throw new FormatException("Username must contain only lethers and the characters \"_\", \".\" and \"@\".");
            }
            return true;
        }

        public static bool ValidateAuthCode(string authCode) 
        {
            if (authCode.Length != AuthenticationCodeLength)
            {
                throw new FormatException("Invalid auth code.");
            }
            return true;
        }

        internal static void ValidatePassword(string password)
        {
            if (password.Length < MinPasswordLength || MaxPasswordLength < password.Length)
            {
                throw new FormatException("Password must be betwean 4 and 20 characters long.");
            }
        }
    }
}
