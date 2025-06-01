using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class CardSpriteProvider
    {
        private readonly Dictionary<string, Sprite> _cardShirts;
        private readonly Dictionary<string, Sprite> _cardFaces;

        public CardSpriteProvider(IEnumerable<Sprite> cardShirts, IEnumerable<Sprite> cardFaces)
        {
            _cardShirts = cardShirts.ToDictionary(s => GenerateId(), s => s);
            _cardFaces = cardFaces.ToDictionary(s => GenerateId(), s => s);
        }

        public IReadOnlyCollection<string> CardShirtIds => _cardShirts.Keys;
        public IReadOnlyCollection<string> CardFaceIds => _cardFaces.Keys;

        public string GetRandomCardShirtId()
        {
            if (_cardShirts.Count == 0)
                throw new InvalidOperationException("No card shirts available.");

            var result = Utils.GetRandomElement(_cardShirts.Keys);
            return result;
        }

        public string GetRandomCardFaceId()
        {
            if (_cardFaces.Count == 0)
                throw new InvalidOperationException("No card faces available.");

            var result = Utils.GetRandomElement(_cardFaces.Keys);
            return result;
        }

        public Sprite GetCardShirt(string id)
        {
            if (_cardShirts.TryGetValue(id, out var sprite))
                return sprite;

            throw new KeyNotFoundException($"Card shirt with ID '{id}' not found.");
        }

        public Sprite GetCardFace(string id)
        {
            if (_cardFaces.TryGetValue(id, out var sprite))
                return sprite;

            throw new KeyNotFoundException($"Card face with ID '{id}' not found.");
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
