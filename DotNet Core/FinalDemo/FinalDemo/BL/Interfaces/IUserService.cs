using FinalDemo.Models.DTOs;

namespace FinalDemo.BL.Interfaces
{
    /// <summary>
    /// Defines methods for user authentication and retrieval.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user based on login credentials.
        /// </summary>
        /// <param name="loginDto">DTO containing username and password.</param>
        /// <returns>Returns user details if authentication is successful.</returns>
        DTOResponse Login(DTOLogin loginDto);

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="signUpDto">DTO containing user registration details.</param>
        /// <returns>Returns the newly created user details.</returns>
        DTOResponse SignUp(DTOSignUp signUpDto);

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>Returns user details if found.</returns>
        DTOResponse GetUserById(int userId);
    }
}
