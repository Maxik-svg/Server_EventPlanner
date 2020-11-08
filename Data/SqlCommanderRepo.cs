using System;
using System.Collections.Generic;
using System.Linq;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Data
{
  public class SqlCommanderRepo : ICommanderRepo
  {
    private readonly CommanderContext _context;

    public SqlCommanderRepo(CommanderContext context)
    {
      _context = context;
    }

    public bool SaveChanges()
    {
      return _context.SaveChanges() >= 0;
    }

    public IEnumerable<Command> GetAllCommands()
    {
      return _context.Commands.ToList();
    }

    public Command GetCommandById(int id)
    {
      return _context.Commands.FirstOrDefault(x => x.Id == id);
    }

    public void CreateCommand(Command cmd)
    {
      if(cmd == null)
        throw new ArgumentNullException(nameof(cmd));

      _context.Commands.Add(cmd);
    }

    public void UpdateCommand(Command cmd)
    {
      //Nothing
    }

    public void DeleteCommand(Command cmd)
    {
      if(cmd == null)
        throw new ArgumentNullException(nameof(cmd));

      _context.Commands.Remove(cmd);
    }

    public IEnumerable<Bracelet> GetAllBracelets()
    {
      return _context.Bracelets.ToList();
    }

    public Bracelet GetBraceletById(int id)
    {
      return _context.Bracelets.FirstOrDefault(x => x.Id == id);
    }

    public void CreateBracelet(Bracelet bracelet)
    {
      if(bracelet == null)
        throw new ArgumentNullException(nameof(bracelet));

      _context.Bracelets.Add(bracelet);
    }

    public void UpdateBracelet(Bracelet bracelet)
    {
      //Nothing
    }

    public void DeleteBracelet(Bracelet bracelet)
    {
      if(bracelet == null)
        throw new ArgumentNullException(nameof(bracelet));

      _context.Bracelets.Remove(bracelet);
    }

    public IEnumerable<Hall> GetAllHalls()
    {
      return _context.Halls.ToList();
    }

    public Hall GetHallById(int id)
    {
      return _context.Halls.FirstOrDefault(x => x.Id == id);
    }

    public void CreateHall(Hall hall)
    {
      if(hall == null)
        throw new ArgumentNullException(nameof(hall));

      _context.Halls.Add(hall);
    }

    public void UpdateHall(Hall hall)
    {
      //Nothing
    }

    public void DeleteHall(Hall hall)
    {
      if(hall == null)
        throw new ArgumentNullException(nameof(hall));

      _context.Halls.Remove(hall);
    }

    public IEnumerable<User> GetAllUsers()
    {
      return _context.Users.ToList();
    }

    public User GetUserById(int id)
    {
      return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public void CreateUser(User user)
    {
      if(user == null)
        throw new ArgumentNullException(nameof(user));

      _context.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
      //Nothing
    }

    public void DeleteUser(User user)
    {
      if(user == null)
        throw new ArgumentNullException(nameof(user));

      _context.Users.Remove(user);
    }

    public IEnumerable<Business> GetAllBusinesses()
    {
      return _context.Businesses.ToList();
    }

    public Business GetBusinessById(int id)
    {
      return _context.Businesses.FirstOrDefault(x => x.Id == id);
    }

    public void CreateBusiness(Business business)
    {
      if(business == null)
        throw new ArgumentNullException(nameof(business));

      _context.Businesses.Add(business);
    }

    public void UpdateBusiness(Business cmd)
    {
      //Nothing
    }

    public void DeleteBusiness(Business business)
    {
      if(business == null)
        throw new ArgumentNullException(nameof(business));

      _context.Businesses.Remove(business);
    }
  }
}
