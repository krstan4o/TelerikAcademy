using System;
using System.Linq;

namespace BlogSystem.Services.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string AuthCode { get; set; }
    }
}