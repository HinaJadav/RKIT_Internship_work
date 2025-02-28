using FinalDemo.Models.DTOs;
using FinalDemo.Models;
using FinalDemo.Models.Enums;

namespace FinalDemo.BL.Interfaces
{
    public interface IBugService
    {
        /// <summary>
        /// Prepares bug data before saving, either for creating a new bug or updating an existing one.
        /// </summary>
        /// <param name="bugDto">Bug details provided by the user.</param>
        /// <param name="operationType">Specifies whether the operation is a creation or an update.</param>
        void PreSaveBug(DTOYMB01 bugDto, OperationType operationType);

        /// <summary>
        /// Checks if the bug data is valid before saving.
        /// </summary>
        /// <returns>Validation result with success or error details.</returns>
        Response ValidateBug();

        /// <summary>
        /// Saves the bug data to the database, handling both new and existing bugs.
        /// </summary>
        /// <returns>Outcome of the save operation.</returns>
        Response SaveBug();

        /// <summary>
        /// Fetches a specific bug by its ID, ensuring the user has the right permissions.
        /// </summary>
        /// <param name="bugId">ID of the bug to retrieve.</param>
        /// <param name="userId">ID of the requesting user.</param>
        /// <returns>Bug details if found, otherwise an error message.</returns>
        DTOBugResponse GetBugById(int bugId, int userId);

        /// <summary>
        /// Retrieves all bugs a user is allowed to access based on role-based permissions.
        /// </summary>
        /// <param name="userId">ID of the requesting user.</param>
        /// <returns>List of accessible bugs.</returns>
        Response GetAllBugs(int userId);

        /// <summary>
        /// Deletes a bug if the user has the necessary permissions.
        /// </summary>
        /// <param name="bugId">ID of the bug to delete.</param>
        /// <param name="userId">ID of the requesting user.</param>
        /// <param name="role">User's role, used to verify delete permissions.</param>
        /// <returns>Indicates whether the deletion was successful.</returns>
        Response DeleteBug(int bugId, int userId, string role);

        /// <summary>
        /// Updates a bug's status, ensuring the user has the necessary role to make changes.
        /// </summary>
        /// <param name="bugId">ID of the bug to update.</param>
        /// <param name="newStatus">New status to assign to the bug.</param>
        /// <param name="role">User's role to check update permissions.</param>
        /// <returns>Result of the status update operation.</returns>
        Response UpdateBugStatus(int bugId, BugStatus newStatus, string role);
    }
}
