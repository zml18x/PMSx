using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Account
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}