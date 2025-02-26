using FinalDemo.Models.DTOs;

namespace FinalDemo.BL.Interfaces
{
    /// <summary>
    /// Interface for managing bug-related operations.
    /// </summary>
    public interface IBugService
    {
        /// <summary>
        /// Creates a new bug in the system.
        /// </summary>
        /// <param name="bugDto">DTO containing bug details.</param>
        /// <param name="userId">ID of the user creating the bug.</param>
        /// <returns>Returns the created bug details.</returns>
        DTOBugResponse CreateBug(DTOBugCreated bugDto, int userId);

        /// <summary>
        /// Updates the status of an existing bug.
        /// </summary>
        /// <param name="bugDto">DTO containing updated bug status.</param>
        /// <param name="userId">ID of the user updating the bug.</param>
        /// <returns>Returns the updated bug details.</returns>
        DTOBugResponse UpdateBugStatus(DTOBugUpdated bugDto, int userId);

        /// <summary>
        /// Retrieves a bug by its unique identifier.
        /// </summary>
        /// <param name="bugId">The unique identifier of the bug.</param>
        /// <returns>Returns the bug details if found.</returns>
        DTOBugResponse GetBugById(int bugId, int userId);

        /// <summary>
        /// Retrieves a list of all bugs based on user role.
        /// Admins can see all bugs, developers and testers see assigned bugs.
        /// </summary>
        /// <param name="userId">The ID of the requesting user.</param>
        /// <returns>A list of bugs based on role-based access.</returns>
        IEnumerable<DTOBugResponse> GetAllBugs(int userId);

        /// <summary>
        /// Deletes a bug from the system.
        /// Only admins can delete bugs.
        /// </summary>
        /// <param name="bugId">The unique identifier of the bug to delete.</param>
        /// <param name="userId">The ID of the user attempting to delete the bug.</param>
        void DeleteBug(int bugId, int userId);
    }
}