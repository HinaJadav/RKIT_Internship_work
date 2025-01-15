using ServiceStack.DataAnnotations;
using System;

namespace ORM.POCO
{
    /// <summary>
    /// Represents the PLA01 entity.
    /// </summary>
    public class PLA01
    {
        /// <summary>
        /// Gets or sets the primary key (auto-incremented).
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int A01F01 { get; set; }

        /// <summary>
        /// Gets or sets the A02F02 field.
        /// </summary>
        [StringLength(50)]
        public string A02F02 { get; set; }

        /// <summary>
        /// Gets or sets the A03F03 field.
        /// </summary>
        [StringLength(50)]
        public string A03F03 { get; set; }

        /// <summary>
        /// Gets or sets the A04F04 field.
        /// </summary>
        [StringLength(50)]
        public string A04F04 { get; set; }

        /// <summary>
        /// Gets or sets the foreign key linking to the GAM01 entity.
        /// </summary>
        [ForeignKey(typeof(GAM01))]
        public int A05F05 { get; set; }

        /// <summary>
        /// Gets or sets the A06F06 field (DateTime).
        /// </summary>
        public DateTime A06F06 { get; set; }
    }
}
