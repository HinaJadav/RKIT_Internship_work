using System.Reflection;

namespace FinalDemo.Extension
{
    /// <summary>
    /// Provides extension methods for converting DTOs to POCOs.
    /// </summary>
    public static class DtoToPocoExtensions
    {
        /// <summary>
        /// Converts a Data Transfer Object (DTO) to a Plain Old CLR Object (POCO) using reflection.
        /// </summary>
        /// <typeparam name="TPoco">The type of the POCO to which the DTO will be converted.</typeparam>
        /// <param name="dto">The DTO object to be converted to a POCO.</param>
        /// <returns>A new instance of the POCO with the properties mapped from the DTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the DTO is null.</exception>
        public static TPoco ToPoco<TPoco>(this object dto) where TPoco : new()
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "DTO cannot be null");

            TPoco poco = new TPoco();
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] pocoProperties = typeof(TPoco).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProp in dtoProperties)
            {
                PropertyInfo pocoProp = pocoProperties.FirstOrDefault(p => p.Name == dtoProp.Name && p.PropertyType == dtoProp.PropertyType);
                if (pocoProp != null)
                {
                    try
                    {
                        pocoProp.SetValue(poco, dtoProp.GetValue(dto));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error mapping property {dtoProp.Name}: {ex.Message}");
                    }
                }
            }
            return poco;
        }
    }
}