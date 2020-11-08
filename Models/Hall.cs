using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class Hall
  {
    [Key] public long Id { get; set; }
    [Required] public Business Business { get; set; }
    [Required] public List<Seat> Seats { get; set; }
  }
}
