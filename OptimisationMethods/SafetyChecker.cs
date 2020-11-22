using System.Collections.Generic;
using System.Linq;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.OptimisationMethods
{
  public static class SafetyChecker
  {
    private const int DangerRange = 2;

    public static bool CheckDangerAndPerformActionsIfNeeded(this IList<IList<Seat>> seats)
    {
      var dangerSeats = seats.SelectMany(
          row => row.Select(seat => seat.State == SeatState.Danger)
          ).ToList();


      return dangerSeats.Count > 0;
    }

    private static void PerformActions(IList<IList<Seat>> seats, IList<Seat> dangerSeats)
    {
      var seatsInDanger = FindSeatsInDanger(seats[0].Count, seats.Count, dangerSeats.Select(seat => seat.Id));

    }

    private static List<long> FindSeatsInDanger(int columnCount, int rowCount, IEnumerable<long> dangerSeatIds)
    {
      var inDangerSeats = new List<long>();

      foreach (var seatId in dangerSeatIds)
      {
        var seatColumn = DefineColumnById(seatId, columnCount);

        for (int i = 0; i < DangerRange; i++)
        {
          var checkedId = seatId + i;
          var checkedColumn = DefineColumnById(checkedId, columnCount);
          if(LiesInRange(seatColumn - DangerRange, seatColumn + DangerRange, checkedColumn))
            inDangerSeats.Add(seatId);

          checkedId = seatId - i;
          if(LiesInRange(seatColumn - DangerRange, seatColumn + DangerRange, checkedColumn))
            inDangerSeats.Add(seatId);

          checkedId = seatId + columnCount * i;
          if(DefineRowById(checkedId, columnCount) < rowCount)
            inDangerSeats.Add(seatId);

          checkedId = seatId + columnCount * i;
          if(DefineRowById(checkedId, columnCount) >= 0)
            inDangerSeats.Add(seatId);
        }
      }

      return inDangerSeats;
    }

    private static bool LiesInRange(long minVal, long maxVal, long checkedNum)
    {
      return minVal > checkedNum && checkedNum > maxVal;
    }

    private static long DefineColumnById(long seatId, int columnCount)
    {
      return seatId - seatId / columnCount * columnCount;
    }

    private static long DefineRowById(long seatId, int columnCount)
    {
      return (seatId - seatId % columnCount) / columnCount;
    }
  }
}
