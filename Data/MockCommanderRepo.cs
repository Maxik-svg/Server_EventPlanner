using System.Collections.Generic;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Data
{
  public class MockCommanderRepo : ICommanderRepo
  {
    public bool SaveChanges()
    {
      throw new System.NotImplementedException();
    }

    public void BackupDb()
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
      var commands = new List<Command>
      {
        new Command {Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle & Pan"},
        new Command {Id = 0, HowTo = "Cut bread", Line = "Get a knife", Platform = "knife & chopping board"},
        new Command {Id = 0, HowTo = "Make cup of tea", Line = "Place teabag in cup", Platform = "Kettle & cup"}
      };

      return commands;
    }

    public Command GetCommandById(int id)
    {
      return new Command {Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle & Pan"};
    }

    public void CreateCommand(Command cmd)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Bracelet> GetAllBracelets()
    {
      throw new System.NotImplementedException();
    }

    public Bracelet GetBraceletById(int id)
    {
      throw new System.NotImplementedException();
    }

    public void CreateBracelet(Bracelet bracelet)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateBracelet(Bracelet bracelet)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteBracelet(Bracelet bracelet)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Hall> GetAllHalls()
    {
      throw new System.NotImplementedException();
    }

    public Hall GetHallById(int id)
    {
      throw new System.NotImplementedException();
    }

    public void CreateHall(Hall hall)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateHall(Hall hall)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteHall(Hall hall)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<User> GetAllUsers()
    {
      throw new System.NotImplementedException();
    }

    public User GetUserById(int id)
    {
      throw new System.NotImplementedException();
    }

    public void CreateUser(User user)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateUser(User user)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteUser(User user)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Business> GetAllBusinesses()
    {
      throw new System.NotImplementedException();
    }

    public Business GetBusinessById(int id)
    {
      throw new System.NotImplementedException();
    }

    public void CreateBusiness(Business business)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateBusiness(Business cmd)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteBusiness(Business business)
    {
      throw new System.NotImplementedException();
    }

    public User Authenticate(string username, string password)
    {
      throw new System.NotImplementedException();
    }
  }
}
