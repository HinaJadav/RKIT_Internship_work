using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWithC__CRUD_.Model.POCO
{
    partial class TEAM01
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int T01F01 { get; set; }

        [Required]
        [StringLength(50)]
        public string T02F02 { get; set; }

        [Required]
        [ForeignKey("GAM01")]
        public int T01F03 { get; set; }

        [Required]
        [StringLength(50)]
        public string T01F04 { get; set; }
        public DateTime T01F05 = DateTime.Now;
    }
}
