using UnityEngine;

namespace Game
{
    public class MiniGameController : MonoBehaviour
    {
        [SerializeField] private int _gridWidth = 4;
        [SerializeField] private int _gridHeight = 4;
        [SerializeField] private Transform _cardParent;
        [SerializeField] private CardView _cardPrefab;
        [Space]
        [SerializeField] private MiniGameSO _gameConfig;
        [Space]
        [SerializeField] private CardController _cardController;

        private MiniGameModel _gameModel;
        private CardSpriteProvider _cardSpriteProvider;

        private void Start()
        {
            _cardSpriteProvider = new CardSpriteProvider(_gameConfig.CardShirts, _gameConfig.CardFaces);
            _gameModel = new MiniGameModel(_gridWidth, _gridHeight, _cardSpriteProvider);

            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameModel.Initialize();
            _cardController.Initialize(_gameModel);

            _gameModel.OnGameCompleted += OnGameCompleted;

            CreateCardViews();
        }

        private void CreateCardViews()
        {
            foreach (var card in _gameModel.Cards)
            {
                var cardView = Instantiate(_cardPrefab, _cardParent);

                var cardFace = _cardSpriteProvider.GetCardFace(card.Face);
                var cardShirt = _cardSpriteProvider.GetCardShirt(card.Shirt);
                cardView.SetCardSprites(cardFace, cardShirt);

                _cardController.RegisterCard(card, cardView);
            }
        }

        private void OnGameCompleted()
        {
            Debug.Log("Game over! All pairs have been found!");
        }

        private void OnDestroy()
        {
            if (_gameModel != null)
            {
                _gameModel.OnGameCompleted -= OnGameCompleted;
            }
        }

    }
}
