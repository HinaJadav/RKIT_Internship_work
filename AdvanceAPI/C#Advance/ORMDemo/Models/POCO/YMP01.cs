using ServiceStack.DataAnnotations;
using System;


namespace ORMDemo.Models.POCO
{
    public class YMP01
    {

        /// <summary>
        /// Gets or sets the primary key (auto-incremented).
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Gets or sets the A02F02 field.
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// Gets or sets the A03F03 field.
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Gets or sets the A04F04 field.
        /// </summary>  
        public string P01F04 { get; set; }

        /// <summary>
        /// Gets or sets the foreign key linking to the GAM01 entity.
        /// </summary>
        public int P01F05 { get; set; }

        /// <summary>
        /// Gets or sets the A06F06 field (DateTime).
        /// </summary>
        public DateTime P01F06 { get; set; }

    }
}
