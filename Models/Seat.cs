using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class Seat
  {
    [Key] public long Id { get; set; }
    [Required] public SeatState State { get; set; }
    [Required] public CostType CostType { get; set; }
    public long? UserId { get; set; }
  }

  public enum SeatState
  {
    Free = 0,
    Occupied = 1,
    Danger = 2, // curr seater is ill
    InDanger = 3, // seat is in danger zone
  }

  public enum CostType
  {
    Cheap = 0,
    Middle = 1,
    Expensive = 2,
  }
}
