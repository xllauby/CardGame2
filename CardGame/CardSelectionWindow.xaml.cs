using CardGame.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CardGame
{
    public partial class CardSelectionWindow : Window
    {
        public Card SelectedCard { get; private set; }

        private List<Card> cards;

        public CardSelectionWindow(List<Card> availableCards)
        {
            InitializeComponent();
            cards = availableCards;
            CardList.ItemsSource = cards.Select(c => c.Display).ToList();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = CardList.SelectedIndex;
            if (selectedIndex >= 0)
            {
                SelectedCard = cards[selectedIndex];
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Выберите карту для хода.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CardList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
