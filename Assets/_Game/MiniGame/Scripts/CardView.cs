using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardView : MonoBehaviour
    {
        public event Action<CardView> OnCardClicked;

        [SerializeField] private Image _cardImage;
        [SerializeField] private Button _cardButton;

        private Sprite _faceSprite;
        private Sprite _shirtSprite;

        private void OnEnable()
        {
            _cardButton.onClick.AddListener(CardView_OnClick);
        }

        private void OnDisable()
        {
            _cardButton.onClick.RemoveListener(CardView_OnClick);
        }

        public void SetCardSprites(Sprite faceSprite, Sprite shirtSprite)
        {
            _faceSprite = faceSprite;
            _shirtSprite = shirtSprite;
            _cardImage.sprite = _shirtSprite;
        }

        public void FlipToFace()
        {
            _cardImage.sprite = _faceSprite;
        }

        public void FlipToShirt()
        {
            _cardImage.sprite = _shirtSprite;
        }

        public void Hide()
        {
            _cardButton.interactable = false;
            _cardImage.enabled = false;
        }

        private void CardView_OnClick()
        {
            OnCardClicked?.Invoke(this);
        }
    }
}