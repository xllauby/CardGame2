using System;
using System.Windows;
using System.Windows.Controls;

namespace CardGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли элемент в ComboBox
            if (playersComboBox.SelectedItem == null)
            {
                ShowWarning("Пожалуйста, выберите количество игроков.", "Ошибка");
                return;
            }

            // Пытаемся получить выбранное значение
            int selectedPlayers = int.Parse(((ComboBoxItem)playersComboBox.SelectedItem).Content.ToString());

            Window1 gameWindow = new Window1(selectedPlayers);
            gameWindow.Show();
            this.Close();
        }

        private void AboutGameButton_Click(object sender, RoutedEventArgs e)
        {
            ShowInfo(
                "Это карточная игра 'Дурак' — классическая русская игра, цель которой — избавиться от всех карт в руке.\n\n" +
                "Основные правила:\n" +
                "• Один игрок начинает атаку, другой — защищается.\n" +
                "• Бить можно картами той же масти, но более высокого достоинства, или козырными картами.\n" +
                "• Карты добираются до 6 после каждого раунда.\n" +
                "• Побеждает тот, кто первым избавится от всех карт.\n\n" +
                "Проигравший — 'Дурак' 😄\n\n" +
                "Удачи и приятной игры!",
                "О игре"
            );
        }

        private void AboutDeveloperButton_Click(object sender, RoutedEventArgs e)
        {
            ShowInfo("Разработчики: Внуковский; Джебко", "О разработчиках");
        }

        private void ShowInfo(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowWarning(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
