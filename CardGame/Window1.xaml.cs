using CardGame.Models;
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
        private List<Card> cardsOnField; // Карты на поле
        private int currentPlayerIndex;
        private Card attackingCard;

        public Window1(int playersCount)
        {
            InitializeComponent();
            PlayersCount = playersCount;
            DataContext = this;

            MessageBox.Show($"Игра началась! Количество игроков: {PlayersCount}", "Начало игры", MessageBoxButton.OK, MessageBoxImage.Information);

            StartGame();
        }

        private Card trumpCard; // ДОБАВЬ в класс Window1

        private void StartGame()
        {
            deck = new Deck();
            deck.Shuffle();

            trumpCard = deck.Draw(); // ДОБАВЛЕНО: выбираем козырную карту
            Card.TrumpSuit = trumpCard.Suit; // Устанавливаем козырную масть
            MessageBox.Show($"Козырь: {trumpCard.Display}", "Козырная карта");
            TrumpCardText.Text = $"Козырь: {trumpCard.Display}";

            deck = new Deck();
            deck.Shuffle();

            playerHand = new List<Card>();
            computerPlayers = new List<ComputerPlayer>();
            cardsOnField = new List<Card>(); // Инициализируем список карт на поле

            // Раздаём карты игроку
            for (int i = 0; i < 6; i++)
            {
                var card = deck.Draw();
                if (card != null)
                    playerHand.Add(card);
            }

            // Раздаём карты компьютерам
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

            // Отображаем карты игрока
            PlayerCardsList.ItemsSource = playerHand.Select(c => c.Display);

            currentPlayerIndex = 0; // Игрок начинает первым
        }

        private void CheckForWinner()
        {
            // Проверяем, есть ли победитель
            if (playerHand.Count == 0)
            {
                MessageBox.Show("Поздравляем! Вы победили!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закрываем окно игры
            }
            else if (computerPlayers.All(p => p.CardsRemaining == 0))
            {
                MessageBox.Show("Все компьютеры проиграли! Вы победили!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Закрываем окно игры
            }
        }

        // Обработчик нажатия кнопки "Выбрать карту"
        private void PlayCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPlayerIndex == 0) // Игрок делает ход
            {
                if (playerHand.Count > 0)
                {
                    // Игрок выбирает первую карту (для простоты)
                    attackingCard = playerHand.First();
                    playerHand.RemoveAt(0); // Убираем выбранную карту из руки

                    // Добавляем карту на поле
                    cardsOnField.Add(attackingCard);
                    MessageBox.Show($"Игрок атакует картой: {attackingCard.Display}");

                    // Обновляем список карт игрока
                    PlayerCardsList.ItemsSource = playerHand.Select(c => c.Display);
                }
            }
            else // Компьютер делает ход
            {
                var computer = computerPlayers[currentPlayerIndex - 1]; // Получаем игрока-компьютера

                if (computer.CardsRemaining > 0)
                {
                    var cardToPlay = computer.ChooseCardToPlay(attackingCard);
                    computer.RemoveCardFromHand(cardToPlay);
                    cardsOnField.Add(cardToPlay);

                    MessageBox.Show($"Компьютер {currentPlayerIndex} играет картой: {cardToPlay.Display}");
                }
            }

            // Переходим к следующему игроку
            currentPlayerIndex = (currentPlayerIndex + 1) % PlayersCount;

            // Обновляем отображение карт на поле
            CardsOnFieldList.ItemsSource = cardsOnField.Select(c => c.Display);

            // Проверяем победителя после хода
            CheckForWinner();
        }
    }
}
