using System;
using System.Collections.Generic;

namespace Game
{
    public class MiniGameModel
    {
        public event Action<CardModel> OnCardFlipped;
        public event Action<CardModel, CardModel> OnPairFound;
        public event Action<CardModel, CardModel> OnPairMismatch;
        public event Action OnGameCompleted;

        private List<CardModel> _cards;
        private List<CardModel> _flippedCards;
        private CardSpriteProvider _cardSpriteProvider;

        private int _gridWidth;
        private int _gridHeight;
        private int _matchedPairs;
        private int _totalPairs;

        public IReadOnlyList<CardModel> Cards => _cards.AsReadOnly();
        public bool IsGameCompleted => _matchedPairs == _totalPairs;

        public MiniGameModel(int gridWidth, int gridHeight, CardSpriteProvider cardSpriteProvider)
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _cardSpriteProvider = cardSpriteProvider;

            _cards = new List<CardModel>();
            _flippedCards = new List<CardModel>();
        }

        public void Initialize()
        {
            _matchedPairs = 0;

            var totalCards = _gridWidth * _gridHeight;
            _totalPairs = totalCards / 2;

            InitializeCards();
        }

        public bool CanFlipCard(CardModel card)
        {
            if (card == null || !card.CanFlip())
                return false;

            if (_flippedCards.Count >= 2)
                return false;

            return true;
        }

        public void FlipCard(CardModel card)
        {
            if (!CanFlipCard(card))
                return;

            card.SetState(CardState.FaceUp);
            _flippedCards.Add(card);

            OnCardFlipped?.Invoke(card);

            if (_flippedCards.Count == 2)
                CheckForMatch();
        }

        public void FlipCardsBack(CardModel first, CardModel second)
        {
            first.SetState(CardState.FaceDown);
            second.SetState(CardState.FaceDown);

            _flippedCards.Clear();
        }
        
        private void InitializeCards()
        {
            _cards.Clear();

            var cardFaces = new List<string>();
            for (int i = 0; i < _totalPairs; i++)
            {
                var cardFaceId = _cardSpriteProvider.GetRandomCardFaceId();
                cardFaces.Add(cardFaceId); // first card
                cardFaces.Add(cardFaceId); // second card
            }

            Utils.ShuffleItems(cardFaces);

            var cardShirt = _cardSpriteProvider.GetRandomCardShirtId();
            for (int i = 0; i < cardFaces.Count; i++)
            {
                _cards.Add(new CardModel(cardShirt, cardFaces[i]));
            }
        }

        private void CheckForMatch()
        {
            if (_flippedCards.Count != 2)
                return;

            var firstCard = _flippedCards[0];
            var secondCard = _flippedCards[1];

            if (firstCard.IsMatchWith(secondCard))
            {
                _matchedPairs++;

                OnPairFound?.Invoke(firstCard, secondCard);

                _flippedCards.Clear();
                
                CheckGameCompletion();
            }
            else
            {
                OnPairMismatch?.Invoke(firstCard, secondCard);
            }
        }

        private void CheckGameCompletion()
        {
            if (IsGameCompleted)
                OnGameCompleted?.Invoke();
        }
    }
}