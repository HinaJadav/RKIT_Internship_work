using ControllerInitializationDemo.Models;

namespace ControllerInitializationDemo.BL
{
    public interface BLIUser
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(int id, User user);
        bool DeleteUser(int id);
    }
}
