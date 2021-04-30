using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class AuthenticateModel
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
