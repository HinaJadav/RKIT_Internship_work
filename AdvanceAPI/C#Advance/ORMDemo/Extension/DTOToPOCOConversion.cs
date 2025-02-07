using System;
using System.Reflection;

namespace ORMDemo.Extension
{
    /// <summary>
    /// Converts a Data Transfer Object (DTO) to a Plain Old CLR Object (POCO).
    /// </summary>
    /// <typeparam name="POCO">The type of the POCO to convert to.</typeparam>
    /// <param name="dto">The DTO object to convert.</param>
    /// <returns>The converted POCO object.</returns>
    public static class DTOToPOCOConversion
    {
        public static POCO Convert<POCO>(this object dto)
        {
            // Get the type of the POCO
            Type pocoType = typeof(POCO);
            // Create an instance of the POCO
            POCO pocoInstance = (POCO)Activator.CreateInstance(pocoType);

            // Get properties of the DTO
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            // Get properties of the POCO
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            // Iterate through each property of the DTO
            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                // Find the corresponding property in the POCO with the same name
                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);

                // If a matching property is found and types are compatible, copy the value
                if (dtoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }
            // Return the converted POCO object
            return pocoInstance;
        }
    }
}