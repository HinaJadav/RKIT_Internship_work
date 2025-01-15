using ServiceStack.DataAnnotations;

namespace ORM.POCO
{
    /// <summary>
    /// Represents the GAM01 entity.
    /// </summary>
    public class GAM01
    {
        /// <summary>
        /// Gets or sets the primary key with auto-increment.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int M01F01 { get; set; }

        /// <summary>
        /// Gets or sets the M02F02 field (string).
        /// </summary>
        [StringLength(50)]
        public string M02F02 { get; set; }

        /// <summary>
        /// Gets or sets the M03F03 field (integer).
        /// </summary>
        [Range(0, 20)]
        public int M03F03 { get; set; }
    }
}
