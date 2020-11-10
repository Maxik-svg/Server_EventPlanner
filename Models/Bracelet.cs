using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class Bracelet
  {
    [Key] public long Id { get; set; }
    public float? Temperature { get; set; }
    public long? UserId { get; set; }
    public User User { get; set; }

    public static void CopyValues(Bracelet from, Bracelet to)
    {
      to.Temperature = from.Temperature;
      to.UserId = from.UserId;
    }
  }
}
