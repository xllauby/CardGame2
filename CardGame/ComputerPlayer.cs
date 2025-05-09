using System.Linq;
using System.Collections.Generic;
using CardGame.Models;

namespace CardGame
{
    public class ComputerPlayer
    {
        private List<Card> hand;

        public ComputerPlayer(List<Card> hand)
        {
            this.hand = hand;
        }

        public Card ChooseCardToPlay(Card attackingCard)
        {
            foreach (var card in hand)
            {
                if (card.CanDefend(attackingCard))
                return card;
            }

            return null; 
        }

        public Card ChooseCardToAttack()
        {
                return hand
                .Where(c => c.Suit != Card.TrumpSuit)
                .OrderBy(c => c.Rank)
                .FirstOrDefault()
                ?? hand.OrderBy(c => c.Rank).FirstOrDefault();
        }

        public void RemoveCardFromHand(Card card)
        {
            hand.Remove(card);
        }

        public void AddToHand(Card card)
        {
            if (card != null)
            hand.Add(card);
        }

        public int CardsRemaining => hand.Count;
        public List<Card> Hand => hand;
    }
}
