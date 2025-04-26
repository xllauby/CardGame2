using System.Linq;  // Для использования FirstOrDefault
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

        // Выбираем карту для защиты или атаки
        public Card ChooseCardToPlay(Card attackingCard)
        {
            // Если можно защититься, выбираем карту для защиты
            foreach (var card in hand)
            {
                if (card.CanDefend(attackingCard))
                {
                    return card;
                }
            }

            // Иначе выбираем карту для атаки (первая карта)
            return hand.FirstOrDefault();
        }

        // Пример метода для создания карты (с использованием преобразования int в Rank)
        public Card CreateCard(int rankValue, Suit suit)
        {
            // Преобразуем int в Rank (например, 2 -> Rank.Two)
            Rank rank = (Rank)rankValue;  // Явное приведение int в Rank

            return new Card(rank, suit);  // Создаем карту с преобразованным Rank и Suit
        }

        public void RemoveCardFromHand(Card card)
        {
            hand.Remove(card);
        }

        public int CardsRemaining => hand.Count;
    }
}
