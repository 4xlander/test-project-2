using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public static class Utils
    {
        public static T GetRandomElement<T>(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new System.ArgumentNullException(nameof(collection));

            var itemsCount = collection.Count();
            if (itemsCount == 0)
                throw new System.InvalidOperationException("Cannot get a random element from an empty collection.");

            var randomIndex = Random.Range(0, itemsCount);
            return collection.ElementAt(randomIndex);
        }

        public static void ShuffleItems(List<string> items)
        {
            for (int i = items.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                (items[randomIndex], items[i]) = (items[i], items[randomIndex]);
            }
        }
    }
}
