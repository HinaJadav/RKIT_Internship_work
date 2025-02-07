using ServiceStack.DataAnnotations;

namespace ORMDemo.Models.POCO
{
    public class YMG01
    {
            /// <summary>
            /// Gets or sets the primary key with auto-increment.
            /// </summary>
            public int G01F01 { get; set; }

            /// <summary>
            /// Gets or sets the M02F02 field (string).
            /// </summary>
            [StringLength(50)]
            public string G01F02 { get; set; }

            /// <summary>
            /// Gets or sets the M03F03 field (integer).
            /// </summary>
            public int G01F03 { get; set; }
        
    }
}