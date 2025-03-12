using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using FinalDemo.Models.POCOs;

namespace FinalDemo.BL.Interfaces
{
    /// <summary>
    /// Provides methods for user authentication, management, and retrieval.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Converts a user DTO to a POCO model for database operations.
        /// </summary>
        /// <param name="dtoUser">User DTO containing input data.</param>
        /// <returns>POCO user model.</returns>
        YMU01 ToPocoUser(DTOYMU01 dtoUser);

        /// <summary>
        /// Prepares user data before saving, handling both creation and update scenarios.
        /// </summary>
        /// <param name="dtoUser">User details provided by the client.</param>
        /// <param name="operationType">Specifies if the operation is an addition or an update.</param>
        void PreSaveUser(DTOYMU01 dtoUser, OperationType operationType);

        /// <summary>
        /// Prepares login data by processing the password for authentication.
        /// </summary>
        /// <param name="dtoUser">User login details.</param>
        void PreLoginUser(DTOLogin dtoUser);

        /// <summary>
        /// Handles necessary preparations before deleting a user.
        /// </summary>
        /// <param name="userId">ID of the user to be deleted.</param>
        /// <param name="operationType">Specifies the delete operation.</param>
        void PreDeleteUser(int userId, OperationType operationType);

        /// <summary>
        /// Validates user data before performing operations such as update or delete.
        /// </summary>
        /// <returns>Validation response indicating success or errors.</returns>
        Response Validation();

        /// <summary>
        /// Saves user data to the database for either new creation or updating existing records.
        /// </summary>
        /// <returns>Result of the save operation.</returns>
        Response Save();

        /// <summary>
        /// Deletes a user from the system, ensuring necessary validations.
        /// </summary>
        /// <returns>Result of the delete operation.</returns>
        Response Delete();

        /// <summary>
        /// Retrieves user details based on their ID.
        /// </summary>
        /// <param name="id">ID of the user to fetch details for.</param>
        /// <returns>User details wrapped in a response DTO.</returns>
        DTOResponse GetById(int id);

        /// <summary>
        /// Authenticates a user and returns a JWT token if login is successful.
        /// </summary>
        /// <param name="loginDto">User credentials for authentication.</param>
        /// <returns>Login result containing a JWT token if successful.</returns>
        Response Login(DTOLogin loginDto);

        /// <summary>
        /// Generates a JWT token for a user based on their ID and role.
        /// </summary>
        /// <param name="userId">User ID included in the token.</param>
        /// <param name="role">User role (e.g., Admin, User) included in the token.</param>
        /// <returns>Generated JWT token as a string.</returns>
        string GenerateJwtToken(int userId, string role);
    }
}
