using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class Command
  {
    [Key] public int Id { get; set; }
    [Required] public string HowTo { get; set; }
    [Required] public string Line { get; set; }
    [Required] public string Platform { get; set; }
  }
}
