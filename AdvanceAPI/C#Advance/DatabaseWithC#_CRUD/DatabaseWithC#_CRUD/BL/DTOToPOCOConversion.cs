using DatabaseWithC__CRUD.Models.DTO;
using DatabaseWithC__CRUD.Models.POCO;

namespace DatabaseWithC__CRUD.BL
{
    /// <summary>
    /// Provides manual mapping functionality between DTO and POCO objects.
    /// </summary>
    public class DTOToPOCOConversion
    {
        /// <summary>
        /// Converts a DTOYMT01 object to a YMT01 POCO object.
        /// </summary>
        /// <param name="dtoymt01">The DTOYMT01 object to convert.</param>
        /// <returns>A corresponding YMT01 POCO object.</returns>
        public static YMT01 dtoToPocoConvert(DTOYMT01 dtoymt01)
        {
            return new YMT01
            {
                T01F01 = dtoymt01.T01101,
                T01F02 = dtoymt01.T01102,
                T01F03 = dtoymt01.T01103,
                T01F04 = dtoymt01.T01104
            };
        }
    }
}
