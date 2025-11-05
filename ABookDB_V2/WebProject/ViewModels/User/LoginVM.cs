using System.ComponentModel.DataAnnotations;

namespace WebProject.ViewModels.User
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
