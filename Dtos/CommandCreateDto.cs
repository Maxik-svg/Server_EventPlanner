using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Dtos
{
  public class CommandCreateDto
  {
    [Required] public string HowTo { get; set; }
    [Required] public string Line { get; set; }
    [Required] public string Platform { get; set; }
  }
}