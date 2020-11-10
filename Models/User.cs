using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class User
  {
    [Key] public long Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; }
    [Required, MaxLength(100)] public string Email { get; set; }
    [Required, MaxLength(40)] public string Password { get; set; }
    [MaxLength(255)] public string Info { get; set; }
    public long? BraceletId { get; set; }
    public Bracelet Bracelet { get; set; }

    public static void CopyValues(User from, User to)
    {
      to.Name = from.Name;
      to.Email = from.Email;
      to.Password = from.Password;
      to.Info = from.Info;
      to.BraceletId = from.BraceletId;
    }
  }
}
