using System.Collections.Generic;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Data
{
  public interface ICommanderRepo
  {
    bool SaveChanges();
    void BackupDb();

    //commands
    IEnumerable<Command> GetAllCommands();
    Command GetCommandById(int id);
    void CreateCommand(Command cmd);
    void UpdateCommand(Command cmd);
    void DeleteCommand(Command cmd);

    //Bracelets
    IEnumerable<Bracelet> GetAllBracelets();
    Bracelet GetBraceletById(int id);
    void CreateBracelet(Bracelet bracelet);
    void UpdateBracelet(Bracelet bracelet);
    void DeleteBracelet(Bracelet bracelet);

    //Halls
    IEnumerable<Hall> GetAllHalls();
    Hall GetHallById(int id);
    void CreateHall(Hall hall);
    void UpdateHall(Hall hall);
    void DeleteHall(Hall hall);

    //Users
    IEnumerable<User> GetAllUsers();
    User GetUserById(int id);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);

    //Businesses
    IEnumerable<Business> GetAllBusinesses();
    Business GetBusinessById(int id);
    void CreateBusiness(Business business);
    void UpdateBusiness(Business cmd);
    void DeleteBusiness(Business business);

    User Authenticate(string username, string password);
  }
}
