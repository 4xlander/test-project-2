namespace Game
{
    public class CardModel
    {
        public string Face { get; private set; }
        public string Shirt { get; private set; }
        public CardState State { get; private set; }

        public CardModel(string cardShirt, string cardFace)
        {
            Face = cardFace;
            Shirt = cardShirt;
            State = CardState.FaceDown;
        }

        public void SetState(CardState newState)
        {
            State = newState;
        }

        public bool CanFlip()
        {
            return State == CardState.FaceDown;
        }

        public bool IsMatchWith(CardModel otherCard)
        {
            return otherCard != null && Face == otherCard.Face;
        }
    }
}
