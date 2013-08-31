using System;
using System.Linq;
using System.Text;

namespace BlogSystem.Services.Persisters
{
    public static class UserPersister
    {

        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 30;
        private const string ValidUsernameCharacters =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_.";
        private const string ValidNicknameCharacters =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_. -";

        private const string SessionKeyChars =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890";
        private static readonly Random rand = new Random();

        private const int SessionKeyLength = 50;

        private const int Sha1Length = 40;

         

        public static string GenerateSessionKey(int userId)
        {
            StringBuilder skeyBuilder = new StringBuilder(SessionKeyLength);
            skeyBuilder.Append(userId);
            while (skeyBuilder.Length < SessionKeyLength)
            {
                var index = rand.Next(SessionKeyChars.Length);
                skeyBuilder.Append(SessionKeyChars[index]);
            }
            return skeyBuilder.ToString();
        }

        #region UserValidations

        public static void ValidateAuthCode(string authCode)
        {
            if (authCode == null || authCode.Length != Sha1Length)
            {
                throw new ArgumentOutOfRangeException("Password should be encrypted");
            }
        }

        public static void ValidateSessionKey(string sessionKey)
        {
            if (sessionKey == null)
            {
                throw new ArgumentNullException("SessionKey key cannot be null");
            }
            else if (sessionKey.Length != SessionKeyLength)
            {
                throw new ArgumentOutOfRangeException("Invalid session key.");
            }
            else if (sessionKey.Any(ch => !SessionKeyChars.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(
                    "Session key is invalid");
            }
        }

        public static void ValidateNickname(string nickName)
        {
            if (nickName == null)
            {
                throw new ArgumentNullException("Display name cannot be null");
            }
            else if (nickName.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Display name must be at least {0} characters long",
                        MinUsernameLength));
            }
            else if (nickName.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Display name must be less than {0} characters long",
                        MaxUsernameLength));
            }
            else if (nickName.Any(ch => !ValidNicknameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(
                    "Display name must contain only Latin letters, digits ., _, spaces, -");
            }
        }

        public static void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null");
            }
            else if (username.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be at least {0} characters long",
                        MinUsernameLength));
            }
            else if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be less than {0} characters long",
                        MaxUsernameLength));
            }
            else if (username.Any(ch => !ValidUsernameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(
                    "Username must contain only Latin letters, digits .,_");
            }
        }

        #endregion
    }
}