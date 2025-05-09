using System.Collections.Generic;
using System;

namespace CardGame.Models
{
    public class Deck
    {
        private List<Card> cards;
        private Random rng = new Random();

        public Deck()
        {
            cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int rank = 6; rank <= 14; rank++)  
                {
                    Rank cardRank = (Rank)rank;
                    cards.Add(new Card(cardRank, suit));
                }
            }
        }

        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]); 
            }
        }

        public Card Draw()
        {
            if (cards.Count == 0)
                return null; 

            Card topCard = cards[0];
            cards.RemoveAt(0);  
            return topCard;
        }
        public int Count => cards.Count;  
    }
}
