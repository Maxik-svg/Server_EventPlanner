using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Server_PHP_For_Business.Models
{
  public class Hall
  {
    [Key] public long Id { get; set; }
    [Required] public Business Business { get; set; }

    [NotMapped, JsonIgnore]
    public List<Seat> Seats
    {
      get => JsonConvert.DeserializeObject<List<Seat>>(_Seats);
      set => _Seats = JsonConvert.SerializeObject(value);
    }

    public static void CopyValues(Hall from, Hall to)
    {
      to.Seats = from.Seats;
    }

    internal string _Seats { get; set; }
  }
}
