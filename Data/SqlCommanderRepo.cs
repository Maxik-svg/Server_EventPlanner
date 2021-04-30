using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server_PHP_For_Business.Helpers;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Data
{
  public class SqlCommanderRepo : ICommanderRepo
  {
    private const string _dbName = "PHPFB_AtaRK";
    private const string _backupPath = @"D:\Programming\WebAPI\Server_PHP_For_Business\BackupDB\Backup.bak";
    private readonly CommanderContext _context;
    private readonly AppSettings _appSettings;

    public SqlCommanderRepo(CommanderContext context, IOptions<AppSettings> appOptions)
    {
      _context = context;
      _appSettings = appOptions.Value;
    }

    public bool SaveChanges()
    {
      return _context.SaveChanges() >= 0;
    }

    public void BackupDb()
    {
      string commandText = $@"BACKUP DATABASE [{_dbName}] TO DISK = N'{_backupPath}' WITH NOFORMAT, INIT, NAME = N'{_dbName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

      SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder("Server=DESKTOP-MAXPC\\M_BUZKO;Initial Catalog=PHPFB_AtaRK;User Id=AtaRK_API;Password=password;");
      using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
      {
        connection.Open();
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = commandText;
          command.CommandType = CommandType.Text;
          command.ExecuteNonQuery();
        }
      }
    }

    private static void RestoreDatabase(string databaseName, string backupPath)
    {
      string commandText = $@"USE [master];
    ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    RESTORE DATABASE [{databaseName}] FROM DISK = N'{backupPath}' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 5;
    ALTER DATABASE [{databaseName}] SET MULTI_USER;";

      SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
      {
        DataSource = "localhost",
        InitialCatalog = "master",
        IntegratedSecurity = true
      };
      using (SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString))
      {
        connection.Open();
        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = commandText;
          command.CommandType = CommandType.Text;
          command.ExecuteNonQuery();
        }
      }
    }

    private static void BackupDatabase(string databaseName, string backupPath)
    {

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

    public User Authenticate(string username, string password)
    {
      var user = _context.Users.SingleOrDefault(u => u.Name == username && u.Password == password);

      // return null if user not found
      if (user == null)
        return null;
      user.Role = "Admin";

      // authentication successful so generate jwt token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString()),
          new Claim(ClaimTypes.Role, user.Role)
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      return user.WithoutPassword();
    }
  }
}
