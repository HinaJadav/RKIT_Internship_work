namespace FinalDemo.Models.Enums
{
    /// <summary>
    /// Defines the status of a bug.
    /// </summary>
    public enum BugStatus
    {
        /// <summary> Bug is reported and open. </summary>
        Open,

        /// <summary> Bug is being worked on. </summary>
        InProgress,

        /// <summary> Bug is fixed but needs verification. </summary>
        Resolved,

        /// <summary> Bug is verified and closed. </summary>
        Closed
    }
}
