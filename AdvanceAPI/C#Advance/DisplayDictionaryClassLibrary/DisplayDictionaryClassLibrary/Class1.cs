namespace DisplayDictionaryClassLibrary
{
    public class Class1
    {
        /// <summary>
        /// This method extends the functionality to display the contents of a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary whose contents are to be displayed.</param>
        public void DisplayDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                Console.WriteLine("Empty dictionary.");
                return;
            }

            foreach (var temp in dictionary)
            {
                Console.WriteLine($"Key: {temp.Key}, Value: {temp.Value}");
            }
        }
    }
   
}