using System.Collections.Generic;

namespace BaseLibrary
{
    public class MyDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public MyDictionary() : base() { }

        /// <summary>
        /// Adds a key-value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public void AddToDictionary(TKey key, TValue value)
        {
            base.Add(key, value);
        }

        /// <summary>
        /// Checks if the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to check in the dictionary.</param>
        /// <returns>True if the dictionary contains the key, otherwise false.</returns>
        public bool ContainsToDictionary(TKey key)
        {
            return (base.ContainsKey(key));
        }

        /// <summary>
        /// Removes the key-value pair with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>True if the key was successfully removed, otherwise false.</returns>
        public bool RemoveFromDictionary(TKey key)
        {
            return (base.Remove(key));
        }

        /// <summary>
        /// Attempts to get the value associated with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to retrieve.</param>
        /// <param name="value">The value associated with the key, if it exists.</param>
        /// <returns>True if the value was found, otherwise false.</returns>
        public bool TryGetValueFromDictionary(TKey key, out TValue value)
        {
            return base.TryGetValue(key, out value);
        }

        /// <summary>
        /// Checks if the dictionary contains the specified value.
        /// </summary>
        /// <param name="value">The value to check in the dictionary.</param>
        /// <returns>True if the dictionary contains the value, otherwise false.</returns>
        public bool ContainsValueInDictionary(TValue value)
        {
            return (base.ContainsValue(value));
        }

        /// <summary>
        /// Clears all elements from the dictionary.
        /// </summary>
        public void ClearDictionary()
        {
            base.Clear();
        }
    }
}
