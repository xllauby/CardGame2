using CardGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CardGame
{
    public partial class Window1 : Window
    {
        public int PlayersCount { get; set; }
        private Deck deck;
        private List<Card> playerHand;
        private List<ComputerPlayer> computerPlayers;
        private List<Card> cardsOnField;
        private int currentPlayerIndex;
        private Card attackingCard;
        private Card trumpCard;

        public Window1(int playersCount)
        {
            InitializeComponent();
            PlayersCount = playersCount;
            DataContext = this;

            MessageBox.Show($"Игра началась! Количество игроков: {PlayersCount}", "Начало игры", MessageBoxButton.OK, MessageBoxImage.Information);
            StartGame();
        }

        private void StartGame()
        {
            deck = new Deck();
            deck.Shuffle();

            trumpCard = deck.Draw();
            Card.TrumpSuit = trumpCard.Suit;
            MessageBox.Show($"Козырь: {trumpCard.Display}", "Козырная карта");
            TrumpCardText.Text = $"Козырь: {trumpCard.Display}";

            deck = new Deck();
            deck.Shuffle();

            playerHand = new List<Card>();
            computerPlayers = new List<ComputerPlayer>();
            cardsOnField = new List<Card>();

            for (int i = 0; i < 6; i++)
            {
                var card = deck.Draw();
                if (card != null)
                    playerHand.Add(card);
            }

            for (int p = 1; p < PlayersCount; p++)
            {
                var hand = new List<Card>();
                for (int i = 0; i < 6; i++)
                {
                    var card = deck.Draw();
                    if (card != null)
                        hand.Add(card);
                }
                computerPlayers.Add(new ComputerPlayer(hand));
            }

            PlayerCardsList.ItemsSource = playerHand.Select(c => c.Display);

            Random rnd = new Random();
            currentPlayerIndex = rnd.Next(PlayersCount);

            UpdatePlayButtonText();

            if (currentPlayerIndex != 0)
            {
                Dispatcher.InvokeAsync(() =>
                {
                    PlayCardButton_Click(null, null);
                });
            }
        }

        private void CheckForWinner()
        {
            if (playerHand.Count == 0)
            {
                MessageBox.Show("Поздравляем! Вы победили!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else if (computerPlayers.All(p => p.CardsRemaining == 0))
            {
                MessageBox.Show("Все компьютеры проиграли! Вы победили!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void PlayCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPlayerIndex == 0)
            {
                if (playerHand.Count > 0)
                {
                    var selectionWindow = new CardSelectionWindow(playerHand);
                    if (selectionWindow.ShowDialog() == true && selectionWindow.SelectedCard != null)
                    {
                        attackingCard = selectionWindow.SelectedCard;
                        playerHand.Remove(attackingCard);
                        cardsOnField.Add(attackingCard);
                        MessageBox.Show($"Игрок атакует картой: {attackingCard.Display}");

                        PlayerCardsList.ItemsSource = playerHand.Select(c => c.Display);
                        CardsOnFieldList.ItemsSource = cardsOnField.Select(c => c.Display);

                        currentPlayerIndex = (currentPlayerIndex + 1) % PlayersCount;
                        UpdatePlayButtonText();
                    }
                }
            }
            else
            {
                var computer = computerPlayers[currentPlayerIndex - 1];

                if (attackingCard == null)
                {
                    var cardToPlay = computer.ChooseCardToAttack();
                    if (cardToPlay != null)
                    {
                        attackingCard = cardToPlay;
                        computer.RemoveCardFromHand(cardToPlay);
                        cardsOnField.Add(attackingCard);

                        MessageBox.Show($"Компьютер {currentPlayerIndex} атакует картой: {cardToPlay.Display}");
                    }
                }
                else
                {
                    var cardToPlay = computer.ChooseCardToPlay(attackingCard);
                    if (cardToPlay != null)
                    {
                        computer.RemoveCardFromHand(cardToPlay);
                        cardsOnField.Add(cardToPlay);
                        MessageBox.Show($"Компьютер {currentPlayerIndex} отбивается картой: {cardToPlay.Display}");
                    }
                    else
                    {
                        MessageBox.Show($"Компьютер {currentPlayerIndex} не может отбиться и забирает карту!");
                        computer.AddToHand(attackingCard);
                    }

                    attackingCard = null;
                }

                CardsOnFieldList.ItemsSource = cardsOnField.Select(c => c.Display);
                currentPlayerIndex = (currentPlayerIndex + 1) % PlayersCount;
                UpdatePlayButtonText();
            }

            CheckForWinner();
        }

        private void UpdatePlayButtonText()
        {
            if (currentPlayerIndex == 0)
                PlayCardButton.Content = "Выбрать карту";
            else
                PlayCardButton.Content = $"Ход компьютера {currentPlayerIndex}";
        }
    }
}
