using System.Collections.Generic;
using System.Linq;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Helpers
{
  public static class UserExtensions
  {
    public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
    {
      if (users == null) return null;

      return users.Select(x => x.WithoutPassword());
    }

    public static User WithoutPassword(this User user)
    {
      if (user == null) return null;

      user.Password = null;
      return user;
    }
  }
}
