using System.Reflection;

namespace FinalDemo.Extension
{
    /// <summary>
    /// Provides extension methods for converting POCOs to DTOs.
    /// </summary>
    public static class PocoToDtoExtensions
    {
        /// <summary>
        /// Converts a Plain Old CLR Object (POCO) to a Data Transfer Object (DTO) using reflection.
        /// </summary>
        /// <typeparam name="TDto">The type of the DTO to which the POCO will be converted.</typeparam>
        /// <param name="poco">The POCO object to be converted to a DTO.</param>
        /// <returns>A new instance of the DTO with the properties mapped from the POCO.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the POCO is null.</exception>
        public static TDto ToDto<TDto>(this object poco) where TDto : new()
        {
            if (poco == null)
                throw new ArgumentNullException(nameof(poco), "POCO cannot be null");

            TDto dto = new TDto();
            PropertyInfo[] pocoProperties = poco.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] dtoProperties = typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var pocoProp in pocoProperties)
            {
                PropertyInfo dtoProp = dtoProperties.FirstOrDefault(p => p.Name == pocoProp.Name && p.PropertyType == pocoProp.PropertyType);
                if (dtoProp != null)
                {
                    try
                    {
                        dtoProp.SetValue(dto, pocoProp.GetValue(poco));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error mapping property {pocoProp.Name}: {ex.Message}");
                    }
                }
            }
            return dto;
        }
    }
}
