using FinalDemo.Models.DTOs;
using FinalDemo.Models;
using FinalDemo.Models.Enums;

namespace FinalDemo.BL.Interfaces
{
    public interface IBugService
    {
        /// <summary>
        /// Prepares bug data before saving, either for new creation or update.
        /// </summary>
        /// <param name="bugDto">DTO containing bug creation data.</param>
        /// <param name="bugId">Optional bug ID for updating an existing bug.</param>
        void PreSaveBug(DTOBugCreated bugDto, int? bugId = null);

        /// <summary>
        /// Validates the bug data before performing save operations.
        /// </summary>
        /// <returns>Response object indicating the result of the validation.</returns>
        Response ValidateBug();

        /// <summary>
        /// Saves the bug based on the operation type (Add, Edit).
        /// </summary>
        /// <returns>Response indicating the outcome of the save operation.</returns>
        Response SaveBug();

        /// <summary>
        /// Retrieves a bug by its ID, considering the user's role and permissions.
        /// </summary>
        /// <param name="bugId">The ID of the bug to retrieve.</param>
        /// <param name="userId">The ID of the user requesting the bug information.</param>
        /// <returns>Response containing bug details if found, or an error message if not found.</returns>
        Response GetBugById(int bugId, int userId);

        /// <summary>
        /// Retrieves all bugs accessible to a given user, considering role-based access control.
        /// </summary>
        /// <param name="userId">The ID of the user requesting the list of bugs.</param>
        /// <returns>Response with a list of bugs accessible to the user.</returns>
        Response GetAllBugs(int userId);

        /// <summary>
        /// Deletes a bug by its ID, considering role-based access control for the user.
        /// </summary>
        /// <param name="bugId">The ID of the bug to delete.</param>
        /// <param name="userId">The ID of the user requesting the deletion.</param>
        /// <param name="role">The role of the user, used to verify permission.</param>
        /// <returns>Response indicating whether the deletion was successful or not.</returns>
        Response DeleteBug(int bugId, int userId, string role);

        /// <summary>
        /// Updates the status of a bug.
        /// </summary>
        /// <param name="bugId">The ID of the bug to update.</param>
        /// <param name="newStatus">The new status to assign to the bug.</param>
        /// <param name="role">The role of the user updating the status, used to verify permission.</param>
        /// <returns>Response indicating the result of the status update operation.</returns>
        Response UpdateBugStatus(int bugId, BugStatus newStatus, string role);
    }
}
