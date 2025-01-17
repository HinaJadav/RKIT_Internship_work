using DatabaseWithC__CRUD.Models.DTO;
using DatabaseWithC__CRUD.Models.POCO;

namespace DatabaseWithC__CRUD.BL
{
    // manual mapping
    public class DTOToPOCOConversion
    {
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