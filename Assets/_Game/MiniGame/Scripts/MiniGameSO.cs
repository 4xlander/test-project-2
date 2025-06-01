using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "MiniGameConfig", menuName = "Game/Config/MiniGameConfig", order = 1)]
    public class MiniGameSO : ScriptableObject
    {
        [SerializeField] private Sprite[] _cardShirts;
        [SerializeField] private Sprite[] _cardFaces;

        public IEnumerable<Sprite> CardShirts => _cardShirts;
        public IEnumerable<Sprite> CardFaces => _cardFaces;
    }
}
