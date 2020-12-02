using System.ComponentModel.DataAnnotations;

namespace Server_PHP_For_Business.Models
{
  public class Seat
  {
    public long Id { get; set; }
    public SeatState State { get; set; }
    public CostType CostType { get; set; }
    public long? UserId { get; set; }
  }

  public enum SeatState
  {
    Free = 0,
    Occupied = 1,
    Danger = 2, // curr seater is ill
    InDanger = 3, // seat is in danger zone
    WaitingForUser = 4, //seat was chosen as optimal for user
  }

  public enum CostType
  {
    Cheap = 0,
    Middle = 1,
    Expensive = 2,
  }
}
