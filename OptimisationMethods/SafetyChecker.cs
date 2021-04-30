using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.OptimisationMethods
{
  public static class SafetyChecker
  {
    private const int DangerRange = 2;

    public static bool CheckDangerAndPerformActionsIfNeeded(this List<List<Seat>> seats, out List<List<Seat>> newSeats)
    {
      var dangerSeatsIds = seats.SelectMany(
        row => row.Where(seat => seat.State == SeatState.Danger)
          .Select(seat => seat.Id)
      ).ToList();

      if (dangerSeatsIds.Count > 0)
        PerformActions(seats, dangerSeatsIds);

      newSeats = seats;

      return dangerSeatsIds.Count > 0;
    }

    private static void PerformActions(List<List<Seat>> seats, IEnumerable<long> dangerSeatsIds)
    {
      var columnCount = seats[0].Count;
      var rowCount = seats.Count;
      var seatsInDanger = FindSeatsInDanger(columnCount, rowCount, dangerSeatsIds);
      var safeFreeSeats = seats
        .SelectMany(row => row.Select(s => s))
        .Where(s => s.State == SeatState.Free && !seatsInDanger.Contains(s.Id))
        .ToList();

      seats.SelectMany(r => r.Where(s => seatsInDanger.Contains(s.Id))).ForAll(s => s.State = SeatState.InDanger);

      foreach (var seatId in seatsInDanger)
      {
        var seat = seats[(int) DefineRowById(seatId, columnCount)][(int) DefineColumnById(seatId, columnCount)];
        if(seat.State == SeatState.Free)
          continue;

        var optimalSeat = FindOptimalSeat(seat, safeFreeSeats);
        safeFreeSeats.Remove(optimalSeat);

        optimalSeat.State = SeatState.WaitingForUser;
        optimalSeat.UserId = seat.UserId;
      }
    }

    private static Seat FindOptimalSeat(Seat forSeat, IList<Seat> freeSeats)
    {
      if (freeSeats == null || freeSeats.Count == 0)
        return null;
      if (freeSeats.Count == 1)
        return freeSeats[0];

      var optimalSeat = freeSeats
        .OrderBy(seat => Math.Abs(seat.Id - forSeat.Id))
        .ThenBy(seat => Math.Abs(seat.CostType - forSeat.CostType))
        .First();

      return optimalSeat;
    }

    private static List<long> FindSeatsInDanger(int columnCount, int rowCount, IEnumerable<long> dangerSeatIds)
    {
      var inDangerSeats = new List<long>();

      foreach (var seatId in dangerSeatIds)
      {
        var seatColumn = DefineColumnById(seatId, columnCount);

        for (var i = 1; i <= DangerRange; i++)
        {
          var right = seatId + i;
          var checkedColumn = DefineColumnById(right, columnCount); // right
          if(checkedColumn.FeatsColumnRestrictions(seatColumn, columnCount))
            inDangerSeats.Add(right);

          var left = seatId - i;
          checkedColumn = DefineColumnById(left, columnCount); // left
          if(checkedColumn.FeatsColumnRestrictions(seatId, columnCount))
            inDangerSeats.Add(left);

          var down = seatId + columnCount * i;
          if(DefineRowById(down, columnCount) < rowCount) // down
            inDangerSeats.Add(down);

          var up = seatId - columnCount * i;
          if(DefineRowById(up, columnCount) >= 0) // up
            inDangerSeats.Add(up);

          if(i > DangerRange/2)
            continue;

          var downRight = down + i;
          checkedColumn = DefineColumnById(downRight, columnCount); // down right
          if(checkedColumn.FeatsColumnRestrictions(seatColumn, columnCount)
             && DefineRowById(downRight, columnCount) < rowCount)
            inDangerSeats.Add(downRight);

          var downLeft = down - i;
          checkedColumn = DefineColumnById(downLeft, columnCount); // down left
          if(checkedColumn.FeatsColumnRestrictions(seatColumn, columnCount)
             && DefineRowById(downLeft, columnCount) < rowCount)
            inDangerSeats.Add(downLeft);

          var upRight = up + i;
          checkedColumn = DefineColumnById(upRight, columnCount); // up right
          if(checkedColumn.FeatsColumnRestrictions(seatColumn, columnCount)
             && DefineRowById(upRight, columnCount) >= 0)
            inDangerSeats.Add(upRight);

          var upLeft = up - 1;
          checkedColumn = DefineColumnById(upLeft, columnCount); // up left
          if(checkedColumn.FeatsColumnRestrictions(seatColumn, columnCount)
             && DefineRowById(upLeft, columnCount) >= 0)
            inDangerSeats.Add(upLeft);

        }
      }

      return inDangerSeats;
    }

    private static bool FeatsColumnRestrictions(this long checkedColumn, long seatColumn,  int columnCount)
    {
      var minColumn = seatColumn - DangerRange > 0 ? seatColumn - DangerRange : 0;
      var maxColumn = seatColumn + DangerRange < columnCount ? seatColumn + DangerRange : columnCount - 1;

      return checkedColumn.BetweenIncludedValues(minColumn, maxColumn);
    }

    private static bool Between(this long checkedNum, long minVal, long maxVal)
    {
      return minVal < checkedNum && checkedNum < maxVal;
    }

    private static bool BetweenIncludedValues(this long checkedNum, long minVal, long maxVal)
    {
      return minVal <= checkedNum && checkedNum <= maxVal;
    }

    private static long DefineColumnById(long seatId, int columnCount)
    {
      if (seatId < 0)
        return -1;
      return seatId - seatId / columnCount * columnCount; //integer division
    }

    private static long DefineRowById(long seatId, int columnCount)
    {
      if (seatId < 0)
        return -1;
      return (seatId - seatId % columnCount) / columnCount;
    }

    private static long ToSeatId(long x, long y, int columnCount)
    {
      if(x < columnCount)
        return y * columnCount + x;

      throw new ArgumentException($"Value x: {x} can't be higher or equal to columnCount: {columnCount}");
    }
  }
}
