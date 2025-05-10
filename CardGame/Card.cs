namespace CardGame.Models
{
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }

        public string Display
        {
            get
            {
                string suitEmoji = string.Empty;

                if (Suit == Suit.Clubs)
                {
                    suitEmoji = "♣️";
                }
                else if (Suit == Suit.Diamonds)
                {
                    suitEmoji = "♦";
                }
                else if (Suit == Suit.Hearts)
                {
                    suitEmoji = "♥️";
                }
                else if (Suit == Suit.Spades)
                {
                    suitEmoji = "♠️";
                }

                return $"{Rank} {suitEmoji}"; 
            }
        }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public static Suit TrumpSuit { get; set; }

        public bool CanDefend(Card attackingCard)
        {
            if (this.Suit == attackingCard.Suit && this.Rank > attackingCard.Rank)
                return true;

            if (this.Suit == TrumpSuit && attackingCard.Suit != TrumpSuit)
                return true;

            return false;
        }
    }
}
