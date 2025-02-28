using System.Reflection;

namespace FinalDemo.Extension
{
    /// <summary>
    /// Provides extension methods for converting DTOs to POCOs.
    /// </summary>
    public static class DtoToPocoExtensions
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