using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWithC__CRUD.Models.POCO
{
    /// <summary>
    /// Represents the T01 entity in the database.
    /// </summary>
    public class YMT01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the T01 entity.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int T01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name for the T01 entity.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string T01F02 { get; set; }

        /// <summary>
        /// Gets or sets the foreign key reference to the GAM01 entity.
        /// </summary>
        [Required]
        [ForeignKey("GAM01")]
        public int T01F03 { get; set; }

        /// <summary>
        /// Gets or sets additional information for the T01 entity.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string T01F04 { get; set; }

        /// <summary>
        /// Gets or sets the timestamp for the T01 entity.
        /// </summary>
        public DateTime T01F05 = DateTime.Now;
    }
}
