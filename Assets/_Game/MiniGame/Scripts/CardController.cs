using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class CardController : MonoBehaviour
    {
        private MiniGameModel _gameModel;
        private Dictionary<CardModel, CardView> _cardViews;

        public void Initialize(MiniGameModel gameModel)
        {
            _gameModel = gameModel;
            _cardViews = new Dictionary<CardModel, CardView>();

            SubscribeToModelEvents();
        }

        public void RegisterCard(CardModel card, CardView cardView)
        {
            _cardViews[card] = cardView;
            cardView.OnCardClicked += OnCardClicked;
        }

        private void OnCardClicked(CardView cardView)
        {
            var card = _cardViews.FirstOrDefault(kvp => kvp.Value == cardView).Key;

            if (card != null && _gameModel.CanFlipCard(card))
            {
                _gameModel.FlipCard(card);
            }
        }

        private void OnCardFlipped(CardModel card)
        {
            if (_cardViews.TryGetValue(card, out CardView cardView))
            {
                cardView.FlipToFace();
            }
        }

        private void OnPairFound(CardModel first, CardModel second)
        {
            StartCoroutine(HideCardsAfterDelay(first, second, 0.5f));
        }

        private IEnumerator HideCardsAfterDelay(CardModel first, CardModel second, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (_cardViews.TryGetValue(first, out CardView firstView))
                firstView.Hide();

            if (_cardViews.TryGetValue(second, out CardView secondView))
                secondView.Hide();
        }

        private void OnPairMismatch(CardModel first, CardModel second)
        {
            StartCoroutine(FlipCardsBackAfterDelay(first, second, 1.0f));
        }

        private IEnumerator FlipCardsBackAfterDelay(CardModel first, CardModel second, float delay)
        {
            yield return new WaitForSeconds(delay);

            _gameModel.FlipCardsBack(first, second);

            if (_cardViews.TryGetValue(first, out CardView firstView))
                firstView.FlipToShirt();

            if (_cardViews.TryGetValue(second, out CardView secondView))
                secondView.FlipToShirt();
        }
        
        private void SubscribeToModelEvents()
        {
            _gameModel.OnCardFlipped += OnCardFlipped;
            _gameModel.OnPairFound += OnPairFound;
            _gameModel.OnPairMismatch += OnPairMismatch;
        }

        private void UnsubscribeFromModelEvents()
        {
            if (_gameModel != null)
            {
                _gameModel.OnCardFlipped -= OnCardFlipped;
                _gameModel.OnPairFound -= OnPairFound;
                _gameModel.OnPairMismatch -= OnPairMismatch;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeFromModelEvents();
        }
    }
}
