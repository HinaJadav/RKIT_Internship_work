using ControllerInitializationDemo.Models;
using System.Collections.Generic;

namespace ControllerInitializationDemo.BL
{
    /// <summary>
    /// Defines business logic operations for managing users.
    /// </summary>
    public interface BLIUser
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A collection of users.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user object if found; otherwise, null.</returns>
        User GetUserById(int id);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>The created user object.</returns>
        User CreateUser(User user);

        /// <summary>
        /// Updates an existing user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>The updated user object.</returns>
        User UpdateUser(int id, User user);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>True if the user was deleted; otherwise, false.</returns>
        bool DeleteUser(int id);
    }
}
