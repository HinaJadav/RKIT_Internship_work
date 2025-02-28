using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using FinalDemo.Models.POCOs;

namespace FinalDemo.BL.Interfaces
{
    /// <summary>
    /// Defines methods for user authentication, management, and retrieval.
    /// </summary>
    public interface IUserService
    {

        YMU01 ToPocoUser(DTOYMU01 dtoUser);

        /// <summary>
        /// Prepares user data before saving, either for new creation or update.
        /// </summary>
        /// <param name="dtoUser">DTO containing user data.</param>
        /// <param name="operationType">The operation type (Add, Edit).</param>
        /// <param name="userId">Optional user ID for update operation.</param>
        void PreSaveUser(DTOYMU01 dtoUser, OperationType operationType, int? userId = null);

        /// <summary>
        /// Prepares user data for login, including password hashing.
        /// </summary>
        /// <param name="dtoUser">DTO containing login data.</param>
        void PreLoginUser(DTOLogin dtoUser);

        /// <summary>
        /// Prepares for user deletion, setting the appropriate operation type.
        /// </summary>
        /// <param name="userId">User ID to delete.</param>
        /// <param name="operationType">The operation type (Delete).</param>
        void PreDeleteUser(int userId, OperationType operationType);

        /// <summary>
        /// Validates the user data for operations like update or delete.
        /// </summary>
        /// <returns>Response object indicating validation status.</returns>
        Response Validation();

        /// <summary>
        /// Saves the user data to the database, either for new users or updates.
        /// </summary>
        /// <returns>Response indicating the outcome of the save operation.</returns>
        Response Save();

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <returns>Response indicating the success or failure of the deletion.</returns>
        Response Delete();

        /// <summary>
        /// Retrieves a user's details by ID.
        /// </summary>
        /// <param name="id">User ID to fetch the details for.</param>
        /// <returns>DTO containing the user details.</returns>
        DTOResponse GetById(int id);

        /// <summary>
        /// Authenticates a user based on provided login credentials and returns a JWT token if successful.
        /// </summary>
        /// <param name="loginDto">DTO containing the login credentials (username and password).</param>
        /// <returns>Response containing the result of the login attempt, including the JWT token if successful.</returns>
        Response Login(DTOLogin loginDto);

        /// <summary>
        /// Generates a JWT token for a user, based on their user ID and role.
        /// </summary>
        /// <param name="userId">User ID to include in the token.</param>
        /// <param name="role">Role of the user (e.g., Admin, User) to include in the token.</param>
        /// <returns>A JWT token as a string.</returns>
        string GenerateJwtToken(int userId, string role);
    }
}
