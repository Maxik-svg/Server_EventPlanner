using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class Business
  {
    [Key] public long Id { get; set; }
    [Required] public string Name { get; set; }
    [Required, MaxLength(100)] public string Email { get; set; }
    [Required, MaxLength(40)] public string Password { get; set; }
    public ICollection<Hall> Halls { get; set; }

    public static void CopyValues(Business from, Business to)
    {
      to.Name = from.Name;
      to.Email = from.Email;
      to.Password = from.Password;
    }
  }
}
